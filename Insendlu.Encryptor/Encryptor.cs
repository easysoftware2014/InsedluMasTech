﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Insendlu.Encryptor
{
    public class Encryptor
    {
        public string Encrypt(string clearText)
        {
            var encryptionKey = "MAKV2SPBNI99212";

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {

                var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);

                encryptor.IV = pdb.GetBytes(16);

                using (var ms = new MemoryStream())
                {

                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {

                        cs.Write(clearBytes, 0, clearBytes.Length);

                        cs.Close();

                    }

                    clearText = Convert.ToBase64String(ms.ToArray());

                }

            }

            return clearText;

        }
        public string Decrypt(string encryptedPass)
        {
            byte[] passByteData = Convert.FromBase64String(encryptedPass);
            var originalPass = Encoding.Unicode.GetString(passByteData);

            return originalPass;
        }
    }
}
