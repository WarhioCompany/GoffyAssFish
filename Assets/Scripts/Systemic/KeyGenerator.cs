using System;
using static UnityEngine.Random;

public static class KeyGenerator
{

    private static char[] libChars = new char[]
    {
        'a', 'z', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
        'q', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm',
        'w', 'x', 'c', 'v', 'b', 'n', '!', '?', '%', '$',
        '*', '-', ';'
    };

    private static char GetRandomChar() { 
        int randomCharIndex = Range(0, libChars.Length);

        int randomCharCaseIndex = Range(0, 1);

        if (randomCharCaseIndex == 0)
            return libChars[randomCharIndex];
        else
            return Char.ToUpper(libChars[randomCharIndex]); 
    }

    public static string Generate(int length) {
        string genKey = string.Empty;

        for(int i = 0; i < length; i++)
        {
            genKey += GetRandomChar();
        }

        return genKey;
    }
}