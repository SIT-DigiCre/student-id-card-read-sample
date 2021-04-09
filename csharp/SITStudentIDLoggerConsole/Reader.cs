using System;
using FelicaLib;

namespace SITStudentID
{
    public class Reader : IDisposable
    {
        private Felica f;

        public Reader()
        {
            f = new Felica();
        }

        public void Dispose()
        {
            f.Dispose();
        }
        ~Reader()
        {
            Dispose();
        }

        private void Polling()
        {
            f.Polling((int)0x8277);
        }
        public Card Read()
        {
            Card card = new Card();
            Polling();
            byte[] data = f.ReadWithoutEncryption(0x010b, 0);
            if (data == null)
            {
                throw new Exception("学籍番号が読み取れません");
            }
            for (int i = 3; i < 10; i++)
            {
                card.ID += (char)data[i];
            }
            data = f.ReadWithoutEncryption(0x010b, 1);
            if (data == null)
            {
                throw new Exception("有効期限が読み取れません");
            }
            for (int i = 0; i < 8; i++)
            {
                card.ValidFromRaw += (char)data[i];
            }
            card.ValidFrom = DateTime.ParseExact(card.ValidFromRaw + "000000", "yyyyMMddHHmmss", null);
            for (int i = 8; i < 16; i++)
            {
                card.ValidToRaw += (char)data[i];
            }
            card.ValidTo = DateTime.ParseExact(card.ValidToRaw + "235959", "yyyyMMddHHmmss", null);
            return card;
        }
    }
}
