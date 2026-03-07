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
        private const int PIECESHADOW = -1;
        private int[,] ShadowedShape = new int[4,4];
        private int posOffsetX,posOffsetY;
        private const int HEIGHT = 23, WIDTH = 10, EMPTYSPACE = 0;
        
        public Controller()
        {
        }
        public ShapeProcess Shape 
        { 
            get { return shape; }
        }
        public GridProcess Grid
        {
            get { return grid; }
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
        public void SpinShape()
        {
            shape.TurnShape();
        }
        public void SoftDrop()
        {
            shape.Drop(false);
        }
        public void HardDrop()
        {
            shape.HardDrop(false);
            shape.Drop(false);
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
                if (placeHolderVal == 4) placeHolderVal = 4;
            }
            return placeHolderVal;
        }
        private bool ValidDropForShadow()//if the drop is valid or not
        {
            int numb = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (posOffsetY+j < 0)
                    {

                    }
                    if (grid.InsideArray(j + posOffsetX, i + posOffsetY - 1))
                    {
                        if ((grid.Grid[j + posOffsetX, i + posOffsetY] == PIECESHADOW || grid.Grid[j + posOffsetX, i + posOffsetY] == shape.Colour) && (grid.Grid[j + posOffsetX, i + posOffsetY - 1] == EMPTYSPACE || grid.Grid[j + posOffsetX, i + posOffsetY - 1] == PIECESHADOW || grid.Grid[j + posOffsetX, i + posOffsetY - 1] == shape.Colour))//Drop is valid if each block is above a zero or itself
                        {
                            numb++;
                        }
                    }
                }
            }
            if (numb > 3)
            {
                return true;
            }
            return false;
        }
        private void InitialiseShadowToZero(int HorizontalMovement)
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (grid.Grid[j,i] == PIECESHADOW)
                    {
                        grid.Grid[j,i] = EMPTYSPACE;
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
                        if (ShadowedShape[j, i] == PIECESHADOW)
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
                        ShadowedShape[j, i] = PIECESHADOW;
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
