using System.Security.Cryptography;
using System.Text;

namespace EncTest.Crypto;

public static class CryptoEngine
{
    const int FIRST_ITEM_LENGTH = 5;
    const int SECENT_ITEM_LENGTH = 8;

    public static string Encrypt(this string input, string key)
    {
        byte[] inputArray = Encoding.UTF8.GetBytes(input);
        Aes tripleDES = Aes.Create();
        tripleDES.Key = Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(this string input, string key)
    {
        byte[] inputArray = Convert.FromBase64String(input);
        Aes tripleDES = Aes.Create();
        tripleDES.Key = Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }

    public static string GenerateKey()
    {
        Random rand = new();
        return $"{10.CreateToken()[0..FIRST_ITEM_LENGTH]}-{10.CreateToken()[0..SECENT_ITEM_LENGTH]}-{rand.Next(9)}";
    }

    public static string CreateToken(this int length)
    {
        Random rnd = new();
        var maximum = DateTime.Now.Second + DateTime.Now.DayOfYear;
        string token = Guid.NewGuid().ToString("N") + "-";
        for (int i = 0; i < length; i++)
        {
            int number = rnd.Next(maximum);
            token += (i * number);
        }
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
    }
}