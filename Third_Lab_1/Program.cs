using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using TXT;

namespace TXT
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.OutputEncoding = UTF8Encoding.UTF8;
                FileProcessor processor = new FileProcessor(10, 29, 
                    "result.txt", "no_file.txt", "bad_data.txt", "overflow.txt");
                               
                processor.ProcessAllFiles();
          
                processor.DisplaySummary();
                
                double[] testValues = { 16, 25, -4, 0, 9 };

                foreach (double value in testValues)
                {
                    try
                    {
                        double result = processor.CalculateSquareRoot(value);
                        Console.WriteLine($"Квадратний корінь з {value}: {result}");
                    }
                    catch (NegativeValueException ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message} (значення: {value})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Глобальна помилка: {ex.Message}");
            }   
        }
    }
    
}
