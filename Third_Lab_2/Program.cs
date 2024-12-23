using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Gif;

namespace ImageProcessorApp
{
    

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть шлях до папки:");
            string directoryPath = Console.ReadLine();

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Папка не існує.");
                return;
            }

            ImageProcessor processor = new ImageProcessor(directoryPath);
            processor.ProcessImages();
        }
    }
}
