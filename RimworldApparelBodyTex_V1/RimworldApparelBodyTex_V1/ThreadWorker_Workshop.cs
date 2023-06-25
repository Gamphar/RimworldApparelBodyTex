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
    internal class ThreadWorker_Workshop
    {
    }

    public partial class Form1 : Form
    {
        //

        bool IsThreadRun_GetAllModWorkshop = false;

        private void startThread_GetAllModWorkshop()
        {
            if (IsThreadRun_GetAllModWorkshop)
            {
                Log("GetAllModWorkshop Already Running");
                MessageBox.Show(btn_GetAllModWorkshop.Text + " is already running.");
                return;
            }

            //setup setting first
            //MySettings[0].BaseSourceTexDir = textBox_sourceTex.Text;
            //MySettings[0].TexDestinationPath = textBox_TexDestinationPath.Text;

            string DirSrc = "";
            this.InvokeEx(f => DirSrc = f.textBox_tab7_sourceData.Text);
            string DirDst = "";
            this.InvokeEx(f => DirDst = f.textBox_tab7_outputData.Text);

            //check up
            if (!Directory.Exists(DirSrc))
            {
                string s = "Source dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            if (!Directory.Exists(DirDst))
            {
                string s = "Destination dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            //start the thread
            var th = new Thread(Thread_GetAllModWorkshop);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread_GetAllModWorkshop()
        {
            IsThreadRun_GetAllModWorkshop = true;
            //Console.WriteLine("Thread ({0}) running.",
            //                  Thread.CurrentThread.ManagedThreadId);
            Log("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;

            //labeling in
            string defaultBtnLabel = "";
            this.InvokeEx(f => defaultBtnLabel = f.btn_GetAllModWorkshop.Text);
            this.InvokeEx(f => f.btn_GetAllModWorkshop.Text = "... In Progress ...");


            //start
            //=================================================================================1
            Thread_DoGetAllModWorkshop();

            //=================================================================================1
            //end

            //labeling out
            this.InvokeEx(f => f.btn_GetAllModWorkshop.Text = defaultBtnLabel);

            //end timer
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;

            Log("Thread ({1}) Done. Runtime {0}.", runTime, Thread.CurrentThread.ManagedThreadId);

            IsThreadRun_GetAllModWorkshop = false;
        }

        private void Thread_DoGetAllModWorkshop()
        {
            //do stuff
            string DirSrc = "";
            this.InvokeEx(f => DirSrc = f.textBox_tab7_sourceData.Text);
            string DirDst = "";
            this.InvokeEx(f => DirDst = f.textBox_tab7_outputData.Text);
            //DirDst_tab7 = DirDst;
            //IList<string> List_XMLFilePaths = new List<string>();
            //DirectorySearchAllFiles_WithRuleset(List_XMLFilePaths, DirSrc, FuncRuleset_ValidXMLFile);

            DataGridView DGV;// = new DataGridView();
            this.InvokeEx(f => DGV = f.dataGridView1);
            int colcount = 0;

            IList<string> List_ModPaths = new List<string>();
            IList<string> List_ModName = new List<string>();
            IList<string> List_ModID = new List<string>();
            IList<string> List_ModUrlID = new List<string>();

            IList<int> MaxColsWidth = new List<int>();
            MaxColsWidth.Add(0);
            MaxColsWidth.Add(0);
            MaxColsWidth.Add(0);
            MaxColsWidth.Add(0);

            //clear old data
            this.InvokeEx(f => f.dataGridView1.Rows.Clear());
            this.InvokeEx(f => f.dataGridView1.Refresh());

            Log("listing workshop mods");

            string dir = DirSrc;
            foreach (string d in Directory.GetDirectories(dir, "*"))
            {

                //
                List_ModPaths.Add(d);
                string modUrlID = Path.GetFileName(d);
                List_ModUrlID.Add(modUrlID);
                string aboutDirPath = Path.Combine(d, "About");
                string aboutFilePath = Path.Combine(aboutDirPath, "About.xml");

                if (File.Exists(aboutFilePath))
                {
                    XmlDocument XMLdoc = new XmlDocument();

                    //XMLdoc.XmlResolver = null;
                    //XMLdoc.LoadXml(XMLFilePath); //not working, seems need to load as text not a file
                    string XMLstring = File.ReadAllText(aboutFilePath);
                    XMLdoc.LoadXml(XMLstring);
                    //XMLdoc.Load(XMLFilePath);

                    XmlNode ModMetaDataNode = XMLdoc.SelectSingleNode("/ModMetaData");

                    if (ModMetaDataNode is null)
                    {
                        Log("<ERROR> Fail to get about xml. Root is null.");
                        return;
                    }

                    //Log("Select nodes");
                    //XmlNodeList ActiveModsNode = ModsConfigDataNode.SelectNodes("/activeMods/li"); //not working
                    XmlNodeList ModNameNode = XMLdoc.SelectNodes("/ModMetaData/name");
                    var ModName = "";
                    if (ModNameNode.Count > 0)
                    {
                        ModName = ModNameNode[0].InnerText;

                    }
                    List_ModName.Add(ModName);
                    //Log("Mod Name = {0}", ModName);


                    XmlNodeList packageIdNode = XMLdoc.SelectNodes("/ModMetaData/packageId");
                    var modID = "";
                    if (packageIdNode.Count > 0)
                    {
                        modID = packageIdNode[0].InnerText;
                        
                    }
                    List_ModID.Add(modID);


                    //date folder modified
                    DateTime dt = Directory.GetLastWriteTime(d);

                    //list mod data in UI
                    Log("Mod Name = {0}, id = {1}, workshop id = {2}, path = {3}", ModName, modID, modUrlID, d);
                    //DGV.RowCount += 1;
                    //this.InvokeEx(f => f.dataGridView1.RowCount +=1);
                    if (colcount == 0)
                    {
                        colcount = 5;
                        this.InvokeEx(f => f.dataGridView1.ColumnCount = colcount);
                        this.InvokeEx(f => f.dataGridView1.Columns[0].HeaderText = "Name");
                        this.InvokeEx(f => f.dataGridView1.Columns[1].HeaderText = "Package ID");
                        this.InvokeEx(f => f.dataGridView1.Columns[2].HeaderText = "ID");
                        this.InvokeEx(f => f.dataGridView1.Columns[3].HeaderText = "Path");
                        this.InvokeEx(f => f.dataGridView1.Columns[3].HeaderText = "Date");


                    }
                    //this.InvokeEx(f => f.dataGridView1[0, f.dataGridView1.RowCount-2].Value = ModName);
                    this.InvokeEx(f => f.dataGridView1.Rows.Add(ModName, modID, modUrlID, d, dt));
                    //this.InvokeEx(f => f.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells));
                    this.InvokeEx(f => f.dataGridView1.AutoResizeColumns());


                }



            }
        }

        void CopyDGVSelectedCellText(DataGridView DGV)
        {
            if (DGV.SelectedCells.Count > 0)
            {
                string text = DGV.SelectedCells[0].Value as string;
            Log(text);
            }
            
        }

    } //form1
}
