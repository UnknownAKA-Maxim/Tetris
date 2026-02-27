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
        private GridProcess gridOne;
        private ShapeProcess shapeOne;
        public Controller()
        {

        }
        public void Run()
        {
            gridOne = new GridProcess();
            shapeOne = new ShapeProcess(gridOne);
        }
        public void IncrementLeft()
        {
            shapeOne.HorizontalMovement("L");
        }
        public void IncrementRight()
        {
            shapeOne.HorizontalMovement("R");
        }
        public void TurnShape()
        {
            shapeOne.TurnShape();
        }
        public void SoftDrop()
        {
            shapeOne.Drop();
        }
        public void HardDrop()
        {

        }
        public void Hold()
        {

        }
        public void Drawer()
        {
            gridOne.DrawGrid();
        }

    }
}
