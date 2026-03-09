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
        private const int HEIGHT = 23, WIDTH = 10,EMPTYSPACE = 0,SHADOWPIECE = -1;
        private int numberOfBlocksSent = 0;
        public int[,] Grid
        {
            get { return grid; }

            set { Grid = value; }
        }
        public int NumberOfBlocksSent
        {
            get { return numberOfBlocksSent; }
            set { if (value >= 0 && value <= 4) numberOfBlocksSent = value; else if (value > 4) numberOfBlocksSent = 4; else numberOfBlocksSent = 0; }//making sure value wont cant be less than 0 or greater that 4(error checking)
        }
        public bool LineIsComplete(int y)//If a Line is full
        {

            int numberOfBlocksFilled = 0;
            for (int i = 0; i < WIDTH; i++)
            {
                if (!BlockIsClear(i, y))
                {
                    numberOfBlocksFilled++;
                }
            }
            if (numberOfBlocksFilled == 10)
            {
                NumberOfBlocksSent++;
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
            if (InsideArray(x, y) && (grid[x, y] == EMPTYSPACE || grid[x, y] == SHADOWPIECE) )
            {
                return true;

            }
            return false;
        }
        public bool LineIsEmpty(int y)//If every block in a line is equal to 0
        {
            for (int i = 0; i < WIDTH; i++)
            {
                if (!BlockIsClear(i, y))
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
        private void ShiftAboveLineDown(int Y)//Moves above line down
        {
            int yIndex = Y;
            for (int i = 0; i < WIDTH; i++)
            {
                Grid[i, yIndex] = Grid[i, yIndex + 1];
            }
        }
        private void ShiftLineUp()
        {
            for (int i = HEIGHT - 1; i > 0; i--)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if ((Grid[j, i] > 7 || BlockIsClear(j, i)) && (Grid[j, i - 1] > 7 || BlockIsClear(j, i - 1)))
                    {
                        Grid[j, i] = Grid[j, i - 1];
                    }
                }
                SetLineToNull(i-1);
            }
        }
        public void FillInGaps()//When Line completed pulls down lines above
        {
            for (int i = HEIGHT - 2; i >= 0; i--)
            {
                if (LineIsComplete(i) || LineIsEmpty(i))
                {
                    ShiftAboveLineDown(i);
                    SetLineToNull(i + 1);
                }
            }
        }
        private void AddALineToTheBoard(int yIndex)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                Grid[i, yIndex] = 9;
            }
        }
        public void AddGarbage(int numberOfGarbageSent)
        {
            Random rngHoleInGarbage = new Random();
            if (numberOfGarbageSent > 0)
            {
                try
                {
                    ShiftLineUp();
                    for (int i = 0; i < numberOfGarbageSent; i++)
                    {
                        AddALineToTheBoard(i);
                        Grid[rngHoleInGarbage.Next(0, 10), i] = EMPTYSPACE;
                    }
                    numberOfGarbageSent = 0;
                }
                catch { }
            }
        }
        public string DrawGrid()
        {
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
