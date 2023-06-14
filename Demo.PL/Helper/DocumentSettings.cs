using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get located Folder Path

            //string folderPath = "C:\\Users\\abdeen\\source\\repos\\FirstDemo.Solution\\Demo.PL\\wwwroot\\Files\\Images\\";
            //string folderPath = Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\"+ folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            // 2. Get File Name and make it UNIQUE
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Save FIle As Streams (Stream :Data per Time)

            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;
        }

        public static void DeleteFile(string filName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, filName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
