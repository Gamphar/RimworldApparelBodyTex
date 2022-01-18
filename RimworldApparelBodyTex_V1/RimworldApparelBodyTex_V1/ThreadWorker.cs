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

            //list about
            AllFileAndFolder_Raw.Clear();
            if (ModFolders.Count > 0)
            {
                foreach( string dir in ModFolders)
                {
                    DirectorySearch(dir); //fill up AllFileAndFolder_Raw

                }

                AllFileAndFolder_Edit = new List<string>(AllFileAndFolder_Raw); //copy for edit

                Log("List about Filename:");
                
                foreach (string filename in AllFileAndFolder_Raw)
                {
                    if (filename.Contains("About.xml"))
                    {

                        if (validAboutXMLbyList(filename, ModActivePackageIdList))
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


            //end magic
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;
       
            Log("Thread Done. Runtime {0}.", runTime);
            
            IsThreadRun_AddNewBody = false;
        }

        

        public void SearchAndCopyTextureApparel(string AboutXMLfilepath)
        {
            string BodyTypeSource = MySettings[0].BodyTypeSource; //"Female";
            string BodyTypeDestination = MySettings[0].BodyTypeDestination; //"FemaleBB";

            //Log(Path.GetDirectoryName(AboutXMLfilepath));
            string ModPath = Path.GetDirectoryName(AboutXMLfilepath); //about folder
            ModPath = Path.GetDirectoryName(ModPath); //dapet root folder,folder with mod name


            //target
            string TexDestinationPath = MySettings[0].TexDestinationPath; //@"E:\Game\Steam\steamapps\common\RimWorld\Mods\ApparelBody Support\Textures";

            //source
            string TexSourcePath;
            //string TexSourcePath = Path.Combine(ModPath, "Textures"); //move below

            //list posible tex source
            ListDir.Clear();
            ListDirectoryPathByName(ModPath, "Textures"); //fill up ListDir



            //processing

            foreach (string dTex in ListDir)
            {
                //source
                TexSourcePath = dTex;


                if (Directory.Exists(TexSourcePath))
                {
                    bool BIncludeBodyTex = MySettings[0].IncludeBodyTexture;//false;
                    foreach (string imagePath in AllFileAndFolder_Edit.AsEnumerable().Reverse())
                    {
                        //
                        iCount_totalItems++;
                        if (
                                //TODO: add option to include or exclude body texture using starting keyword Female_Naked_ or Male_Naked_ or Naked_
                                (
                                    BIncludeBodyTex &&
                                    imagePath.Contains(TexSourcePath) && imagePath.Contains('_' + BodyTypeSource + '_')
                                )
                                ||
                                (
                                    !BIncludeBodyTex &&
                                    imagePath.Contains(TexSourcePath) && imagePath.Contains('_' + BodyTypeSource + '_') &&
                                    !Path.GetFileNameWithoutExtension(imagePath).StartsWith("Female_Naked_" + BodyTypeSource + '_') &&
                                    !Path.GetFileNameWithoutExtension(imagePath).StartsWith("Male_Naked_" + BodyTypeSource + '_') &&
                                    !Path.GetFileNameWithoutExtension(imagePath).StartsWith("Naked_" + BodyTypeSource + '_')
                                )

                           )
                        {
                            string NewImagePath = imagePath;
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
                                File.Copy(imagePath, NewImagePath, true);
                            } else
                            {
                                Log("<Skip Copy> File texture already exsist. Overwrite = {0}, Path = {1}", MySettings[0].Overwrite, NewImagePath);
                                iCount_skipNewTex++;
                            }

                            AllFileAndFolder_Edit.Remove(imagePath); //try to remove cus already used
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


            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.LoadXml(XMLstring);

            XmlNode RootNodeCheck = XMLdoc.SelectSingleNode("/ModMetaData");

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
        List<string> AllFileAndFolder_Edit;

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


            ListBodyTypToList(ListBodyType);

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

        //end of public partial class Form1 : Form
    }
    //end of namespace RimworldApparelBodyTex_V1
}
