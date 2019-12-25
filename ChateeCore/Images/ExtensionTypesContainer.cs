using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public static class ExtensionTypesContainer
    {
        public static List<string> ExtensionsTypesToStringList = new List<string>(Enum.GetNames(typeof(ExtensionType)));
        public static bool IsImage(string filePath)
        {
            var extension = new FileInfo(filePath).Extension.Replace(".", "");
            if (extension == "png" || extension == "jpg" || extension == "jpeg")
                return true;
            return false;
        }
        public static string SetFileTypeImage(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            var extension = fileInfo.Extension.Replace(".", string.Empty);
            if (ExtensionTypesContainer.ExtensionsTypesToStringList.Contains(extension))
                return  "pack://application:,,,/Images/Files/" + extension + ".png";
            else if (string.Equals(extension, "docx"))
                return "pack://application:,,,/Images/Files/" + "doc.png";
            else
                return "pack://application:,,,/Images/Files/" + "default.png";
        }
    }
}
