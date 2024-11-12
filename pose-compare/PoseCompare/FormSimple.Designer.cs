namespace PoseCompare
{
    partial class FormSimple
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
            splitContainer1 = new SplitContainer();
            _pct1 = new PictureBox();
            _txt1 = new TextBox();
            _pct2 = new PictureBox();
            _txt2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_pct1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_pct2).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_pct1);
            splitContainer1.Panel1.Controls.Add(_txt1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_pct2);
            splitContainer1.Panel2.Controls.Add(_txt2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 400;
            splitContainer1.TabIndex = 0;
            // 
            // _pct1
            // 
            _pct1.Dock = DockStyle.Fill;
            _pct1.Location = new Point(0, 150);
            _pct1.Name = "_pct1";
            _pct1.Size = new Size(400, 300);
            _pct1.SizeMode = PictureBoxSizeMode.Zoom;
            _pct1.TabIndex = 0;
            _pct1.TabStop = false;
            // 
            // _txt1
            // 
            _txt1.Dock = DockStyle.Top;
            _txt1.Location = new Point(0, 0);
            _txt1.Multiline = true;
            _txt1.Name = "_txt1";
            _txt1.Size = new Size(400, 150);
            _txt1.TabIndex = 1;
            _txt1.TextChanged += _txt1_TextChanged;
            // 
            // _pct2
            // 
            _pct2.Dock = DockStyle.Fill;
            _pct2.Location = new Point(0, 150);
            _pct2.Name = "_pct2";
            _pct2.Size = new Size(396, 300);
            _pct2.SizeMode = PictureBoxSizeMode.Zoom;
            _pct2.TabIndex = 2;
            _pct2.TabStop = false;
            // 
            // _txt2
            // 
            _txt2.Dock = DockStyle.Top;
            _txt2.Location = new Point(0, 0);
            _txt2.Multiline = true;
            _txt2.Name = "_txt2";
            _txt2.Size = new Size(396, 150);
            _txt2.TabIndex = 3;
            _txt2.TextChanged += _txt2_TextChanged;
            // 
            // FormSimple
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "FormSimple";
            Text = "FormSimple";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_pct1).EndInit();
            ((System.ComponentModel.ISupportInitialize)_pct2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox _pct1;
        private TextBox _txt1;
        private PictureBox _pct2;
        private TextBox _txt2;
    }
}