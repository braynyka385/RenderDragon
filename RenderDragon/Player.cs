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
            headingX = map(mX, 0, w, 0, 360);
            headingY = map(mX, 0, h, 0, 360);
        }
        public double[] GetHeading()

            
        {
            return new double[]
            {
                headingX, headingY
            };
        }

        public double[] GetPos()
        {
            return pos;
        }
        public bool isPointInView(double x, double y, double z)
        {
            double theta = hFOV/2;
            double alpha = vFOV/2;
            double xLen = 2*Math.Tan(theta)/z;
            xLen += 10;
            double yLen = 2*Math.Tan(alpha)/z;
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
            double theta = hFOV / 2;
            double alpha = vFOV / 2;

            double headingFrom90 = headingX;

            while(headingFrom90 > 90)
            {
                headingFrom90-=90;
            }

            double xOff = Math.Abs(pos[0] - x);
            double zOff = Math.Abs(pos[2] - z);

            double similarHyp = 1;
            double similarZ = Math.Cos(DegToRad(headingFrom90));
            double similarX = Math.Sin(DegToRad(headingFrom90));



            double xLen = Math.Abs((z + pos[2])*similarZ - (x + pos[0])*similarX) / Math.Tan(theta);
            xLen *= 2;
            xLen += 1;
            double yLen = -(z + pos[2]) / Math.Tan(alpha);
            yLen *= 2;
            yLen += 1;


            double x2 = map((x*similarZ) + (z*similarX), 0 - (pos[0]*similarZ + pos[2]*similarX), xLen - (pos[0]*similarZ + pos[2]*similarX), 0.5, 1.0);
            double y2 = map(y, 0 - pos[1], yLen - pos[1], 0.50, 1.0);

            return new double[] { x2, y2 };
        }
        /*public double[] ProjectPoint(double x, double y, double z)
        {
            double theta = hFOV / 2;
            double alpha = vFOV / 2;
            double xProp = Math.Cos(cameraDirX);
            double zProp = Math.Cos(cameraDirX);
            double zPort = zProp * (z + pos[2]);
            double xLen = -(z + pos[2]) / Math.Tan(theta);
            xLen *= 2;
            xLen += 1;
            double yLen = -(z + pos[2]) / Math.Tan(alpha);
            yLen *= 2;
            yLen += 1;

            double x2 = map(x, 0 - pos[0], xLen - pos[0], 0.50, 1.0);
            double y2 = map(y, 0 - pos[1], yLen - pos[1], 0.50, 1.0);

            return new double[] { x2, y2 };
        }*/
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
