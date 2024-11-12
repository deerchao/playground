using System.Data;

namespace PoseCompare
{
    public partial class FormSimple : Form
    {
        private double[] _landmarks1;
        private double[] _landmarks2;

        public FormSimple()
        {
            InitializeComponent();
        }

        private void _txt1_TextChanged(object sender, EventArgs e)
        {
            var pos = _txt1.Text.Split(',', ';')
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToList();
            if (pos.Count != 66 ||
                pos.Any(x => !double.TryParse(x, out _)))
            {
                return;
            }

            var positions = pos.Select(x => double.Parse(x)).ToList();
            var maxX = 0.0;
            var maxY = 0.0;
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            for (var i = 0; i < positions.Count; i++)
            {
                if (i % 2 == 0)
                {
                    maxX = Math.Max(maxX, positions[i]);
                    minX = Math.Min(minX, positions[i]);
                }
                else
                {
                    maxY = Math.Max(maxY, positions[i]);
                    minY = Math.Min(minY, positions[i]);
                }
            }

            if (maxX == 0.0 || maxY == 0.0)
                return;

            var w = maxX - minX;
            var h = maxY - minY;
            var padding = 10;
            var ratio = w < 400.0 ? 400.0 / w : 1;
            var width = (int)(w * ratio) + padding * 2;
            var height = (int)(h * ratio) + padding * 2;

            var image = new Bitmap(width, height);
            using var g = Graphics.FromImage(image);

            g.Clear(Color.White);
            DrawLandmarks(g, positions, ratio, padding, minX, maxX, minY);

            _pct1.Image = image;
            _landmarks1 = positions.ToArray();

            if (_landmarks2 != null)
            {
                var d = Similarity.ComputeAll(_landmarks1, _landmarks2);
                var tip = Similarity.GenerateDebugMessage(d);
                _txt1.Text = tip;
            }
        }

        private void DrawLandmarks(Graphics g, List<double> landmarks, double ratio, int padding, double minX, double maxX, double minY)
        {
            DrawConnection(g, ratio, landmarks, 11, 12, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 12, 24, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 24, 23, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 23, 11, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 11, 13, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 13, 15, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 12, 14, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 14, 16, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 23, 25, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 25, 27, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 24, 26, padding, minX, maxX, minY);
            DrawConnection(g, ratio, landmarks, 26, 28, padding, minX, maxX, minY);

            DrawJoint(g, ratio, landmarks, 0, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 15, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 13, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 11, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 12, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 14, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 16, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 24, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 26, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 28, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 27, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 25, padding, minX, maxX, minY);
            DrawJoint(g, ratio, landmarks, 23, padding, minX, maxX, minY);

        }

        private void DrawConnection(Graphics g, double ratio, List<double> landmarks, int index1, int index2, double padding, double minX, double maxX, double minY)
        {
            var x1 = (landmarks[index1 * 2] - minX) * ratio + padding;
            var y1 = (landmarks[index1 * 2 + 1] - minY) * ratio + padding;
            var x2 = (landmarks[index2 * 2] - minX) * ratio + padding;
            var y2 = (landmarks[index2 * 2 + 1] - minY) * ratio + padding;

            var width = 3;

            using var pen = index1 % 2 == 0 && index2 % 2 == 0
                ? new Pen(Color.Blue, width)
                : index1 % 2 == 1 && index2 % 2 == 1
                ? new Pen(Color.Red, width)
                : new Pen(Color.Black, width);

            g.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        private void DrawJoint(Graphics g, double ratio, List<double> landmarks, int index, double padding, double minX, double maxX, double minY)
        {
            var x = (landmarks[index * 2] - minX) * ratio + padding;
            var y = (landmarks[index * 2 + 1] - minY) * ratio + padding;
            var r = 3;
            var totalWidth = (maxX - minX) * ratio;

            var brush = index % 2 == 0 ? Brushes.Blue : Brushes.Red;
            g.FillEllipse(brush, (float)(x - r), (float)(y - r), r * 2, r * 2);
            using var font = new Font(Font.FontFamily.Name, (float)totalWidth / 30, GraphicsUnit.Pixel);

            g.DrawString(index.ToString("d"), font, brush, (float)(x + r), (float)(y + r));
        }

        private void _txt2_TextChanged(object sender, EventArgs e)
        {
            var pos = _txt2.Text.Split(',', ';')
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToList();
            if (pos.Count != 66 ||
                pos.Any(x => !double.TryParse(x, out _)))
            {
                return;
            }

            var positions = pos.Select(x => double.Parse(x)).ToList();
            var maxX = 0.0;
            var maxY = 0.0;
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            for (var i = 0; i < positions.Count; i++)
            {
                if (i % 2 == 0)
                {
                    maxX = Math.Max(maxX, positions[i]);
                    minX = Math.Min(minX, positions[i]);
                }
                else
                {
                    maxY = Math.Max(maxY, positions[i]);
                    minY = Math.Min(minY, positions[i]);
                }
            }

            if (maxX == 0.0 || maxY == 0.0)
                return;

            var w = maxX - minX;
            var h = maxY - minY;
            var padding = 10;
            var ratio = w < 400.0 ? 400.0 / w : 1;
            var width = (int)(w * ratio) + padding * 2;
            var height = (int)(h * ratio) + padding * 2;

            var image = new Bitmap(width, height);
            using var g = Graphics.FromImage(image);

            g.Clear(Color.White);
            DrawLandmarks(g, positions, ratio, padding, minX, maxX, minY);

            _pct2.Image = image;
            _landmarks2 = positions.ToArray();

            if (_landmarks1 != null)
            {
                var d = Similarity.ComputeAll(_landmarks1, _landmarks2);
                var tip = Similarity.GenerateDebugMessage(d);
                _txt2.Text = tip;
            }
        }
    }
}
