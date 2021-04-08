import datetime
import nfc
from .sit_id_card import SITIDCard

class Reader:
    def __init__(self, callback):
        self.callback = callback
    def on_connect(self, tag):
        sitidcard = SITIDCard()
        sc = nfc.tag.tt3.ServiceCode(4, 0x010B)
        # ID
        bc = nfc.tag.tt3.BlockCode(0, service=0)
        data = tag.read_without_encryption([sc], [bc])
        sitidcard.id = data[3:10].decode('utf-8')

        # Validity period
        bc = nfc.tag.tt3.BlockCode(1, service=0)
        data = tag.read_without_encryption([sc], [bc])
        sitidcard.valid_from = datetime.datetime.fromisoformat(
            "{}-{}-{} 00:00:00".format(
                data[0:4].decode('utf-8'),
                data[4:6].decode('utf-8'),
                data[6:8].decode('utf-8')
            )
        )
        sitidcard.valid_to = datetime.datetime.fromisoformat(
            "{}-{}-{} 23:59:59".format(
                data[8:12].decode('utf-8'),
                data[12:14].decode('utf-8'),
                data[14:16].decode('utf-8')
            )
        )

        self.callback(sitidcard)
    def read(self):
        with nfc.ContactlessFrontend('usb') as clf:
            clf.connect(rdwr={'on-connect': self.on_connect})
