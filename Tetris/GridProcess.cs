using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GridProcess
    {
        private readonly int[,] grid = new int[WIDTH, HEIGHT];
        private const int HEIGHT = 23, WIDTH = 10;
        public int[,] Grid
        {
            get { return grid; }

            set { Grid = value; }
        }
        public bool LineIsComplete(int y)//If a Line is full
        {

            int numberOfBlocksFilled = 0;
            for (int i = 0; i < WIDTH; i++)
            {
                if (grid[i, y] != 0)
                {
                    numberOfBlocksFilled++;
                }
            }
            if (numberOfBlocksFilled == 10)
            {
                return true;
            }
            else return false;
        }
        public bool InsideArray(int x, int y)//Still inside the grid
        {
            if (x >= 0 && x <= WIDTH - 1 && y >= 0 && y <= HEIGHT - 1)
            {
                return true;
            }
            else return false;
        }
        public bool BlockIsClear(int x, int y)
        {
            if (InsideArray(x, y) && grid[x, y] == 0)
            {
                return true;

            }
            return false;
        }
        public bool LineIsEmpty(int y)//If every block in a line is equal to 0
        {
            for (int i = 0; i < WIDTH; i++)
            {
                if (grid[i, y] == 1)
                {
                    return false;
                }
            }
            return true;
        }
        public void SetLineToNull(int LineCompleted)//Setting the entire line to 0 or null
        {
            for (int i = 0; i < WIDTH; i++)
            {
                grid[i, LineCompleted] = 0;
            }
        }
        public void ShiftAboveLineDown(int Y)//removes gaps in the board
        {
            int yIndex = Y;
            for (int i = 0; i < WIDTH; i++)
            {
                Grid[i, yIndex] = Grid[i, yIndex + 1];
            }
        }
        public void FillInGaps()//When Line completed pulls down lines above
        {
            for (int i = 0; i < HEIGHT - 1; i++)
            {
                if (LineIsComplete(i) || LineIsEmpty(i))
                {
                    ShiftAboveLineDown(i);
                    SetLineToNull(i + 1);
                }
            }
        }

        public string DrawGrid()
        {
            Console.Clear();
            string output = "";
            for (int i = HEIGHT - 1; i >= 0; i--)
            {
                for (int j = WIDTH - 1; j >= 0; j--)
                {
                    if (Grid[j, i] == 1)
                        output += Grid[j, i];
                    else
                        output += Grid[j, i];
                }
                output += "\n";
            }
            return output;
        }
    }
}
