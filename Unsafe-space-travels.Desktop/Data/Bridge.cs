using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Unsafe_space_travels.Desktop.Data
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    sealed public class Bridge
    {
        public string[] SaveFiles()
        {
            return Directory.GetFiles(StartUp.SaveGamesFolderPath, false, ".yaml").OrderBy(x => File.GetInfo(x).LastWriteTimeUtc).Select(x => Path.GetFileName(x)).ToArray();
        }

        public string SaveSaveFile(string _fileName, string _data)
        {
            string fileName = Path.ReplaceInvalidFileNameChars(_fileName);
            string filePath = Path.Combine(StartUp.SaveGamesFolderPath, fileName + ".yaml");

            File.WriteAllText(filePath, _data);

            return fileName;
        }

        public string LoadSaveFile(string _fileName)
        {
            string filePath = Path.Combine(StartUp.SaveGamesFolderPath, _fileName + ".yaml");
            return File.ReadAllText(filePath);
        }

        public bool DeleteSaveFile(string _fileName)
        {
            string fileName = Path.ReplaceInvalidFileNameChars(_fileName);
            string filePath = Path.Combine(StartUp.SaveGamesFolderPath, fileName + ".yaml");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}