using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderDragon
{
    internal class Player
    {
        UInt16 cameraDirectionX = UInt16.MaxValue / 2;
        UInt16 cameraDirectionY = UInt16.MaxValue / 2;

        double[] pos = new double[3]; //x,y,z
        public Player(double x, double y, double z)
        {

        }
    }

}
