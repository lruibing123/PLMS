using AForge.Video.DirectShow;
using PLMS.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PLMS.WinformUI
{
    public partial class FormSimulate : Form
    {
        private bool isEnter, isLeave;
        private Thread enterThread;
        private Thread leaveThread;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice source;
        private Bitmap bitmap;

        public FormSimulate()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void EnterScan()
        {
            while (isEnter)
            {
                Thread.Sleep(1000);
                bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
                string licemsePlateNum = LicensePlateOCR.Get(bitmap);
                if (licemsePlateNum != null)
                {
                    string result;
                    label1.Text = VehicleBLL.VehicleEnter(licemsePlateNum, out result) ? "欢迎进入" : "";
                    label2.Text = result;
                    label3.Text = AdminBLL.GetActiveParkNum() + " / " + AdminBLL.GetParkingSpotNum();
                    FormManage form = (FormManage)Owner;
                    form.Refresh();                    
                    Thread.Sleep(8000);
                }
                else
                {
                    label1.Text = "检测失败";
                    label2.Text = "";
                }
            }
            label1.Text = label2.Text = "";
        }

        private void LeaveScan()
        {
            while (isLeave)
            {
                Thread.Sleep(1000);
                bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
                string licemsePlateNum = LicensePlateOCR.Get(bitmap);
                if (licemsePlateNum != null)
                {
                    string result;
                    label1.Text = VehicleBLL.VehicleLeave(licemsePlateNum, out result) ? "欢迎离开" : "";
                    label2.Text = result;
                    label3.Text = AdminBLL.GetActiveParkNum() + " / " + AdminBLL.GetParkingSpotNum();
                    BaseBLL.SaveALL();
                    FormManage form = (FormManage)Owner;
                    form.Refresh();                    
                    Thread.Sleep(8000);
                }
                else
                {
                    label1.Text = "检测失败";
                    label2.Text = "";
                }
            }
            label1.Text = label2.Text = "";
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            isEnter = true; isLeave = false;            
            if (enterThread == null || enterThread.ThreadState == ThreadState.Stopped)
            {
                enterThread = new Thread(EnterScan);
                enterThread.Start();
            }                
            if (leaveThread != null && leaveThread.ThreadState == ThreadState.Running)
            {
                leaveThread.Abort();
                leaveThread = null;
            }
        }    

        private void buttonLeave_Click(object sender, EventArgs e)
        {
            isEnter = false; isLeave = true;
            if (leaveThread == null || leaveThread.ThreadState == ThreadState.Stopped)
            {
                leaveThread = new Thread(LeaveScan);
                leaveThread.Start();
            }                
            if (enterThread != null && enterThread.ThreadState != ThreadState.Stopped)
            {
                enterThread.Abort();
                enterThread = null;
            }
        }

        private void FormSimulate_Load(object sender, EventArgs e)
        {
            Location = new Point(800, 100);
            BaseBLL.InitAll();
            isEnter = isLeave = false;

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            comboBox1.DataSource = videoDevices;
            List<string> names = new List<string>();
            foreach (FilterInfo info in videoDevices)
                names.Add(info.Name);
            comboBox1.DataSource = names;
            comboBox1.SelectedIndex = 0;
        }

        private void comboBoxCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoDevices.Count > 0)
            {
                source = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                videoSourcePlayer1.SignalToStop();
                videoSourcePlayer1.WaitForStop();
                videoSourcePlayer1.VideoSource = source;
            }
        }

        private void buttonStartCamera_Click(object sender, EventArgs e)
        {
            comboBoxCamera_SelectedIndexChanged(null, null);
            videoSourcePlayer1.Start();
            isEnter = isLeave = false;
        }

        private void buttonStopAll_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            source.Stop();
            isEnter = isLeave = false;
        }

        private void FormSimulate_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
            source.Stop();
            if (bitmap != null) bitmap.Dispose();
        }
    }
}
