using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System.Diagnostics.Metrics;

namespace TestingHelpers;
public static class RandomStringGenerator
{
    private static readonly Random _random = new();
    private static int _counter = 0;


    //--------------------------// 

    public static string Generate(int maxLength = 20)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Characters(chars, maxLength));
    }

    //--------------------------// 

    private static char[] Characters(string chars, int count)
    {
        var charsArray = chars.ToCharArray();
        return [.. Enumerable.Range(0, count).Select(_ => charsArray[_random.Next(charsArray.Length)])];
    }

    //--------------------------// 

    public static string Word(int min = 4, int max = 10) =>
        RandomizerFactory
            .GetRandomizer(new FieldOptionsTextWords() { Min = min, Max = max })
            ?.Generate()
            ?.Split(' ')
            ?.FirstOrDefault()
            ?? Generate();

    //--------------------------// 

    public static string Sentence(int min = 4, int max = 10) =>
        RandomizerFactory
            .GetRandomizer(new FieldOptionsTextWords() { Min = min, Max = max })
            ?.Generate()
            ?? Generate();

    //--------------------------// 

    public static string FirstName()
    {
        // Generate a random word
        var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
        return randomizerFirstName?.Generate() ?? Generate();
    }

    //--------------------------// 

    public static string LastName()
    {
        // Generate a random word
        var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
        return randomizerLastName?.Generate() ?? Generate();
    }

    //--------------------------// 

    public static string PhoneNumber()
    {
        // Irish mobile numbers start with 08x, e.g., 087, 086, 085, etc.
        string[] prefixes = ["083", "085", "086", "087", "089"];
        string prefix = prefixes[_random.Next(prefixes.Length)];
        string number = $"{_random.Next(100, 999)} {_random.Next(1000, 9999)}";
        return $"{prefix} {number}";
    }


    //--------------------------// 

    public static string Email()
    {
        return RandomizerFactory.GetRandomizer(new FieldOptionsEmailAddress())
            .Generate() ?? $"testemail{++_counter}@test.com";
    }


}//Cls