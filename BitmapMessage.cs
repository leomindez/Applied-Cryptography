using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BitmapMessage
{

    public string Hide(byte[] bitmap, string message)
    {
        List<byte[]> bitmapBytes = GetBitmapBytes(bitmap);
        List<byte[]> messageBytes = GetMessageBytes(message);
        byte[] bitmapHeaders = bitmap.Take(26).ToArray();
        string hiddenMessage = XorBitMapMessage(bitmapHeaders, bitmapBytes, messageBytes);
        return hiddenMessage;
    }

    private List<byte[]> GetBitmapBytes(byte[] bitmap)
    {
        int offset = 26;
        int step = 4;

        List<byte[]> bitmapBytes = new List<byte[]>();
        for (int index = offset; index < bitmap.Length; index += step)
        {
            byte[] item = bitmap.Skip(index).Take(step).ToArray();
            bitmapBytes.Add(item);
        }
        return bitmapBytes;
    }

    private List<byte[]> GetMessageBytes(string message)
    {
        List<byte> messageBytes = message.Split(" ").Select(hex => Convert.ToByte(hex, 16)).ToList();
        List<byte[]> mask = messageBytes.Select(b =>
            {
                byte b1 = (byte)((b & 0xC0) >> 6);
                byte b2 = (byte)((b & 0x30) >> 4);
                byte b3 = (byte)((b & 0xC) >> 2);
                byte b4 = ((byte)(b & 0x3));
                byte[] maskBytes = new byte[] { b1, b2, b3, b4 };
                return maskBytes;
            }
        ).ToList();
        return mask;
    }

    private string XorBitMapMessage(byte[] bitmapHeaders, List<byte[]> bitmap, List<byte[]> message)
    {
        List<byte> hiddenMessage = new List<byte>(bitmapHeaders);
        for (int i = 0; i < message.Count; i++)
        {
            byte[] bitmapBytes = bitmap[i];
            byte[] messageBytes = message[i];
            for (int j = 0; j < 4; j++)
            {
                int xor = bitmapBytes[j] ^ messageBytes[j];
                hiddenMessage.Add((byte)xor);
            }
        }
        return BitConverter.ToString(hiddenMessage.ToArray()).Replace("-", " ");
    }
}