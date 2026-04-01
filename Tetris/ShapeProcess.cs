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
        private const int EMPTYSPACE = 0;
        private int[,] currentShape = new int[4, 4];
        private GridProcess grid = new GridProcess();
        private int currentRotation = 0;
        private int posOffsetX = 3, posOffsetY = 19;
        private int colour = 1, HeldColour = 1;//both set to one just to initialise
        private Stack<int> currentBag = new Stack<int>();
        private Stack<int> SecondBag = new Stack<int>();
        private int currentShapeNumber = 0;//the Shapes assigned number in shapeChooser
        private int HeldValue = 0;//The Shapes number assigned to the held value
        private bool HoldIsPossible = true, ShapeIsHeld = false;
        private bool garbageCanbeRecievedToLocalGrid;
        private bool gameEnds = false;
        private int pendingGarbage = 0;
        public ShapeProcess(GridProcess Grid)
        {
            currentShape = CurrentShape;
            grid = Grid;
            MapShapeToArray();
        }
        public void QueueGarbage(int lines)
        {
            pendingGarbage += lines;
        }
        public bool GameEnds
        {
            get { return gameEnds; } set { gameEnds = value; }
        }
        public bool GarbageCanBeRecievedToLocalGrid
        {
            get { return garbageCanbeRecievedToLocalGrid; }
            set { garbageCanbeRecievedToLocalGrid = value; }
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
        public int[,] DisplayCurrentBag()
        {

            return null;
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
        public int[,] CurrentShape
        {
            get { if (!IsEmptyOrNull(currentShape)) return currentShape; return ShapeChoose(); }
            set { currentShape = value; }
        }
        private void NewBag(Stack<int> bag)
        {
            int[] placeHolderValueForBag = new int[7];
            int count = 0;
            Stack<int> PlaceHolderBag = new Stack<int>();
            bool valueHasBeenUsed;
            int placeholderVal;
            Random rng = new Random();
            for (int SecondBag = 0; SecondBag < 2; SecondBag++)
            {
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
                        bag.Push(placeholderVal);
                        count++;
                    }
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
                    if (grid.InsideArray(PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]) && (grid.InsideArray(PosOffsetX + pWidth, PosOffsetY + pHeight)))//if inside the grid
                    {
                        if (grid.Grid[PosOffsetX + pWidth, PosOffsetY + pHeight] == colour)//if the original
                        {
                            if (!grid.BlockIsClear(PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]) && grid.Grid[PosOffsetX + XYCoords[0], PosOffsetY + XYCoords[1]] != colour)
                            {
                                overLaps = true;
                            }
                        }
                    }
                    else { overLaps = true; }
                    
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
                int[,] placeHolderVAL = new int[4, 4];
                for (int pHeight = 0; pHeight <= 3; pHeight++)
                {
                    for (int pWidth = 0; pWidth <= 3; pWidth++)
                    {
                        int[] XYCoords = NinetyDegreeSpin(pWidth, pHeight, CurrentRotation);
                        placeHolderVAL[XYCoords[0/*X*/], XYCoords[1/*Y*/]] = CurrentShape[pWidth, pHeight];
                    }
                }
                CurrentShape = placeHolderVAL;
                if (CurrentRotation >= 3) { CurrentRotation = 0; }
                else { CurrentRotation += 1; }
                InitialiseGridsShapeToZero(0);
            }
            grid = MapShapeToArray();
        }
        public int[,] ShapeChoose()//Implemented using the bag system
        {
            if (IsEmptyOrNullStack(CurrentBag))
            {
                NewBag(CurrentBag);
            }
            int[,] shape = new int[4, 4];
            currentShapeNumber = CurrentBag.Pop();
            switch (currentShapeNumber)
            {
                case 1:
                    //I PIECE
                    Colour = 1;
                    shape[2, 0] = colour; shape[2, 1] = colour; shape[2, 2] = colour; shape[2, 3] = colour;
                    break;
                case 2:
                    //O PIECE
                    Colour = 2;
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour; shape[2, 2] = colour;
                    break;
                case 3:
                    //S PIECE
                    Colour = 3;
                    shape[1, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[2, 2] = colour;
                    break;
                case 4:
                    //Z PIECE
                    Colour = 4;
                    shape[2, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour;
                    break;
                case 5:
                    //L PIECE
                    Colour = 5;
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[2, 2] = colour; shape[2, 3] = colour;
                    break;
                case 6:
                    //J PIECE
                    Colour = 6;
                    shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour; shape[1, 3] = colour;
                    break;
                case 7:
                    //T PIECE
                    Colour = 7;
                    shape[1, 0] = colour; shape[1, 1] = colour; shape[2, 1] = colour; shape[1, 2] = colour;
                    break;
            }
            return shape;
        }
        public int[,] PrintHeldShape()
        {
            int[,] HeldshapeMatch = new int[4, 4];
            switch (HeldValue)
            {
                case 1:
                    //I PIECE
                    HeldColour = 1;
                    HeldshapeMatch[2, 0] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[2, 2] = HeldColour; HeldshapeMatch[2, 3] = HeldColour;
                    break;
                case 2:
                    //O PIECE
                    HeldColour = 2;
                    HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[1, 2] = HeldColour; HeldshapeMatch[2, 2] = HeldColour;
                    break;
                case 3:
                    //S PIECE
                    HeldColour = 3;
                    HeldshapeMatch[1, 0] = HeldColour; HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[2, 2] = HeldColour;
                    break;
                case 4:
                    //Z PIECE
                    HeldColour = 4;
                    HeldshapeMatch[2, 0] = HeldColour; HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[1, 2] = HeldColour;
                    break;
                case 5:
                    //L PIECE
                    HeldColour = 5;
                    HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[2, 2] = HeldColour; HeldshapeMatch[2, 3] = HeldColour;
                    break;
                case 6:
                    //J PIECE
                    HeldColour = 6;
                    HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[1, 2] = HeldColour; HeldshapeMatch[1, 3] = HeldColour;
                    break;
                case 7:
                    //T PIECE
                    HeldColour = 7;
                    HeldshapeMatch[1, 0] = HeldColour; HeldshapeMatch[1, 1] = HeldColour; HeldshapeMatch[2, 1] = HeldColour; HeldshapeMatch[1, 2] = HeldColour;
                    break;
            }
            return HeldshapeMatch;
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
                        if ((grid.Grid[j + PosOffsetX, i + PosOffsetY] == colour) && (grid.BlockIsClear(j + PosOffsetX, i + PosOffsetY - 1) || (grid.Grid[j + PosOffsetX, i + PosOffsetY - 1] == colour)))//Drop is valid if each block is above a zero or itself
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
        public void Drop(bool PhantomMove)
        {
            if (ValidDrop())
            {
                InitialiseGridsShapeToZero(0);
                PosOffsetY--;
                MapShapeToArray();
            }
            else 
            if(PhantomMove == false)
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
                    if ((PosOffsetY + i) >= 0 && (PosOffsetX + j) >= EMPTYSPACE && (PosOffsetX + j) <= 9)
                    {
                        if (grid.Grid[PosOffsetX + j, PosOffsetY + i] == colour)
                        {
                            grid.Grid[PosOffsetX + j, PosOffsetY + i] += 7;//This is for placed values just incase other parts of the program are affected
                        }
                    }
                }
            }
            //Initialise a new shape here
            if (pendingGarbage > 0)
            {
                grid.AddGarbage(pendingGarbage);
                pendingGarbage = 0;
            }

            GarbageCanBeRecievedToLocalGrid = true;
            HoldIsPossible = true;
            CreateNewShape();
        }
        public void InitialiseGridsShapeToZero(int HorizontalMovement)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((PosOffsetY + i) >= 0 && (PosOffsetX + j + HorizontalMovement) >= 0 && (PosOffsetX + j + HorizontalMovement) <= 9)
                    {
                        if (grid.Grid[PosOffsetX + j + HorizontalMovement, PosOffsetY + i] == colour)
                        {
                            grid.Grid[PosOffsetX + j + HorizontalMovement, PosOffsetY + i] = 0;
                        }
                    }
                }
            }
        }
        private void CreateNewShape()
        {
            PosOffsetX = 3;
            PosOffsetY = 19;
            CurrentRotation = 0;
            CurrentShape = ShapeChoose();
            MapShapeToArray();
        }
        public GridProcess MapShapeToArray()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CurrentShape[j, i] == colour)
                    {
                        if(grid.Grid[PosOffsetX + j, PosOffsetY + i] != EMPTYSPACE && grid.Grid[PosOffsetX + j, PosOffsetY + i] != Colour) 
                        { gameEnds = true; }
                        grid.Grid[PosOffsetX + j, PosOffsetY + i] = CurrentShape[j, i];
                    }
                }
            }
            return grid;
        }
        public bool ValidHorizontalMovement(string directionParameter)
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
                            if ((grid.Grid[j + posOffsetX, i + posOffsetY] == colour) && ((grid.Grid[j + PosOffsetX - 1, i + posOffsetY] == 0) || (grid.Grid[j + PosOffsetX - 1, i + posOffsetY] == colour)))//Sideways Movement is valid if each block is above a zero or itself
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
                            if ((grid.Grid[j + posOffsetX, i + posOffsetY] == colour) && ((grid.Grid[j + PosOffsetX + 1, i + posOffsetY] == 0) || (grid.Grid[j + PosOffsetX + 1, i + posOffsetY] == colour)))//Sideways Movement is valid if each block is above a zero or itself
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
        public void HardDrop(bool PhantomMove)
        {
            do
            {
                Drop(PhantomMove);
            } while (ValidDrop() == true);            
        }
        public void Hold()
        {
            if (HoldIsPossible)
            {
                if (ShapeIsHeld)//Pushing current value held onto the bag stack
                {
                    InitialiseGridsShapeToZero(0);
                    currentBag.Push(HeldValue);
                    HeldValue = currentShapeNumber;
                    CreateNewShape();
                }
                else//Pushing current Shape onto held value
                {
                    HeldValue = currentShapeNumber;
                    InitialiseGridsShapeToZero(0);
                    CreateNewShape();
                    ShapeIsHeld = true;
                }
                HoldIsPossible = false;
            }
        }

    }
}
