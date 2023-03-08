using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenderDragon
{
    public partial class GameScreen : UserControl
    {
        SolidBrush sB = new SolidBrush(Color.Black);
        Player player;
        List<Block> blocks = new List<Block>();
        public GameScreen()
        {
            InitializeComponent();
            player = new Player(0, 0, 0, 90);
            Block b = new Block(1, 0, 1);
            blocks.Add(b);
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Block b in blocks)
            {
                List<int[]> vs = new List<int[]>();
                bool blockIsVisible = false;
                int[,] points = b.GetPoints();
                for(int i = 0; i < 8; i++)
                {
                    int[] p = new int[3];
                    for(int j = 0; j < 3; j++)
                    {
                        p[j] = points[i,j];
                    }
                    if (player.isPointInView(p[0], p[1], p[2]))
                    {
                        vs.Add(p);
                        blockIsVisible = true;
                    }
                }

                if (blockIsVisible)
                {
                    foreach (int[] loc in vs)
                    {
                        double[] drawLoc = player.ProjectPoint(loc[0], loc[1], loc[2]);

                        int drawX = Convert.ToInt32(drawLoc[0] * this.Width);
                        int drawY = Convert.ToInt32(drawLoc[1] * this.Height);

                        e.Graphics.FillEllipse(sB, drawX, drawY, 4, 4);
                    }
                }
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            player.Move(0.00, 0, -0.01);
            label1.Text = player.GetPos()[2].ToString();
            Refresh();
        }
        //https://en.wikipedia.org/wiki/Perspective_(graphical)
    }
}
