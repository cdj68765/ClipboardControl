﻿namespace ClipboardControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.剪切板监控 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出 = new System.Windows.Forms.ToolStripMenuItem();
            this.Start = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 剪切板监控
            // 
            this.剪切板监控.Icon = ((System.Drawing.Icon)(resources.GetObject("剪切板监控.Icon")));
            this.剪切板监控.Text = "剪切板监控";
            this.剪切板监控.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(76, 26);
            // 
            // 退出
            // 
            this.退出.Name = "退出";
            this.退出.Size = new System.Drawing.Size(75, 22);
            this.退出.Text = "退出";
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.Location = new System.Drawing.Point(12, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(99, 62);
            this.Start.TabIndex = 0;
            this.Start.Text = "启动";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Status.ForeColor = System.Drawing.Color.Red;
            this.Status.Location = new System.Drawing.Point(117, 12);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(69, 20);
            this.Status.TabIndex = 1;
            this.Status.Text = "未启动";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 86);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Start);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 125);
            this.MinimumSize = new System.Drawing.Size(300, 125);
            this.Name = "Form1";
            this.Text = "Caxa剪切板监控";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon 剪切板监控;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出;
    }
}