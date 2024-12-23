using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
namespace TXT
{
    class FileProcessor
    {
        private readonly int startIndex;
        private readonly int endIndex;

        private readonly string resultFilePath;
        private readonly string noFilePath;
        private readonly string badDataPath;
        private readonly string overflowPath;

        private readonly List<(string FileName, BigInteger Product)> products = new List<(string, BigInteger)>();
        private readonly List<string> missingFiles = new List<string>();
        private readonly List<string> badDataFiles = new List<string>();
        private readonly List<string> overflowFiles = new List<string>();

        public FileProcessor(int startIndex, int endIndex, string resultFilePath, string noFilePath, string badDataPath, string overflowPath)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.resultFilePath = resultFilePath;
            this.noFilePath = noFilePath;
            this.badDataPath = badDataPath;
            this.overflowPath = overflowPath;
        }

        public void ProcessAllFiles()
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                string fileName = $"{i}.txt";

                try
                {
                    ProcessFile(fileName);
                }
                catch (FileNotFoundException)
                {
                    HandleException(fileName, missingFiles);
                }
                catch (FormatException)
                {
                    HandleException(fileName, badDataFiles);
                }
                catch (OverflowException)
                {
                    HandleException(fileName, overflowFiles);
                }
            }

            WriteResultsToFile();
        }

        private void ProcessFile(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                BigInteger number1 = ParseLine(lines[0]);
                BigInteger number2 = ParseLine(lines[1]);
                BigInteger product = CalculateProduct(number1, number2);

                products.Add((fileName, product));
            }
            catch (IndexOutOfRangeException)
            {
                throw new FormatException("Файл не містить достатньо рядків.");
            }
        }

        private BigInteger ParseLine(string line)
        {
            try
            {
                BigInteger number = BigInteger.Parse(line.Trim());
                    
                return number;
            }
            catch(OverflowException)
            {
                throw new OverflowException();
            }
            catch (Exception)
            {
                throw new FormatException("Некоректний формат даних.");
            }
        }

        private BigInteger CalculateProduct(BigInteger num1, BigInteger num2)
        {
            try
            {
                BigInteger product = num1 * num2;

                return product > int.MaxValue || product < int.MinValue
                ? throw new OverflowException()
                : product;
        
            }
            catch (OverflowException)
            {
                throw;
            }
        }

        private void HandleException(string fileName, List<string> targetList)
        {
            targetList.Add(fileName);
        }

        private double CalculateAverage(BigInteger totalSum, int count)
        {
            
            return (double)totalSum / count;
        }

        private void WriteResultsToFile()
        {
            using (StreamWriter writer = new StreamWriter(resultFilePath))
            {
                writer.WriteLine("Добутки файлів:");
                BigInteger totalSum = 0;

                foreach (var (fileName, product) in products)
                {
                    writer.WriteLine($"{fileName}, Добуток: {product}");
                    totalSum += product;
                }

                writer.WriteLine("-----------------------------------");
                writer.WriteLine($"Сума всіх добутків: {totalSum}");

                try
                {
                    double average = CalculateAverage(totalSum, products.Count);
                    writer.WriteLine($"Середнє арифметичне: {average:F2}");
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"Помилка: {ex.Message}");
                }
            }

            WriteToFile(noFilePath, missingFiles);
            WriteToFile(badDataPath, badDataFiles);
            WriteToFile(overflowPath, overflowFiles);
        }

        private void WriteToFile(string filePath, List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }

        public void DisplaySummary()
        {
            Console.WriteLine($"Обробка завершена. Результати збережено у файлі {resultFilePath}.");
            Console.WriteLine($"Відсутні файли: {missingFiles.Count}");
            Console.WriteLine($"Некоректні файли: {badDataFiles.Count}");
            Console.WriteLine($"Файли з переповненням: {overflowFiles.Count}");
        }
        public double CalculateSquareRoot(double value)
        {
            return value < 0
                ? throw new NegativeValueException("Значення не може бути від'ємним.")
                : Math.Sqrt(value);
        }
    }
    
}
