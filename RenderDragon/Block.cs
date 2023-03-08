using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderDragon
{
    class Block
    {
        private int[] pos = new int[3];
        
        public Block(int x, int y, int z)
        {
            pos[0] = x;
            pos[1] = y; 
            pos[2] = z;
            
        }

        public int[,] GetPoints()
        {
            int[,] points = new int[8,3];
            int x = pos[0];
            int y = pos[1];
            int z = pos[2];
            int[] p =
            {
                x, y, z
            };
            points = InsertSmallerArray(points, p, 0);
            p[0]++;
            points = InsertSmallerArray(points, p, 1);
            p[1]++;
            points = InsertSmallerArray(points, p, 2);
            p[0]--;
            points = InsertSmallerArray(points, p, 3);
            p[1]--;
            p[2]++;
            points = InsertSmallerArray(points, p, 4);
            p[0]++;
            points = InsertSmallerArray(points, p, 5);
            p[1]++;
            points = InsertSmallerArray(points, p, 6);
            p[0]--;
            points = InsertSmallerArray(points, p, 7);
            return points;
        }

        private int[,] InsertSmallerArray(int[,] fA, int[] a, int i)
        {
            for(int j = 0; j < a.Length; j++)
            {
                fA[i,j] = a[j];
            }
            return fA;
        }
       
    }
}
