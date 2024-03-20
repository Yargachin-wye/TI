using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO.Compression;

namespace TI
{
    public static class Lab3
    {
        static string file1 = "../../../res/random_sequence_1.txt";
        static string file2 = "../../../res/random_sequence_2.txt";
        static string file3 = "../../../res/random_sequence_3.txt";
        static string fileCoded = "../../../res/file_coded.txt";
        public static void Show()
        {
            FileGenerator.GenerateFile1(file1);
            FileGenerator.GenerateFile2(file2);
            FileGenerator.GenerateFile3(file3);
            int blockSize = 4;

            EncodeFileCompress(file1, fileCoded, blockSize);

            byte[] origBytes = File.ReadAllBytes(file1);
            byte[] encodedBytes = File.ReadAllBytes(fileCoded);

            double redundancy = (double)encodedBytes.Length / origBytes.Length;

            Console.WriteLine($"Размер блока: {blockSize}, Избыточность: {redundancy:P}");
        }
        public static void EncodeFileSHA(string inputFile, string outputFile, int blockSize)
        {
            using (FileStream inputStream = File.OpenRead(inputFile))
            using (FileStream outputStream = File.Create(outputFile))
            {
                byte[] buffer = new byte[blockSize];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, blockSize)) > 0)
                {

                    // Пример кодирования блока данных с использованием SHA-256
                    string hash = ComputeSHA256Hash(buffer);
                    byte[] hashBytes = Encoding.UTF8.GetBytes(hash);

                    outputStream.Write(hashBytes, 0, hashBytes.Length);
                }
            }
        }
        public static void EncodeFileCompress(string inputFile, string outputFile, int blockSize)
        {
            using (FileStream inputStream = File.OpenRead(inputFile))
            using (FileStream outputStream = File.Create(outputFile))
            using (DeflateStream compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
            {
                byte[] buffer = new byte[blockSize];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, blockSize)) > 0)
                {
                    // Сжатие блока данных
                    compressStream.Write(buffer, 0, bytesRead);
                }
            }
        }
        public static string ComputeSHA256Hash(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(data);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Форматирование в шестнадцатеричный вид
                }
                return builder.ToString();
            }
        }
    }
}
