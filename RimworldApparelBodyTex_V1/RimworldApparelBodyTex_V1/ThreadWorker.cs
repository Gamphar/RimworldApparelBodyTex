using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RimworldApparelBodyTex_V1
{
    class ThreadWorker
    {
    }

    public partial class Form1 : Form
    {
        bool IsThreadRun_AddNewBody = false;
        int iCount_addNewTex = 0;
        int iCount_skipNewTex = 0;
        int iCount_totalItems = 0;
        bool IsThreadRun_ListBodyTypToList = false;
        bool IsThreadRun_GenBaseCoreApparel = false;
        IList<string> ValidExt = new List<string>();

        private void Thread_AddNewBody()
        {
            IsThreadRun_AddNewBody = true;
            Console.WriteLine("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;



            //do the magic
            iCount_addNewTex = 0;
            iCount_skipNewTex = 0;
            iCount_totalItems = 0;

            /*
             * OLD COPY, NEW COPY BELOW
            //list about
            AllFileAndFolder_Raw.Clear();
            if (ModFolders.Count > 0)
            {
                foreach( string dir in ModFolders)
                {
                    DirectorySearch(dir); //fill up AllFileAndFolder_Raw

                }

                AllFileAndFolder_Edit = new List<string>(AllFileAndFolder_Raw); //copy for edit


                bool bIgnoreActiveMod = checkBox1.Checked;
                Log("List about Filename:");                
                foreach (string filename in AllFileAndFolder_Raw)
                {
                    if (filename.Contains("About.xml"))
                    {

                        //if (validAboutXMLbyList(filename, ModActivePackageIdList))
                        if ((!bIgnoreActiveMod & validAboutXMLbyList(filename, ModActivePackageIdList)) | (bIgnoreActiveMod))
                        {
                            Log(filename);
                            SearchAndCopyTextureApparel(filename);
                        }
                        
                    }
                    
                }

                Log("Added = {0}, Skipped = {1}, Checked = {2}", iCount_addNewTex, iCount_skipNewTex, iCount_totalItems);

            } else
            {
                Log("No mod folder selected");
            }
            */

            //NEW COPY
            ValidExt.Clear();
            ValidExt.Add(".jpg");
            ValidExt.Add(".png");
            ValidExt.Add(".dds");
            foreach (string ActiveModPath in ListFolderActiveMod)
            {
                SearchAndCopyTextureApparel(ActiveModPath);
                Log("Added = {0}, Skipped = {1}, Checked = {2}", iCount_addNewTex, iCount_skipNewTex, iCount_totalItems);
            }

            //end magic
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;
       
            Log("Thread Done. Runtime {0}.", runTime);
            
            IsThreadRun_AddNewBody = false;
        }


               
        //public void SearchAndCopyTextureApparel(string AboutXMLfilepath)
        public void SearchAndCopyTextureApparel(string ActiveModPath)
        {
            string BodyTypeSource = MySettings[0].BodyTypeSource; //"Female";
            string BodyTypeDestination = MySettings[0].BodyTypeDestination; //"FemaleBB";

            //Log(Path.GetDirectoryName(AboutXMLfilepath));
            /*
            string ModPath = Path.GetDirectoryName(AboutXMLfilepath); //about folder
            ModPath = Path.GetDirectoryName(ModPath); //dapet root folder,folder with mod name
            */
            string ModPath = ActiveModPath;


            //target
            string TexDestinationPath = MySettings[0].TexDestinationPath; //@"E:\Game\Steam\steamapps\common\RimWorld\Mods\ApparelBody Support\Textures";

            //source
            string TexSourcePath;
            //string TexSourcePath = Path.Combine(ModPath, "Textures"); //move below

            //list posible tex source
            ListDir.Clear();
            ListDirectoryPathByName(ModPath, "Textures"); //fill up ListDir



            //processing
            IList<string> AllFilePaths = new List<string>();

            foreach (string dTex in ListDir)
            {
                //source
                TexSourcePath = dTex;


                if (Directory.Exists(TexSourcePath))
                {
                    bool BIncludeBodyTex = MySettings[0].IncludeBodyTexture;//false;

                    AllFilePaths.Clear(); //refill each text source
                    DirectorySearchAllFiles(AllFilePaths, TexSourcePath);
                    
                    //foreach (string imagePath in AllFileAndFolder_Edit.AsEnumerable().Reverse())
                    foreach (string FilePath in AllFilePaths)
                    {
                        //
                        iCount_totalItems++;
                        if (
                                
                                (
                                    ValidExt.Contains(Path.GetExtension(FilePath).ToLower())
                                )
                                &
                                (
                                    //TODO: add option to include or exclude body texture using starting keyword Female_Naked_ or Male_Naked_ or Naked_
                                    (
                                        BIncludeBodyTex &&
                                        FilePath.Contains(TexSourcePath) && FilePath.Contains('_' + BodyTypeSource + '_')
                                    )
                                    ||
                                    (
                                        !BIncludeBodyTex &&
                                        FilePath.Contains(TexSourcePath) && FilePath.Contains('_' + BodyTypeSource + '_') &&
                                        !Path.GetFileNameWithoutExtension(FilePath).StartsWith("Female_Naked_" + BodyTypeSource + '_') &&
                                        !Path.GetFileNameWithoutExtension(FilePath).StartsWith("Male_Naked_" + BodyTypeSource + '_') &&
                                        !Path.GetFileNameWithoutExtension(FilePath).StartsWith("Naked_" + BodyTypeSource + '_')
                                    )
                                )

                           )
                        {
                            string NewImagePath = FilePath;
                            NewImagePath = NewImagePath.Replace(TexSourcePath, TexDestinationPath); //replace dir
                            NewImagePath = NewImagePath.Replace('_' + BodyTypeSource + '_', '_' + BodyTypeDestination + '_'); //replace body

                            //Log("NewImagePath = {0}", NewImagePath);



                            if ((File.Exists(NewImagePath) && MySettings[0].Overwrite) || (!File.Exists(NewImagePath))) //donot overwrite mod below it, except desired
                            {

                                if (!File.Exists(NewImagePath))
                                {
                                    string NewDir = Path.GetDirectoryName(NewImagePath);

                                    if (!Directory.Exists(NewDir))
                                    {
                                        Directory.CreateDirectory(NewDir);
                                    }
                                }

                                Log("<Do Copy> NewImagePath = {0}", NewImagePath);
                                iCount_addNewTex++;
                                File.Copy(FilePath, NewImagePath, true);
                            } else
                            {
                                Log("<Skip Copy> File texture already exsist. Overwrite = {0}, Path = {1}", MySettings[0].Overwrite, NewImagePath);
                                iCount_skipNewTex++;
                            }

                            //AllFileAndFolder_Edit.Remove(imageFilePath); //try to remove cus already used
                        }
                    }
                }

            }


        }


        

        public bool validAboutXMLbyList(string aboutxmlFilePath, List<ModActivePackageId> ActivePackageIdList)
        {
            //
            if (!File.Exists(aboutxmlFilePath))
            {
                Log("about xml not found. "+ aboutxmlFilePath);
                return false;
            }

            string XMLstring = File.ReadAllText(aboutxmlFilePath);

            //case sensitive
            XMLstring = XMLstring.Replace("ModMetaData", "ModMetaData", StringComparison.OrdinalIgnoreCase);
            XMLstring = XMLstring.Replace("packageId", "packageId", StringComparison.OrdinalIgnoreCase);

            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.LoadXml(XMLstring);

            XmlNode RootNodeCheck = XMLdoc.SelectSingleNode("/ModMetaData"); //NOTE: case sensitive

            if (RootNodeCheck is null)
            {
                Log("<ERROR> Fail to validity about xml. Root is null. "+ aboutxmlFilePath);
                return false;
            }


            //check id
            XmlNode packageIdNode = XMLdoc.SelectSingleNode("/ModMetaData/packageId");

            if (packageIdNode is null)
            {
                Log("<ERROR> Fail to validity about xml. No packageId. "+ aboutxmlFilePath);
                return false;
            }

            string packageId = packageIdNode.InnerText.ToLower();

            //ModActivePackageId searchPackageId = new ModActivePackageId();
            //searchPackageId.PackageId = packageId;

            foreach(ModActivePackageId M in ModActivePackageIdList)
            {
                if (M.PackageId==packageId)
                return true;
            }


            //
            return false;
        }

        List<string> AllFileAndFolder_Raw = new List<string>();
        List<string> AllFileAndFolder_Edit = new List<string>();

        public void DirectorySearch(string dir)
        {
            
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    //str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                    AllFileAndFolder_Raw.Add(dir + @"\" + Path.GetFileName(f));
                }
                foreach (string d in Directory.GetDirectories(dir, "*"))
                {

                    DirectorySearch(d);
                }
                

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DirectorySearchAllFiles(IList<string> FileList, string dir, IList<string> ExtListFilter = null)
        {

            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    //str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                    //FileList.Add(dir + @"\" + Path.GetFileName(f));

                    //if (
                    //    (ExtListFilter!=null & ExtListFilter.Contains(Path.GetExtension(f).ToLower()) )
                    //    |
                    //    ExtListFilter == null
                    //    )
                    bool b = false;
                    if (ExtListFilter!=null)
                    {
                        b = ExtListFilter.Contains(Path.GetExtension(f).ToLower());

                    }
                    else
                    {
                        b = true;
                    }

                    if (b)
                    {
                        FileList.Add(f);
                    }
                    
                }
                foreach (string d in Directory.GetDirectories(dir, "*"))
                {

                    DirectorySearchAllFiles(FileList, d, ExtListFilter);
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        IList<string> ListDir = new List<string>();
        public void ListDirectoryPathByName(string dirRootPath, string dirNameTarget)
        {
            
            try
            {
                //foreach (string f in Directory.GetFiles(dir))
                //{
                //    //str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                //    AllFileAndFolder_Raw.Add(dir + @"\" + Path.GetFileName(f));
                //}
                foreach (string d in Directory.GetDirectories(dirRootPath, "*"))
                {
                    string dirName = new DirectoryInfo(d).Name;
                    if (dirName.ToLower() == dirNameTarget.ToLower())
                    {
                        ListDir.Add(d);
                    }
                    ListDirectoryPathByName(d, dirNameTarget);
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
        public void Thread_ListBodyTypToList(object param)
        {
            IsThreadRun_ListBodyTypToList = true;
            DateTime timeStart = DateTime.Now;

            Log("<START> List Body Type thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);

            var p = (Param_Thread_ListBodyTypToList)param;
            ThreadStart_ListBodyTypToList(p);
            ComboBox cb = p.cb;
            ComboBox cb_dest = p.cb_dest;

            IList<string> ListBodyType = new List<string>();

            //====================================================1

            //Main Processing
            ListBodyTypToList(ListBodyType);

            //====================================================1
            if (ListBodyType != null && ListBodyType.Count > 0)
            {
                foreach (string BT in ListBodyType)
                {
                    this.InvokeEx(f => cb.Items.Add(BT));
                    this.InvokeEx(f => cb_dest.Items.Add(BT));
                }
            } else { Log("No Body Type found..."); }

            //cb.Enabled = true;
            //this.InvokeEx(f => EnableComboBox(cb, true));
            ThreadEnd_ListBodyTypToList(p);

            DateTime timeEnd = DateTime.Now;
            TimeSpan timeRun = timeEnd - timeStart;

            Log("<DONE> List Body Type thread ({0}), {1} ",
                              Thread.CurrentThread.ManagedThreadId, timeRun);

            IsThreadRun_ListBodyTypToList = false;
        }

        public void ThreadStart_ListBodyTypToList(Param_Thread_ListBodyTypToList p)
        {
            this.InvokeEx(f => EnableComboBox(p.cb, false));
            this.InvokeEx(f => EnableComboBox(p.cb_dest, false));
        }

        public void ThreadEnd_ListBodyTypToList(Param_Thread_ListBodyTypToList p)
        {
            this.InvokeEx(f => EnableComboBox(p.cb, true));
            this.InvokeEx(f => EnableComboBox(p.cb_dest, true));
        }

        public void EnableComboBox(ComboBox cb, bool boolean)
        {
            cb.Enabled = boolean;
        }

        public void Thread_GenBaseCoreApparel(object param)
        {
            IsThreadRun_GenBaseCoreApparel = true;
            DateTime timeStart = DateTime.Now;

            Log("<START> Gen Base Core Apparel thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);

            var p = (Param_Thread_ListBodyTypToList)param;
            ThreadStart_ListBodyTypToList(p);
            ComboBox cb = p.cb;
            ComboBox cb_dest = p.cb_dest;

            IList<string> ListBodyType = new List<string>();

            //====================================================1

            //Main Processing
            Thread_DoGenBaseCoreApparel(p);

            //gen base core style too
            Thread_DoGenBaseCoreApparelStyle();

            //====================================================1


            //cb.Enabled = true;
            //this.InvokeEx(f => EnableComboBox(cb, true));
            ThreadEnd_ListBodyTypToList(p);

            DateTime timeEnd = DateTime.Now;
            TimeSpan timeRun = timeEnd - timeStart;

            Log("<DONE> Gen Base Core Apparel thread ({0}), {1} ",
                              Thread.CurrentThread.ManagedThreadId, timeRun);

            IsThreadRun_GenBaseCoreApparel = false;
        }


        //setup
        IList<ApparelObject> List_Apparels = new List<ApparelObject>();
        IList<string> List_MissingItems = new List<string>();


        private void Thread_DoGenBaseCoreApparel(Param_Thread_ListBodyTypToList p)
        {
            string BodyTypeSrc = MySettings[0].BodyTypeSource;
            string BodyTypeDst = MySettings[0].BodyTypeDestination;
            string key_OldBodyType = string.Format("_{0}_", BodyTypeSrc);
            string key_NewBodyType = string.Format("_{0}_", BodyTypeDst);


            IList<string> ListXMLFilePaths = new List<string>();

            //get all xml from the base core game and dlc
            string baseGameDataDir = @"E:\Game\Steam\steamapps\common\RimWorld\Data";
            //clear current apprel
            List_Apparels.Clear();
            List_MissingItems.Clear();

            IList<string> ExtListFilter = new List<string>();
            ExtListFilter.Add(".xml");

            DirectorySearchAllFiles(ListXMLFilePaths, baseGameDataDir, ExtListFilter);

            if (ListXMLFilePaths.Count > 0)
            {
                foreach(string XMLFilePath in ListXMLFilePaths)
                {
                    Log("======================check xml for apparel start");
                    Thread_SearchAndProcessApparelXML(XMLFilePath);
                    Log("======================check xml for apparel end");
                }

                Log("Done listing base core apparel");
                Log("apparel found = {0}", List_Apparels.Count);

                //setup source
                string sourceTexDir = textBox_sourceTex.Text;
                string destinationTexDir = textBox_TexDestinationPath.Text;
                ValidExt.Clear();
                ValidExt.Add(".jpg");
                ValidExt.Add(".png");
                ValidExt.Add(".dds");

                IList<string> List_SourceRawTex = new List<string>();
                IList<string> List_SourceBodyTex = new List<string>();

                bool bUseStructureSRC = true;

                if (bUseStructureSRC)
                {
                    //get all image file source tex - if using structure folder
                    DirectorySearchAllFiles(List_SourceRawTex, sourceTexDir, ValidExt);
                }
                else
                {
                    //get all image file source tex - if no structure folder, in single folder
                    foreach (string f in Directory.GetFiles(sourceTexDir))
                    {
                        if (ValidExt.Contains(Path.GetExtension(f).ToLower()))
                        {
                            List_SourceRawTex.Add(f);
                        }
                    }
                }

                //filter src only source apparel body type
                Log("filter src only source apparel body type");
                foreach (string f in List_SourceRawTex)
                {
                    Log("check body type on {0} contain {1}", f, key_OldBodyType);
                    string name = Path.GetFileName(f);
                    //string key = string.Format("_{0}_",BodyTypeSrc);
                    if (name.Contains(key_OldBodyType))
                    {
                        List_SourceBodyTex.Add(f);
                    }
                }

                

                //log all apparels and copy
                IList<string> List_TexToCopy = new List<string>();
                foreach (ApparelObject apparel in List_Apparels)
                {
                    string s = "----\r\nxml = {0}\r\ndefName = {1}\r\ntex = {2}\r\nwornTex = {3}\r\ntex parent = {4}";
                    Log(s, apparel.XMLFilePath, apparel.defName, apparel.texPath, apparel.wornGraphicPath, apparel.bTexParent);


                    

                    //gen filename target
                    string imageFileNameWithoutExt = Path.GetFileName(apparel.texPath);
                    string key_imageDir = Path.GetDirectoryName(apparel.texPath.Replace("/", "\\"));
                    string ImageDir = key_imageDir;
                    if (ImageDir == "")
                    {
                        ImageDir = apparel.texPath.Replace("/", "\\");
                    }
                    ImageDir = string.Format("{0}\\{1}", destinationTexDir, ImageDir);
                    Log("ImageDir = {0}", ImageDir);

                    //make sure dest exist
                    if (!Directory.Exists(ImageDir))
                    {
                        Directory.CreateDirectory(ImageDir);
                    }

                    //browse
                    Log("browse file");
                    bool bUseStructureDST = true;
                    List_TexToCopy.Clear();
                    foreach (string f in List_SourceBodyTex)
                    {
                        Log("===check List_SourceBodyTex = {0}", f);
                        string name = Path.GetFileName(f);
                        if (name.StartsWith(imageFileNameWithoutExt))
                        {
                            if (bUseStructureDST)
                            {
                                if (f.ToLower().Contains(key_imageDir.ToLower()))
                                {
                                    List_TexToCopy.Add(f);
                                }
                            } else
                            {
                                List_TexToCopy.Add(f);
                            }
                            
                            //List_SourceRawTex.Remove(f);
                        }
                    }

                    //remove
                    foreach (string fSrc in List_TexToCopy)
                    {
                        List_SourceRawTex.Remove(fSrc);
                    }

                    //copy
                    foreach (string fSrc in List_TexToCopy)
                    {
                        string newBodyTypeApparel = Path.GetFileName(fSrc).Replace(key_OldBodyType, key_NewBodyType, StringComparison.OrdinalIgnoreCase);
                        string fDst = Path.Combine(ImageDir, newBodyTypeApparel);
                        Log("Copy {0} >>> to >>> {1}", fSrc, fDst);
                        File.Copy(fSrc, fDst, false);
                    }


                }

                //Log all missing items
                if (List_MissingItems.Count > 0)
                {
                    Log("Total missing item = {0}", List_MissingItems.Count);
                }

                int i = 0;
                foreach(string s in List_MissingItems)
                {
                    i++;
                    Log("Missing Item#{0}, {1}", i, s);
                }
                

            } else
            {
                Log("No XML, nothing to process");
            }
            

        }

        private bool Thread_SearchAndProcessApparelXML(string XMLFilePath)
        {
            //Apparel_Various.xml
            string ApparelVariousXMLFilePath = XMLFilePath; // @"E:\Game\Steam\steamapps\common\RimWorld\Data\Core\Defs\ThingDefs_Misc\Apparel_Various.xml";

            if (!File.Exists(ApparelVariousXMLFilePath))
            {
                Log("Apparel_Various xml not found. " + ApparelVariousXMLFilePath);
                return false;
            }

            string XMLstring = File.ReadAllText(ApparelVariousXMLFilePath);

            //case sensitive
            //XMLstring = XMLstring.Replace("ModMetaData", "ModMetaData", StringComparison.OrdinalIgnoreCase);
            //XMLstring = XMLstring.Replace("packageId", "packageId", StringComparison.OrdinalIgnoreCase);

            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.LoadXml(XMLstring);

            XmlNode RootNode = XMLdoc.SelectSingleNode("/Defs"); //NOTE: case sensitive

            if (RootNode is null)
            {
                Log("<ERROR> Fail to validity Apparel_Various xml. Root is null. " + ApparelVariousXMLFilePath);
                return false;
            }


            //get ThingDefNodes
            XmlNode ThingDefNode = XMLdoc.SelectSingleNode("/Defs/ThingDef");

            if (ThingDefNode is null)
            {
                Log("<ERROR> Fail to validity Apparel_Various xml. No ThingDef. " + ApparelVariousXMLFilePath);
                return false;
            }

            //preview
            //string ThingDef_string = ThingDefNode.InnerText.ToLower();
            //Log("First ThingDef = {0}", ThingDef_string);

            //ModActivePackageId searchPackageId = new ModActivePackageId();
            //searchPackageId.PackageId = packageId;


            
            



            Log("root node childs = {0}", RootNode.ChildNodes.Count);

            //=====================================================================1 start
            Log("=============================base apparel start");
            foreach (XmlNode nodeThingDef in RootNode.ChildNodes)
            {
                int iAdd = 0;
                //string XMLFilePath = "";
                //string defName = "";
                //string label = "";
                //string texPath = "";
                //string wornGraphicPath = "";
                ApparelObject Apparel = new ApparelObject();
                bool bValidApparel = false;

                //thingCategories, man this logic going bad.. need optim later, for now just use it cus im tired need sleep
                XmlNode thingCategoriesNode = nodeThingDef.SelectSingleNode("thingCategories");
                if(thingCategoriesNode != null)
                {
                    //if (thingCategoriesNode.ChildNodes.Count>0)
                    foreach(XmlNode li in thingCategoriesNode.ChildNodes)
                    {
                        string sCat = li.InnerText;
                        bValidApparel = sCat.ToLower().Equals("apparel");
                    }
                }


                //defname
                XmlNode defNameNode = nodeThingDef.SelectSingleNode("defName");
                if (defNameNode == null)
                {
                    //Log("defName is null, {0}", nodeThingDef.Attributes);
                }
                else //not null
                {
                    
                    Log("defName = {0}", defNameNode.InnerText);
                    string defName = defNameNode.InnerText;
                    bValidApparel = defName.ToLower().StartsWith("apparel") & bValidApparel; //bValidApparel from thingCategories above

                    if (bValidApparel)
                    {                        
                        iAdd++;
                        Apparel.defName = defNameNode.InnerText;
                    }
                    
                }

                if (bValidApparel)
                {
                    //label
                    XmlNode labelNode = nodeThingDef.SelectSingleNode("label");
                    if (labelNode != null)
                    {
                        Log("label = {0}", labelNode.InnerText);
                        iAdd++;
                        Apparel.label = labelNode.InnerText;
                    }

                    //texPath
                    XmlNode texPathNode = nodeThingDef.SelectSingleNode("graphicData/texPath");
                    if (texPathNode != null)
                    {
                        Log("textPath = {0}", texPathNode.InnerText);
                        iAdd++;
                        Apparel.texPath = texPathNode.InnerText;
                    } else
                    {
                        //try to search in current file
                        //But first, check for parent
                        XmlAttribute thingDefParentNameAttr = nodeThingDef.Attributes["ParentName"];
                        if (thingDefParentNameAttr != null)
                        {
                            string ParentName = thingDefParentNameAttr.Value;

                            //now search again the thingdef
                            foreach (XmlNode ThingDef1 in RootNode.ChildNodes)
                            {
                                if (ThingDef1.Attributes != null)
                                {
                                    XmlAttribute name = ThingDef1.Attributes["Name"];
                                    if (name != null)
                                        if (name.Value.ToLower() == ParentName.ToLower())
                                        {
                                            //now serach graphic texPath in parent
                                            XmlNode texPathNode1 = ThingDef1.SelectSingleNode("graphicData/texPath");
                                            if (texPathNode1 != null)
                                            {
                                                Log("textPath = {0}", texPathNode1.InnerText);
                                                iAdd++;
                                                Apparel.texPath = texPathNode1.InnerText;
                                                Apparel.bTexParent = true;
                                            }


                                            //
                                            break; //stop next search because we found the parent things
                                        }
                                }
                                
                            }

                        }

                    }


                    //wornGraphicPath
                    XmlNode wornGraphicPathNode = nodeThingDef.SelectSingleNode("apparel/wornGraphicPath");
                    if (wornGraphicPathNode != null)
                    {
                        Log("wornGraphicPath = {0}", wornGraphicPathNode.InnerText);
                        iAdd++;
                        Apparel.wornGraphicPath = wornGraphicPathNode.InnerText;
                    }




                    
                }
                if (iAdd > 0)
                {
                    Apparel.XMLFilePath = ApparelVariousXMLFilePath;
                    List_Apparels.Add(Apparel);
                }

            }
            Log("=============================base apparel end");

            
            //=====================================================================1 end



            return true;
        }

        private void Thread_DoGenBaseCoreApparelStyle()
        {
            //TODO: GEN BASE CORE APPAREL STYLE
        }

        //end of public partial class Form1 : Form
    }
    //end of namespace RimworldApparelBodyTex_V1


}
