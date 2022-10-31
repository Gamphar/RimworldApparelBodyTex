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

        private bool FuncRuleset_ValidApparelTex_Female(string fileName)
        {
            string fn_lower = Path.GetFileName(fileName).ToLower();
            string ext_lower = Path.GetExtension(fileName).ToLower();
            bool bValid = fn_lower.Contains("_female_") &
                (
                    ext_lower.Equals(".jpg")
                    | ext_lower.Equals(".jpeg")
                    | ext_lower.Equals(".png")
                    | ext_lower.Equals(".dds")
                );
            return bValid;
        }

        private bool FuncRuleset_ValidXMLFile(string fileName)
        {            
            string ext_lower = Path.GetExtension(fileName).ToLower();
            bool bValid = 
                (
                    ext_lower.Equals(".xml")                    
                );
            return bValid;
        }

        public void DirectorySearchAllFiles_WithRuleset(IList<string> FileList, string dir, Func<string, bool> FuncRuleset)
        {

            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {                   
                    bool b = FuncRuleset(f);

                    if (b)
                    {
                        FileList.Add(f);
                    }

                }
                foreach (string d in Directory.GetDirectories(dir, "*"))
                {

                    DirectorySearchAllFiles_WithRuleset(FileList, d, FuncRuleset);
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

                //save list
                string s = GetComboboxItemsToString( cb);
                File.WriteAllText("set_ListBodyType.txt", s);

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

        public string GetComboboxItemsToString(ComboBox cb)
        {
            string s = "";
            List<string> list = new List<string>();
            
            foreach (string BT in cb.Items)
            {
                list.Add(BT);
            }

            s = string.Join("\n", list.ToArray());

            return s;
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
            //IList<string> ActiveModDirs = new List<string>(ListFolderActiveMod);
            IList<string> ActiveModDirs = new List<string>(); //for now this for base core so no need ListFolderActiveMod, just use rimworld data
            ActiveModDirs.Add(MySettings[0].RimworldData);
            //====================================================1

            //Main Processing
            bool bSkipMainProcess = false; //skip for test gen style purpose
            
            if(!bSkipMainProcess)
            Thread_DoGenBaseCoreApparel(ActiveModDirs);

            //gen base core style too
            Thread_DoGenApparelStyle(ActiveModDirs);

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
        IList<ApparelObject> List_Apparels_style = new List<ApparelObject>();
        IList<string> List_MissingItems = new List<string>();


        private void Thread_DoGenBaseCoreApparel(IList<string> ActiveModDirs)
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
                List_ApparelCat.Clear(); //untuk keperluan check kategori pakaian yg ada kata kunci apparel
                foreach (string XMLFilePath in ListXMLFilePaths)
                {
                    Log("======================check xml for apparel start");
                    Log("check apparel xml = {0}", XMLFilePath);
                    Thread_SearchAndProcessApparelXML(XMLFilePath);
                    Log("======================check xml for apparel end");
                }

                //log apparel cat
                List_ApparelCat = List_ApparelCat.Distinct().ToList();
                int k = 0;
                foreach(string sCat in List_ApparelCat)
                {
                    k++;
                    Log("Apparel cat#{0} = {1}",k, sCat);
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
                    Log(s, apparel.XMLFilePath, apparel.ApparelID, apparel.texPath, apparel.wornGraphicPath, apparel.bTexParent);


                    

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
                        if (!File.Exists(fDst))
                        {
                            File.Copy(fSrc, fDst, false);
                        }
                        else
                        {
                            Log("SKIP COPY FILE ALREADY EXISTS");
                        }
                        
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

        IList<string> List_ApparelCat = new List<string>();
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
            Log(">>>>>>>>>>=====================base apparel start");
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

                //thingCategories, check apparel validity from thingCategories that contain tag apparel
                string sCat = "";
                XmlNode thingCategoriesNode = nodeThingDef.SelectSingleNode("thingCategories");
                if(thingCategoriesNode != null)
                {
                    //if (thingCategoriesNode.ChildNodes.Count>0)
                    foreach(XmlNode li in thingCategoriesNode.ChildNodes)
                    {
                        sCat = li.InnerText;
                        bValidApparel = sCat.ToLower().Equals("apparel") | sCat.ToLower().Equals("apparelarmor") | sCat.ToLower().Equals("apparelnoble");
                        if (sCat.ToLower().Contains("apparel"))
                        {
                            List_ApparelCat.Add(sCat);
                        }
                        break;
                    }
                }


                /*
                //defname, sadly on some mods not following the base game format for the file name, so startwith is not good for determining validity of apparel
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
                */



                if (bValidApparel)
                {
                    Log("valid {0}", sCat);
                    //defName
                    XmlNode defNameNode = nodeThingDef.SelectSingleNode("defName");
                    if (defNameNode != null)
                    {
                        Log("defName = {0}", defNameNode.InnerText);
                        iAdd++;
                        Apparel.ApparelID = defNameNode.InnerText;

                        //label
                        XmlNode labelNode = nodeThingDef.SelectSingleNode("label");
                        if (labelNode != null)
                        {
                            Log("label = {0}", labelNode.InnerText);
                            iAdd++;
                            Apparel.label = labelNode.InnerText;
                        }


                        //wornGraphicPath
                        XmlNode wornGraphicPathNode = nodeThingDef.SelectSingleNode("apparel/wornGraphicPath");
                        if (wornGraphicPathNode != null)
                        {
                            Log("wornGraphicPath = {0}", wornGraphicPathNode.InnerText);
                            iAdd++;
                            Apparel.wornGraphicPath = wornGraphicPathNode.InnerText;
                        }



                        //texPath
                        XmlNode texPathNode = nodeThingDef.SelectSingleNode("graphicData/texPath");
                        if (texPathNode != null)
                        {
                            Log("textPath = {0}", texPathNode.InnerText);
                            iAdd++;
                            Apparel.texPath = texPathNode.InnerText;
                        }
                        else
                        {
                            //if texPathNode is null on first search then try to search in current file
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



                    } else
                    {
                        //if defName is null check if this abstract and search the child inheretor
                        //but first store current name attribute so we know who use the current thingdef as parent
                        XmlAttribute thingDefNameAttr = nodeThingDef.Attributes["Name"];
                        if (thingDefNameAttr != null)
                        {
                            string AsParentName = thingDefNameAttr.Value;
                            //now search again the thingdef with ParentName is AsParentName
                            foreach (XmlNode ThingDef0 in RootNode.ChildNodes)
                            {
                                if (ThingDef0.Attributes != null)
                                {
                                    XmlAttribute name = ThingDef0.Attributes["ParentName"];
                                    if (name != null)
                                        if (name.Value.ToLower() == AsParentName.ToLower())
                                        {
                                            //now serach defName in child
                                            XmlNode defNameNode0 = ThingDef0.SelectSingleNode("defName");
                                            if (defNameNode0 != null)
                                            {
                                                Log("defNameNode0 = {0}", defNameNode0.InnerText);
                                                iAdd++;
                                                Apparel.ApparelID = defNameNode0.InnerText;
                                                Apparel.bDefNameChild = true;

                                                //label in child
                                                XmlNode labelNode = ThingDef0.SelectSingleNode("label");
                                                if (labelNode != null)
                                                {
                                                    Log("label in child = {0}", labelNode.InnerText);
                                                    iAdd++;
                                                    Apparel.label = labelNode.InnerText;
                                                }



                                                //wornGraphicPath in child
                                                XmlNode wornGraphicPathNode = ThingDef0.SelectSingleNode("apparel/wornGraphicPath");
                                                if (wornGraphicPathNode != null)
                                                {
                                                    Log("wornGraphicPath in child = {0}", wornGraphicPathNode.InnerText);
                                                    iAdd++;
                                                    Apparel.wornGraphicPath = wornGraphicPathNode.InnerText;
                                                }




                                                //texPath in child
                                                XmlNode texPathNode = ThingDef0.SelectSingleNode("graphicData/texPath");
                                                if (texPathNode != null)
                                                {
                                                    Log("textPath in child = {0}", texPathNode.InnerText);
                                                    iAdd++;
                                                    Apparel.texPath = texPathNode.InnerText;
                                                }
                                                else
                                                {
                                                    //if texPathNode is null on first search then try to search in current file
                                                    //But first, check for parent
                                                    XmlAttribute thingDefParentNameAttr = ThingDef0.Attributes["ParentName"];
                                                    if (thingDefParentNameAttr != null)
                                                    {
                                                        string ParentName = thingDefParentNameAttr.Value;

                                                        //now search again the thingdef
                                                        foreach (XmlNode ThingDef1 in RootNode.ChildNodes)
                                                        {
                                                            if (ThingDef1.Attributes != null)
                                                            {
                                                                XmlAttribute name1 = ThingDef1.Attributes["Name"];
                                                                if (name1 != null)
                                                                    if (name1.Value.ToLower() == ParentName.ToLower())
                                                                    {
                                                                        //now serach graphic texPath in parent
                                                                        XmlNode texPathNode1 = ThingDef1.SelectSingleNode("graphicData/texPath");
                                                                        if (texPathNode1 != null)
                                                                        {
                                                                            Log("textPath in child parent = {0}", texPathNode1.InnerText);
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





                                            }


                                            //
                                            break; //stop next search because we found the parent things
                                        }
                                }
                            }
                        }
                    }

                    ////label
                    //XmlNode labelNode = nodeThingDef.SelectSingleNode("label");
                    //if (labelNode != null)
                    //{
                    //    Log("label = {0}", labelNode.InnerText);
                    //    iAdd++;
                    //    Apparel.label = labelNode.InnerText;
                    //}

                    ////texPath
                    //XmlNode texPathNode = nodeThingDef.SelectSingleNode("graphicData/texPath");
                    //if (texPathNode != null)
                    //{
                    //    Log("textPath = {0}", texPathNode.InnerText);
                    //    iAdd++;
                    //    Apparel.texPath = texPathNode.InnerText;
                    //} else
                    //{
                    //    //if texPathNode is null on first search then try to search in current file
                    //    //But first, check for parent
                    //    XmlAttribute thingDefParentNameAttr = nodeThingDef.Attributes["ParentName"];
                    //    if (thingDefParentNameAttr != null)
                    //    {
                    //        string ParentName = thingDefParentNameAttr.Value;

                    //        //now search again the thingdef
                    //        foreach (XmlNode ThingDef1 in RootNode.ChildNodes)
                    //        {
                    //            if (ThingDef1.Attributes != null)
                    //            {
                    //                XmlAttribute name = ThingDef1.Attributes["Name"];
                    //                if (name != null)
                    //                    if (name.Value.ToLower() == ParentName.ToLower())
                    //                    {
                    //                        //now serach graphic texPath in parent
                    //                        XmlNode texPathNode1 = ThingDef1.SelectSingleNode("graphicData/texPath");
                    //                        if (texPathNode1 != null)
                    //                        {
                    //                            Log("textPath = {0}", texPathNode1.InnerText);
                    //                            iAdd++;
                    //                            Apparel.texPath = texPathNode1.InnerText;
                    //                            Apparel.bTexParent = true;
                    //                        }


                    //                        //
                    //                        break; //stop next search because we found the parent things
                    //                    }
                    //            }
                                
                    //        }

                    //    }

                    //}


                    ////wornGraphicPath
                    //XmlNode wornGraphicPathNode = nodeThingDef.SelectSingleNode("apparel/wornGraphicPath");
                    //if (wornGraphicPathNode != null)
                    //{
                    //    Log("wornGraphicPath = {0}", wornGraphicPathNode.InnerText);
                    //    iAdd++;
                    //    Apparel.wornGraphicPath = wornGraphicPathNode.InnerText;
                    //}




                    
                }

                if (iAdd > 0)
                {
                    Apparel.XMLFilePath = ApparelVariousXMLFilePath;
                    List_Apparels.Add(Apparel);
                }

            }
            Log(">>>>>>>>>>=====================base apparel end");

            
            //=====================================================================1 end



            return true;
        }

        private void Thread_DoGenApparelStyle(IList<string> ActiveModDirs)
        {
            //TODO: GEN APPAREL STYLE
            Log("START SEARCH STYLE");
            //get all xml in dir mod or dir base core
            IList<string> ValidXMLExt = new List<string>();
            ValidXMLExt.Add(".xml");
            IList<string> List_XMLFiles = new List<string>();
            foreach (string ActiveModDir in ActiveModDirs)
            {
                Log("search XML in {0}", ActiveModDir);
                DirectorySearchAllFiles(List_XMLFiles, ActiveModDir, ValidXMLExt);
            }


            //search style in xml
            StyleCats.Clear();
            foreach (string XMLFilePath in List_XMLFiles)
            {
                Log("Search style in xml {0}", XMLFilePath);
                Thread_SearchStyleInXML(XMLFilePath);
            }
            Log("Style found = {0}", StyleCats.Count);

            //filter only apparel style
            //ApparelStyleCats.Clear();
            foreach (StyleCategoryDefObject s in StyleCats)
            {
                string ss = "------\r\ndefName = {0}\r\nlabel = {1}\r\nthingDef = {2}\r\nstyleDef = {3}\r\nxml file = {4}";
                if (s.thingDefStyles.Count > 0)
                {
                    foreach(thingDefStyleObject t in s.thingDefStyles)
                    {
                        
                        Log(ss, s.StyleType, s.label, t.ApparelID, t.StyleID, s.XMLFilePath);
                        //if( List_Apparels.Contains()  t.thingDef.ToLower())
                        //foreach(ApparelObject apparel in List_Apparels) //TODO: need optimize
                        for(int j = 0; j<List_Apparels.Count;j++)
                        {
                            ApparelObject apparel = List_Apparels[j];
                            string thingDef_SRC = apparel.ApparelID;
                            string thingDef_DST = t.ApparelID;
                            Log("yo cek ApparelID out, thingDef_SRC = {0} == thingDef_DST = {1}", thingDef_SRC, thingDef_DST);
                            if (thingDef_SRC.ToLower() == thingDef_DST.ToLower())
                            {
                                Log("Found Style for apparel ID = {0}, set style ID = {1}", thingDef_SRC, t.StyleID);
                                //ApparelStyleCats.Add(s); // need to change , dont use this
                                /*
                                ThingStyleDefObject ApparelStyle = new ThingStyleDefObject();
                                ApparelStyle.StyleID = t.StyleID;
                                ApparelStyle.wornGraphicPath = "placeholder";
                                ApparelStyle.texPath = "placeholder";

                                //apparel.ApparelStyle = ApparelStyle;
                                List_Apparels[j].ApparelStyle = ApparelStyle;
                                */
                                //
                                if(List_Apparels[j].ApparelStyle == null)
                                {
                                    List_Apparels[j].ApparelStyle = new ThingStyleDefObject();
                                }


                                List_Apparels[j].ApparelStyle.StyleID = t.StyleID;
                                List_Apparels[j].ApparelStyle.wornGraphicPath = "placeholder";
                                List_Apparels[j].ApparelStyle.texPath = "placeholder";

                                break;
                            }
                        }
                    }

                }
                else
                {
                   
                    Log(ss, s.StyleType, s.label, null, null, s.XMLFilePath);
                }
                
            }


            //log ApparelStyleCats
            //Log("ApparelStyleCats found = {0}", ApparelStyleCats.Count);
            //int i = 0;
            //foreach(StyleCategoryDefObject s in ApparelStyleCats)
            //{
            //    i++;
            //    Log("Apparel style cat onnly #{0} = {1}", i, s.StyleType);
            //}




            //now search image for the style
            //search ThingStyleDef
            //ApparelStyles.Clear();
            foreach (string XMLFilePath in List_XMLFiles)
            {
                Log("Search ThingStyleDef in xml {0}", XMLFilePath);
                Thread_TrySearchAndAssignStyleToListApparel(List_Apparels, XMLFilePath);
                
            }

            Log("List_Apparels_style count = {0}", List_Apparels_style.Count);
            int i = 0;
            foreach (ApparelObject Apparel in List_Apparels_style)
            {
                i++;
                string slog = "Apparel & Style #{0}\r\n______ApparelID = {1}, tex = {2}\r\n______StyleID = {3}, tex style = {4}";
                string sStyleID = Apparel.ApparelStyle != null ? Apparel.ApparelStyle.StyleID : "null";
                string stexPath = Apparel.ApparelStyle != null ? Apparel.ApparelStyle.texPath : "null";
                Log(slog, i, Apparel.ApparelID, Apparel.texPath, sStyleID, stexPath);
            }






            //===================1
            //setup source
            string BodyTypeSrc = MySettings[0].BodyTypeSource;
            string BodyTypeDst = MySettings[0].BodyTypeDestination;
            string key_OldBodyType = string.Format("_{0}_", BodyTypeSrc);
            string key_NewBodyType = string.Format("_{0}_", BodyTypeDst);


            string sourceTexDir = MySettings[0].BaseSourceTexDir;// textBox_sourceTex.Text;
            string destinationTexDir = MySettings[0].TexDestinationPath;// textBox_TexDestinationPath.Text;
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

            IList<string> List_TexToCopy = new List<string>();

            //setup dir and file source
            foreach (ApparelObject apparel in List_Apparels_style)
            {
                string s = "copy style tex----\r\nxml = {0}\r\ndefName = {1}\r\ntex = {2}\r\nwornTex = {3}\r\ntex parent = {4}";
                Log(s, apparel.XMLFilePath, apparel.ApparelID, apparel.texPath, apparel.wornGraphicPath, apparel.bTexParent);




                //gen filename target
                string imageFileNameWithoutExt = Path.GetFileName(apparel.ApparelStyle.texPath);
                string key_imageDir = Path.GetDirectoryName(apparel.ApparelStyle.texPath.Replace("/", "\\"));
                string ImageDir = key_imageDir;
                if (ImageDir == "")
                {
                    ImageDir = apparel.ApparelStyle.texPath.Replace("/", "\\");
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
                        }
                        else
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
                    if (!File.Exists(fDst))
                    {
                        File.Copy(fSrc, fDst, false);
                    }
                    else
                    {
                        Log("SKIP COPY FILE ALREADY EXISTS");
                    }

                }


            }
            //===================1 end





            Log("END SEARCH STYLE");

            //compare thingDef from styles to list_apparel

        }

        IList<StyleCategoryDefObject> StyleCats = new List<StyleCategoryDefObject>();
        //IList<StyleCategoryDefObject> ApparelStyleCats = new List<StyleCategoryDefObject>();

        private bool Thread_SearchStyleInXML(string XMLFilePath)
        {
            string XMLstring = File.ReadAllText(XMLFilePath);

            //case sensitive
            //XMLstring = XMLstring.Replace("ModMetaData", "ModMetaData", StringComparison.OrdinalIgnoreCase);
            //XMLstring = XMLstring.Replace("packageId", "packageId", StringComparison.OrdinalIgnoreCase);

            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.LoadXml(XMLstring);

            XmlNode RootNode = XMLdoc.SelectSingleNode("/Defs"); //NOTE: case sensitive

            if (RootNode is null)
            {
                Log("<ERROR> Fail to validity StyleCategoryDef xml. Root is null. " + XMLFilePath);
                return false;
            }

            XmlNodeList StyleCategoryDefNodes = RootNode.SelectNodes("StyleCategoryDef");

            if (StyleCategoryDefNodes.Count <= 0) 
            {                
                return false;
            }


            //IList<StyleCategoryDefObject> Styles = new List<StyleCategoryDefObject>();

            foreach (XmlNode StyleCategoryDefNode in StyleCategoryDefNodes)
            {
                StyleCategoryDefObject StyleCat = new StyleCategoryDefObject();

                //defName
                XmlNode defNameNode = StyleCategoryDefNode.SelectSingleNode("defName");
                if (defNameNode!=null)
                {
                    StyleCat.StyleType = defNameNode.InnerText;
                }

                //Label
                XmlNode labelNode = StyleCategoryDefNode.SelectSingleNode("label");
                if (labelNode != null)
                {
                    StyleCat.label = labelNode.InnerText;
                }

                //thingDefStyles
                XmlNode thingDefStylesNode = StyleCategoryDefNode.SelectSingleNode("thingDefStyles");
                if (thingDefStylesNode != null)
                {
                    XmlNodeList liNodes = thingDefStylesNode.SelectNodes("li");
                    if (liNodes.Count > 0)
                    {
                        foreach(XmlNode liNode in liNodes)
                        {
                            thingDefStyleObject thingDefStyle = new thingDefStyleObject();
                            //thingDef
                            XmlNode thingDefNode = liNode.SelectSingleNode("thingDef");
                            if (thingDefNode != null)
                            {
                                thingDefStyle.ApparelID = thingDefNode.InnerText;
                            }

                            //styleDef
                            XmlNode styleDefNode = liNode.SelectSingleNode("styleDef");
                            if (styleDefNode != null)
                            {
                                thingDefStyle.StyleID = styleDefNode.InnerText;
                            }

                            if (StyleCat.thingDefStyles == null)
                            {
                                StyleCat.thingDefStyles = new List<thingDefStyleObject>();
                            }

                            StyleCat.thingDefStyles.Add(thingDefStyle);
                        }

                    }
                }

                StyleCat.XMLFilePath = XMLFilePath;
                StyleCats.Add(StyleCat);
            }

            

            return true;
        }

        //IList<ThingStyleDefObject> ApparelStyles = new List<ThingStyleDefObject>();

        private void Thread_TrySearchAndAssignStyleToListApparel(IList<ApparelObject> Apparels, string XMLFilePath)
        {
            string XMLstring = File.ReadAllText(XMLFilePath);

            //case sensitive
            //XMLstring = XMLstring.Replace("ModMetaData", "ModMetaData", StringComparison.OrdinalIgnoreCase);
            //XMLstring = XMLstring.Replace("packageId", "packageId", StringComparison.OrdinalIgnoreCase);

            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.LoadXml(XMLstring);

            XmlNode RootNode = XMLdoc.SelectSingleNode("/Defs"); //NOTE: case sensitive

            if (RootNode is null)
            {
                Log("<ERROR> Fail to validity ThingStyleDef xml. Root is null. " + XMLFilePath);
                return;
            }

            XmlNodeList ThingStyleDefNodes = RootNode.SelectNodes("ThingStyleDef");

            if (ThingStyleDefNodes.Count <= 0)
            {
                return;
            }

            //IList<ApparelStyleObject> ApparelStyles = new List<ApparelStyleObject>();


            foreach (XmlNode ThingStyleDefNode in ThingStyleDefNodes)
            {
                ThingStyleDefObject ApparelStyle = new ThingStyleDefObject();

                //defName
                XmlNode defNameNode = ThingStyleDefNode.SelectSingleNode("defName");
                if (defNameNode != null)
                {
                    ApparelStyle.StyleID = defNameNode.InnerText;
                    Log("ApparelStyle.defName = {0}", ApparelStyle.StyleID);
                }

                //wornGraphicPath
                XmlNode wornGraphicPathNode = ThingStyleDefNode.SelectSingleNode("wornGraphicPath");
                if (wornGraphicPathNode != null)
                {
                    ApparelStyle.wornGraphicPath = wornGraphicPathNode.InnerText;
                    Log("ApparelStyle.wornGraphicPath = {0}", ApparelStyle.wornGraphicPath);
                }

                //texPath
                XmlNode texPathNode = ThingStyleDefNode.SelectSingleNode("graphicData/texPath");
                if (texPathNode != null)
                {
                    ApparelStyle.texPath = texPathNode.InnerText;
                    Log("ApparelStyle.texPath = {0}", ApparelStyle.texPath);
                }

                //filter only apparel
                Log("Apparels count :: {0}", Apparels.Count);
                Log("List_Apparels count :: {0}", List_Apparels.Count);
                //bool bValid = false;
                //foreach (ApparelObject apparel in Apparels)
                for (int j = 0; j < Apparels.Count; j++)
                {
                    ApparelObject apparel = List_Apparels[j];
                    if (apparel.ApparelStyle != null)
                    {
                        Log("apparel.ApparelStyle.StyleID = {0}", apparel.ApparelStyle.StyleID);

                        if (ApparelStyle.StyleID != null)
                        {
                            if (apparel.ApparelStyle.StyleID.ToLower() == ApparelStyle.StyleID.ToLower())
                            {
                                Log("Found image path for style, apparel id = {0}, style id = {1}", apparel.ApparelID, apparel.ApparelStyle.StyleID);
                                //insert tex of style
                                List_Apparels[j].ApparelStyle.wornGraphicPath = ApparelStyle.wornGraphicPath;
                                List_Apparels[j].ApparelStyle.texPath = ApparelStyle.texPath;

                                //filter apparel that have style
                                List_Apparels_style.Add(List_Apparels[j]);
                                break;
                            }
                        }
                        else
                        {
                            Log("ApparelStyle.StyleID is null for xml node {0}", ThingStyleDefNode.Name);
                        }
                        
                    }
                    else
                    {
                        Log("apparel.ApparelStyle is null");
                    }
                    


                    //if(Apparel_StyleCat.thingDefStyles != null)
                    //{
                    //    foreach (thingDefStyleObject tds in Apparel_StyleCat.thingDefStyles)
                    //    {
                    //        Log("check apparel style compare, {0} == {1}", tds.StyleID, ApparelStyle.StyleID);

                    //        if (ApparelStyle.StyleID != null)
                    //            if (tds.StyleID.ToLower() == ApparelStyle.StyleID.ToLower())
                    //            {
                    //                ApparelStyles.Add(ApparelStyle);
                    //                bValid = true;
                    //                break;
                    //            }
                    //    }
                    //}
                    
                    //if (bValid)
                    //{
                    //    break;
                    //}
                    
                }



                

                //----------------------------------------------------
                //copy apparel style tex
                //foreach (string fSrc in List_TexToCopy)
                //{
                //    string newBodyTypeApparel = Path.GetFileName(fSrc).Replace(key_OldBodyType, key_NewBodyType, StringComparison.OrdinalIgnoreCase);
                //    string fDst = Path.Combine(ImageDir, newBodyTypeApparel);
                //    Log("Copy {0} >>> to >>> {1}", fSrc, fDst);
                //    if (!File.Exists(fDst))
                //    {
                //        File.Copy(fSrc, fDst, false);
                //    }
                //    else
                //    {
                //        Log("SKIP COPY FILE ALREADY EXISTS");
                //    }

                //}

            }


            

        }

        //end of public partial class Form1 : Form
    }
    //end of namespace RimworldApparelBodyTex_V1


}
