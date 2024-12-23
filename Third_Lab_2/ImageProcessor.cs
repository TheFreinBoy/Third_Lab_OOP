using ImageProcessorApp;
using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Gif;


public class ImageProcessor
    {
        private readonly string _directoryPath;

        public ImageProcessor(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void ProcessImages()
        {
            string[] files = Directory.GetFiles(_directoryPath);
            foreach (string fileName in files)
            {
                if (IsImageFile(fileName))
                {
                    ProcessImage(fileName);
                }
            }
        }

        private bool IsImageFile(string fileName)
        {
            string[] validExtensions = { ".bmp", ".gif", ".tif", ".tiff", ".jpeg", ".jpg", ".png", ".jfif"};
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            return Array.Exists(validExtensions, ext => ext == extension);
        }

        private void ProcessImage(string fileName)
        {
            try
            {
                using (Image image = Image.Load(fileName))
                {
                    image.Mutate(x => x.Flip(FlipMode.Horizontal));

                    string newFileName = Path.Combine(
                        Path.GetDirectoryName(fileName),
                        Path.GetFileNameWithoutExtension(fileName) + "-mirrored.gif");

                    image.Save(newFileName, new GifEncoder());
                    Console.WriteLine($"Зображення збережено: {newFileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вдалося обробити файл {fileName}: {ex.Message}");
            }
        }
    }