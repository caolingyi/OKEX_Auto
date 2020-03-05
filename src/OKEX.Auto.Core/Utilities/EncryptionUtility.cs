using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OKEX.Auto.Core.Utilities
{
    public static class EncryptionUtility
    {
        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        public static string EncryptSHA1(string input, string salt)
        {
            // 将密码和salt值转换成字节形式并连接起来
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            // 选择算法，对连接后的值进行散列
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);

            // 以字符串形式返回散列值
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// MD5 加密静态方法
        /// </summary>
        /// <param name="EncryptString">待加密的密文</param>
        /// <returns>returns</returns>
        public static string MD5Encrypt(string EncryptString)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                throw (new Exception("密文不得为空"));
            }

            MD5 m_ClassMD5 = new MD5CryptoServiceProvider();

            string m_strEncrypt = "";
            try
            {
                m_strEncrypt = BitConverter.ToString(
                    m_ClassMD5.ComputeHash(
                        Encoding.Default.GetBytes(EncryptString)
                        )
                    ).Replace("-", "");

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_ClassMD5.Clear();
            }
            return m_strEncrypt;
        }


        public static string MD5EncryptForSMS(string text)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        /// <summary>
        /// DES 加密(数据加密标准，速度较快，适用于加密大量数据的场合)
        /// </summary>
        /// <param name="EncryptString">待加密的密文</param>
        /// <param name="EncryptKey">加密的密钥</param>
        /// <returns>returns</returns>
        public static string DESEncrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(EncryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (EncryptKey.Length != 8)
            {
                throw (new Exception("密钥必须为8位"));
            }

            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strEncrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(
                    m_stream,
                    m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV),
                    CryptoStreamMode.Write);

                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_cstream.Close();
                m_cstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_DESProvider.Clear();
            }

            return m_strEncrypt;
        }

        /// <summary>
        /// DES 解密(数据加密标准，速度较快，适用于加密大量数据的场合)
        /// </summary>
        /// <param name="DecryptString">待解密的密文</param>
        /// <param name="DecryptKey">解密的密钥</param>
        /// <returns>returns</returns>
        public static string DESDecrypt(string DecryptString, string DecryptKey)
        {

            if (string.IsNullOrEmpty(DecryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(DecryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (DecryptKey.Length != 8)
            {
                throw (new Exception("密钥必须为8位"));
            }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strDecrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close(); m_stream.Dispose();
                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_DESProvider.Clear();
            }
            return m_strDecrypt;
        }

        /// <summary>
        /// RC2 加密(用变长密钥对大量数据进行加密)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        /// <returns>returns</returns>
        public static string RC2Encrypt(string EncryptString, string EncryptKey)
        {

            if (string.IsNullOrEmpty(EncryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(EncryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (EncryptKey.Length < 5 || EncryptKey.Length > 16)
            {
                throw (new Exception("密钥必须为5-16位"));
            }
            string m_strEncrypt = "";
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_cstream.Close();
                m_cstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_RC2Provider.Clear();
            }
            return m_strEncrypt;
        }

        /// <summary>
        /// RC2 解密(用变长密钥对大量数据进行加密)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        /// <returns>returns</returns>
        public static string RC2Decrypt(string DecryptString, string DecryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(DecryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            if (DecryptKey.Length < 5 || DecryptKey.Length > 16)
            {
                throw (new Exception("密钥必须为5-16位"));
            }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strDecrypt = "";
            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();
            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_cstream.Close();
                m_cstream.Dispose();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_RC2Provider.Clear();
            }
            return m_strDecrypt;
        }

        /// <summary>
        /// 3DES 加密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey1">密钥一</param>
        /// <param name="EncryptKey2">密钥二</param>
        /// <param name="EncryptKey3">密钥三</param>
        /// <returns>returns</returns>

        public static string DES3Encrypt(string EncryptString, string EncryptKey1, string EncryptKey2, string EncryptKey3)
        {
            string m_strEncrypt = "";
            try
            {
                m_strEncrypt = DESEncrypt(EncryptString, EncryptKey3);
                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey2);
                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return m_strEncrypt;

        }

        /// <summary>
        /// 3DES 解密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey1">密钥一</param>
        /// <param name="DecryptKey2">密钥二</param>
        /// <param name="DecryptKey3">密钥三</param>
        /// <returns>returns</returns>
        public static string DES3Decrypt(string DecryptString, string DecryptKey1, string DecryptKey2, string DecryptKey3)
        {
            string m_strDecrypt = "";
            try
            {
                m_strDecrypt = DESDecrypt(DecryptString, DecryptKey1);
                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey2);
                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return m_strDecrypt;
        }

        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(EncryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            string m_strEncrypt = "";

            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
                m_csstream.Write(m_btEncryptString, 0, m_btEncryptString.Length); m_csstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_csstream.Close();
                m_csstream.Dispose();

            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_AESProvider.Clear();
            }
            return m_strEncrypt;

        }

        /// <summary>  
        /// AES Byte类型 加密  
        /// </summary>  
        /// <param name="data">待加密明文</param>  
        /// <param name="keyVal">密钥值</param>  
        /// <param name="ivVal">加密辅助向量</param>  
        /// <returns></returns>  
        public static byte[] AesByte(this byte[] data, string keyVal, string ivVal)
        {
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(keyVal.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(ivVal.PadRight(bVector.Length)), bVector, bVector.Length);
            byte[] cryptograph;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bVector), CryptoStreamMode.Write))
                    {
                        cStream.Write(data, 0, data.Length);
                        cStream.FlushFinalBlock();
                        cryptograph = mStream.ToArray();
                    }
                }
            }
            catch
            {
                cryptograph = null;
            }
            return cryptograph;
        }

        /// <summary>  
        /// AES Byte类型 解密  
        /// </summary>  
        /// <param name="data">待解密明文</param>  
        /// <param name="keyVal">密钥值</param>  
        /// <param name="ivVal">加密辅助向量</param> 
        /// <returns></returns>  
        public static byte[] UnAesByte(this byte[] data, string keyVal, string ivVal)
        {
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(keyVal.PadRight(bKey.Length)), bKey, bKey.Length);
            byte[] bVector = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(ivVal.PadRight(bVector.Length)), bVector, bVector.Length);
            byte[] original;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream(data))
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bVector), CryptoStreamMode.Read))
                    {
                        using (MemoryStream originalMemory = new MemoryStream())
                        {
                            byte[] buffer = new byte[1024];
                            int readBytes;
                            while ((readBytes = cStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                originalMemory.Write(buffer, 0, readBytes);
                            }

                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch
            {
                original = null;
            }
            return original;
        }

        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string DecryptString, string DecryptKey)
        {

            if (string.IsNullOrEmpty(DecryptString))
            {
                throw (new Exception("密文不得为空"));
            }
            if (string.IsNullOrEmpty(DecryptKey))
            {
                throw (new Exception("密钥不得为空"));
            }
            string m_strDecrypt = "";
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            Rijndael m_AESProvider = Rijndael.Create();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
                m_csstream.Write(m_btDecryptString, 0, m_btDecryptString.Length); m_csstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_csstream.Close();
                m_csstream.Dispose();

            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                m_AESProvider.Clear();
            }
            return m_strDecrypt;
        }

        /// <summary>
        /// 生成密钥
        /// <param name="PrivateKey">私钥</param>
        /// <param name="PublicKey">公钥</param>
        /// <param name="KeySize">密钥长度：512,1024,2048，4096，8192</param>
        /// </summary>
        public static void Generator(out string PrivateKey, out string PublicKey, int KeySize = 2048)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KeySize);
            PrivateKey = rsa.ToXmlString(true); //将RSA算法的私钥导出到字符串PrivateKey中 参数为true表示导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
            PublicKey = rsa.ToXmlString(false); //将RSA算法的公钥导出到字符串PublicKey中 参数为false表示不导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
        }

        /// <summary>
        /// AES加密类
        /// </summary>
        public static class AesEncryptHelper
        {
            /// <summary>
            /// 256位处理key 
            /// </summary>
            /// <param name="keyArray"></param>
            /// <returns></returns>
            private static byte[] GetAesKey(byte[] keyArray)
            {
                byte[] newArray = new byte[32];
                for (int i = 0; i < newArray.Length; i++)
                {
                    if (i >= keyArray.Length)
                    {
                        newArray[i] = 0;
                    }
                    else
                    {
                        newArray[i] = keyArray[i];
                    }
                }
                return newArray;
            }

            /// <summary>
            /// 使用AES加密字符串
            /// </summary>
            /// <param name="content">加密内容</param>
            /// <param name="key">秘钥，需要128位、256位.....</param>
            /// <returns>Base64字符串结果</returns>
            public static string AesEncrypt(string content, string key)
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new ArgumentNullException(nameof(content));
                }
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                keyArray = GetAesKey(keyArray);
                using (Aes aes = Aes.Create())
                {
                    aes.IV = Encoding.UTF8.GetBytes("0000000000000000");
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = keyArray;
                    var encryptor = aes.CreateEncryptor();
                    byte[] resultArray = encryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Convert.ToBase64String(resultArray);
                }
            }

            /// <summary>
            /// 使用AES解密字符串
            /// </summary>
            /// <param name="content">内容</param>
            /// <param name="key">秘钥，需要128位、256位.....</param>
            /// <returns>UTF8解密结果</returns>
            public static string AesDecrypt(string content, string key)
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new ArgumentNullException(nameof(content));
                }
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(nameof(key));
                }
                byte[] encryptBytes = Convert.FromBase64String(content);
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                keyArray = GetAesKey(keyArray);
                using (Aes aes = Aes.Create())
                {
                    aes.IV = Encoding.UTF8.GetBytes("0000000000000000");
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = keyArray;
                    var decryptor = aes.CreateDecryptor();
                    byte[] resultArray = decryptor.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
                    return Encoding.UTF8.GetString(resultArray);
                }
            }
        }
    }

}
