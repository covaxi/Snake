namespace SnakeBattle
{
    partial class MainForm
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
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.lbUrl = new System.Windows.Forms.Label();
            this.bConnect = new System.Windows.Forms.Button();
            this.bResize = new System.Windows.Forms.Button();
            this.lvLog = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Location = new System.Drawing.Point(81, 12);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(448, 20);
            this.tbUrl.TabIndex = 0;
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Location = new System.Drawing.Point(12, 15);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(63, 13);
            this.lbUrl.TabIndex = 1;
            this.lbUrl.Text = "Server URL";
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(15, 38);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 3;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bResize
            // 
            this.bResize.Location = new System.Drawing.Point(96, 38);
            this.bResize.Name = "bResize";
            this.bResize.Size = new System.Drawing.Size(75, 23);
            this.bResize.TabIndex = 5;
            this.bResize.Text = "Resize";
            this.bResize.UseVisualStyleBackColor = true;
            this.bResize.Click += new System.EventHandler(this.bResize_Click);
            // 
            // lvLog
            // 
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(12, 67);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(517, 248);
            this.lvLog.TabIndex = 6;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 327);
            this.Controls.Add(this.lvLog);
            this.Controls.Add(this.bResize);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.lbUrl);
            this.Controls.Add(this.tbUrl);
            this.Name = "MainForm";
            this.Text = "Snake battle";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bResize;
        private System.Windows.Forms.ListView lvLog;
    }
}

