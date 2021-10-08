using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SeaCodeLib.Common
{
    public class RSAHelp
    {
        public static string EncryptByPrivateKey(string privateKey, string toEncrypt)
        {
            string rv = "";
            try
            {
                RSACryptoServiceProvider rsap = CreateRsaProviderFromPrivateKey(privateKey);
                byte[] PlainTextBArray = (new UnicodeEncoding()).GetBytes(toEncrypt);
                byte[] CypherTextBArray = rsap.Encrypt(PlainTextBArray, false);
                rv = Convert.ToBase64String(CypherTextBArray);
            }
            catch (Exception ex)
            {
                throw new Exception("EncryptByPrivateKey error! " + ex.Message);
            }
            return rv;
        }

        public static string DecryptByPrivateKey(string privateKey, string toDecrypt, string charcode = "unicode")
        {
            string rv = "";
            try
            {
                RSACryptoServiceProvider rsaCryptoServiceProvider = CreateRsaProviderFromPrivateKey(privateKey);
                byte[] res = rsaCryptoServiceProvider.Decrypt(Convert.FromBase64String(toDecrypt), false);
                if (charcode.ToLower() == "unicode")
                {
                    rv = Encoding.Unicode.GetString(res);
                }
                else if (charcode.ToLower() == "utf8")
                {
                    rv = Encoding.UTF8.GetString(res);
                }
                else
                {
                    throw new Exception("charcode no support!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DecryptByPrivateKey error! " + ex.Message);
            }
            return rv;
        }

        private static RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = System.Convert.FromBase64String(privateKey);
            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();
            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");
                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");
                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");
                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }
            RSA.ImportParameters(RSAparams);
            return RSA;
        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();
            if (bt == 0x81)
                count = binr.ReadByte();
            else
              if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }
            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }
    }
}
