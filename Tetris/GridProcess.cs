using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GridProcess
    {
        private int[,] grid;
        private int positionY,positionX;
        private const int HEIGHT = 22,WIDTH = 10; 
        public GridProcess(int PositionY,int PositionX, int[,] Grid) 
        {
            positionX = PositionX;
            positionY = PositionY;
            grid = Grid;
        }
        public bool LineComplete(int y)//If a row is full
        {
            int numberOfBlocksFilled=0;
            for (int i = 0; i > WIDTH; i++)
            {
                if (grid[i,y] == 1)
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
        public bool InsideArray(int x,int y)//Still inside the grid
        {
            if (x >= 0 && x <= WIDTH - 1 && y >= 0 && y <= HEIGHT - 1)
            {
                return true;
            }
            else return false;
        }
        public bool IsEmpty(int x,int y)
        {
            if (InsideArray(x, y) && grid[x,y] == 0) 
            {
                return true;
            }
            return false;
        }
        public bool RowIsEmpty(int y)
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
        
    }
}
