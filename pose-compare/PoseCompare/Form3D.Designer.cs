namespace PoseCompare;

partial class Form3D
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        panel1 = new Panel();
        _lblFrameIndex = new Label();
        _chkMirror = new CheckBox();
        _cmbCamera = new ComboBox();
        _btnCamera = new Button();
        _btnVideo = new Button();
        _timer = new System.Windows.Forms.Timer(components);
        _editor = new Plot3D.Editor3D();
        splitContainer1 = new SplitContainer();
        _pctFrame = new PictureBox();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pctFrame).BeginInit();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(_lblFrameIndex);
        panel1.Controls.Add(_chkMirror);
        panel1.Controls.Add(_cmbCamera);
        panel1.Controls.Add(_btnCamera);
        panel1.Controls.Add(_btnVideo);
        panel1.Dock = DockStyle.Left;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(200, 450);
        panel1.TabIndex = 0;
        // 
        // _lblFrameIndex
        // 
        _lblFrameIndex.AutoSize = true;
        _lblFrameIndex.Location = new Point(13, 101);
        _lblFrameIndex.Name = "_lblFrameIndex";
        _lblFrameIndex.Size = new Size(13, 15);
        _lblFrameIndex.TabIndex = 6;
        _lblFrameIndex.Text = "0";
        // 
        // _chkMirror
        // 
        _chkMirror.AutoSize = true;
        _chkMirror.Location = new Point(13, 70);
        _chkMirror.Name = "_chkMirror";
        _chkMirror.Size = new Size(52, 19);
        _chkMirror.TabIndex = 5;
        _chkMirror.Text = "镜像";
        _chkMirror.UseVisualStyleBackColor = true;
        // 
        // _cmbCamera
        // 
        _cmbCamera.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbCamera.FormattingEnabled = true;
        _cmbCamera.Location = new Point(99, 12);
        _cmbCamera.Name = "_cmbCamera";
        _cmbCamera.Size = new Size(95, 23);
        _cmbCamera.TabIndex = 4;
        _cmbCamera.DropDown += _cmbCamera_DropDown;
        // 
        // _btnCamera
        // 
        _btnCamera.Location = new Point(12, 12);
        _btnCamera.Name = "_btnCamera";
        _btnCamera.Size = new Size(84, 23);
        _btnCamera.TabIndex = 0;
        _btnCamera.Text = "打开摄像头";
        _btnCamera.UseVisualStyleBackColor = true;
        _btnCamera.Click += _btnCamera_Click;
        // 
        // _btnVideo
        // 
        _btnVideo.Location = new Point(12, 41);
        _btnVideo.Name = "_btnVideo";
        _btnVideo.Size = new Size(75, 23);
        _btnVideo.TabIndex = 0;
        _btnVideo.Text = "打开视频";
        _btnVideo.UseVisualStyleBackColor = true;
        _btnVideo.Click += _btnVideo_Click;
        // 
        // _timer
        // 
        _timer.Enabled = true;
        _timer.Interval = 10;
        _timer.Tick += _timer_Tick;
        // 
        // _editor
        // 
        _editor.BackColor = Color.White;
        _editor.BorderColorFocus = Color.FromArgb(51, 153, 255);
        _editor.BorderColorNormal = Color.FromArgb(180, 180, 180);
        _editor.Dock = DockStyle.Fill;
        _editor.LegendPos = Plot3D.Editor3D.eLegendPos.BottomLeft;
        _editor.Location = new Point(0, 0);
        _editor.Name = "_editor";
        _editor.Normalize = Plot3D.Editor3D.eNormalize.Separate;
        _editor.Raster = Plot3D.Editor3D.eRaster.Labels;
        _editor.Size = new Size(296, 450);
        _editor.TabIndex = 1;
        _editor.TooltipMode = Plot3D.Editor3D.eTooltip.UserText | Plot3D.Editor3D.eTooltip.Coord;
        _editor.TopLegendColor = Color.FromArgb(200, 200, 150);
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(200, 0);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(_pctFrame);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(_editor);
        splitContainer1.Size = new Size(600, 450);
        splitContainer1.SplitterDistance = 300;
        splitContainer1.TabIndex = 2;
        // 
        // _pctFrame
        // 
        _pctFrame.Dock = DockStyle.Fill;
        _pctFrame.Location = new Point(0, 0);
        _pctFrame.Name = "_pctFrame";
        _pctFrame.Size = new Size(300, 450);
        _pctFrame.SizeMode = PictureBoxSizeMode.Zoom;
        _pctFrame.TabIndex = 0;
        _pctFrame.TabStop = false;
        // 
        // Form3D
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(splitContainer1);
        Controls.Add(panel1);
        Name = "Form3D";
        Text = "Form1";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_pctFrame).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private Button _btnCamera;
    private Button _btnVideo;
    private ComboBox _cmbCamera;
    private System.Windows.Forms.Timer _timer;
    private CheckBox _chkMirror;
    private Plot3D.Editor3D _editor;
    private Label _lblFrameIndex;
    private SplitContainer splitContainer1;
    private PictureBox _pctFrame;
}