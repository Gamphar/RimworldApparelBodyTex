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
    class Template_ThreadWorker
    {
    }

    public partial class Form1 : Form
    {
        //XXTREADNAMEXX
        bool IsThreadRun_XXTREADNAMEXX = false;

        private void startThread_XXTREADNAMEXX()
        {


            if (IsThreadRun_XXTREADNAMEXX)
            {
                Log("XXTREADNAMEXX Already Running");
                MessageBox.Show(btn_XXTREADNAMEXX.Text + " is already running.");
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
            var th = new Thread(Thread_XXTREADNAMEXX);
            th.IsBackground = true;
            th.Start();
            //Thread.Sleep(1000);
            Log("Main thread ({0}) ",
                              Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread_XXTREADNAMEXX()
        {
            IsThreadRun_XXTREADNAMEXX = true;
            //Console.WriteLine("Thread ({0}) running.",
            //                  Thread.CurrentThread.ManagedThreadId);
            Log("Thread ({0}) running.",
                              Thread.CurrentThread.ManagedThreadId);

            //start run timer
            DateTime beginTime = DateTime.Now;

            //labeling in
            string defaultBtnLabel = "";
            this.InvokeEx(f => defaultBtnLabel = f.btn_XXTREADNAMEXX.Text);
            this.InvokeEx(f => f.btn_XXTREADNAMEXX.Text = "... In Progress ...");


            //start
            //=================================================================================1
            Thread_DoXXTREADNAMEXX();

            //=================================================================================1
            //end

            //labeling out
            this.InvokeEx(f => f.btn_XXTREADNAMEXX.Text = defaultBtnLabel);

            //end timer
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - beginTime;

            Log("Thread ({1}) Done. Runtime {0}.", runTime, Thread.CurrentThread.ManagedThreadId);

            IsThreadRun_XXTREADNAMEXX = false;
        }


        private void Thread_DoXXTREADNAMEXX()
        {
            //do stuff


        }

    }
}
