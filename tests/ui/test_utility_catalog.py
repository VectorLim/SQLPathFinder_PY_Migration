from vg2c.kind import Kind
from vg2c_ui.services.utility_catalog import UtilityCatalog


def test_catalog_derives_method_contract_from_registered_utility():
    method = UtilityCatalog().resolve("ctx.external.run", Kind.EXTERNAL_RUN)

    assert method.utility.name == "external"
    assert method.utility.class_name == "ExternalProcess"
    assert method.utility.method == "run"
    assert method.utility.return_type == "int"
    assert [parameter.name for parameter in method.parameters] == [
        "argv",
        "cwd",
        "env",
        "check",
    ]
    assert method.utility.fallback is False


def test_catalog_uses_generic_fallback_for_unseen_call():
    method = UtilityCatalog().resolve("custom.unseen.call", Kind.UNKNOWN)

    assert method.utility.class_name == "UnknownUtility"
    assert method.utility.fallback is True
