// Console Smart Password Generator
using System;
using System.Security.Cryptography;
using System.Text;


class Strings
{
    public static readonly string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static readonly string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
    public static readonly string Digits = "0123456789";
    public static readonly string SpecialCharacters = "!@#$%^&*()";
}

class CharacterPrinter
{
    public void PrintCharacter(char symbol, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write(symbol);
        }
        Console.WriteLine();
    }
}


class SmartRandomGen
{
    private int GetSeed(string input)
    {
        byte[] hash;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        int seed = BitConverter.ToInt32(hash, 0);
        return seed;
    }

    public string GenerateSmartRandomString(int length, string secret)
    {
        int seedHash = this.GetSeed(secret);
        Random rand = new Random(seedHash);
        string allCharacters = Strings.UppercaseLetters + Strings.LowercaseLetters + Strings.Digits + Strings.SpecialCharacters;
        char[] password = new char[length];

        for (int i = 0; i < length; i++)
        {
            int randomIndex = rand.Next(0, allCharacters.Length);
            password[i] = allCharacters[randomIndex];
        }

        return new string(password);
    }

}


class ConsoleSmartPasswordGeneratorApp
{
    static void Main(string[] args)
    {
        CharacterPrinter printer = new CharacterPrinter();
        Console.WriteLine("*** Console Smart Password Generator ***");
        SmartRandomGen smartRandomGen = new SmartRandomGen();
        string password = "";
        Console.WriteLine("Enter password length: ");
        printer.PrintCharacter('-', 23);
        int passwordLength = 0;
        try
        {
            passwordLength = Convert.ToInt32(Console.ReadLine());
            printer.PrintCharacter('-', 23);
        }
        catch (Exception e)
        {
            passwordLength = 12;
            Console.WriteLine("Error! You have not entered a password length. I use 12 characters.");
            printer.PrintCharacter('-', 23);
        }
        if (passwordLength <= 0)
        {
            passwordLength = 12;
            Console.WriteLine("Error! Invalid password length. I use 12 characters.");
            printer.PrintCharacter('-', 23);
        }
        else if (passwordLength > 1000)
        {
            passwordLength = 1000;
            Console.WriteLine("Error! The password cannot be longer than 1000 characters. I use 1000 characters.");
            printer.PrintCharacter('-', 23);
        }
        string secretPhrase = "";
        Console.WriteLine("Enter the secret phrase: ");
        printer.PrintCharacter('-', 23);
        secretPhrase = Console.ReadLine();
        printer.PrintCharacter('-', 23);
        password = smartRandomGen.GenerateSmartRandomString(passwordLength, secretPhrase);
        Console.WriteLine(password);
        printer.PrintCharacter('-', 23);
        Console.WriteLine("Press <Enter> to exit...");
        Console.Read();
        printer.PrintCharacter('-', 23);
        Console.WriteLine("=== Smart Legion Lab ===");
    }
}