using System;
using System.IO;

static class FileGenerator
{
    
    public static void GenerateFile1(string filePath)
    {
        // Размер файла в байтах (больше 10 килобайт)
        long fileSizeInBytes = 10 * 1024;

        // Генерация случайной последовательности символов
        Random random = new Random();
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";
        int numCharacters = characters.Length;

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            while (new FileInfo(filePath).Length < fileSizeInBytes)
            {
                char randomChar = characters[random.Next(numCharacters)];
                writer.Write(randomChar);
            }
        }

        Console.WriteLine($"Файл {filePath} успешно создан.");
    }
    public static void GenerateFile2(string filePath)
    {
        // Размер файла в байтах (больше 10 килобайт)
        long fileSizeInBytes = 10 * 1024;

        // Набор вероятностей символов (можно изменить по вашему усмотрению)
        double[] probabilities = { 0.2, 0.3, 0.1, 0.2, 0.2 };

        Random random = new Random();
        const string characters = "ABCDE"; // Символы, соответствующие набору вероятностей
        int numCharacters = characters.Length;

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            while (new FileInfo(filePath).Length < fileSizeInBytes)
            {
                char randomChar = GetRandomCharacter(characters, numCharacters, probabilities, random);
                writer.Write(randomChar);
            }
        }

        Console.WriteLine($"Файл {filePath} успешно создан.");
    }
    static char GetRandomCharacter(string characters, int numCharacters, double[] probabilities, Random random)
    {
        double randomValue = random.NextDouble();
        double cumulativeProbability = 0;

        for (int i = 0; i < numCharacters; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return characters[i];
            }
        }

        // Если не удалось выбрать символ по заданным вероятностям, вернуть последний символ
        return characters[numCharacters - 1];
    }

    public static void GenerateFile3(string filePath)
    {
        // Текст стихотворения
        string poemText =
@"Two roads diverged in a yellow wood,
And sorry I could not travel both
And be one traveler, long I stood
And looked down one as far as I could
To where it bent in the undergrowth;

Then took the other, as just as fair,
And having perhaps the better claim,
Because it was grassy and wanted wear;
Though as for that the passing there
Had worn them really about the same,

And both that morning equally lay
In leaves no step had trodden black.
Oh, I kept the first for another day!
Yet knowing how way leads on to way,
I doubted if I should ever come back.

I shall be telling this with a sigh
Somewhere ages and ages hence:
Two roads diverged in a wood, and I—
I took the one less traveled by,
And that has made all the difference.";

        // Запись текста стихотворения в файл
        File.WriteAllText(filePath, poemText);

        Console.WriteLine($"Файл {filePath} успешно создан.");
    }
}

class Program
{
    static string file1 = "../../../res/random_sequence_1.txt";
    static string file2 = "../../../res/random_sequence_2.txt";
    static string file3 = "../../../res/random_sequence_3.txt";
    static void Main(string[] args)
    {
        FileGenerator.GenerateFile1(file1);
        FileGenerator.GenerateFile2(file2);
        FileGenerator.GenerateFile3(file3);

        Console.WriteLine($"Entrop H1 = {Math.Round(CalculateShannonEntropy1(file1), 5)}");
        Console.WriteLine($"Entrop H2 = {Math.Round(CalculateShannonEntropy2(file1), 5)}");
        Console.WriteLine($"Entrop H3 = {Math.Round(CalculateShannonEntropy3(file1), 5)} \n");

        Console.WriteLine($"Entrop H1 = {Math.Round(CalculateShannonEntropy1(file2), 5)}");
        Console.WriteLine($"Entrop H2 = {Math.Round(CalculateShannonEntropy2(file2), 5)}");
        Console.WriteLine($"Entrop H3 = {Math.Round(CalculateShannonEntropy3(file2), 5)}\n");

        Console.WriteLine($"Entrop H1 = {Math.Round(CalculateShannonEntropy1(file3), 5)}");
        Console.WriteLine($"Entrop H2 = {Math.Round(CalculateShannonEntropy2(file3), 5)}");
        Console.WriteLine($"Entrop H3 = {Math.Round(CalculateShannonEntropy3(file3), 5)}\n");

        Console.WriteLine($"Max Entrop = {Math.Round(CalculateMaxEntropy(5), 5)}\n");

    }
    static double CalculateShannonEntropy1(string filePath)
    {
        // Считываем текст из файла
        string text = File.ReadAllText(filePath);

        // Определяем частоты отдельных символов файла
        Dictionary<char, int> charFrequencies = new Dictionary<char, int>();
        foreach (char c in text)
        {
            if (!charFrequencies.ContainsKey(c))
                charFrequencies[c] = 0;
            charFrequencies[c]++;
        }

        // Рассчитываем оценку энтропии по формуле Шеннона
        double entropy = 0;
        int totalCharacters = text.Length;
        foreach (var frequency in charFrequencies.Values)
        {
            double probability = (double)frequency / totalCharacters;
            entropy -= probability * Math.Log(probability, 2);
        }

        return entropy;
    }
    static double CalculateShannonEntropy2(string filePath)
    {
        // Считываем текст из файла
        string text = File.ReadAllText(filePath);

        // Определяем частоты всех последовательных пар символов в файле
        Dictionary<string, int> pairFrequencies = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 1; i++)
        {
            string pair = text.Substring(i, 2);
            if (!pairFrequencies.ContainsKey(pair))
                pairFrequencies[pair] = 0;
            pairFrequencies[pair]++;
        }

        // Рассчитываем оценку энтропии по формуле Шеннона
        double entropy = 0;
        int totalPairs = text.Length - 1;
        foreach (var frequency in pairFrequencies.Values)
        {
            double probability = (double)frequency / totalPairs;
            entropy -= probability * Math.Log(probability, 2);
        }

        return entropy;
    }
    static double CalculateShannonEntropy3(string filePath)
    {
        // Считываем текст из файла
        string text = File.ReadAllText(filePath);

        // Определяем частоты всех последовательных троек символов в файле
        Dictionary<string, int> tripletFrequencies = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 2; i++)
        {
            string triplet = text.Substring(i, 3);
            if (!tripletFrequencies.ContainsKey(triplet))
                tripletFrequencies[triplet] = 0;
            tripletFrequencies[triplet]++;
        }

        // Рассчитываем оценку энтропии по формуле Шеннона и делим на 3
        double entropy = 0;
        int totalTriplets = text.Length - 2;
        foreach (var frequency in tripletFrequencies.Values)
        {
            double probability = (double)frequency / totalTriplets;
            entropy -= probability * Math.Log(probability, 2);
        }

        entropy /= 3; // Делим на 3, как указано в задании

        return entropy;
    }

    static double CalculateMaxEntropy(int alphabetSize)
    {
        // Предполагаем, что каждый символ имеет одинаковую вероятность
        double probability = 1.0 / alphabetSize;

        // Рассчитываем максимальную энтропию по формуле Шеннона
        double maxEntropy = -alphabetSize * probability * Math.Log(probability, 2);
        return maxEntropy;
    }
}