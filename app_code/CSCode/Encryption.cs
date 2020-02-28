using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Encryption
/// </summary>
public class Encryption
{
	public Encryption()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string ConvertStringToHex(String input, System.Text.Encoding encoding)
    {
        Byte[] stringBytes = encoding.GetBytes(input);
        StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
        foreach (byte b in stringBytes)
        {
            sbBytes.AppendFormat("{0:X2}", b);
        }
        return sbBytes.ToString();
    }

    public static string ConvertHexToString(String hexInput, System.Text.Encoding encoding)
    {
        int numberChars = hexInput.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
        }
        return encoding.GetString(bytes);
    }
}