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
        Pen p = new Pen(Color.Black);
        Player player;
        List<Block> blocks = new List<Block>();
        bool[] pressedKeys = new bool[6];
        public GameScreen()
        {
            Random random = new Random();
            InitializeComponent();
            player = new Player(1, 1, 0, 90);
            for(int x = -12; x < 13; x++)
            {
                for(int z = -12; z < 13; z++)
                {
                    Block b = new Block(x, 0, z);
                    blocks.Add(b);
                }
                
            }
            
            
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
                    if (true)//player.isPointInView(p[0], p[1], p[2])
                    {
                        vs.Add(p);
                        blockIsVisible = true;
                    }
                }

                if (blockIsVisible)
                {
                    int[,] displayLocations = new int[8, 2];
                    for(int i = 0; i < vs.Count; i++)
                    {

                        int[] loc = vs[i];
                        double[] drawLoc = player.ProjectPoint(loc[0], loc[1], loc[2]);
                        if(drawLoc[0] < 1 && drawLoc[1] < 1 && drawLoc[0] > 0 && drawLoc[1] > 0)
                        {
                            int drawX = Convert.ToInt32(drawLoc[0] * this.Width);
                            int drawY = Convert.ToInt32(drawLoc[1] * this.Height);
                            displayLocations[i, 0] = drawX;
                            displayLocations[i, 1] = drawY;
                            e.Graphics.FillEllipse(sB, drawX, drawY, 4, 4);
                        }
                        
                        
                    }
                    DrawCubeLines(e.Graphics, displayLocations);

                }
            }
        }

        private void DrawCubeLines(Graphics g, int[,] points)
        {
            for(int i = 1; i < 4; i++)
            {
                if(points[i, 0] != 0 && points[i-1, 0] != 0)
                    g.DrawLine(p, points[i - 1, 0], points[i - 1, 1], points[i, 0], points[i, 1]);

            }
           // g.DrawLine(p, points[3, 0], points[3, 1], points[7, 0], points[7, 1]);
            for (int i = 5; i < 8; i++)
            {
                if (points[i, 0] != 0 && points[i - 1, 0] != 0)
                    g.DrawLine(p, points[i - 1, 0], points[i - 1, 1], points[i, 0], points[i, 1]);
            }
            for (int i = 0; i < 4; i++)
            {
                if (points[i, 0] != 0 && points[i +4, 0] != 0)
                    g.DrawLine(p, points[i, 0], points[i, 1], points[i + 4, 0], points[i + 4, 1]);
            }
            if (points[0, 0] != 0 && points[3, 0] != 0)
                g.DrawLine(p, points[0, 0], points[0, 1], points[3, 0], points[3, 1]);
            if (points[4, 0] != 0 && points[7, 0] != 0)
                g.DrawLine(p, points[4, 0], points[4, 1], points[7, 0], points[7, 1]);

        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            double div = 10;
            player.Move(((pressedKeys[2] ? 1 : 0) - (pressedKeys[3] ? 1 : 0))/div,
                ((pressedKeys[4] ? 1 : 0) - (pressedKeys[5] ? 1 : 0)) / div,
                ((pressedKeys[0] ? 1 : 0) - (pressedKeys[1] ? 1 : 0)) / div);
            label1.Text = player.GetCameraDir()[0].ToString();
            Refresh();
        }
        private void GameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            player.MoveCamera(e.X, e.Y, this.Width, this.Height);
        }
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = true;
                    break;
                case Keys.S:
                    pressedKeys[1] = true;
                    break;
                case Keys.A:
                    pressedKeys[2] = true;
                    break;
                case Keys.D:
                    pressedKeys[3] = true;
                    break;
                case Keys.Space:
                    pressedKeys[4] = true;
                    break;
                case Keys.ShiftKey:
                    pressedKeys[5] = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = false;
                    break;
                case Keys.S:
                    pressedKeys[1] = false;
                    break;
                case Keys.A:
                    pressedKeys[2] = false;
                    break;
                case Keys.D:
                    pressedKeys[3] = false;
                    break;
                case Keys.Space:
                    pressedKeys[4] = false;
                    break;
                case Keys.ShiftKey:
                    pressedKeys[5] = false;
                    break;
            }
        }

        
        //https://en.wikipedia.org/wiki/Perspective_(graphical)
    }
}
