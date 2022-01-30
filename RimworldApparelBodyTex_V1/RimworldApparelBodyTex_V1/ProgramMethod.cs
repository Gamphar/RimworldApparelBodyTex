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
    class ProgramMethod
    {
    }

    public partial class Form1 : Form
    {


        private void LogList(IList<string> List_Items, string Prefix)
        {
            int i = 0;
            foreach (string Item in List_Items)
            {
                i++;
                Log("#{0}, {1}{2}", i, Prefix, Item);
            }

        }


        public void ListModBodyTypeDefNameToComboBox(Param_Thread_ListBodyTypToList p)
        {

            if (!IsThreadRun_ListBodyTypToList)
            {
                //cb.Enabled = false;

                IList<string> ListBodyType = new List<string>();
                //ListBodyTypToList(ListBodyType);
                stratThread_ListBodyTypeToComboBox(p);
                /*
                if (ListBodyType != null && ListBodyType.Count > 0)
                {
                    foreach (string BT in ListBodyType)
                    {
                        cb.Items.Add(BT);
                    }
                }

                cb.Enabled = true;
                */
            } else { Log("<WARNING> Not allowed multiple instances of List Mod Body Type."); }


        }

        

        public void stratThread_ListBodyTypeToComboBox(Param_Thread_ListBodyTypToList p)
        {
            
            

            var th = new Thread(Thread_ListBodyTypToList);
            th.IsBackground = true;
            th.Start(p);
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        IList<string> ListFolderActiveMod = new List<string>();

        public void ListBodyTypToList(IList<string> ListBodyType)
        {
            IList<string> MDirs = new List<string>();
            MDirs.Clear();
            foreach (object item in checkedListBox_modFolder.CheckedItems)
            {
                //Log(item.ToString());

                MDirs.Add(item.ToString());
                Log("Add checking for mod folder = {0}", item.ToString());
            }

            if (MDirs.Count > 0)
            {
                IList<string> AllFilePaths_Raw = new List<string>();

                //List all files in selected mod folder
                foreach(string ModFolder in MDirs)
                {
                    Log("Listing all raw file from mod folder {0}", ModFolder);
                    ListAllFilesInDirectory(ModFolder, true, AllFilePaths_Raw);
                }

                Log("Listing {0} files", AllFilePaths_Raw.Count);

                //save AllFilePaths_Raw
                string AllFilePaths_Raw_s = string.Join("\r\n", AllFilePaths_Raw);
                File.WriteAllText("dump_AllFilePaths_Raw.txt", AllFilePaths_Raw_s);

                bool bIgnoreActiveMod = checkBox_ignoreActiveMod.Checked;

                //IList<string> ListFolderActiveMod = new List<string>();
                ListFolderActiveMod.Clear(); //reset ListFolderActiveMod
                foreach (string filename in AllFilePaths_Raw)
                {
                    if (filename.Contains("About.xml"))
                    {
                        

                        if ((!bIgnoreActiveMod & validAboutXMLbyList(filename, ModActivePackageIdList)) | (bIgnoreActiveMod))
                        {
                            Log("======================================================");
                            Log(filename);
                            string ModPath = Path.GetDirectoryName(filename); //about folder
                            if (!MDirs.Contains(ModPath)) //prevent adding MDirs itself when get about folder
                            {
                                ModPath = Path.GetDirectoryName(ModPath); //dapet root folder, folder with mod name
                                Log("Mod folder = {0}", ModPath);

                                if (!MDirs.Contains(ModPath)) //prevent adding MDirs itself when get root folder, folder with mod name
                                {

                                    //prevent futher about in version folder
                                    string modName = ModPath;
                                    string rootModName = "";

                                    string rootDir = "";
                                    foreach(string rd in MDirs)
                                    {
                                        if (ModPath.Contains(rd))
                                        {
                                            rootDir = rd;
                                            break;
                                        }
                                    }



                                    //foreach (string rootDir in MDirs)
                                    {
                                        Log("remove part = {0} from {1}", rootDir, modName);
                                        modName = modName.Replace(rootDir,"");
                                        if (modName.StartsWith("\\"))
                                        {
                                            Log("remove \\");
                                            modName = modName.Remove(0,1);
                                        }
                                        Log("remove part result = {0}", modName);
                                        if (modName == "")
                                        {
                                            //do nothing
                                            //break;
                                        } else
                                        {

                                            

                                            Log("modName 1= {0}", modName);
                                            if (modName != ModPath & rootModName == "")
                                            {
                                                Log("set rootModName = {0}", rootDir);
                                                rootModName = rootDir;

                                            }

                                            modName = modName.Split('\\')[0];
                                            Log("modName 2= {0}", modName);

                                            if (rootModName != "")
                                            {
                                                Log("break");
                                                //break;
                                            }
                                        }
                                        

                                        
                                    }
                                    if (rootModName != "" & modName !="")
                                    {
                                        ModPath = Path.Combine(rootModName, modName);
                                    }
                                    

                                    ListFolderActiveMod.Add(ModPath);
                                    Log("Add active mod = "+ModPath);
                                }
                            }


                            Log("======================================================");
                        }
                        
                    }

                }

                //remove duplicate
                Log("remove dupe");
                ListFolderActiveMod = ListFolderActiveMod.Distinct().ToList();

                //log it
                foreach(string sModPath in ListFolderActiveMod)
                {
                    Log("log active mod = {0}", sModPath);
                }
                Log("done log");
                //save ListFolderActiveMod
                File.WriteAllText("dump_ListFolderActiveMod.txt", string.Join("\r\n", ListFolderActiveMod) );

                IList<string> scanLog = new List<string>(); //for log file
                IList<string> AllXMLInActiveMod = new List<string>();

                int iFolderActiveMod = 0;
                int iTotalFolderActiveMod = ListFolderActiveMod.Count();
                foreach (string FolderActiveMod in ListFolderActiveMod)
                {
                    iFolderActiveMod++;
                    this.InvokeEx(f => f.textBox2.Text = iFolderActiveMod.ToString() + " / " + iTotalFolderActiveMod.ToString());
                    this.InvokeEx(f => f.label_FolderActiveMod.Text = FolderActiveMod);
                    

                    AllXMLInActiveMod.Clear();
                    ListAllFilesInDirectoryWithKeyword(FolderActiveMod, true, AllXMLInActiveMod, "*.xml");

                    //save AllXMLInActiveMod
                    //File.WriteAllText("dump_AllXMLInActiveMod.txt", string.Join("\r\n", AllXMLInActiveMod));

                    int totalXml = AllXMLInActiveMod.Count;
                    int i = 0;
                    //Log("Total xml = {0}", totalXml);
                    scanLog.Add(string.Format("#{0}, FolderActiveMod = {1}, totalXml = {2}", iFolderActiveMod, FolderActiveMod, totalXml));

                    foreach (string XMLFile in AllXMLInActiveMod)
                    {
                        i++;
                        this.InvokeEx(f => f.textBox1.Text = i.ToString()+" / "+totalXml.ToString());
                        this.InvokeEx(f => f.label_XMLFile.Text = XMLFile);
                        //string defNameBodyType = GetXMLNodeInnerText("/Defs/BodyTypeDef/defName", XMLFile);
                        IList<string> defNameBodyTypes = new List<string>();
                        defNameBodyTypes = GetListXMLNodeInnerText("/Defs/BodyTypeDef/defName", XMLFile);
                        scanLog.Add(string.Format("------#{0}, XMLFile = {1}, defNameBodyTypes count = {2}", i, XMLFile, defNameBodyTypes==null?0:defNameBodyTypes.Count));

                        if (defNameBodyTypes != null)
                        foreach(string defNameBodyType in defNameBodyTypes)
                        {
                            if (defNameBodyType != "")
                            {
                                Log("Found Body Type = {0}, Loc = {1}", defNameBodyType, XMLFile);
                                if (ListBodyType.Contains(defNameBodyType))
                                {
                                    Log("<SKIP> {0} already listed.", defNameBodyType);
                                }
                                else
                                {
                                    Log("<ADD> Body Type {0}", defNameBodyType);
                                    ListBodyType.Add(defNameBodyType);
                                }

                            }
                        }

                        
                    }
                }

                //save scanLog
                File.WriteAllText("dump_scanLog.txt", string.Join("\r\n", scanLog));



            } else
            {
                Log("Fail to list body type. No Mod folder.");
            }
                

        }

        public string GetXMLNodeInnerText(string NodePath, string XMLFilePath)
        {
            try
            {
                string XMLstring = File.ReadAllText(XMLFilePath);





                XmlDocument XMLdoc = new XmlDocument();

                XMLdoc.LoadXml(XMLstring);



                XmlNode defNameNode = XMLdoc.SelectSingleNode(NodePath);

                if (defNameNode is null)
                {
                    //Log("<ERROR> Fail to get XML node Inner text. Cant find node path. " + XMLFilePath);
                    return "";
                }

                string value = defNameNode.InnerText;
                return value;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Log("<ERROR> Fail to get XML node Inner text. Not valid XML. msg = {0} | path = {1}.", ex.Message, XMLFilePath);
                return "";
            }


        }

        public IList<string> GetListXMLNodeInnerText(string NodePath, string XMLFilePath)
        {
            try
            {
                string XMLstring = File.ReadAllText(XMLFilePath);

                IList<string> InnerTexts = new List<string>();



                XmlDocument XMLdoc = new XmlDocument();

                XMLdoc.LoadXml(XMLstring);



                //XmlNode defNameNode = XMLdoc.SelectSingleNode(NodePath);
                XmlNodeList defNameNodes = XMLdoc.SelectNodes(NodePath);


                if (defNameNodes is null || defNameNodes.Count<=0)
                {
                    //Log("<ERROR> Fail to get XML node Inner text. Cant find node path. " + XMLFilePath);
                    return null;
                }

                foreach(XmlNode defNameNode in defNameNodes)
                {
                    InnerTexts.Add(defNameNode.InnerText);
                }

                return InnerTexts;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Log("<ERROR> Fail to get XML node Inner text. Not valid XML. msg = {0} | path = {1}.", ex.Message, XMLFilePath);
                return null;
            }


        }

        public void ListAllFilesInDirectory(string dir, bool BincludeSubDir, IList<string> ListFilePath)
        {
            try
            {
                bool bSkipModFolder = false;
                IList<string> ListFilePath_temp = new List<string>();
                foreach (string f in Directory.GetFiles(dir))
                {

                    if (Path.GetFileName(f).ToLower() == "[skip].txt")
                    {
                        bSkipModFolder = true;
                        break;
                    }
                    else
                    {
                        //str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                        ListFilePath_temp.Add(dir + @"\" + Path.GetFileName(f));
                    }
                    
                }

                //copy if not skipped
                if (!bSkipModFolder && ListFilePath_temp.Count>0)
                {
                    //add
                    foreach(string s in ListFilePath_temp)
                    {
                        ListFilePath.Add(s);
                    }
                }

                if(BincludeSubDir && !bSkipModFolder)
                foreach (string d in Directory.GetDirectories(dir, "*"))
                {

                        ListAllFilesInDirectory(d, BincludeSubDir, ListFilePath);
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListAllFilesInDirectoryWithKeyword(string dir, bool BincludeSubDir, IList<string> ListFilePath, string keyword)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir, keyword))
                {
                    //str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                    ListFilePath.Add(dir + @"\" + Path.GetFileName(f));
                }

                if (BincludeSubDir)
                    foreach (string d in Directory.GetDirectories(dir, "*"))
                    {

                        ListAllFilesInDirectoryWithKeyword(d, BincludeSubDir, ListFilePath, keyword);
                    }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
