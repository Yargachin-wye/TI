using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI
{
    
    public class Lab2
    {
        static string file1 = "../../../res/random_sequence_1.txt";
        static string file2 = "../../../res/random_sequence_2.txt";
        static string file3 = "../../../res/random_sequence_3.txt";

        static string saofile = "../../../res/lab2_sao.txt";
        static Random random = new Random();
        static string PreprocessFile(string filename)
        {
            string line = File.ReadAllText(filename).ToLower();
            line = System.Text.RegularExpressions.Regex.Replace(line, @"[^\w\s]", "");
            line = line.Replace("\n", "");
            return line;
        }

        static double CalcEntropyModified(string line, int symbInRow)
        {
            List<string> splitLine = Enumerable.Range(0, line.Length - symbInRow + 1)
                                               .Select(i => line.Substring(i, symbInRow))
                                               .ToList();

            var actualProbability = splitLine.GroupBy(x => x)
                                             .ToDictionary(g => g.Key, g => (double)g.Count() / line.Length);

            double result = -actualProbability.Values.Sum(x => x * Math.Log(x, 2));
            return result / symbInRow;
        }

        static void CountAlphabetEntropyUsingCreatedFile(string filename, int symbolsStreak)
        {
            for (int i = 1; i <= symbolsStreak; i++)
            {
                Console.WriteLine($"Энтропия для {i} символа(ов) подряд: {CalcEntropyModified(filename, i)}");
            }
        }

        public static int Show()
        {
            Dictionary<char, double> equalProb = new Dictionary<char, double> { { 'a', 1.0 / 3 }, { 'b', 1.0 / 3 }, { 'c', 1.0 / 3 } };
            Dictionary<char, double> diffProb = new Dictionary<char, double> { { 'a', 0.1 }, { 'b', 0.3 }, { 'c', 0.6 } };

            Console.WriteLine("F1");
            FileGenerator.GenerateFile1(file1);
            FileGenerator.GenerateFile2(file2);
            FileGenerator.GenerateFile3(file3);

            Console.WriteLine("текст 1");
            CountAlphabetEntropyUsingCreatedFile(PreprocessFile(file1), 2);
            Console.WriteLine("текст 2");
            CountAlphabetEntropyUsingCreatedFile(PreprocessFile(file2), 2);
            Console.WriteLine("текст 3");
            CountAlphabetEntropyUsingCreatedFile(PreprocessFile(file3), 2);
            return 0;
        }
        
    }
}
