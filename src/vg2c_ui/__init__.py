"""Local read-only visual editor for vg2c workflows."""


def create_app(*args, **kwargs):
    from vg2c_ui.app import create_app as app_factory

    return app_factory(*args, **kwargs)


__all__ = ["create_app"]
