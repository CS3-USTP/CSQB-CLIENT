using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CubeClient
{
    public static class Token
    {
        private static readonly HttpClient client = new();

        public static string? Fetch(string url)
        {
            try
            {
                return Task.Run(async () =>
                {
                    // Create HttpRequestMessage to include cache control headers
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
                    { // Disable caching
                        NoCache = true,
                        NoStore = true,
                        MaxAge = TimeSpan.Zero
                    };

                    // Send the request and get the response
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode(); // Throw if not successful

                    // Read the response content as a string
                    string responseString = await response.Content.ReadAsStringAsync();

                    // Decrypt the response
                    byte[] KEY2_BYTES = new byte[] { 0x09, 0x33, 0x04, 0xf8, 0x2a, 0x14, 0x66, 0xca, 0x92, 0xc3, 0xf1, 0x29, 0x37, 0x8b, 0x17, 0xfb, 0x01, 0x6e, 0xe4, 0x2b, 0xb4, 0x26, 0x9a, 0x2e, 0xd5, 0x0f, 0xc6, 0xfa, 0x7a, 0x8c, 0x9f, 0x43 };
                    byte[] IV2_BYTES = new byte[] { 0x75, 0x72, 0xec, 0xc6, 0x22, 0xc8, 0xd9, 0xb7, 0x43, 0x20, 0xed, 0x9c, 0xc1, 0x99, 0x31, 0x27 };

                    // Perform decryption (ROT13) before AES decryption
                    string level1Decryption = Rot13(responseString);

                    // Perform decryption (AES) using KEY2 and IV2
                    return Decrypt(level1Decryption, KEY2_BYTES, IV2_BYTES);
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
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

        public static byte[] HexToBytes(string hex)
        {
            int numberOfChars = hex.Length;
            byte[] bytes = new byte[numberOfChars / 2];
            for (int i = 0; i < numberOfChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
