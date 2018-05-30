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
            if (Properties.Settings.Default.AutoRun)
            {
                AutoRun.CheckState = CheckState.Checked;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                剪切板监控.Icon = Resource1.clipboard_80px_1121225_easyicon_net;
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
                               剪切板监控.Icon = Resource1.clipboard_80px_1121225_easyicon_net;
                           }));
                       }
                       else if (ClipboardTask?.IsAlive != true && ClipboardChange?.Status != TaskStatus.Running)
                       {
                           Invoke(new Action(() =>
                           {
                               Status.Text = @"关闭中....";
                               Status.ForeColor = Color.Red;
                               CancelInfoClipboard.Cancel();
                               Start.Text = @"启动";
                               剪切板监控.Icon = Resource1.Clipboard_Plan_128px_1185105_easyicon_net;
                           }));
                       }
                   }
                   else
                   {
                       if (ClipboardChange?.Status == TaskStatus.Running)
                       {
                           Invoke(new Action(() =>
                           {
                               剪切板监控.Icon = Resource1.clipboard_80px_1121225_easyicon_net;
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
                               剪切板监控.Icon = Resource1.Clipboard_Plan_128px_1185105_easyicon_net;
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
                LoopStatus?.Abort();
                ClipboardTask?.Abort();
            };
        }

        private Thread ClipboardTask;

        private void StartMonitor()
        {
            ClipboardTask = new Thread(() =>
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
                     if (!ret && ClipboardChange?.Status != TaskStatus.Running)
                     {
                         StartClipboardChange();
                     }
                     else if (!ret && ClipboardChange?.Status == TaskStatus.Running)
                     {
                         Thread.Sleep(5000);
                         continue;
                     }
                     else
                     {
                         if (ClipboardChange?.Status == TaskStatus.Running)
                         {
                             CancelInfoClipboard.Cancel();
                         }
                         if (!CancelInfoMonitor.IsCancellationRequested)
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

            ClipboardChange = Task.Factory.StartNew(() =>
           {
               {
                   var LastS = string.Empty;
                   while (!CancelInfoClipboard.IsCancellationRequested)
                   {
                       var Temp = ClipboardControl.GetText(ClipboardFormat.CF_UNICODETEXT);
                       if (!string.IsNullOrEmpty(Temp) && Temp != LastS)
                       {
                           ClipboardControl.SetText(Temp);
                           LastS = Temp;
                       }

                       Thread.Sleep(50);
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
            if (e.Button == MouseButtons.Right) return;
            if (LoopStatus.IsAlive != true)
            {
                LoopStatus.Start();
            }
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void 退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 剪切板监控_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(e.X, e.Y);
            }
        }
    }
}