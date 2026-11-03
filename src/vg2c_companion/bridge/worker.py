"""Background polling worker for the native observer client."""

from __future__ import annotations

import queue
import threading
import time

from ..models import ObserverSnapshot, replace_oldest
from .client import ObserverClient

_POLL_INTERVAL_SECONDS = 1.0
_IDLE_WAIT_SECONDS = 0.25
_HEARTBEAT_SECONDS = 10.0


class ObserverWorker:
    """Poll the native helper away from Tk and retain only the newest frame."""

    def __init__(
        self, client: ObserverClient, pid: int | None = None, hwnd: int | None = None
    ) -> None:
        self._client = client
        self._pid = pid
        self._hwnd = hwnd
        self._target_lock = threading.Lock()
        self._target_epoch = 0
        self._frames: queue.Queue[tuple[int, ObserverSnapshot]] = queue.Queue(maxsize=1)
        self._stop = threading.Event()
        self._thread = threading.Thread(
            target=self._run, name="vg2c-observer", daemon=True
        )
        self._thread.start()

    def drain(self) -> ObserverSnapshot | None:
        latest = None
        with self._target_lock:
            epoch = self._target_epoch
            while True:
                try:
                    frame_epoch, frame = self._frames.get_nowait()
                    if frame_epoch == epoch:
                        latest = frame
                except queue.Empty:
                    return latest

    def close(self) -> None:
        self._stop.set()
        self._client.close()
        self._thread.join(timeout=2)

    def select_target(self, pid: int | None, hwnd: int | None) -> None:
        with self._target_lock:
            self._pid = pid
            self._hwnd = hwnd
            self._target_epoch += 1
            self._discard_frames()

    def invalidate_frames(self) -> None:
        with self._target_lock:
            self._target_epoch += 1
            self._discard_frames()

    def _discard_frames(self) -> None:
        while True:
            try:
                self._frames.get_nowait()
            except queue.Empty:
                return

    def _run(self) -> None:
        should_poll = self._client.should_poll
        polled_epoch = None
        last_poll = float("-inf")
        while not self._stop.is_set():
            with self._target_lock:
                pid, hwnd = self._pid, self._hwnd
                epoch = self._target_epoch
            first = (
                epoch != polled_epoch
                or time.monotonic() - last_poll >= _HEARTBEAT_SECONDS
            )
            if hwnd is None or not should_poll(pid, first=first):
                self._stop.wait(_IDLE_WAIT_SECONDS)
                continue
            polled_epoch = epoch
            last_poll = time.monotonic()
            frame = self._client.poll(pid, hwnd)
            with self._target_lock:
                if epoch != self._target_epoch:
                    continue
            replace_oldest(self._frames, (epoch, frame))
            self._stop.wait(_POLL_INTERVAL_SECONDS)
