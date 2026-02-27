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
        const int WIDTH = 10, HEIGHT = 23, STARTINGPOSITION = 5;
        int[,] Board = new int[WIDTH, HEIGHT];
        int PositionY = 0, PositionX = STARTINGPOSITION;
        int movement = 0; 
        static Controller PlayerOne = new Controller();
        private void timer1_Tick(object sender, EventArgs e)
        {
            ProcessBoard(sender,e);
        }
        private void ProcessBoard(object sender, EventArgs e)
        {
            Board = PlayerOne.Drawer();
            PlayerOne.SoftDrop();
            this.Invalidate();
            this.Update();
        }
        public Form1()
        {
            PlayerOne.Run();
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
            for (int i = HEIGHT-1; i >= 0; i--)
            {
                for (int j = WIDTH-1; j >= 0; j--)
                {
                    Brush b = new SolidBrush(GetCol(Board[j, i]));
                    Brush SquareColour = new SolidBrush(GetCol(Board[j, i]));
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
                case 7: return Color.White;
                case 8: return Color.Black;
                case 9: return Color.Red;
                case 10: return Color.Green;
                case 11: return Color.Blue;
                case 12: return Color.Yellow;
                case 13: return Color.Chartreuse;
                case 14: return Color.White;
                default:
                    return Color.FromArgb(233, 116, 81);
            }
        }
    }
}