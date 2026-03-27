using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace Tetris
{
    internal class Controller
    {
        private GridProcess grid;
        private ShapeProcess shape;
        private Tile[,] ShadowedShape = new Tile[4,4];
        private int posOffsetX,posOffsetY;
        private const int HEIGHT = 23, WIDTH = 10, EMPTYSPACE = 0;
        private int garbageBuffered = 0;
        private bool garbageCanBeReceived;
        public Controller()
        {
        }
        public bool GarbageCanBeReceived
        {
            get { return garbageCanBeReceived; } 
            set { garbageCanBeReceived = shape.GarbageCanBeRecievedToLocalGrid; }
        }
        public ShapeProcess Shape 
        { 
            get { return shape; }
        }
        public GridProcess Grid
        {
            get { return grid; }
        }
        public int[] GetDisplay()
        {
            shape.DisplayCurrentBag();
            return null;
        }
        public void Run()
        {
            grid = new GridProcess();
            shape = new ShapeProcess(grid);
            GarbageCanBeReceived = shape.GarbageCanBeRecievedToLocalGrid;
        }
        public void IncrementLeft()
        {
            shape.HorizontalMovement("L");
        }
        public void IncrementRight()
        {
            shape.HorizontalMovement("R");
        }
        public void SpinShape()
        {
            shape.TurnShape();
        }
        public void SoftDrop()
        {
            shape.Drop(false);
            GarbageCanBeReceived = shape.GarbageCanBeRecievedToLocalGrid;
        }
        public void HardDrop()
        {
            shape.HardDrop(false);
            shape.Drop(false);
            GarbageCanBeReceived = shape.GarbageCanBeRecievedToLocalGrid;
        }
        public void Hold()
        {
            shape.Hold();
        }
        public Tile[,] GridDrawer(ref bool GameStart)
        {
            if (shape.GameEnds)
            {
                GameStart = false;
            }
            return grid.Grid;
        }
        public Tile[,] DrawHeldShape()
        {
            return shape.PrintHeldShape();
        }
        public void AddGarbageToBoard(int numberOfLinesSent)
        {
            if (numberOfLinesSent > 0)
            {
                grid.AddGarbage(numberOfLinesSent);
                shape.GarbageCanBeRecievedToLocalGrid = false;
            }
        }
        public int NumberOfGarbageSent(bool GarbageCanBeSent)
        {
            int placeHolderVal = 0;
            if (grid.NumberOfBlocksSent == 1) placeHolderVal = 0;
            if (grid.NumberOfBlocksSent == 2) placeHolderVal = 1;
            if (grid.NumberOfBlocksSent == 3) placeHolderVal = 2;
            if (grid.NumberOfBlocksSent == 4) placeHolderVal = 4;
            grid.NumberOfBlocksSent = 0;
            return placeHolderVal;
        }
        private bool ValidDropForShadow()//if the drop is valid or not
        {
            int numb = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (grid.InsideArray(j + posOffsetX, i + posOffsetY - 1))
                    {
                        if (!grid.Grid[j + posOffsetX, i + posOffsetY].HasFlag(Tile.Placed)
                         && !grid.Grid[j + posOffsetX, i + posOffsetY - 1].HasFlag(Tile.Placed))
                         //Fairly sure this previously massive if statement can just be replaced by this.
                        {
                            numb++;
                        }
                    }
                }
            }
            return numb > 3;
        }
        private void InitialiseShadowToZero(int HorizontalMovement)
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (grid.Grid[j,i] == Tile.Shadow)
                    {
                        grid.Grid[j,i] = Tile.Empty;
                    }
                }
            }
        }
        private void MapShadowToArray()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (grid.BlockIsClear(posOffsetX + j, posOffsetY + i))
                    {
                        if (ShadowedShape[j, i] == Tile.Shadow)
                        {
                            grid.Grid[posOffsetX + j, posOffsetY + i] = ShadowedShape[j, i];
                        }
                    }
                }
            }
        }
        public void Drop()
        {
            if (ValidDropForShadow())
            {
                InitialiseShadowToZero(0);
                posOffsetY--;
                MapShadowToArray();
            }
        }
        public void DisplayPieceShadow()
        {
            posOffsetX = shape.PosOffsetX; 
            posOffsetY = shape.PosOffsetY;
            ShadowedShape = shape.CurrentShape;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(ShadowedShape[j, i] == shape.Colour)
                    {
                        ShadowedShape[j, i] = Tile.Shadow;
                    }
                }
            }
            while (ValidDropForShadow())
            {
                Drop();
            }
        }
    }
}
