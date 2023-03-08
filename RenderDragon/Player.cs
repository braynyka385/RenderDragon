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

        public double[] GetPos()
        {
            return pos;
        }
        public bool isPointInView(double x, double y, double z)
        {
            double theta = hFOV/2;
            double alpha = vFOV/2;
            double xLen = 2*Math.Tan(theta)/z;
            xLen += 1;
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

        public double[] ProjectPoint(int x, int y, int z)
        {
            double theta = hFOV / 2;
            double alpha = vFOV / 2;
            double xLen = 1 * Math.Tan(theta) / (pos[2]+z);
            xLen += 1;
            double yLen = 1 * Math.Tan(alpha) / (pos[2]+z);
            yLen += 1;

            double x2 = map(x, 0 - pos[0], xLen - pos[0], 0, 1.0);
            double y2 = map(y, 0 - pos[1], yLen - pos[1], 0, 1.0);

            return new double[] { x2, y2 };
        }

        private double map(double s, double a1, double a2, double b1, double b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }
    }

}
