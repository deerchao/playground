using System.Drawing;
using System.Runtime.InteropServices;
using CSnakes.Runtime;
using CSnakes.Runtime.Python;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenCvSharp;
using OpenCvSharp.Extensions;

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        var home = Path.Join(Environment.CurrentDirectory, "."); // where your infer.py file located
        var envFolder = Path.Join(home, ".venv"); // where packages in requirements.txt are installed
        var dllFolder = @"C:\Users\<myusername>\AppData\Local\Programs\Python\Python312"; // where python3.dll located
        var version = "3.12.5"; // ensure with `python --version`

        services.WithPython()
            .FromFolder(dllFolder, version)
            .WithHome(home)
            .WithVirtualEnvironment(envFolder)
            .WithPipInstaller();
    });

var app = builder.Build();
var python = app.Services.GetRequiredService<IPythonEnvironment>();


var mat = Cv2.ImRead(@"Bill.Gates.jpg");

Console.Write(mat.Width);
var buffer = new byte[mat.Width * mat.Height * mat.Channels()];
Marshal.Copy(mat.Data, buffer, 0, buffer.Length);

var landmarks = python.Infer().MediapipeBodyPose(mat.Width, mat.Height, mat.Channels(), PyObject.From(buffer))
                    .ToArray();

DrawPose();

Cv2.ImWrite("output.jpg", mat);


void DrawPose()
{
    DrawJoint(0);
    DrawJoint(15);
    DrawJoint(13);
    DrawJoint(11);
    DrawJoint(12);
    DrawJoint(14);
    DrawJoint(16);
    DrawJoint(24);
    DrawJoint(26);
    DrawJoint(28);
    DrawJoint(27);
    DrawJoint(25);
    DrawJoint(23);

    DrawConnection(11, 12);
    DrawConnection(12, 24);
    DrawConnection(24, 23);
    DrawConnection(23, 11);

    DrawConnection(11, 13);
    DrawConnection(13, 15);

    DrawConnection(12, 14);
    DrawConnection(14, 16);

    DrawConnection(23, 25);
    DrawConnection(25, 27);

    DrawConnection(24, 26);
    DrawConnection(26, 28);
}

void DrawJoint(int joint)
{
    var x = (int)(landmarks[joint * 2] * mat.Width);
    var y = (int)(landmarks[joint * 2 + 1] * mat.Height);

    var color = joint % 2 == 0 ? Scalar.Red : Scalar.Green;
    Cv2.Circle(mat, x, y, 3, color, Cv2.FILLED);
}

void DrawConnection(int joint1, int joint2)
{
    var x1 = (int)(landmarks[joint1 * 2] * mat.Width);
    var y1 = (int)(landmarks[joint1 * 2 + 1] * mat.Height);
    var x2 = (int)(landmarks[joint2 * 2] * mat.Width);
    var y2 = (int)(landmarks[joint2 * 2 + 1] * mat.Height);

    Cv2.Line(mat, x1, y1, x2, y2, Scalar.Blue);
}
