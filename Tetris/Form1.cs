using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        const int WIDTH = 11, HEIGHT = 20, STARTINGPOSITION = 5;
        int[,] Board = new int[WIDTH, HEIGHT];
        int PositionY = 0, PositionX = STARTINGPOSITION;
        int movement = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            PositionX += movement;
            movement = 0;
            Board[PositionX, PositionY] = 1;
            if (PositionY == 19)
            {
                PositionX = STARTINGPOSITION;
                PositionY = 1;
            }
            ProcessBoard(sender,e);
        }
        private void ProcessBoard(object sender, EventArgs e)
        {
            if (PositionY < 19)//drops ontop of another block
            {

                if (Board[PositionX + 1, PositionY] != 1)
                {
                    Board[PositionX, PositionY] = 0;
                    PositionY++;

                }

                else
                {
                    PositionY = 1;
                    PositionX = STARTINGPOSITION;
                }
            }
            this.Invalidate();
            this.Update();
        }
        public Form1()
        {
            InitializeComponent();
        }//

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DoubleBuffered = true;
            const int SPACEBETWEENSQUARES = 2;
            const int SQUARESIZE = 8;
            const int OFFSETX = 5, OFFSETY = 5;
            int boardPositionX = OFFSETX;
            int boardPositionY = OFFSETY;
            Rectangle[,] square = new Rectangle[WIDTH, HEIGHT];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Brush b = new SolidBrush(GetCol(Board[j, i]));
                    Brush SquareColour = new SolidBrush(Color.Plum);
                    boardPositionX += SPACEBETWEENSQUARES + SQUARESIZE;
                    square[j, i] = new Rectangle(boardPositionX, boardPositionY, SQUARESIZE, SQUARESIZE);
                    if (Board[j, i] == 1)
                    {
                        e.Graphics.FillRectangle(SquareColour, square[j, i]);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(b, square[j, i]);
                    }
                }
                boardPositionY += SPACEBETWEENSQUARES + SQUARESIZE;
                boardPositionX = OFFSETX;
            }
        }

        private static Color GetCol(int colNum)//Each colour is represented as different number
        {
            switch (colNum)
            {
                case 1: return Color.Black;
                case 2: return Color.Red;
                case 3: return Color.Green;
                case 4: return Color.Blue;
                case 5: return Color.Yellow;
                case 6: return Color.Chartreuse;
                default:
                    return Color.FromArgb(233, 116, 81);
            }
        }
    }
}