using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace Tetris
{
    internal class ShapeProcess : Block
    {
        private int[,] currentShape = new int[4, 4];
        private GridProcess grid = new GridProcess();
        private int currentRotation = 0;
        private int posOffsetX = 3, posOffsetY = 19;
        private int colour = 1;
        private Stack<int> currentBag = new Stack<int>();
        public ShapeProcess(GridProcess Grid)
        {
            currentShape = CurrentShape;
            grid = Grid;
            MapShapeToArray();
        }
        public int Colour
        {
            get { return colour; }
            set { colour = value; }
        }
        public int PosOffsetX
        {
            get { return posOffsetX; }
            set { posOffsetX = value; }
        }
        private Stack<int> CurrentBag
        {
            get { return currentBag; }
            set { currentBag = value; }
        }
        public int PosOffsetY
        {
            get { return posOffsetY; }

            set { posOffsetY = value; }
        }
        public int CurrentRotation
        {
            get { return currentRotation; }

            set { currentRotation = value; }
        }
        private bool IsEmptyOrNull(Array array)
        {
            foreach (int x in array)
            {
                if (x == colour)
                    return false;
            }
            return true;
        }
        private bool IsEmptyOrNullStack(Stack<int> stack)
        {
            foreach (int x in stack)
            {
                if (x != 0)
                    return false;
            }
            return true;
        }
        private int[,] CurrentShape
        {
            get { if (!IsEmptyOrNull(currentShape)) return currentShape; return ShapeChoose(); }
            set { currentShape = value; }
        }
        private void NewBag()
        {
            int[] placeHolderValueForBag = new int[7];
            int count = 0;
            bool valueHasBeenUsed;
            int placeholderVal;
            Random rng = new Random();
            while (count != 7)
            {
                valueHasBeenUsed = false;
                placeholderVal = rng.Next(1, 8);
                for (int i = 0; i < placeHolderValueForBag.Length - 1; i++)
                {
                    if (placeholderVal == placeHolderValueForBag[i])
                    {
                        valueHasBeenUsed = true;
                    }
                }
                if (!valueHasBeenUsed)
                {
                    placeHolderValueForBag[count] = placeholderVal;
                    CurrentBag.Push(placeholderVal);
                    count++;
                }
            }
        }
        private bool ValidTurn()
        {
            //If a turn is valid or not
            bool overLaps = false;

            for (int pHeight = 0; pHeight <= 3; pHeight++)
            {
                for (int pWidth = 0; pWidth <= 3; pWidth++)
                {
                    int[] XYCoords = NinetyDegreeSpin(pWidth, pHeight, CurrentRotation);
                    if (PosOffsetY == -1)
                    {

                    }
                    if (grid.InsideArray(PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]) && (grid.InsideArray(PosOffsetX + pWidth, PosOffsetY + pHeight)))//if inside the grid
                    {
                        if (grid.Grid[PosOffsetX + pWidth, PosOffsetY + pHeight] == colour)//if 
                        {
                            if (grid.Grid[PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]] != 0 && grid.Grid[PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]] != colour)
                            {
                                overLaps = true;
                            }
                        }
                    }
                    else overLaps = true;
                }
            }
            if (overLaps)
            {
                return false;
            }
            return true;
        }
        public void TurnShape() //spins the shape
        {
            if (ValidTurn())
            {
                //if (CurrentRotation == 3) ;
                int[,] placeHolderVAL = new int[4, 4];
                for (int pHeight = 0; pHeight <= 3; pHeight++)
                {
                    for (int pWidth = 0; pWidth <= 3; pWidth++)
                    {
                        int[] XYCoords = NinetyDegreeSpin(pWidth, pHeight, CurrentRotation);
                        placeHolderVAL[XYCoords[0/*X*/], XYCoords[1/*Y*/]] = currentShape[pWidth, pHeight];
                    }
                }
                CurrentShape = placeHolderVAL;
                if (currentRotation >= 3) { currentRotation = 0; }
                else { currentRotation += 1; }
                InitialiseGridsShapeToZero(0);
            }
            grid = MapShapeToArray();

        }
        public int[,] ShapeChoose()//Implemented using the bag system
        {
            if (IsEmptyOrNullStack(CurrentBag))
            {
                NewBag();
            }
            int[,] shape = new int[4, 4];
            int currentShapeNumber = CurrentBag.Pop();
            switch (currentShapeNumber)
            {
                case 1:
                    //I PIECE
                    shape[2, 0] = colour; shape[2, 1] = colour; shape[2, 2] = colour; shape[2, 3] = colour;
                    break;
                case 2:
                    //O PIECE
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour; shape[2, 2] = colour;
                    break;
                case 3:
                    //S PIECE
                    shape[1, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[2, 2] = colour;
                    break;
                case 4:
                    //Z PIECE
                    shape[2, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour;
                    break;
                case 5:
                    //L PIECE
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[2, 2] = colour; shape[2, 3] = colour;
                    break;
                case 6:
                    //J PIECE
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour; shape[1, 3] = colour;
                    break;
                case 7:
                    //T PIECE
                    shape[1, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour;
                    break;
            }
            return shape;
        }
        public void PrintShape()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(CurrentShape[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private bool ValidDrop()//if the drop is valid or not
        {
            int numb = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (grid.InsideArray(j + posOffsetX, i + posOffsetY - 1))
                    {
                        if ((grid.Grid[j + posOffsetX, i + posOffsetY] == colour) && ((grid.Grid[j + PosOffsetX, i + posOffsetY - 1] == 0) || (grid.Grid[j + PosOffsetX, i + posOffsetY - 1] == colour)))//Drop is valid if each block is above a zero or itself
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
        public void Drop()
        {
            if (ValidDrop())
            {
                InitialiseGridsShapeToZero(0);
                PosOffsetY--;
                MapShapeToArray();
            }
            else
            {
                //Call the method that turns all values to placed block values                
                BlockIsPlaced();
            }

        }
        private void BlockIsPlaced()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((PosOffsetY + i) >= 0 && (PosOffsetX + j) >= 0 && (PosOffsetX + j) <= 9)
                    {
                        if (grid.Grid[PosOffsetX + j, PosOffsetY + i] == colour)
                        {
                            grid.Grid[PosOffsetX + j, PosOffsetY + i] += 7;//This is for placed values just incase other parts of the program are affected
                        }
                    }
                }
            }
            //Initialise a new shape here
            for (int i = 0; i <= 12; i++)
            {
                grid.FillInGaps();
            }
            CreateNewShape();
        }
        private void InitialiseGridsShapeToZero(int HorizontalMovement)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((PosOffsetY + i) >= 0 && (PosOffsetX + j + HorizontalMovement) >= 0 && (PosOffsetX + j + HorizontalMovement) <= 9)
                    {
                        if (grid.Grid[PosOffsetX + j + HorizontalMovement, PosOffsetY + i] == colour)
                        {
                            grid.Grid[PosOffsetX + j+ HorizontalMovement, PosOffsetY + i] = 0;
                        }
                    }
                }
            }
        }
        private void CreateNewShape()
        {
            Random rng = new Random();
            PosOffsetX = 3;
            PosOffsetY = 19;
            CurrentRotation = 0;
            Colour = rng.Next(1, 8);
            CurrentShape = ShapeChoose();
            MapShapeToArray();
            
        }
        private GridProcess MapShapeToArray()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CurrentShape[j, i] == colour)
                    {
                        grid.Grid[PosOffsetX + j, PosOffsetY + i] = CurrentShape[j, i];
                    }
                }
            }
            return grid;
        }
        private bool ValidHorizontalMovement(string directionParameter)
        {
            if (directionParameter == "R")
            {
                int count = 0;
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        if (grid.InsideArray(j + posOffsetX - 1, i + posOffsetY) && grid.InsideArray(j + posOffsetX, i + posOffsetY))
                        {
                            if ((grid.Grid[j + posOffsetX, i + posOffsetY] == colour) && ((grid.Grid[j + PosOffsetX - 1, i + posOffsetY] == 0) || (grid.Grid[j + PosOffsetX - 1, i + posOffsetY] == colour)))//Drop is valid if each block is above a zero or itself
                            {
                                count++;
                            }
                        }
                    }
                }
                if (count == 4) return true;
            }
            else
            {
                int count = 0;
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        if (grid.InsideArray(j + posOffsetX + 1, i + posOffsetY) && grid.InsideArray(j + posOffsetX, i + posOffsetY))
                        {
                            if ((grid.Grid[j + posOffsetX, i + posOffsetY] == colour) && ((grid.Grid[j + PosOffsetX + 1, i + posOffsetY] == 0) || (grid.Grid[j + PosOffsetX + 1, i + posOffsetY] == colour)))//Drop is valid if each block is above a zero or itself
                            {
                                count++;
                            }
                        }
                    }
                }
                if (count == 4) return true;
            }
            return false;
        }
        public void HorizontalMovement(string direction)
        {

            if (direction == "L" && ValidHorizontalMovement(direction))
            {
                PosOffsetX++;
                InitialiseGridsShapeToZero(-1);
            }
            if (direction == "R" && ValidHorizontalMovement(direction))
            {
                PosOffsetX--;
                InitialiseGridsShapeToZero(1);
            }
            MapShapeToArray();
        }
        public void HardDrop()
        {
            for(int i = 0; i < 23-PosOffsetY; i++)
            {

            }
        }
    }
}
