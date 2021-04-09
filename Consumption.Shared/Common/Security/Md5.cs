using System;
using System.Security.Cryptography;
using System.Text;

namespace Consumption.Shared.Common
{
    public class Md5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        //public static string md5(string str, int code)
        //{
        //    string strEncrypt = string.Empty;
        //    if (code == 16)
        //    {
        //        strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
        //    }

        //    if (code == 32)
        //    {
        //        strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        //    }

        //    return strEncrypt;
        //}
        public static string GetMD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">要加密的文本</param>
        /// <param name="sKey">秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string Text, string sKey = "test")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            // md4j = ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey = "test")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }

        /// <summary> 
        /// 获取时间戳 
        /// </summary> 
        /// <returns></returns> 
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }


        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);
            m5.Clear();
            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }



        public static string md5(string str)
        {
            Console.WriteLine(str);
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(str);
                bytHash = md5.ComputeHash(bytValue);
                md5.Clear();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                str = sTemp.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return str;
        }

        public static string toMd5(string str)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(str);
                bytHash = md5.ComputeHash(bytValue);
                md5.Clear();

                StringBuilder hex = new StringBuilder(bytHash.Length * 2);
                byte[] var6 = bytHash;
                int var5 = bytHash.Length;

                for (int var4 = 0; var4 < var5; ++var4)
                {
                    byte b = var6[var4];
                    if ((b & 255) < 16)
                    {
                        hex.Append("0");
                    }

                    hex.Append(String.Format("{0:X}", b & 255));
                }

                return hex.ToString().ToLower();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return str;
        }

        //public static String string2MD5(String inStr)
        //{
        //    MessageDigest md5 = null;
        //    try
        //    {
        //        md5 = MessageDigest.getInstance("MD5");
        //    }
        //    catch (Exception e)
        //    {
        //        System.out.println(e.toString());
        //        e.printStackTrace();
        //        return "";
        //    }
        //    char[] charArray = inStr.toCharArray();
        //    byte[] byteArray = new byte[charArray.length];

        //    for (int i = 0; i < charArray.length; i++)
        //        byteArray[i] = (byte)charArray[i];
        //    byte[] md5Bytes = md5.digest(byteArray);
        //    StringBuffer hexValue = new StringBuffer();
        //    for (int i = 0; i < md5Bytes.length; i++)
        //    {
        //        int val = ((int)md5Bytes[i]) & 0xff;
        //        if (val < 16)
        //            hexValue.append("0");
        //        hexValue.append(Integer.toHexString(val));
        //    }
        //    return hexValue.toString();


        //    StringBuilder hex = new StringBuilder(hash.length * 2);
        //    byte[] var6 = hash;
        //    int var5 = hash.length;

        //    for (int var4 = 0; var4 < var5; ++var4)
        //    {
        //        byte b = var6[var4];
        //        if ((b & 255) < 16)
        //        {
        //            hex.append("0");
        //        }

        //        hex.append(Integer.toHexString(b & 255));
        //    }

        //    return hex.toString();
        //}

        ///
        /// 把string转为UTF8String类型
        ///
        ///
        ///
        public static string UTF8String(string content)
        {
            string reString = null;
            char[] ac = content.ToCharArray();
            int num;
            foreach (char c in ac)
            {
                num = (int)c;
                string n = num.ToString("X2");
                if (n.Length == 4)
                    reString += "\\u" + n;
                else
                    reString += c;
            }

            return reString;
        }

        public static string ecodingJava_C(String str)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding defaultCode = Encoding.Default;
            byte[] utf8Bytes = defaultCode.GetBytes(str);
            //     byte[] defaultBytes = Encoding.Convert(utf8, defaultCode, utf8Bytes);
            char[] defaultChars = new char[defaultCode.GetCharCount(utf8Bytes, 0, utf8Bytes.Length)];
            string aaa = Encoding.Default.GetString(utf8Bytes);
            defaultCode.GetChars(utf8Bytes, 0, utf8Bytes.Length, defaultChars, 0);
            return new string(defaultChars);
        }


        public static string UrlEncode(string input)
        {

            if (input == null || "".Equals(input)) return "";

            StringBuilder sb = new StringBuilder();

            byte[] byStr = System.Text.Encoding.Default.GetBytes(input);

            for (int i = 0; i < byStr.Length; i++)

            {

                sb.Append(@"%" + Convert.ToString(byStr[i], 16));

            }



            return (sb.ToString());
        }


        public static string CharToUTF8(string content)
        {
            if (string.IsNullOrEmpty(content)) return "";
            byte[] buffer1 = Encoding.Default.GetBytes(content);
            byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);

            return Encoding.UTF8.GetString(buffer2, 0, buffer2.Length);
        }

        public static string CharToUTF8Two(string content)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            String unicodeString = utf8.GetString(Encoding.Default.GetBytes(content));
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }




    }
}
