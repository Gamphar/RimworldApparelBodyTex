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
    class ThreadWorker_GenBaseByDirectCopy
    {
    }
    public partial class Form1 : Form
    {
        //GenBaseCoreApparelByDirectCopy
        bool IsThreadRun_GenBaseCoreApparelByDirectCopy = false;

        private void startThread_GenBaseCoreApparelByDirectCopy()
        {
            

            if (IsThreadRun_GenBaseCoreApparelByDirectCopy)
            {
                Log("GenBaseCoreApparelByDirectCopy Already Running");
                MessageBox.Show(btn_GenBaseCoreApparelByDirectCopy.Text + " is already running.");
                return;
            }

            //setup setting first
            MySettings[0].BaseSourceTexDir = textBox_sourceTex.Text;
            MySettings[0].TexDestinationPath = textBox_TexDestinationPath.Text;

            string DirSrc = MySettings[0].BaseSourceTexDir;
            string DirDst = MySettings[0].TexDestinationPath;

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
            var th = new Thread(Thread_GenBaseCoreApparelByDirectCopy);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread_GenBaseCoreApparelByDirectCopy()
        {
            IsThreadRun_GenBaseCoreApparelByDirectCopy = true;
            //Console.WriteLine("Thread ({0}) running.",
            //                  Thread.CurrentThread.ManagedThreadId);
            Log("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;

            //labeling in
            string defaultBtnLabel = "";
            this.InvokeEx(f => defaultBtnLabel = f.btn_GenBaseCoreApparelByDirectCopy.Text);
            this.InvokeEx(f => f.btn_GenBaseCoreApparelByDirectCopy.Text = "... In Progress ...");


            //start
            //=================================================================================1
            Thread_DoGenBaseCoreApparelByDirectCopy();

            //=================================================================================1
            //end

            //labeling out
            this.InvokeEx(f => f.btn_GenBaseCoreApparelByDirectCopy.Text = defaultBtnLabel);

            //end timer
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;

            Log("Thread ({1}) Done. Runtime {0}.", runTime, Thread.CurrentThread.ManagedThreadId);

            IsThreadRun_GenBaseCoreApparelByDirectCopy = false;
        }


        private void Thread_DoGenBaseCoreApparelByDirectCopy() 
        {
            string DirSrc = MySettings[0].BaseSourceTexDir;
            string DirDst = MySettings[0].TexDestinationPath;
            string BodyTypeSrc = MySettings[0].BodyTypeSource;
            string BodyTypeDst = MySettings[0].BodyTypeDestination;
            string key_OldBodyType = string.Format("_{0}_", BodyTypeSrc);
            string key_NewBodyType = string.Format("_{0}_", BodyTypeDst);
            IList<string> List_ApparelImageTexs = new List<string>();
            DirectorySearchAllFiles_WithRuleset(List_ApparelImageTexs, DirSrc, FuncRuleset_ValidApparelTex_Female); //get all apparels for female body type

            //log first
            LogList(List_ApparelImageTexs, "src apparel img tex = ");

            //copy
            int iSkip = 0;
            int iCopy = 0;
            foreach(string ApparelImageTexFilePath in List_ApparelImageTexs)
            {
                string oldFilePath = ApparelImageTexFilePath;
                string baseFilePath = oldFilePath.Replace(DirSrc, "", StringComparison.OrdinalIgnoreCase); //remove src dir to get dest dir structure
                string newBaseFilePath = baseFilePath.Replace(key_OldBodyType, key_NewBodyType, StringComparison.OrdinalIgnoreCase);
                string newFilePath = string.Format("{0}{1}", DirDst, newBaseFilePath); //sudah ada \ di baseFilePath jadi tinggal gabung
                string newFileDirPath = Path.GetDirectoryName(newFilePath);
                if (!Directory.Exists(newFileDirPath))
                {
                    Directory.CreateDirectory(newFileDirPath);
                }

                if (!File.Exists(newFilePath))
                {
                    iCopy++;
                    Log("<<COPY>> #{2} Copy {0} ->>to->> {1}", oldFilePath, newFilePath, iCopy);
                    File.Copy(oldFilePath, newFilePath);
                }
                else
                {
                    iSkip++;
                    Log("<<SKIP>> #{2} Copy {0} ->>to->> {1}", oldFilePath, newFilePath, iSkip);
                }
                
            }


        }

    }

}
