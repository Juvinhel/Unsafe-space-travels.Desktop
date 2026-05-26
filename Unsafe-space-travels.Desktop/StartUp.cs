using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommandLine;
using Lemon;
using Unsafe_space_travels.Desktop.Data;

namespace Unsafe_space_travels.Desktop
{
    static public class StartUp
    {
        [STAThread]
        static public void Main(string[] _args)
        {
            try
            {
                WebFolderPath = Path.MakeRooted(Path.Combine(Program.MyFolderPath, "web"));
                SaveGamesFolderPath = Path.MakeRooted(Path.Combine(Program.MyFolderPath, "saves"));
                Directory.Create(WebFolderPath);
                VersionFilePath = Path.Combine(WebFolderPath, "version.txt");
                ReleaseNotesFilePath = Path.Combine(WebFolderPath, "release-notes.txt");

                Parser parser = new Parser(with => with.CaseInsensitiveEnumValues = true);
                ParserResult<Arguments> result = parser.ParseArguments<Arguments>(_args);

                Config.Load();

                App app = new App();
                app.InitializeComponent();
                app.Run();

                Config.Save();
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached) throw;
                else MessageBox.Show(ex.Message, ex.GetType().Name);
            }
        }

        static public string WebFolderPath { get; private set; }
        static public string SaveGamesFolderPath { get; private set; }
        static public string VersionFilePath { get; private set; }
        static public string ReleaseNotesFilePath { get; private set; }
    }

    [Verb("run", isDefault: true)]
    sealed public class Arguments
    {
    }
}
