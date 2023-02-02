using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace GenericMusicClient.Utils;

public class Crypto
{
    static Func<string> GetRandomStr = () =>
    {
        var chs = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        var r = new Random();
        StringBuilder sb = new();
        for (var i = 0; i < 16; i++)
        {
            sb.Append(chs[r.Next(0, chs.Length)]);
        }

        return sb.ToString();
    };

    public static Dictionary<string, string> NeteaseEncrypt(string s)
    {
        var AesKey = Encoding.UTF8.GetBytes("0CoJUm6Qyw8W8jud");
        var AesIV = Encoding.UTF8.GetBytes("0102030405060708");
        var AesSecKey = Encoding.UTF8.GetBytes(GetRandomStr());
        //var b = Encoding.UTF8.GetBytes(s);

        var ret = new Dictionary<string, string>();
        var p = AesEncrypt(
            AesEncrypt(
                s, AesKey, AesIV, CipherMode.CBC
            ),
            AesSecKey, AesIV, CipherMode.CBC
        );
        ret.Add("params", p);
        ret.Add("encSecKey", nRSAEncrypt(AesSecKey));
        return ret;
    }

    public static string AesEncrypt(string s, byte[] key, byte[] iv, CipherMode mode)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        using var aes = Aes.Create();
        aes.IV = iv;
        aes.Key = key;
        aes.Mode = mode;
        aes.Padding = PaddingMode.PKCS7;
        var encryptor = aes.CreateEncryptor();
        return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
    }

    public static string nRSAEncrypt(byte[] bytes)
    {
        var modulus = BigInteger.Parse(
            "157794750267131502212476817800345498121872783333389747424011531025366277535262539913701806290766479189477533597854989606803194253978660329941980786072432806427833685472618792592200595694346872951301770580765135349259590167490536138082469680638514416594216629258349130257685001248172188325316586707301643237607"
        );
        var exponent = BigInteger.Parse("65537");
        var data = new BigInteger(bytes.ToArray());
        var result = BigInteger.ModPow(data, exponent, modulus);
        return BitConverter.ToString(
                result.ToByteArray().Reverse().ToArray()
            )
            .TrimStart(new char[] { '0' })
            .Replace("-", "")
            .ToLower();
    }
}