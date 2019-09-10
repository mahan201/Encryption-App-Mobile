using System;
using System.Collections.Generic;
using System.Text;

namespace SyncfusionXamarinApp1
{
    class Encryption
    {
        public string NewKey()
        {
            StringBuilder Key = new StringBuilder();
            Random random = new Random();
            int length = random.Next(7, 12);

            for (int i = 0; i < length; i++)
            {
                int ASCII = random.Next(33, 122);
                char Character = (char)ASCII;
                Key.Append(Character);
            }


            return Key.ToString();
        }

        public string Encrypt(string OldText, string Key)
        {
            StringBuilder NewText = new StringBuilder();
            int len = OldText.Length;
            int Key_Loop = 0;


            for (int i = 0; i < len; i++)
            {
                char chr = OldText[i];
                int oldascii = (int)chr;
                Console.WriteLine(oldascii);

                if (oldascii == 13)
                {
                    continue;
                }
                if (oldascii == 10)
                {
                    NewText.Append(chr);
                    continue;
                }

                char Key_Char = Key[Key_Loop];
                int Key_ASCII = (int)Key_Char;
                int NewAscii;
                int check = oldascii + Key_ASCII;
                while (true)
                {
                    if (check > 122)
                    {
                        NewAscii = check - 91;
                        check = NewAscii;
                        continue;
                    }

                    else if (check <= 122)
                    {
                        NewAscii = check;
                        break;
                    }
                }

                char NewChar = (char)NewAscii;
                NewText.Append(NewChar);
                Key_Loop = Key_Loop + 1;
                if (Key_Loop == Key.Length)
                {
                    Key_Loop = 0;
                }


            }

            return NewText.ToString();
        }

        public string Decrypt(string oldstr, string Key)
        {
            StringBuilder newstr = new StringBuilder();
            int len = oldstr.Length;
            int Key_Lopp = 0;

            for (int i =0; i<len; i++)
            {
                char chr = oldstr[i];
                int oldascii = (int)chr;

                if (oldascii == 10)
                {
                    newstr.Append(chr);
                    continue;
                }

                char Key_char = Key[Key_Lopp];
                int KeyAss = (int)Key_char;

                int newascii;

                int check = oldascii - KeyAss;

                while (true)
                {
                    if (check<32)
                    {
                        newascii = check + 91;
                        check = newascii;
                        continue;
                    }

                    if (check >= 32)
                    {
                        newascii = check;
                        break;
                    }
                }

                char nchar = (char)newascii;
                newstr.Append(nchar);
                Key_Lopp = Key_Lopp + 1;
                if (Key_Lopp == Key.Length)
                {
                    Key_Lopp = 0;
                }
            }

            return newstr.ToString();
        }
            

        
        
    }
}
