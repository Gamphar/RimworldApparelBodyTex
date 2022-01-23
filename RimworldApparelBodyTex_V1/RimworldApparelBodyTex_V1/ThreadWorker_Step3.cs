using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RimworldApparelBodyTex_V1
{
    class ThreadWorker_Step3
    {
    }
    public partial class Form1 : Form
    {
        //ExcludeDstFromSrc
        bool IsThreadRun_ExcludeDstFromSrc = false;

        private void startThread_ExcludeDstFromSrc()
        {

            if (IsThreadRun_ExcludeDstFromSrc)
            {
                Log("ExcludeDstFromSrc Already Running");
                MessageBox.Show(btn_ExcludeDestinationFromSource.Text+" is already running.");
                return;
            }

            //check up
            if( !Directory.Exists( textBox_s3_TexSourcePath.Text))
            {
                string s = "Source dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            if (!Directory.Exists(textBox_s3_TexDestinationPath.Text))
            {
                string s = "Destination dir not exists";
                Log(s);
                MessageBox.Show(s);
                return;
            }

            //setup setting first
            MySettings[0].Step3_TexSourcePath = textBox_s3_TexSourcePath.Text;
            MySettings[0].Step3_TexDestinationPath = textBox_s3_TexDestinationPath.Text;

            //start the thread
            var th = new Thread(Thread_ExcludeDstFromSrc);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread_ExcludeDstFromSrc()
        {
            IsThreadRun_AddNewBody = true;
            //Console.WriteLine("Thread ({0}) running.",
            //                  Thread.CurrentThread.ManagedThreadId);
            Log("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;

            //start
            //=================================================================================1
            Thread_DoMoveTexDstToExcludeFolder();

            //=================================================================================1
            //end
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;

            Log("Thread ({1}) Done. Runtime {0}.", runTime, Thread.CurrentThread.ManagedThreadId);

            IsThreadRun_AddNewBody = false;
        }

        private void Thread_DoMoveTexDstToExcludeFolder()
        {
            string DirSrc = MySettings[0].Step3_TexSourcePath;
            string DirDst = MySettings[0].Step3_TexDestinationPath;

            ValidExt.Clear();
            ValidExt.Add(".jpg");
            ValidExt.Add(".png");
            ValidExt.Add(".dds");

            //records all moved file later
            IList<string> List_MovedFiles = new List<string>();

            //list all tex source
            IList<string> List_TexSrcFilePaths = new List<string>();            
            DirectorySearchAllFiles(List_TexSrcFilePaths, DirSrc, ValidExt);

            //list all tex destination
            IList<string> List_TexDstFilePaths = new List<string>();
            DirectorySearchAllFiles(List_TexDstFilePaths, DirDst, ValidExt);

            //compare and move
            string modDir = Path.GetDirectoryName(DirDst);
            string excludeDir = string.Format("{0}\\{1}", modDir,"[Exclude]");
            if (!Directory.Exists(excludeDir))
            {
                Directory.CreateDirectory(excludeDir);
            }
            IList<string> List_TexSrcFilePaths_Temp = new List<string>(List_TexSrcFilePaths);
            foreach (string TexDstFilePath in List_TexDstFilePaths)
            {
                string TexDstFileName = Path.GetFileName(TexDstFilePath);
                //search if exists in source
                string TexSrcFilePath_Move = "";
                bool bFound = false;
                foreach (string TexSrcFilePath in List_TexSrcFilePaths_Temp)
                {
                    string TexSrcFileName = Path.GetFileName(TexSrcFilePath);
                    //compare
                    if(TexDstFileName.ToLower()== TexSrcFileName.ToLower())
                    {
                        //found it
                        TexSrcFilePath_Move = TexSrcFilePath;
                        bFound = true;
                        //copy
                        if (bFound)
                        {


                            string basePartFilePath = TexDstFilePath.Replace(modDir+"\\","",StringComparison.OrdinalIgnoreCase);
                            Log("basePartFilePath = {0}", basePartFilePath);
                            string MoveExcludeFilePath = Path.Combine(excludeDir, basePartFilePath);
                            Log("MoveExcludeFilePath = {0}", MoveExcludeFilePath);

                            if (File.Exists(MoveExcludeFilePath))
                            {
                                string dir = Path.GetDirectoryName(MoveExcludeFilePath);
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }

                                string fileNameNoExt = Path.GetFileNameWithoutExtension(MoveExcludeFilePath);
                                string ext = Path.GetExtension(MoveExcludeFilePath);
                                string newFileName = fileNameNoExt + "_old_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext;
                                string MoveExcludeFilePath_Backup = Path.Combine(dir, newFileName);
                                File.Replace(TexDstFilePath, MoveExcludeFilePath, MoveExcludeFilePath_Backup);
                            }
                            else
                            {
                                string dir = Path.GetDirectoryName(MoveExcludeFilePath);
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.Move(TexDstFilePath, MoveExcludeFilePath);
                            }

                            //records moved file
                            List_MovedFiles.Add(MoveExcludeFilePath);


                        }
                        //
                        break; //go next TexDstFilePath
                    }                    
                }

                //remove the already found item
                if (bFound & TexSrcFilePath_Move != "")
                    List_TexSrcFilePaths_Temp.Remove(TexSrcFilePath_Move);
                //


            }

            //log any moved files
            Log("List_MovedFiles count = {0}", List_MovedFiles.Count);
            int i = 0;
            foreach(string s in List_MovedFiles)
            {
                i++;
                Log("New Moved File #{0} = {1}", i, s);
            }


        }
    }
}
