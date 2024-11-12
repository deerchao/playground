using System.Text;

namespace PoseCompare
{

    class Similarity
    {
        public static double Compute(double[] landmarks1, double[] landmarks2)
        {
            return ComputeAll(landmarks1, landmarks2)["final"];
        }

        public static Dictionary<string, double> ComputeAll(double[] landmarks1, double[] landmarks2)
        {
            var dict = new Dictionary<string, double>
            {
                { "0:1", Angle(landmarks1, 11, 0, 12) },
                { "13:1", Angle(landmarks1, 15, 13, 11) },
                { "11:1", Angle(landmarks1, 13, 11, 23) },
                { "23:1", Angle(landmarks1, 11, 23, 25) },
                { "25:1", Angle(landmarks1, 23, 25, 27) },
                { "14:1", Angle(landmarks1, 16, 14, 12) },
                { "12:1", Angle(landmarks1, 14, 12, 24) },
                { "24:1", Angle(landmarks1, 12, 24, 26) },
                { "26:1", Angle(landmarks1, 24, 26, 28) },

                { "0:2", Angle(landmarks2, 11, 0, 12) },
                { "13:2", Angle(landmarks2, 15, 13, 11) },
                { "11:2", Angle(landmarks2, 13, 11, 23) },
                { "23:2", Angle(landmarks2, 11, 23, 25) },
                { "25:2", Angle(landmarks2, 23, 25, 27) },
                { "14:2", Angle(landmarks2, 16, 14, 12) },
                { "12:2", Angle(landmarks2, 14, 12, 24) },
                { "24:2", Angle(landmarks2, 12, 24, 26) },
                { "26:2", Angle(landmarks2, 24, 26, 28) },
            };

            var diffs = new List<double>();
            foreach (var key in new[] { "0", "11", "12", "13", "14", "23", "24", "25", "26" })
            {
                var diff = Diff(dict, key);
                diffs.Add(diff);
                dict.Add(key, diff);
            }
            var weights = new[]
            {
                0.08,
                0.18,
                0.18,
                0.15,
                0.15,
                0.06,
                0.06,
                0.07,
                0.07,
            };
            var finalScore = 1.0 - diffs.Zip(weights, (d, w) => d * w).Sum();
            dict.Add("final", finalScore);
            return dict;
        }

        public static string GenerateDebugMessage(Dictionary<string, double> scores)
        {
            var sb = new StringBuilder();
            sb.Append($"final score: {scores["final"]:f2}\r\n");
            sb.Append($"0: {scores["0"]:f2} = {scores["0:1"]:f2}, {scores["0:2"]:f2}\r\n");
            sb.Append($"11: {scores["11"]:f2} = {scores["11:1"]:f2}, {scores["11:2"]:f2}\r\n");
            sb.Append($"12: {scores["12"]:f2} = {scores["12:1"]:f2}, {scores["12:2"]:f2}\r\n");
            sb.Append($"13: {scores["13"]:f2} = {scores["13:1"]:f2}, {scores["13:2"]:f2}\r\n");
            sb.Append($"14: {scores["14"]:f2} = {scores["14:1"]:f2}, {scores["14:2"]:f2}\r\n");
            sb.Append($"23: {scores["23"]:f2} = {scores["23:1"]:f2}, {scores["23:2"]:f2}\r\n");
            sb.Append($"24: {scores["24"]:f2} = {scores["24:1"]:f2}, {scores["24:2"]:f2}\r\n");
            sb.Append($"25: {scores["25"]:f2} = {scores["25:1"]:f2}, {scores["25:2"]:f2}\r\n");
            sb.Append($"26: {scores["26"]:f2} = {scores["26:1"]:f2}, {scores["26:2"]:f2}\r\n");
            return sb.ToString();
        }


        public static double[] ApplyMirror(double[] landmarks)
        {
            var result = new double[]
                {
                    landmarks[0*2],
                    landmarks[0*2 + 1],

                    landmarks[4*2],
                    landmarks[4*2+1],

                    landmarks[5*2],
                    landmarks[5*2+1],

                    landmarks[6*2],
                    landmarks[6*2+1],


                    landmarks[1*2],
                    landmarks[1*2+1],

                    landmarks[2*2],
                    landmarks[2*2+1],

                    landmarks[3*2],
                    landmarks[3*2+1],

                    landmarks[8*2],
                    landmarks[8*2+1],

                    landmarks[7*2],
                    landmarks[7*2+1],

                    landmarks[10*2],
                    landmarks[10*2+1],

                    landmarks[9*2],
                    landmarks[9*2+1],

                    landmarks[12*2],
                    landmarks[12*2+1],

                    landmarks[11*2],
                    landmarks[11*2+1],

                    landmarks[14*2],
                    landmarks[14*2+1],

                    landmarks[13*2],
                    landmarks[13*2+1],

                    landmarks[16*2],
                    landmarks[16*2+1],

                    landmarks[15*2],
                    landmarks[15*2+1],

                    landmarks[18*2],
                    landmarks[18*2+1],

                    landmarks[17*2],
                    landmarks[17*2+1],

                    landmarks[20*2],
                    landmarks[20*2+1],

                    landmarks[19*2],
                    landmarks[19*2+1],

                    landmarks[22*2],
                    landmarks[22*2+1],

                    landmarks[21*2],
                    landmarks[21*2+1],

                    landmarks[24*2],
                    landmarks[24*2+1],

                    landmarks[23*2],
                    landmarks[23*2+1],

                    landmarks[26*2],
                    landmarks[26*2+1],

                    landmarks[25*2],
                    landmarks[25*2+1],

                    landmarks[28*2],
                    landmarks[28*2+1],

                    landmarks[27*2],
                    landmarks[27*2+1],

                    landmarks[30*2],
                    landmarks[30*2+1],

                    landmarks[29*2],
                    landmarks[29*2+1],

                    landmarks[32*2],
                    landmarks[32*2+1],

                    landmarks[31*2],
                    landmarks[31*2+1],
                };

            return result;
        }

        private static double Diff(Dictionary<string, double> scores, string index)
        {
            var a1 = scores[$"{index}:1"];
            var a2 = scores[$"{index}:2"];

            var d = Math.Abs(a1 - a2) / 360;
            return d;
        }

        private static double X(double[] landmarks, int joint)
        {
            return landmarks[joint * 2];
        }
        private static double Y(double[] landmarks, int joint)
        {
            return landmarks[joint * 2 + 1];
        }

        private static double Angle(double[] landmarks, int jointA, int jointB, int jointC)
        {
            return Angle(X(landmarks, jointA), Y(landmarks, jointA),
                X(landmarks, jointB), Y(landmarks, jointB),
                X(landmarks, jointC), Y(landmarks, jointC));
        }

        private static double Angle(double xa, double ya, double xb, double yb, double xc, double yc)
        {
            var cbx = Math.Atan2(yc - yb, xc - xb);

            var abx = Math.Atan2(ya - yb, xa - xb);

            var angle = cbx - abx;
            if (angle < 0)
                angle = angle + Math.PI * 2;
            return angle * 180 / Math.PI;
        }
    }
}
