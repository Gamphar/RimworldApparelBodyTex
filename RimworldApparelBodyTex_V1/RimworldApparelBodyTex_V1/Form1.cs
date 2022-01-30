using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace RimworldApparelBodyTex_V1
{

    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //global var?
        List<ModActivePackageId> ModActivePackageIdList = new List<ModActivePackageId>();

        //global settings, optional, just for 1-stop-access looks, or in da future using profile.
        IList<GlobalSettings> MySettings = new List<GlobalSettings>();


        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalSettings MySettingDefault = new GlobalSettings();
            MySettingDefault.BodyTypeSource = "Female";
            MySettingDefault.BodyTypeDestination = "FemaleBB";
            MySettingDefault.TexDestinationPath = @"E:\Game\Steam\steamapps\common\RimWorld\Mods\ApparelBody Support\Textures";
            MySettingDefault.Overwrite = false;
            MySettingDefault.IncludeBodyTexture = false;
            MySettingDefault.RimworldData = @"E:\Game\Steam\steamapps\common\RimWorld\Data";
            MySettingDefault.BaseSourceTexDir = @"D:\Game\MOD\Rimworld\Base Tex\Texture - Base Combine 3 DLC";

            MySettings.Add(MySettingDefault);


            textBox_TexDestinationPath.Text = MySettings[0].TexDestinationPath;
            textBox_rimworldData.Text = MySettings[0].RimworldData;
            textBox_sourceTex.Text = MySettings[0].BaseSourceTexDir;

        }


        private void btn_getModConfig_Click(object sender, EventArgs e)
        {
            string baseModConfigFilePath = @"AppData\LocalLow\Ludeon Studios\RimWorld by Ludeon Studios\Config\ModsConfig.xml";
            string ModConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), baseModConfigFilePath);
            textBox_modConfig.Text = ModConfigFilePath;
            textBox_xml.Text = File.ReadAllText(ModConfigFilePath);
            //Log(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            //Log(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            //Log(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            //Log(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }


        public void Log(string msg)
        {
            string titleInfo = "log";
            msg = String.Format("Thread ({0}) info: {1}:: {2}.",
                              Thread.CurrentThread.ManagedThreadId, titleInfo, msg);


            this.InvokeEx(f => f.textBox_log.AppendText(msg + "\r\n"));

        }

        public void Log(string msg, params object[] o)
        {

            msg = String.Format(msg,
                              o);

            this.InvokeEx(f => f.textBox_log.AppendText(msg + "\r\n"));

        }

        private void btn_listModLoadOrder_Click(object sender, EventArgs e)
        {
            //string XMLFilePath = textBox_modConfig.Text;
            //Log(XMLFilePath);

            //XDocument XMLdoc = XDocument.Load(XMLFilePath);
            XmlDocument XMLdoc = new XmlDocument();

            //XMLdoc.XmlResolver = null;
            //XMLdoc.LoadXml(XMLFilePath); //not working, seems need to load as text not a file
            XMLdoc.LoadXml(textBox_xml.Text);
            //XMLdoc.Load(XMLFilePath);

            XmlNode ModsConfigDataNode = XMLdoc.SelectSingleNode("/ModsConfigData");

            if (ModsConfigDataNode is null)
            {
                Log("<ERROR> Fail to list mod load order. Root is null.");
                return;
            }

            Log("Select nodes");
            //XmlNodeList ActiveModsNode = ModsConfigDataNode.SelectNodes("/activeMods/li"); //not working
            XmlNodeList ActiveModsNode = XMLdoc.SelectNodes("/ModsConfigData/activeMods/li");
            int i = 0;
            foreach (XmlNode Li in ActiveModsNode)
            {
                i++;
                Log("#"+i.ToString()+" Li = " + Li.InnerText);
                ModActivePackageId ActiveModID = new ModActivePackageId();
                ActiveModID.PackageId = Li.InnerText;
                ModActivePackageIdList.Add(ActiveModID);
            }

        }

        private void btn_addModFolder_Click(object sender, EventArgs e)
        {
            if (textBox_rootModFolder.Text == "")
            {
                checkedListBox_modFolder.Items.Add(@"E:\Game\Steam\steamapps\workshop\content\294100");
                checkedListBox_modFolder.Items.Add(@"E:\Game\Steam\steamapps\common\RimWorld\Mods");
            }
            else if (textBox_rootModFolder.Text.ToLower() == "kantor")
            {
                checkedListBox_modFolder.Items.Add(@"D:\Fendhi Baru\Misc\Steam\steamapps\common\RimWorld\Mods");
            } else
            {
                checkedListBox_modFolder.Items.Add(textBox_rootModFolder.Text);
            }

            //auto checked all item at first
            for(int i = 0; i<checkedListBox_modFolder.Items.Count;i++)
            {
                checkedListBox_modFolder.SetItemChecked(i, true);
            }
            
            //LogInstalledSoftware();
        }


        private void LogInstalledSoftware()
        {



            string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 548680";
            using (RegistryKey subkey = Registry.LocalMachine.OpenSubKey(path))
            {
                if (subkey.GetValue("DisplayName") != null)
                {

                }
                else
                {

                }
            }



                return;
            List<string> registry_keys = new List<string>() {
              @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
              @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            foreach(string reg_key in registry_keys)
            {
                Log(reg_key);
                ListUninstallAppsFromRegKey(reg_key);
            }



        }


        public void ListUninstallAppsFromRegKey(string reg_key)
        {
            List<InstalledProgram> installedprograms = new List<InstalledProgram>();
            string registry_key = reg_key;// @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            //using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    Log("subkey_name = {0}", subkey_name);
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (subkey.GetValue("DisplayName") != null)
                        {

                            InstalledProgram app = new InstalledProgram();
                            app.DisplayName = subkey.GetValue("DisplayName").ToString();

                            if (subkey.GetValue("InstallLocation") != null)
                            {
                                app.InstallLocation = subkey.GetValue("InstallLocation").ToString();
                            }
                            else if (subkey.GetValue("InstallDir") != null)
                            {
                                app.InstallLocation = subkey.GetValue("InstallDir").ToString();
                            }

                            installedprograms.Add(app);
                        }
                    }
                }
            }

            foreach (InstalledProgram app in installedprograms)
            {
                Log("Installed = {0}, Loc = {1}", app.DisplayName, app.InstallLocation);
            }
        }

        

        List<string> ModFolders = new List<string>();

        private void Btn_MulaiCek_Click(object sender, EventArgs e)
        {
            if (!IsThreadRun_AddNewBody)
            {
                //set new mod folder list

                ModFolders.Clear();
                foreach (object item in checkedListBox_modFolder.CheckedItems)
                {
                    Log(item.ToString());

                    ModFolders.Add(item.ToString());
                }

                startThread_AddNewBody();
            }

        }

        private void startThread_AddNewBody()
        {
            //TODO:thread check new chat list
            var th = new Thread(Thread_AddNewBody);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void checkBox_overwrite_CheckedChanged(object sender, EventArgs e)
        {
            if (IsThreadRun_AddNewBody)
            {
                checkBox_overwrite.Checked = MySettings[0].Overwrite;
            }
            else
            {
                MySettings[0].Overwrite = checkBox_overwrite.Checked;
            }

        }

        private void checkBox_IncludeBodyTex_CheckedChanged(object sender, EventArgs e)
        {
            if (IsThreadRun_AddNewBody)
            {
                checkBox_IncludeBodyTex.Checked = MySettings[0].IncludeBodyTexture;
            }
            else
            {
                MySettings[0].IncludeBodyTexture = checkBox_IncludeBodyTex.Checked;
            }

        }

        private void btn_UpdateBodyType_Click(object sender, EventArgs e)
        {
            IList<string> ModFolder = new List<string>();
            FillUpStringListFromCheckedListkBox(ModFolder, checkedListBox_modFolder, 1); //only get checked items

            if (ModFolder.Count<=0)
            {
                Log("Updating body type, but nothing to check, please select root of mod folder first.");
                MessageBox.Show("Checked any root mod folder first!");
                return;
            }

            //set default
            comboBox_BodyTypeSource.Items.Clear();
            comboBox_BodyTypeSource.Items.Add("Fat");
            comboBox_BodyTypeSource.Items.Add("Female");
            comboBox_BodyTypeSource.Items.Add("Hulk");
            comboBox_BodyTypeSource.Items.Add("Male");
            comboBox_BodyTypeSource.Items.Add("Thin");



            comboBox_BodyTypeDestination.Items.Clear();
            comboBox_BodyTypeDestination.Items.Add("Fat");
            comboBox_BodyTypeDestination.Items.Add("Female");
            comboBox_BodyTypeDestination.Items.Add("Hulk");
            comboBox_BodyTypeDestination.Items.Add("Male");
            comboBox_BodyTypeDestination.Items.Add("Thin");

            //add from active mod
            Param_Thread_ListBodyTypToList p = new Param_Thread_ListBodyTypToList();
            p.cb = comboBox_BodyTypeSource;
            p.cb_dest = comboBox_BodyTypeDestination;

            ListModBodyTypeDefNameToComboBox(p);
        }

        public void FillUpStringListFromCheckedListkBox(IList<string> IList, CheckedListBox checkedListBox, int iMode)
        {

            switch (iMode)
            {
                case 0: //List unchecked items
                    foreach (object item in checkedListBox.Items)
                    {
                        if (!checkedListBox.CheckedItems.Contains(item))
                        {
                            IList.Add(item.ToString());
                        }
                    }
                    break;
                case 1: //List checked items
                    foreach (object item in checkedListBox.CheckedItems)
                    {
                        IList.Add(item.ToString());
                    }
                    break;
                case 2: //List all items
                    foreach (object item in checkedListBox.Items)
                    {
                        IList.Add(item.ToString());
                    }
                    break;
            }

        }

        

        private void comboBox_BodyTypeSource_TextChanged(object sender, EventArgs e)
        {
            MySettings[0].BodyTypeSource = comboBox_BodyTypeSource.Text;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox_BodyTypeDestination_TextChanged(object sender, EventArgs e)
        {
            MySettings[0].BodyTypeDestination = comboBox_BodyTypeDestination.Text;
        }

        private void textBox_TexDestinationPath_TextChanged(object sender, EventArgs e)
        {
            MySettings[0].TexDestinationPath = textBox_TexDestinationPath.Text;
        }

        private void CheckAndSetHARDestinationLocAsSourceLoc()
        {
            if (checkBox_sameAsSource.Checked)
            {
                textBox_BodyTextDestination.Text = textBox_BodyTextSource.Text;
            }
        }
        private void btn_genHarBodyTexFromVanillaTex_Click(object sender, EventArgs e)
        {
            string srcDir = textBox_BodyTextSource.Text;
            string dstDir = textBox_BodyTextDestination.Text;

            if (!Directory.Exists(srcDir))
            {
                MessageBox.Show("Source Directory is not exist!");
                return;
            }

            if (!Directory.Exists(dstDir))
            {
                MessageBox.Show("Destination Directory is not exist!");
                return;
            }

            //========================

            //get file image in the source
            Log("source path = {0}", srcDir);
            IList<string> ListImageFilePath = new List<string>();
            
            ValidExt.Clear();
            ValidExt.Add(".jpg");
            ValidExt.Add(".png");
            ValidExt.Add(".dds");
            DirectorySearchAllFiles(ListImageFilePath, srcDir, ValidExt);
            //foreach (string f in Directory.GetFiles(srcDir))
            //{
            //    bool bValidExt = Path.GetExtension(f).ToLower() == ".png" | Path.GetExtension(f).ToLower() == ".jpg";
            //    bool bValidName = Path.GetFileName(f).ToLower().StartsWith("naked");
            //    Log("bValidExt {0}, bValidName {1}, path = {2}", bValidExt, bValidName, f);
            //    if (bValidExt & bValidName)
            //    {
            //        Log("Found {0}, {1}", Path.GetExtension(f).ToLower(), f);
            //        ListImageFilePath.Add(f);
            //    } else
            //    {
            //        Log("Not valid, {0}", f);
            //    }
            //}


            //filter only body tex included
            IList<string> ListImageFilePath_Loop = new List<string>(ListImageFilePath);
            foreach(string f in ListImageFilePath_Loop)
            {
                bool bValidName = Path.GetFileName(f).ToLower().StartsWith("naked");
                if (bValidName)
                {
                    Log("Found {0}, {1}", Path.GetExtension(f).ToLower(), f);

                } else
                {
                    ListImageFilePath.Remove(f);
                }
            }


            //log first the founded sources
            Log("=================================");
            Log("List Source total : #{0}", ListImageFilePath.Count);
            for (int i = 0;i<ListImageFilePath.Count & ListImageFilePath.Count>0; i++)
            {
                string sPath = ListImageFilePath[i];
                Log("#{0}, {1}",i+1, sPath);
            }
            Log("=================================");
            //process vanilla to HAR format
            foreach (string srcFilePath in ListImageFilePath)
            {
                string sourceFileName = Path.GetFileName(srcFilePath);



                //female
                string destinationFileName_female = "Female_" + sourceFileName;
                string dstFilePath_female = Path.Combine(dstDir, destinationFileName_female);
                Log("Copy from {0} to {1}", srcFilePath, dstFilePath_female);
                File.Copy(srcFilePath, dstFilePath_female, true); //do overwrite

                //male
                string destinationFileName_male = "Male_" + sourceFileName;
                string dstFilePath_male = Path.Combine(dstDir, destinationFileName_male);
                Log("Copy from {0} to {1}", srcFilePath, dstFilePath_male);
                File.Copy(srcFilePath, dstFilePath_male, true); //do overwrite
            }


            //check all body exists
            /*
                Naked_Thin_south.png
                Naked_Thin_north.png
                Naked_Thin_east.png
                Naked_Male_south.png
                Naked_Male_north.png
                Naked_Male_east.png
                Naked_Hulk_south.png
                Naked_Hulk_north.png
                Naked_Hulk_east.png
                Naked_Female_south.png
                Naked_Female_north.png
                Naked_Female_east.png
                Naked_Fat_south.png
                Naked_Fat_north.png
                Naked_Fat_east.png
                
             */
            IList<string> ListBaseBodyImage = new List<string>();
            ListBaseBodyImage.Add("Naked_Thin_south.png");
            ListBaseBodyImage.Add("Naked_Thin_north.png");
            ListBaseBodyImage.Add("Naked_Thin_east.png");
            ListBaseBodyImage.Add("Naked_Male_south.png");
            ListBaseBodyImage.Add("Naked_Male_north.png");
            ListBaseBodyImage.Add("Naked_Male_east.png");
            ListBaseBodyImage.Add("Naked_Hulk_south.png");
            ListBaseBodyImage.Add("Naked_Hulk_north.png");
            ListBaseBodyImage.Add("Naked_Hulk_east.png");
            ListBaseBodyImage.Add("Naked_Female_south.png");
            ListBaseBodyImage.Add("Naked_Female_north.png");
            ListBaseBodyImage.Add("Naked_Female_east.png");
            ListBaseBodyImage.Add("Naked_Fat_south.png");
            ListBaseBodyImage.Add("Naked_Fat_north.png");
            ListBaseBodyImage.Add("Naked_Fat_east.png");


            int iFail = 0;
            foreach(string body in ListBaseBodyImage)
            {
                //check female
                string destinationFileName_female = "Female_" + body;
                string dstFilePath_female = Path.Combine(dstDir, destinationFileName_female);
                if (!File.Exists(dstFilePath_female))
                {
                    Log("base tex fail to generate har tex \r\nbase = {0}\r\n har = {1}", body, dstFilePath_female);
                    iFail++;
                }

                //check male
                string destinationFileName_male = "Female_" + body;
                string dstFilePath_male = Path.Combine(dstDir, destinationFileName_female);
                if (!File.Exists(dstFilePath_male))
                {
                    Log("base tex fail to generate har tex \r\nbase = {0}\r\n har = {1}", body, dstFilePath_male);
                    iFail++;
                }
            }

            if (iFail > 0)
            {
                Log("CONTAIN FAILED GEN, CHECK ABOVE LOG.");
            }
            
            Log("Done Gen HAR Body Tex from Vanilla Text");

        }

        private void checkBox_sameAsSource_CheckedChanged(object sender, EventArgs e)
        {
            CheckAndSetHARDestinationLocAsSourceLoc();
        }

        private void textBox_BodyTextSource_TextChanged(object sender, EventArgs e)
        {
            CheckAndSetHARDestinationLocAsSourceLoc();
        }

        private void textBox_BodyTextDestination_TextChanged(object sender, EventArgs e)
        {
            CheckAndSetHARDestinationLocAsSourceLoc();
        }

        private void btn_remModFolder_Click(object sender, EventArgs e)
        {
            if (checkedListBox_modFolder.SelectedItems != null & checkedListBox_modFolder.SelectedItems.Count>0)
            {
                checkedListBox_modFolder.Items.Remove(checkedListBox_modFolder.SelectedItems[0]);
            }
        }

        private void btn_genBaseCoreApparel_Click(object sender, EventArgs e)
        {
            bool bSkipCheck = false;

            if (!bSkipCheck & ListFolderActiveMod == null)
            {
                MessageBox.Show("something wrong, ListFolderActiveMod is not assigned");
                return;
            }
            else if (!bSkipCheck & ListFolderActiveMod.Count<=0)
            {
                MessageBox.Show("Please update body type first");
                return;
            }

            if (!bSkipCheck & comboBox_BodyTypeSource.Text == "")
            {
                MessageBox.Show("Please select source body type first");
                return;
            }
            if (!bSkipCheck & comboBox_BodyTypeDestination.Text == "")
            {
                MessageBox.Show("Please select destination body type first");
                return;
            }

            Param_Thread_ListBodyTypToList p = new Param_Thread_ListBodyTypToList();
            p.cb = comboBox_BodyTypeSource;
            p.cb_dest = comboBox_BodyTypeDestination;
            startThread_GenBaseCoreApparel(p);
        }


        private void startThread_GenBaseCoreApparel(Param_Thread_ListBodyTypToList p)
        {
           
            if (!IsThreadRun_GenBaseCoreApparel)
            {

                var th = new Thread(Thread_GenBaseCoreApparel);
                th.IsBackground = true;
                th.Start(p);
                //Thread.Sleep(1000);
                Log("Main thread ({0}) ",
                                  Thread.CurrentThread.ManagedThreadId);


            }
            else { Log("<WARNING> Not allowed multiple instances of Gen Base Core Apparel."); }
        }

        private void textBox_rimworldData_TextChanged(object sender, EventArgs e)
        {
            MySettings[0].RimworldData = textBox_rimworldData.Text;
        }

        private void textBox_sourceTex_TextChanged(object sender, EventArgs e)
        {
            MySettings[0].BaseSourceTexDir = textBox_sourceTex.Text;
        }

        private void btn_CopyFromStep2_Click(object sender, EventArgs e)
        {
            textBox_s3_TexDestinationPath.Text = textBox_TexDestinationPath.Text;
        }

        private void btn_CopyToStep2_Click(object sender, EventArgs e)
        {
            textBox_TexDestinationPath.Text = textBox_s3_TexDestinationPath.Text;
        }

        private void btn_ExcludeDestinationFromSource_Click(object sender, EventArgs e)
        {
            startThread_ExcludeDstFromSrc();
        }

        private void btn_GenBaseCoreApparelByDirectCopy_Click(object sender, EventArgs e)
        {
            startThread_GenBaseCoreApparelByDirectCopy();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_GenThingDefXML_Click(object sender, EventArgs e)
        {
            startThread_GenThingDefXML();
        }
    }


    public static class ISynchronizeInvokeExtensions
    {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this != null)
            {
                if (@this.InvokeRequired)
                {
                    @this.Invoke(action, new object[] { @this });
                }
                else
                {
                    action(@this);
                }
            }
        }
    }
}
