using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace BatallaNaval.Persistence
{
    class EncoderMD5
    {
        public static string Encode(string pass)
        {
            string hash = "Cuando creíamos que teniamos todas las respuestas, de pronto cambiaron todas las preguntas";
            byte[] data = UTF8Encoding.UTF8.GetBytes(pass);
            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }public static string Decode(string pass)
        {
            string hash = "Cuando creíamos que teniamos todas las respuestas, de pronto cambiaron todas las preguntas";
            byte[] data = Convert.FromBase64String(pass);
            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateDecryptor ();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }

    }
}
