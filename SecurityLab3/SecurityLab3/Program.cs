// See https://aka.ms/new-console-template for more information

using System;
using SecurityLab3;

class Program
{
    public static String GenerateRandomName()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[32];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
    public static int Main()
    {
        var client = new NetworkClient();
        
        client.InitAccount(GenerateRandomName());
        
        return 0;
    }
}

