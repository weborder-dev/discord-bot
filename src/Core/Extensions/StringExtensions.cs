using System.Text;

namespace DiscordBot.Core.Extensions;

public static class StringExtensions
{
    #region Public Methods

    public static ReadOnlySpan<byte> HexStringToByteSpan(this string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }

    public static ReadOnlySpan<byte> ToByteSpan(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

    public static byte[] ToByteArray(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }

   
    #endregion
}