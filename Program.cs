﻿using System;

namespace P1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bmpBytes = new byte[] {
            0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
            0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
            0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,
            0x18,0x00,0x00,0x00,0xFF,0xFF,0xFF,0xFF,
            0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,
            0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
            0x00,0x00,0xFF,0x00,0x00,0xFF,0xFF,0xFF,
            0xFF,0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,
            0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00,
            0x00,0x00
            };

            string message = args[0];
            BitmapMessage bitmapMessage = new BitmapMessage();
            string hiddenMessage = bitmapMessage.Hide(bmpBytes, message);
            Console.WriteLine(hiddenMessage);
        }
    }
}
