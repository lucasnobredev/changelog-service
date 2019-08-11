using ChangeLogWeb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChangeLogWeb.Services
{
    public class SecurityService
    {
        private readonly ITeamRepository _teamRepository;

        public SecurityService(
            ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public string EncryptSecretKey(Stream requestBody, string secretKey)
        {
            using (var reader = new StreamReader(requestBody))
            {
                var txt = reader.ReadToEndAsync().Result;

                var secret = Encoding.ASCII.GetBytes(secretKey);
                var payloadBytes = Encoding.ASCII.GetBytes(txt);

                using (var hmSha1 = new HMACSHA1(secret))
                {
                    var hash = hmSha1.ComputeHash(payloadBytes);

                    return ToHexString(hash);
                }
            }
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        public static string ToHexString(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}
