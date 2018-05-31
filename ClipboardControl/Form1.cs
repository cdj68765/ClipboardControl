using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ClipboardControl.Properties;

namespace ClipboardControl
{
    public partial class Form1 : Form
    {
        private readonly Thread LoopStatus;

        public static bool start;



        public Form1()
        {
            InitializeComponent();
            new Form2();
            if (Settings.Default.AutoRun)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                start = true;
            }
            var CloseCheck = new CancellationTokenSource();
            LoopStatus = new Thread(() =>
            {
                do
                {
                    Thread.Sleep(500);
                    if (start)
                    {
                        Invoke(new Action(() =>
                        {
                            Status.Text = @"运行中....";
                            Status.ForeColor = Color.Green;
                            Start.Text = @"关闭";
                        }));
                        var GetText = new StringBuilder(256);
                        NativeMethods.GetWindowTextW(NativeMethods.GetForegroundWindow(IntPtr.Zero),
                            GetText, 256);
                        if (GetText.ToString().StartsWith("CAXA"))
                            剪切板监控.Icon = Resource1.clipboard_80px_1121225_easyicon_net;
                        else
                            剪切板监控.Icon = Resource1.Clipboard_Plan_128px_1185105_easyicon_net;
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            Status.Text = @"关闭中....";
                            Status.ForeColor = Color.Red;
                            剪切板监控.Icon = Resource1.Clipboard_Plan_128px_1185105_easyicon_net;
                            Start.Text = @"启动";
                        }));
                    }
                } while (true);
            });
            FormClosing += delegate
            {
                Settings.Default.AutoRun = start;
                Settings.Default.Save();
                CloseCheck.Cancel();
                LoopStatus?.Abort();
            };

            if (LoopStatus?.IsAlive != true)
            {
                Thread.Sleep(2000);
                LoopStatus.Start();
            }

            Start.Click += delegate { start = Start.Text == @"启动"; };
            退出.Click += delegate { Close(); };
            Deactivate += delegate
            {
                if (WindowState == FormWindowState.Minimized) ShowInTaskbar = false;
            };
            剪切板监控.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right) 剪切板监控.ContextMenuStrip = contextMenuStrip1;
            };
            剪切板监控.MouseDoubleClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right) return;
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            };

        }
    }
}