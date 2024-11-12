using System.IO;
using System.Runtime.InteropServices;
using CSnakes.Runtime;
using CSnakes.Runtime.Python;
using GitHub.secile.Video;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PoseCompare;

public partial class FormCompare : Form
{
    private VideoCapture _capture;
    private int _width;
    private int _height;
    private int _frameCount;
    private int _frameIndex;

    private double[] _landmarks = [];

    private bool _paused;
    private bool _isSecondary;
    private FormCompare _secondaryForm;
    private bool _isCamera;


    public FormCompare()
    {
        InitializeComponent();

        _pctFrame.SizeMode = PictureBoxSizeMode.Zoom;
    }


    public event EventHandler LandmarksChanged;

    public System.Drawing.Size GetImageSize() => new System.Drawing.Size(_width, _height);

    public IReadOnlyList<double> GetLandmarks()
    {
        return _landmarks;
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        _cmbCamera.DataSource = UsbCamera.FindDevices();
        Text = _isSecondary ? "2" : "1";
        if (!_isSecondary)
        {
            _secondaryForm = new FormCompare
            {
                _isSecondary = true
            };
            _secondaryForm.LandmarksChanged += OnSecondaryLandmarksChanged;
            _secondaryForm.Show();
        }
    }

    private void OnSecondaryLandmarksChanged(object? sender, EventArgs e)
    {
        ComputeDiff();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (_paused)
        {
            switch (keyData)
            {
                case Keys.Left:
                    {
                        if (_isCamera)
                            break;

                        _frameIndex -= 2;
                        if (_frameIndex < 0)
                            _frameIndex = 0;
                        _capture.Set(VideoCaptureProperties.PosFrames, _frameIndex);
                        NextFrame();
                        return true;
                    }
                case Keys.Right:
                    {
                        if (_isCamera)
                            break;

                        NextFrame();
                        return true;
                    }
                case Keys.Home:
                    {
                        if (_isCamera)
                            break;

                        _frameIndex = 0;
                        _capture.Set(VideoCaptureProperties.PosFrames, _frameIndex);
                        NextFrame();
                        return true;
                    }
                case Keys.End:
                    {
                        if (_isCamera)
                            break;

                        _frameIndex = _frameCount - 1;
                        _capture.Set(VideoCaptureProperties.PosFrames, _frameIndex);
                        NextFrame();
                        return true;
                    }
                case Keys.PageUp:
                    {
                        if (_isCamera)
                            break;

                        _frameIndex -= 101;
                        if (_frameIndex < 0)
                            _frameIndex = 0;
                        _capture.Set(VideoCaptureProperties.PosFrames, _frameIndex);
                        NextFrame();
                        return true;
                    }
                case Keys.PageDown:
                    {
                        if (_isCamera)
                            break;

                        _frameIndex += 99;
                        if (_frameIndex >= _frameCount)
                            _frameIndex = _frameCount - 1;
                        _capture.Set(VideoCaptureProperties.PosFrames, _frameIndex);
                        NextFrame();
                        return true;
                    }
                case Keys.Enter:
                    {
                        ComputeDiff();
                        return true;
                    }
            }
        }

        if (keyData == Keys.Space)
        {
            _paused = !_paused;
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }


    private void NextFrame()
    {
        if (_capture != null)
        {
            var mat = new Mat();
            if (_capture?.Read(mat) == true)
            {
                if (mat.Empty())
                    return;

                if (_chkMirror.Checked)
                    Cv2.Flip(mat, mat, FlipMode.Y);

                var buffer = new byte[mat.Width * mat.Height * mat.Channels()];
                Marshal.Copy(mat.Data, buffer, 0, buffer.Length);

                var landmarks = Program.Python.Infer()
                    .MediapipeBodyPose(mat.Width, mat.Height, mat.Channels(), PyObject.From(buffer))
                    .ToArray();

                var image = mat.ToBitmap();
                if (landmarks.Length > 0)
                    DrawLandmarks(landmarks, image);

                var label = $"{_frameIndex}[0,{_frameCount})";
                _frameIndex++;
                UpdateUI(image, label);

                _landmarks = landmarks;
                OnLandmarksChanged();
            }
        }
    }

    private void OnLandmarksChanged()
    {
        LandmarksChanged?.Invoke(this, EventArgs.Empty);
        ComputeDiff();
    }

    private void ComputeDiff()
    {
        if (_isSecondary)
            return;

        if (_landmarks.Length == 0 ||
            _secondaryForm._landmarks.Length == 0)
            return;

        var scores = Similarity.ComputeAll(_landmarks, _secondaryForm._landmarks);
        _lblScore.Text = scores["final"].ToString("f2");
        _lblDetails.Text = Similarity.GenerateDebugMessage(scores);
        _secondaryForm._lblScore.Text = _lblScore.Text;
        _secondaryForm._lblDetails.Text = _lblDetails.Text;
    }

    private void DrawLandmarks(IReadOnlyList<double> landmarks, Bitmap image)
    {
        using (var g = Graphics.FromImage(image))
        {
            DrawJoint(g, image, landmarks, 0);
            DrawJoint(g, image, landmarks, 15);
            DrawJoint(g, image, landmarks, 13);
            DrawJoint(g, image, landmarks, 11);
            DrawJoint(g, image, landmarks, 12);
            DrawJoint(g, image, landmarks, 14);
            DrawJoint(g, image, landmarks, 16);
            DrawJoint(g, image, landmarks, 24);
            DrawJoint(g, image, landmarks, 26);
            DrawJoint(g, image, landmarks, 28);
            DrawJoint(g, image, landmarks, 27);
            DrawJoint(g, image, landmarks, 25);
            DrawJoint(g, image, landmarks, 23);

            DrawConnection(g, image, landmarks, 11, 12);
            DrawConnection(g, image, landmarks, 12, 24);
            DrawConnection(g, image, landmarks, 24, 23);
            DrawConnection(g, image, landmarks, 23, 11);

            DrawConnection(g, image, landmarks, 11, 13);
            DrawConnection(g, image, landmarks, 13, 15);

            DrawConnection(g, image, landmarks, 12, 14);
            DrawConnection(g, image, landmarks, 14, 16);

            DrawConnection(g, image, landmarks, 23, 25);
            DrawConnection(g, image, landmarks, 25, 27);

            DrawConnection(g, image, landmarks, 24, 26);
            DrawConnection(g, image, landmarks, 26, 28);
        }
    }

    private void DrawJoint(Graphics g, Image image, IReadOnlyList<double> landmarks, int index)
    {
        var pen = index % 2 == 0 ? Pens.Blue : Pens.Red;
        g.DrawString(index.ToString("d"), Font, index % 2 == 0 ? Brushes.Blue : Brushes.Red, (float)(landmarks[index * 2] * image.Width - 2),
                (float)(landmarks[index * 2 + 1] * image.Height - 2));
        g.DrawRectangle(index % 2 == 0 ? Pens.Blue : Pens.Red,
            new RectangleF((float)(landmarks[index * 2] * image.Width - 2),
                (float)(landmarks[index * 2 + 1] * image.Height - 2),
                4,
                4));
        g.FillRectangle(index % 2 == 0 ? Brushes.Blue : Brushes.Red,
            new RectangleF((float)(landmarks[index * 2] * image.Width - 2),
                (float)(landmarks[index * 2 + 1] * image.Height - 2),
                4,
                4));
    }

    private void DrawConnection(Graphics g, Image image, IReadOnlyList<double> landmarks, int index1, int index2)
    {
        var odd1 = index1 % 2 == 1;
        var odd2 = index2 % 2 == 1;
        var pen = odd1 && odd2
            ? Pens.Red
            : !odd1 && !odd2
            ? Pens.Blue
            : Pens.Green;
        g.DrawLine(pen,
            (float)(landmarks[index1 * 2] * image.Width),
            (float)(landmarks[index1 * 2 + 1] * image.Height),
            (float)(landmarks[index2 * 2] * image.Width),
            (float)(landmarks[index2 * 2 + 1] * image.Height)
        );
    }

    private void UpdateUI(Image image, string label)
    {
        var oldImage = _pctFrame.Image;
        _pctFrame.Image = image;
        _lblFrameIndex.Text = label;
        oldImage?.Dispose();
    }

    private void _btnVideo_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog
        {
            Filter = "*.mp4|*.mp4",
        };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            _capture?.Release();
            _capture = new VideoCapture(dlg.FileName);

            _width = (int)_capture.Get(VideoCaptureProperties.FrameWidth);
            _height = (int)_capture.Get(VideoCaptureProperties.FrameHeight);
            _frameCount = (int)_capture.Get(VideoCaptureProperties.FrameCount);
            _frameIndex = 0;

            _isCamera = false;
            _paused = false;
        }
    }

    private void _btnCamera_Click(object sender, EventArgs e)
    {
        int cameraIndex = _cmbCamera.SelectedIndex;
        if (cameraIndex < 0)
            return;

        if (_capture != null)
            _capture.Release();

        _isCamera = true;
        _capture = new VideoCapture(cameraIndex);
        _capture.Set(VideoCaptureProperties.FrameWidth, 1080);
        _capture.Set(VideoCaptureProperties.FrameHeight, 1920);
        _width = (int)_capture.Get(VideoCaptureProperties.FrameWidth);
        _height = (int)_capture.Get(VideoCaptureProperties.FrameHeight);
        _frameCount = -1;
        _frameIndex = 0;

        _paused = false;
    }

    private void _cmbCamera_DropDown(object sender, EventArgs e)
    {
        _cmbCamera.DataSource = UsbCamera.FindDevices();
    }

    private void _timer_Tick(object sender, EventArgs e)
    {
        if (_paused || _capture == null)
            return;

        NextFrame();
    }
}