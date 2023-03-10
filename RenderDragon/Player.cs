using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderDragon
{
    internal class Player
    {
        double cameraDirX = 0;
        double cameraDirY = 0;
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
            cameraDirX = map(mX, 0, w, 0, 90);
            cameraDirY = map(mX, 0, h, 0, 90);
        }
        public double[] GetCameraDir()

            
        {
            return new double[]
            {
                cameraDirX, cameraDirY
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
            return false;
        }

        public double[] ProjectPoint(double x, double y, double z)
        {
            double deltaX = x - pos[0];
            double deltaZ = z - pos[2];
            double camX = DegToRad(cameraDirX);
            double theta = hFOV / 2;
            double alpha = vFOV / 2;
            double dist = Math.Sqrt((deltaX * deltaX) + (deltaZ * deltaZ));
            double rX = Math.Sin(camX) * dist;
            double rZ = Math.Cos(camX) * dist;
            double XZPositional = (Math.Sin(camX) * pos[0]) + (Math.Cos(camX) * pos[2]);
            double bXZPositional = (Math.Sin(camX) * x) + (Math.Cos(camX) * z);

            double actZee = rX + rZ;
            double xLen = -actZee / Math.Tan(theta);
            xLen *= 2;
            xLen += 1;
            double yLen = -(z + pos[2]) / Math.Tan(alpha);
            yLen *= 2;
            yLen += 1;

            double x2 = map(-bXZPositional, 0 - XZPositional, xLen - XZPositional, 0.50, 1.0);
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
