using System;
using System.Collections.Generic;
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
}
