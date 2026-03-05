using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class TetrisAIProcess
    {
        private readonly int HEIGHT = 23, WIDTH = 10;
        private Controller AIPlayerControls;
        private ShapeProcess CurrentShapeManaged;
        private GridProcess CurrentGrid;
        private List<List<int>> AllEndPositions = new List<List<int>>();
        private const int STARTINGPOSITIONX = 3,MAXIMUMAMOUNTOFMOVES = 40, STARTINGPOSITIONY =19,EMPTYSPACE = 0;
        private int PointsAllocated = 0;
        public TetrisAIProcess(Controller PlayerTwo) 
        {
            AIPlayerControls = PlayerTwo;
            CurrentShapeManaged = AIPlayerControls.Shape;
        }
        public void UpdateGrid(Controller UpdatedGrid)
        {
            AIPlayerControls = UpdatedGrid;
            CurrentGrid = AIPlayerControls.Grid;
        }
        private int[] PositonChosen()
        {
            int [] bestpositionDecidedCoords = new int[2];
            int count = 0;
            do
            {
                bestpositionDecidedCoords = AllEndPositions[0,0,0,count];
            } while (AllEndPositions.Count > 0);
            return null;
        }
        public string DecisionMade()
        {
            string patternToReachBestPosition = "";
            return patternToReachBestPosition;
        }
        private int HolesCreated(int OffsetX ,int OffsetY)
        {
            int numberOfHolesCreated=0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CurrentGrid.InsideArray(j + OffsetX, i + OffsetY) && CurrentGrid.InsideArray(j + OffsetX, i + OffsetY-1))
                    {
                        if (CurrentGrid.Grid[j + OffsetX, i + OffsetY] == CurrentShapeManaged.Colour)
                        {
                            if (CurrentGrid.Grid[j + OffsetX, i + OffsetY-1] == EMPTYSPACE)
                            {
                                PointsAllocated++;
                            }
                        }
                    }
                }
            }
            return numberOfHolesCreated;
        }
        private int[] PositonIsPossible(int OffsetX,int OffsetY)
        {
            PointsAllocated += HolesCreated(OffsetX,OffsetY);
            PointsAllocated += OffsetY;
            int[] PositonXY = {CurrentShapeManaged.PosOffsetX, CurrentShapeManaged.PosOffsetY,PointsAllocated};
            
            return PositonXY;
        }
        private void FindAllPossiblePositions()
        {
            int posXOffset = STARTINGPOSITIONX,posYOffset = STARTINGPOSITIONY;
            for (int i = 0; i < WIDTH - 1; i++)
            {
                AIPlayerControls.IncrementLeft();
            }
            for (int i = 0; i < WIDTH - 1; i++)
            {
                AIPlayerControls.HardDrop();
                AllEndPositions.Add(PositonIsPossible(posXOffset,posYOffset));
                AIPlayerControls.IncrementRight();
            }
        }

    }
}
