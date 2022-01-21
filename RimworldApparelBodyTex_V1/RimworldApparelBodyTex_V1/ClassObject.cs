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

    public class Param_Thread_ListBodyTypToList
    {
        //public IList<string> ListBodyType { get; set; }
        public ComboBox cb { get; set; }

        public ComboBox cb_dest { get; set; }
    }


    public class ApparelObject
    {
        public string XMLFilePath { get; set; }
        public string defName { get; set; }
        public string label { get; set; }
        public string texPath { get; set; }
        public string wornGraphicPath { get; set; }
        public string thingCategories { get; set; }
        public bool bTexParent { get; set; }
    }
}
