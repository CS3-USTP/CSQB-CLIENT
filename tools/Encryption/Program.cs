
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption {
    class Program {

        static readonly string secretMessage = @"

    version: 1.0.0

    Ahoy, ye explorers!

    I be mighty impressed ye’ve stumbled upon this level. I was plannin' to bury it down, but thought it'd be a jolly good time to leave a message for ye.

    This here problem be crafted for curious computer swashbucklers. A fine crew like yerself should be able to uncover it, aye? If ye can't, well shiver me timbers, ye might not be the scallywag I’m searchin' for.

    I hope ye have a grand time readin’ the code. Should ye have any questions, don’t hesitate to holler.

    Tell me yer grand tale and picture the treasure ye've uncovered.
    Fair winds and good luck!

    - cs3.ustp@gmail.com
    
";

        static readonly byte[] KEY1 =
        [
            0x2b, 0x7e, 0x4a, 0xb3, 0x4c, 0xcc, 0x51, 0x31, 0xe6, 0x05, 0x02, 0x05, 0x48, 0x32, 0x00, 0xf5,
            0x80, 0xc0, 0x4f, 0x5b, 0x36, 0xa0, 0xa3, 0x71, 0x9b, 0x85, 0x1c, 0xd0, 0x92, 0x98, 0x50, 0x80
        ];

        static readonly byte[] IV1 =
        [
            0x0d, 0x75, 0xcf, 0xcf, 0xb4, 0xbf, 0x31, 0xbd, 0x70, 0x0e, 0xc5, 0x07, 0x5d, 0xf5, 0x53, 0xa6
        ];

        static readonly byte[] KEY2 =
        [
            0x09, 0x33, 0x04, 0xf8, 0x2a, 0x14, 0x66, 0xca, 0x92, 0xc3, 0xf1, 0x29, 0x37, 0x8b, 0x17, 0xfb,
            0x01, 0x6e, 0xe4, 0x2b, 0xb4, 0x26, 0x9a, 0x2e, 0xd5, 0x0f, 0xc6, 0xfa, 0x7a, 0x8c, 0x9f, 0x43
        ];

        static readonly byte[] IV2 =
        [
            0x75, 0x72, 0xec, 0xc6, 0x22, 0xc8, 0xd9, 0xb7, 0x43, 0x20, 0xed, 0x9c, 0xc1, 0x99, 0x31, 0x27
        ];


        static void Main(string[] args)
        {
            string token;
            string proxyToken;
            string minecraftToken;

            Console.Write("Enter proxy token: ");
            
            proxyToken = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(proxyToken))
            {
                Console.WriteLine("Proxy token cannot be empty.");
                return;
            }

            Console.Write("Enter minecraft token: ");
            
            minecraftToken = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(minecraftToken))
            {
                Console.WriteLine("Minecraft token cannot be empty.");
                return;
            }

            /* encryption */
            token = Rot13(proxyToken+minecraftToken+secretMessage);
            token = Rot13(Encrypt(Rot13(Encrypt(token, KEY1, IV1)), KEY2, IV2)); 
            Console.WriteLine(token);               
            
            /* decryption */
            // token = Rot13(token);
            // token = Decrypt(token, KEY2, IV2);
            // token = Rot13(token);
            // token = Decrypt(token, KEY1, IV1);
            // Console.WriteLine(token);


        }

        public static string Decrypt(string encryptedBase64, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CFB;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }


        /** for reference only, DO NOT SHIP TO PRODUCTION */
        static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CFB;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Rot13(string text)
        {
            var result = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsLower(c) ? 'a' : 'A';
                    result.Append((char)((((c - offset) + 13) % 26) + offset));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }


    }
}