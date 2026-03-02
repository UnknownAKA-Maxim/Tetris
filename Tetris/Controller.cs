using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace Tetris
{
    internal class Controller
    {
        private GridProcess grid;
        private ShapeProcess shape;
        public Controller()
        {

        }
        public void Run()
        {
            grid = new GridProcess();
            shape = new ShapeProcess(grid);
        }
        public void IncrementLeft()
        {
            shape.HorizontalMovement("L");
        }
        public void IncrementRight()
        {
            shape.HorizontalMovement("R");
        }
        public void TurnShape()
        {
            shape.TurnShape();
        }
        public void SoftDrop()
        {
            shape.Drop();
        }
        public void HardDrop()
        {
            shape.HardDrop();
        }
        public void Hold()
        {
            shape.Hold();
        }
        public int[,] GridDrawer()
        {
            return grid.Grid;
        }
        public int[,] DrawHeldShape()
        {
            return shape.PrintHeldShape();
        }
        public void AddGarbageToBoard(int numberOfLinesSent)
        {
            grid.AddGarbage(numberOfLinesSent);
        }
        public int NumberOfGarbageSent()
        {
            int placeHolderVal = grid.NumberOfBlocksSent;
            if (placeHolderVal > 0) 
            { 
                grid.NumberOfBlocksSent = 0;
                if (placeHolderVal == 1) placeHolderVal = 0 ;
                if (placeHolderVal == 2) placeHolderVal = 1;
                if (placeHolderVal == 3) placeHolderVal = 2;
                if (placeHolderVal == 4) placeHolderVal = 4;//
            }
            return placeHolderVal;
        }
    }
}
