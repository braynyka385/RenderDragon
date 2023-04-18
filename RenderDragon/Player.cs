using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderDragon
{
    internal class Player
    {
        double headingX = 0;
        double headingY = 0;
        byte hFOV;
        byte vFOV = 90;


        double[] pos = new double[3]; //x,y,z
        public Player(double x, double y, double z, byte FOV)
        {
            hFOV = FOV;
            pos[0] = x;
            pos[1] = y;
            pos[2] = z;
        }

        public void Move(double x, double y, double z)
        {
            pos[0] += x;
            pos[1] += y;
            pos[2] += z;
        }
        public void MoveCamera(double mX, double mY, int w, int h)
        {
            headingX = map(mX, 0, w, 0, 90);
            headingY = map(mX, 0, h, 0, 90);
        }
        public double[] GetHeading()
        {
            return new double[] { headingX, headingY };
        }

        public double[] GetPos()
        {
            return pos;
        }
        public bool isPointInView(double x, double y, double z)
        {
            double theta = hFOV / 2;
            double alpha = vFOV / 2;
            double xLen = 2 * Math.Tan(theta) / z;
            xLen += 10;
            double yLen = 2 * Math.Tan(alpha) / z;
            yLen += 1;

            double deltaX = pos[0] - x;
            double deltaY = pos[1] - y;
            if (z > pos[2] && deltaX < xLen && deltaY < yLen)
            {
                return true;
            }
            //return false;
            return true;
        }
        public double[] ProjectPoint(double x, double y, double z)
        {
           // headingX = 0;
            double cosHX = Math.Cos(DegToRad(headingX));
            double sinHX = Math.Sin(DegToRad(headingX));
            double theta = hFOV / 2;
            double alpha = vFOV / 2;
            double xLen = -(cosHX*(z + pos[2]) + sinHX * (x + pos[0])) / Math.Tan(theta); //Use dist formula on the denominator of this--maybe?
            xLen *= 2;
            xLen += 1;
            double yLen = -(z + pos[2]) / Math.Tan(alpha);
            yLen *= 2;
            yLen += 1;

            double xZDist = Math.Sqrt((cosHX * x) * (cosHX * x) + (sinHX * z) * (sinHX * z));
            double xZPOffDist = Math.Sqrt((cosHX * pos[0])* (cosHX * pos[0]) + (sinHX * pos[2])* (sinHX * pos[2]));

            double x2 = map(xZDist, 0 - xZPOffDist, xLen - xZPOffDist, 0.50, 1.0);
            double y2 = map(y, 0 - pos[1], yLen - pos[1], 0.50, 1.0);

            return new double[] { x2, y2 };
        }

        //public double[] ProjectPoint(double x, double y, double z)
        //{
        //    double deltaX = x - pos[0];
        //    double deltaZ = z - pos[2];
        //    double theta = hFOV / 2;
        //    double alpha = vFOV / 2;
        //    double xLen = -(z + pos[2]) / Math.Tan(theta);
        //    xLen *= 2;
        //    xLen += 1;
        //    double yLen = -(z + pos[2]) / Math.Tan(alpha);
        //    yLen *= 2;
        //    yLen += 1;

        //    double x2 = map(x, 0 - pos[0], xLen - pos[0], 0.50, 1.0);
        //    double y2 = map(y, 0 - pos[1], yLen - pos[1], 0.50, 1.0);

        //    return new double[] { x2, y2 };
        //}
        private double DegToRad(double deg)
        {
            return deg * 0.01745329;
        }
        private double DistanceTo(double x, double y, double z)
        {
            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }


        private double map(double s, double a1, double a2, double b1, double b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }
    }

}
