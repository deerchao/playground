using System.Runtime.InteropServices;
using CSnakes.Runtime;
using CSnakes.Runtime.Python;
using GitHub.secile.Video;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using static Plot3D.Editor3D;

namespace PoseCompare;

public partial class Form3D : Form
{
    private VideoCapture _capture;
    private int _width;
    private int _height;
    private int _frameCount;
    private int _frameIndex;

    private double[] _landmarks = [];

    private bool _paused;
    private bool _isCamera;
    private readonly List<cObject3D> _objects = new();


    public Form3D()
    {
        InitializeComponent();

        SetupEditor();
    }

    private void SetupEditor()
    {
        _editor.Raster = eRaster.Labels;
        _editor.Normalize = eNormalize.MaintainXYZ;
        _editor.LegendPos = eLegendPos.AxisEnd;

        _editor.AxisX.LegendText = "X";
        _editor.AxisY.LegendText = "Y";
        _editor.AxisZ.LegendText = "Z";
        _editor.TopLegendColor = Color.Empty;
        _editor.UndoBuffer.Enabled = false;
        _editor.Selection.Enabled = false;

        var i_Inputs = new[]
        {
            new cUserInput(MouseButtons.Middle, Keys.None, eMouseAction.ThetaAndPhi),
            new cUserInput(MouseButtons.Left, Keys.Control, eMouseAction.Rho),
            new cUserInput(MouseButtons.Left, Keys.Shift, eMouseAction.Move),
        };

        _editor.SetUserInputs(i_Inputs);
        _editor.AddMessageData(new cMessgData("Shift 拖动平移, 点击选择，中键旋转，滚轮缩放", -10, 10, Color.Gray));
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

                var buffer = new byte[mat.Width * mat.Height * mat.Channels()];
                Marshal.Copy(mat.Data, buffer, 0, buffer.Length);
                if (_chkMirror.Checked)
                    Cv2.Flip(mat, mat, FlipMode.Y);

                var landmarks = Program.Python.Infer()
                    .MediapipeBodyPose3d(mat.Width, mat.Height, mat.Channels(), PyObject.From(buffer))
                    .ToArray();

                _pctFrame.Image = mat.ToBitmap();

                //for (var i = 0; i < landmarks.Length; i += 3)
                //{
                //    landmarks[i] *= mat.Width;
                //    landmarks[i + 1] *= mat.Height;
                //    landmarks[i + 2] *= mat.Width;
                //}
                DrawLandmarks(landmarks);

                _lblFrameIndex.Text = _frameIndex.ToString("d");
                _frameIndex++;
                _landmarks = landmarks;
            }
        }
    }

    private void DrawLandmarks(double[] landmarks)
    {
        ResetEditor();

        DrawJoint(landmarks, 0);
        DrawJoint(landmarks, 15);
        DrawJoint(landmarks, 13);
        DrawJoint(landmarks, 11);
        DrawJoint(landmarks, 12);
        DrawJoint(landmarks, 14);
        DrawJoint(landmarks, 16);
        DrawJoint(landmarks, 24);
        DrawJoint(landmarks, 26);
        DrawJoint(landmarks, 28);
        DrawJoint(landmarks, 27);
        DrawJoint(landmarks, 25);
        DrawJoint(landmarks, 23);

        DrawConnection(landmarks, 11, 12);
        DrawConnection(landmarks, 12, 24);
        DrawConnection(landmarks, 24, 23);
        DrawConnection(landmarks, 23, 11);

        DrawConnection(landmarks, 11, 13);
        DrawConnection(landmarks, 13, 15);

        DrawConnection(landmarks, 12, 14);
        DrawConnection(landmarks, 14, 16);

        DrawConnection(landmarks, 23, 25);
        DrawConnection(landmarks, 25, 27);

        DrawConnection(landmarks, 24, 26);
        DrawConnection(landmarks, 26, 28);

        _editor.Invalidate();
    }

    private void ResetEditor()
    {
        _editor.RemoveObjects(_objects.ToArray());
        _objects.Clear();
    }

    private void DrawConnection(double[] landmarks, int joint1, int joint2)
    {
        var x1 = landmarks[joint1 * 3];
        var y1 = landmarks[joint1 * 3 + 1];
        var z1 = landmarks[joint1 * 3 + 2];

        var x2 = landmarks[joint2 * 3];
        var y2 = landmarks[joint2 * 3 + 1];
        var z2 = landmarks[joint2 * 3 + 2];
        var thickness = 3;

        var data = new cLineData(null);
        var color = Color.Green;
        data.AddSolidLine(new cPoint3D(x1, y1, z1), new cPoint3D(x2, y2, z2), thickness, new Pen(color));
        _editor.AddRenderData(data);
        _objects.AddRange(data.AllLines);
    }
    private void DrawJoint(double[] landmarks, int joint)
    {
        var x = landmarks[joint * 3];
        var y = landmarks[joint * 3 + 1];
        var z = landmarks[joint * 3 + 2];

        var thickness = 3;
        var color = Color.Red;

        var points = new cScatterData(null);
        points.AddShape(new cPoint3D(x, y, z), eScatterShape.Triangle, thickness, new SolidBrush(color));
        _editor.AddRenderData(points);
        _objects.AddRange(points.AllShapes);
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