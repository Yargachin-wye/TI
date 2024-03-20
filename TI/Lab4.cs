using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI
{
    public class Lab4
    {
        static string file1 = "../../../res/example_1.txt";
        static string file2 = "../../../res/example_2.txt";
        static string file3 = "../../../res/example_3.txt";
        static string file4 = "../../../res/example_4.txt";
        static string file5 = "../../../res/example_5.txt";
        public static void Show(string[] args)
        {

            int[,] generatorMatrix = ReadMatrixFromFile(file1);

            // Размерность кода
            int dimension = generatorMatrix.GetLength(1);

            // Количество кодовых слов
            int codewordsCount = (int)Math.Pow(2, dimension);

            // Минимальное кодовое расстояние
            int minDistance = CalculateMinDistance(generatorMatrix);

            // Вывод характеристик
            Console.WriteLine($"Размерность кода: {dimension}");
            Console.WriteLine($"Количество кодовых слов: {codewordsCount}");
            Console.WriteLine($"Минимальное кодовое расстояние: {minDistance}");
        }
        static int[,] ReadMatrixFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] dimensions = lines[0].Split(' ');
                int rowCount = int.Parse(dimensions[0]);
                int colCount = int.Parse(dimensions[1]);
                int[,] matrix = new int[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    string[] rowValues = lines[i + 1].Split(' ');
                    for (int j = 0; j < colCount; j++)
                    {
                        matrix[i, j] = int.Parse(rowValues[j]);
                    }
                }

                return matrix;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
                return null;
            }
        }

        static int CalculateMinDistance(int[,] matrix)
        {
            int minDistance = int.MaxValue;
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            for (int i = 0; i < rowCount - 1; i++)
            {
                for (int j = i + 1; j < rowCount; j++)
                {
                    int distance = 0;
                    for (int k = 0; k < colCount; k++)
                    {
                        if (matrix[i, k] != matrix[j, k])
                        {
                            distance++;
                        }
                    }
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }

            return minDistance;
        }
    }
}
