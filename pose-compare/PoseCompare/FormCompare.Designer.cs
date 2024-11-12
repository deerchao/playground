namespace PoseCompare;

partial class FormCompare
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
        _cmbCamera = new ComboBox();
        _lblDetails = new Label();
        _lblScore = new Label();
        _lblFrameIndex = new Label();
        _btnCamera = new Button();
        _btnVideo = new Button();
        _pctFrame = new PictureBox();
        _timer = new System.Windows.Forms.Timer(components);
        _chkMirror = new CheckBox();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_pctFrame).BeginInit();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(_chkMirror);
        panel1.Controls.Add(_cmbCamera);
        panel1.Controls.Add(_lblDetails);
        panel1.Controls.Add(_lblScore);
        panel1.Controls.Add(_lblFrameIndex);
        panel1.Controls.Add(_btnCamera);
        panel1.Controls.Add(_btnVideo);
        panel1.Dock = DockStyle.Left;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(200, 450);
        panel1.TabIndex = 0;
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
        // _lblDetails
        // 
        _lblDetails.AutoSize = true;
        _lblDetails.Location = new Point(24, 235);
        _lblDetails.Name = "_lblDetails";
        _lblDetails.Size = new Size(82, 30);
        _lblDetails.TabIndex = 3;
        _lblDetails.Text = "0:0.01 92,93\r\n11: 0.02 94,108";
        // 
        // _lblScore
        // 
        _lblScore.AutoSize = true;
        _lblScore.Font = new Font("Segoe UI", 36F);
        _lblScore.Location = new Point(12, 142);
        _lblScore.Name = "_lblScore";
        _lblScore.Size = new Size(116, 65);
        _lblScore.TabIndex = 2;
        _lblScore.Text = "0.98";
        // 
        // _lblFrameIndex
        // 
        _lblFrameIndex.AutoSize = true;
        _lblFrameIndex.Location = new Point(24, 207);
        _lblFrameIndex.Name = "_lblFrameIndex";
        _lblFrameIndex.Size = new Size(48, 15);
        _lblFrameIndex.TabIndex = 1;
        _lblFrameIndex.Text = "0[0,100)";
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
        // _pctFrame
        // 
        _pctFrame.Dock = DockStyle.Fill;
        _pctFrame.Location = new Point(200, 0);
        _pctFrame.Name = "_pctFrame";
        _pctFrame.Size = new Size(600, 450);
        _pctFrame.TabIndex = 1;
        _pctFrame.TabStop = false;
        // 
        // _timer
        // 
        _timer.Enabled = true;
        _timer.Interval = 10;
        _timer.Tick += _timer_Tick;
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
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(_pctFrame);
        Controls.Add(panel1);
        Name = "Form1";
        Text = "Form1";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_pctFrame).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private PictureBox _pctFrame;
    private Button _btnCamera;
    private Button _btnVideo;
    private Label _lblScore;
    private Label _lblFrameIndex;
    private Label _lblDetails;
    private ComboBox _cmbCamera;
    private System.Windows.Forms.Timer _timer;
    private CheckBox _chkMirror;
}