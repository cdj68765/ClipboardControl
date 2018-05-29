using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardControl
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource CancelInfoMonitor = new CancellationTokenSource();
        private CancellationTokenSource CancelInfoClipboard = new CancellationTokenSource();
        private readonly Thread LoopStatus;

        public Form1()
        {
            InitializeComponent();
            if (Properties.Settings.Default.DateCollect == null)
            {
                Properties.Settings.Default.DateCollect = new Hashtable();
            }
            if (Properties.Settings.Default.AutoRun)
            {
                AutoRun.CheckState = CheckState.Checked;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                CancelInfoMonitor = new CancellationTokenSource();
                StartMonitor();
            }

            this.Deactivate += delegate
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    ShowInTaskbar = false;
                }
            };
            CancellationTokenSource CloseCheck = new CancellationTokenSource();
             LoopStatus = new Thread(() =>
            {
                do
                {
                    if (AutoRun.Checked || ClipboardTask?.IsAlive == true)
                    {
                        if (ClipboardTask?.IsAlive == true)
                        {
                            Invoke(new Action(() =>
                            {
                                Status.Text = @"运行中....";
                                Status.ForeColor = Color.Green;
                                Start.Text = @"关闭";
                            }));

                        }
                        else if (ClipboardTask?.IsAlive != true)
                        {
                            Invoke(new Action(() =>
                            {
                                Status.Text = @"关闭中....";
                                Status.ForeColor = Color.Red;
                                CancelInfoClipboard.Cancel();
                                Start.Text = @"启动";
                            }));

                        }
                    }
                    else
                    {

                        if (ClipboardChange?.Status == TaskStatus.Running)
                        {
                            Invoke(new Action(() =>
                            {
                                Status.Text = @"运行中....";
                                Status.ForeColor = Color.Green;
                                Start.Text = @"关闭";
                            }));
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                Status.Text = @"关闭中....";
                                Status.ForeColor = Color.Red;
                                Start.Text = @"启动";
                            }));
                        }
                    }


                    Thread.Sleep(500);
                } while (!CloseCheck.IsCancellationRequested);
            });
            this.FormClosing += delegate
            {
                Properties.Settings.Default.AutoRun = AutoRun.Checked;
                Properties.Settings.Default.Save();
                CloseCheck.Cancel();
                LoopStatus.Abort();
            };
        }

        private Thread ClipboardTask;
        private void StartMonitor()
        {
            ClipboardTask =new Thread(() =>
            {
                while (!CancelInfoMonitor.IsCancellationRequested)
                {
                    var ret = NativeMethods.EnumWindows((hWnd, lParam) =>
                    {
                        StringBuilder GetText = new StringBuilder(256);
                        NativeMethods.GetWindowTextW(hWnd, GetText, 256);
                        if (GetText.ToString().StartsWith("CAXA"))
                        {
                            return false;
                        }

                        return true;
                    }, 0);
                    if (!ret&& ClipboardChange?.Status != TaskStatus.Running)
                    {
                        StartClipboardChange();
                    }
                    else
                    {
                        if (ClipboardChange?.Status == TaskStatus.Running)
                        {
                            CancelInfoClipboard.Cancel();
                        }
                        if(!CancelInfoMonitor.IsCancellationRequested)
                        Thread.Sleep(5000);
                    }
                }
            });
            ClipboardTask.Start();
        }

        private Task ClipboardChange;

        private void StartClipboardChange()
        {
            CancelInfoClipboard = new CancellationTokenSource();

            ClipboardChange =  Task.Factory.StartNew(() =>
            {
                {
                    var LastS = string.Empty;
                    while (!CancelInfoClipboard.IsCancellationRequested)
                    {
                        var Temp = ClipboardControl.GetText(ClipboardFormat.CF_UNICODETEXT);
                        if (!string.IsNullOrEmpty(Temp) && Temp != LastS)
                        {
                            ThreadPool.QueueUserWorkItem((object state) =>
                            {
                                if (Properties.Settings.Default.DateCollect.Contains(Temp))
                                {
                                    Properties.Settings.Default.DateCollect[Temp] =
                                     (int.Parse(Properties.Settings.Default.DateCollect[Temp].ToString()) + 1).ToString();
                                }
                                else
                                {
                                    Properties.Settings.Default.DateCollect.Add(Temp, 0);
                                }
                            });
                            ClipboardControl.SetText(Temp);
                            LastS = Temp;
                        }

                        Thread.Sleep(100);
                    }
                }
            });
        }


        private void Start_Click(object sender, EventArgs e)
        {
            if (LoopStatus?.IsAlive != true)
            {
                LoopStatus.Start();
            }

            if (Start.Text == "启动")
            {
                if (AutoRun.Checked)
                {
                    if (ClipboardTask?.IsAlive != true)
                    {
                        CancelInfoMonitor = new CancellationTokenSource();
                        StartMonitor();
                    }

                }
                else
                {
                    if (ClipboardChange?.Status != TaskStatus.Running)
                    {
                        StartClipboardChange();
                    }
                }
            }
            else
            {

                if (ClipboardTask?.IsAlive == true)
                {
                    ClipboardTask.Abort();

                }

                else
                {
                    if (ClipboardChange?.Status == TaskStatus.Running)
                    {
                        CancelInfoClipboard.Cancel();
                    }
                }
            }
        }

        private void 剪切板监控_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LoopStatus.IsAlive != true)
            {
                LoopStatus.Start();
            }
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }
    }
}
