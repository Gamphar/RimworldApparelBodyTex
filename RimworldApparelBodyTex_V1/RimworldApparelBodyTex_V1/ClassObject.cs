using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RimworldApparelBodyTex_V1
{
    class ClassObject
    {
    }


    public class ModMetaData
    {
        public string Name { get; set; }
        public string PackageId { get; set; }
    }

    public class ModActivePackageId
    {
        public string PackageId { get; set; }
    }

    public class GlobalSettings
    {
        public bool Overwrite { get; set; }
        public string BodyTypeSource { get; set; } //use defName
        public string BodyTypeDestination { get; set; } //use defName
        public string TexDestinationPath { get; set; }
        public bool IncludeBodyTexture { get; set; }
        public string RimworldData { get; set; }
        public string BaseSourceTexDir { get; set; }
        public string Step3_TexSourcePath { get; set; }
        public string Step3_TexDestinationPath { get; set; }
    }

    public class InstalledProgram
    {
        public string DisplayName { get; set; }
        public string Version { get; set; }
        public string InstalledDate { get; set; }
        public string Publisher { get; set; }
        public string UnninstallCommand { get; set; }
        public string ModifyPath { get; set; }

        public string InstallLocation { get; set; }
    }

    public class Param_Thread_ListBodyTypToList
    {
        //public IList<string> ListBodyType { get; set; }
        public ComboBox cb { get; set; }

        public ComboBox cb_dest { get; set; }
    }


    public class ApparelObject
    {
        public string XMLFilePath { get; set; }
        public string ApparelID { get; set; }
        public string label { get; set; }
        public string texPath { get; set; }
        public string wornGraphicPath { get; set; }
        public string thingCategories { get; set; }
        public bool bTexParent { get; set; }
        public bool bDefNameChild { get; set; }
        public ThingStyleDefObject ApparelStyle { get; set; }
    }

    public class ThingStyleDefObject
    {
        public string StyleID { get; set; } //as styleDef in thingDefStyleObject
        public string wornGraphicPath { get; set; }
        public string texPath { get; set; }

    }

    public class StyleCategoryDefObject
    {
        public string XMLFilePath { get; set; }
        public string StyleType { get; set; }
        public string label { get; set; }
        public IList<thingDefStyleObject> thingDefStyles { get; set; }        
    }

    public class thingDefStyleObject
    {
        public string ApparelID { get; set; }
        public string StyleID { get; set; }

    }

    //================
    public class ThingDefObject
    {
        public string name { get; set; }
        public string parentName { get; set; }
        public bool IsAbstract { get; set; }
        public string defName { get; set; }
        public string label { get; set; }
        public string wornGraphicPath { get; set; }
        public string texPath { get; set; }
        public string xmlFilePath { get; set; }

    }

    //folder watcher
    class MyClassCS
    {
        void Main()
        {
            var watcher = new FileSystemWatcher(@"C:\path\to\folder");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Console.WriteLine(value);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}
