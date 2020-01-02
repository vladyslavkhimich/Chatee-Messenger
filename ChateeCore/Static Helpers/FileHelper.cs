using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public static class FileHelper
    {
        public static byte[] ConvertFileToArrayOfBytes(string filePath)
        {       
            return File.ReadAllBytes(filePath);
        }
        public static void ConvertArrayOfBytesToAvatar(string avatarFileName, byte[] avatarBytes)
        {
            string avatarPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/ImageDatabase/Avatars/" + avatarFileName;
            File.WriteAllBytes(avatarPath, avatarBytes);
        }
        public static string ComputeFileCheckSum(string filePath)
        {
            using(var md5 = MD5.Create())
            {
                using(var stream = File.OpenRead(filePath))
                {
                    var fileCheckSum = md5.ComputeHash(stream);
                    return BitConverter.ToString(fileCheckSum).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public static string GetFilePathWithCheckSum(string fileCheckSum, string relativeDirectoryPath, string fileExtensions = "")
        {
            string currentAppDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string fullDirectoryPath = currentAppDirectory + relativeDirectoryPath;
            string[] filePathes;
            if (!string.IsNullOrEmpty(fileExtensions))
            {
                filePathes = Directory.GetFiles(fullDirectoryPath, fileExtensions, SearchOption.TopDirectoryOnly);
            }
            else
            {
                filePathes = Directory.GetFiles(fullDirectoryPath);
            }
            foreach (var filePath in filePathes)
                if (ComputeFileCheckSum(filePath) == fileCheckSum)
                    return filePath;
            return string.Empty;
        }
    }
}
