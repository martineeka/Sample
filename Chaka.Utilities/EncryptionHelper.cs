using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chaka.Utilities
{
    public class EncryptionHelper
    {
        public static decimal DecimalEncyption(decimal value, string key)
        {
            //Random rnd = new Random();
            //decimal randomNumbers = rnd.Next(1, 99);

            string valueInString = value.ToString();
            valueInString = valueInString.PadLeft(15, '0');

            var enkriptedKey = RijndaelHelper.Encrypt(key, key);

            byte[] keyInBytes = Encoding.UTF8.GetBytes(enkriptedKey);

            decimal randomNumber = 0;
            for (int i = 0; i < keyInBytes.Length; i++)
            {
                randomNumber = (keyInBytes[i] * i) + randomNumber;
                randomNumber = (i * 255) + randomNumber;
            }

            string randomString = randomNumber.ToString();
            //for (int i = 0; i < randomNumbers; i++)
            //{
            //    randomNumber = randomNumber + (8 * i);
            //}

            int j = 0;

            string cipherInString = "";
            for (int i = 0; i < valueInString.Length; i++)
            {
                if (j >= randomString.Length)
                    j = 0;
                int indexof;
                int plainstring = (int)Char.GetNumericValue(valueInString[i]);
                int keystring = (int)Char.GetNumericValue(randomString[j]) >> 1;
                //if (plainstring == 0)
                //{
                //    indexof = keystring;
                //}
                //else
                //{
                //indexof = plainstring - 7;
                //indexof = (indexof < 0) ? indexof * (-1) : indexof;
                //indexof = indexof ^ keystring;

                //indexof = plainstring - 7;
                //indexof = indexof * (-1);
                //indexof = indexof ^ keystring;
                indexof = plainstring >> 1;
                indexof = indexof + 1;
                indexof = indexof ^ keystring;
                //}
                cipherInString = cipherInString + indexof;
                j++;
            }
            j = 0;
            //var plainInString = "";
            //for (int i = 0; i < cipherInString.Length; i++)
            //{
            //    if (j >= randomString.Length)
            //        j = 0;
            //    int indexof;
            //    int cipherstring = (int)Char.GetNumericValue(cipherInString[i]);//cipher.Substring(i, 4);
            //    int keystring = (int)Char.GetNumericValue(randomString[j]);// randomString.Substring(j, 4);
            //    if (cipherstring == keystring)
            //    {
            //        indexof = 0;
            //    }
            //    else
            //    {
            //        indexof = cipherstring ^ keystring;
            //        indexof = (indexof + 8 >= 10) ? (indexof - 8) * (-1) : indexof + 8;
            //    }
            //    plainInString = plainInString + indexof;
            //    j++;
            //}
            return Convert.ToDecimal(cipherInString);

        }
        public static decimal DecimalDecription(decimal value, string key)
        {
            //Random rnd = new Random();
            //decimal randomNumbers = rnd.Next(1, 99);

            string cipherInString = value.ToString();

            var enkriptedKey = RijndaelHelper.Encrypt(key, key);

            byte[] keyInBytes = Encoding.UTF8.GetBytes(enkriptedKey);

            decimal randomNumber = 0;
            for (int i = 0; i < keyInBytes.Length; i++)
            {
                randomNumber = (keyInBytes[i] * i) + randomNumber;
                randomNumber = (i * 255) + randomNumber;
            }

            string randomString = randomNumber.ToString();
            //for (int i = 0; i < randomNumbers; i++)
            //{
            //    randomNumber = randomNumber + (8 * i);
            //}

            int j = 0;

            var plainInString = "";
            for (int i = 0; i < cipherInString.Length; i++)
            {
                if (j >= randomString.Length)
                    j = 0;
                int indexof;
                int cipherstring = (int)Char.GetNumericValue(cipherInString[i]);//cipher.Substring(i, 4);
                int keystring = (int)Char.GetNumericValue(randomString[j]) >> 1;// randomString.Substring(j, 4);
                                                                                //if (cipherstring == keystring)
                                                                                //{
                                                                                //    indexof = 0;
                                                                                //}
                                                                                //else
                                                                                //{
                                                                                //indexof = cipherstring ^ keystring;
                                                                                //indexof = (indexof + 7 >= 10) ? (indexof - 7) * (-1) : indexof + 7;
                                                                                //indexof = indexof*(-1);
                                                                                //indexof = indexof + 7;
                                                                                //}
                indexof = cipherstring ^ keystring;
                indexof = indexof - 1;
                indexof = indexof << 1;

                plainInString = plainInString + indexof;
                j++;
            }
            var noZeroStart = plainInString.TrimStart('0');
            return Convert.ToDecimal(noZeroStart);

        }

     
        public static string EncryptUrlParam(string urlParam)
        {
            string key = "jdsg432387#";

            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] EncryptKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(urlParam);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            var base64 = Convert.ToBase64String(mStream.ToArray()).Replace("+", "0pXPl5").Replace("/", "8rYj3i").Replace("=", "12Y13e");
            return base64;
        }

        public static string DecryptUrlParam(string urlParam)
        {
            urlParam = urlParam.Replace("0pXPl5", "+").Replace("8rYj3i", "/").Replace("12Y13e", "=");
            string key = "jdsg432387#";

            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[urlParam.Length];

            byte[] DecryptKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(urlParam);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        public static string EncryptFromDb(string text)
        {
            string result;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StarbridgesConnectionString"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.ufn_Encrypt('" + text + "') result", conn))
            {
                try
                {
                    conn.Open();
                    result = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception)
                {

                    return text;
                }

            }
            return result;
        }
        public static string DecryptFromDb(string text)
        {
            string result;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StarbridgesConnectionString"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.ufn_Decrypt('" + text + "')", conn))
            {
                try
                {
                    conn.Open();
                    result = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception)
                {

                    return text;
                }

            }
            return result;
        }

    }
}
