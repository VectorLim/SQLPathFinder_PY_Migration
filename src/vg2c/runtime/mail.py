"""SMTP operation extracted from utilities.mail; all file inputs use the job root."""
import smtplib
from email.message import EmailMessage
from vg2c.runtime.values import job_path

class _Settings:
    KEYRING_SERVICE = "SMTP"
    DEFAULT_SMTP_HOST = "smtpauth.intel.com"
    DEFAULT_SMTP_PORT = 587

def _load_credential():
    import keyring
    cred = keyring.get_credential(_Settings.KEYRING_SERVICE, None)
    if cred is None:
        raise RuntimeError(f"No credential found for service '{_Settings.KEYRING_SERVICE}' in Windows Credential Manager. Add a generic credential with:\n  cmdkey /generic:{_Settings.KEYRING_SERVICE} /user:<email> /pass:<password>")
    return cred

def send_mail(to: str, subject: str, body: str, attachments: list[str] | None=None, from_addr: str | None=None, enabled: bool=True, *, workdir) -> None:
    if not enabled:
        return
    cred = _load_credential()
    sender = from_addr or cred.username
    msg = EmailMessage()
    msg['Subject'] = subject
    msg['From'] = sender
    msg['To'] = to
    msg.set_content(_resolve_body(body, workdir))
    for att_path in attachments or []:
        p = job_path(att_path, workdir)
        if p.exists():
            msg.add_attachment(p.read_bytes(), maintype='application', subtype='octet-stream', filename=p.name)
    try:
        with smtplib.SMTP(_Settings.DEFAULT_SMTP_HOST, _Settings.DEFAULT_SMTP_PORT) as smtp:
            smtp.ehlo()
            smtp.starttls()
            smtp.ehlo()
            smtp.login(cred.username, cred.password)
            smtp.send_message(msg)
    except smtplib.SMTPAuthenticationError as exc:
        raise RuntimeError('SMTP authentication failed. Check the SMTP credential stored in Windows Credential Manager.') from exc
    except (smtplib.SMTPException, OSError) as exc:
        raise RuntimeError(f'SMTP send failed: {exc}') from exc

def _resolve_body(body: str, workdir) -> str:
    path = job_path(body, workdir)
    if body and path.exists() and path.is_file():
        return path.read_text(encoding='utf-8', errors='replace')
    return body
