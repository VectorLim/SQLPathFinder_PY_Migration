import contextlib
import io
import logging
import os

ENV_MODE = os.environ.get('ENV_MODE', 'test').lower()


def update_lot_attributes(facility, lot, attr_list, skip_value, logger=None):
    logger = logger if logger is not None else logging.getLogger(__name__)
    if isinstance(attr_list, str):
        attr_list = (attr_list,)
    if isinstance(skip_value, set) and len(skip_value) == 1:
        skip_value = next(iter(skip_value))
    if not isinstance(skip_value, str) or not skip_value:
        raise ValueError('skip_value must be a nonempty string or a single-string set')
    if not attr_list or any(not isinstance(attr_id, str) or not attr_id for attr_id in attr_list):
        raise ValueError('attr_list must contain nonempty attribute ID strings')
    # Import after the caller has parsed its own command-line arguments.
    import aed_client
    from aed_client import ManufacturingService

    try:
        with contextlib.redirect_stdout(io.StringIO()):
            # Preserve the original helper's PROD service context and test-mode write guard.
            aed_client.facility = facility
            aed_client.environment = 'PROD'
            aed_client.protocol = 'HTTPS'
            aed_client_instance = ManufacturingService()
            response = aed_client_instance.lot_status(lot)
            response.raise_for_status()
            lot_status = response.json()
            logger.info(f"lot status: {lot_status}")
            if lot_status['Success'] != True:
                logger.error(f"Request failed with ResultCode : {lot_status['ResultCode']}")
                raise Exception(f"Request failed with ResultCode : {lot_status['ResultCode']}")

            skip = 'N'
            has_updated = False
            found_value = False

            attributes = lot_status['Result']['Attributes'] if lot_status['Result'] else None
            if not attributes:
                logger.warning(f"No attributes with facility: {facility}, lot: {lot}")
                return skip

            for attr_id in attr_list:
                attr = next((attr for attr in attributes if attr['ID'] == attr_id), None)
                value = attr.get('Value') if attr is not None else None
                if value == skip_value:
                    logger.info(f"Already updated None Value to '{skip_value}' for facility: {facility}, lot: {lot}, id: {attr_id}")
                    found_value = True
                    break

            if not found_value:
                for attr_id in attr_list:
                    attr = next((attr for attr in attributes if attr['ID'] == attr_id), None)
                    value = attr.get('Value') if attr is not None else None
                    if value is None:
                        # Update the attribute value
                        if ENV_MODE != 'prod':
                            update_response = {'Success': True}
                        else:
                            response = aed_client_instance.lot_set_attr(lot, attr_id, skip_value)
                            response.raise_for_status()
                            update_response = response.json()

                        if update_response['Success'] == True:
                            has_updated = True
                            if ENV_MODE != 'prod':
                                logger.info(f"ENV_MODE='{ENV_MODE}', skip lot_set_attr for facility: {facility}, lot: {lot}, id: {attr_id}")
                            else:
                                logger.info(f"Successfully updated None Value to '{skip_value}' for facility: {facility}, lot: {lot}, id: {attr_id}")
                        else:
                            logger.error(f"Failed to update None Value to '{skip_value}' for facility: {facility}, lot: {lot}, id: {attr_id}")
                            raise Exception(f"ERROR occurred in lot_set_attr: Response code != 200")
                        break
            if has_updated:
                skip = 'Y'
            return skip
    except Exception as e:
        logger.error(f"Error in update_lot_attributes with facility: {facility}, lot: {lot}--{e}")
        raise


