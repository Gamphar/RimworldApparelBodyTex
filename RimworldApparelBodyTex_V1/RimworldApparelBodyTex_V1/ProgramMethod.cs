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

                foreach(string ModFolder in MDirs)
                {
                    Log("Listing all raw file from mod folder {0}", ModFolder);
                    ListAllFilesInDirectory(ModFolder, true, AllFilePaths_Raw);
                }

                Log("Listing {0} files", AllFilePaths_Raw.Count);


                IList<string> ListFolderActiveMod = new List<string>();
                foreach (string filename in AllFilePaths_Raw)
                {
                    if (filename.Contains("About.xml"))
                    {

                        if (validAboutXMLbyList(filename, ModActivePackageIdList))
                        {
                            Log(filename);
                            string ModPath = Path.GetDirectoryName(filename); //about folder
                            ModPath = Path.GetDirectoryName(ModPath); //dapet root folder,folder with mod name
                            Log("Mod folder = {0}", ModPath);

                            ListFolderActiveMod.Add(ModPath);

                        }

                    }

                }

                IList<string> AllXMLInActiveMod = new List<string>();

                int iFolderActiveMod = 0;
                int iTotalFolderActiveMod = ListFolderActiveMod.Count();
                foreach (string FolderActiveMod in ListFolderActiveMod)
                {
                    iFolderActiveMod++;
                    this.InvokeEx(f => f.textBox2.Text = iFolderActiveMod.ToString() + " / " + iTotalFolderActiveMod.ToString());
                    AllXMLInActiveMod.Clear();
                    ListAllFilesInDirectoryWithKeyword(FolderActiveMod, true, AllXMLInActiveMod, "*.xml");


                    int totalXml = AllXMLInActiveMod.Count;
                    int i = 0;
                    //Log("Total xml = {0}", totalXml);

                    foreach(string XMLFile in AllXMLInActiveMod)
                    {
                        i++;
                        this.InvokeEx(f => f.textBox1.Text = i.ToString()+" / "+totalXml.ToString());
                        //string defNameBodyType = GetXMLNodeInnerText("/Defs/BodyTypeDef/defName", XMLFile);
                        IList<string> defNameBodyTypes = new List<string>();
                        defNameBodyTypes = GetListXMLNodeInnerText("/Defs/BodyTypeDef/defName", XMLFile);

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
