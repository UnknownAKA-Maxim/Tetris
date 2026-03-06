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
        private readonly int WIDTH = 10,HEIGHT = 23;
        private Controller AIPlayerControls;
        private ShapeProcess CurrentShapeManaged;
        private GridProcess CurrentGrid;
        private List<int[]> AllEndPositions = new List<int[]>();
        private const int STARTINGPOSITIONX = 3,MAXIMUMAMOUNTOFMOVES = 40, STARTINGPOSITIONY = 19,EMPTYSPACE = 0;
        private string patternToReachBestPosition;
        public TetrisAIProcess(Controller PlayerTwo) 
        {
            AIPlayerControls = PlayerTwo;
            CurrentShapeManaged = AIPlayerControls.Shape;
        }
        public void UpdateControllerClass(Controller UpdatedGrid)
        {
            patternToReachBestPosition = "";
            AIPlayerControls = UpdatedGrid;
            CurrentGrid = AIPlayerControls.Grid;
            CurrentShapeManaged = AIPlayerControls.Shape;
            AllEndPositions = new List<int[]>();
            FindAllPossiblePositions();
        }
        private int[] PositionChosen()
        {
            int[] placeHolderValue = AllEndPositions[0];
            int[] bestPositionDecidedCoords = { placeHolderValue[0], placeHolderValue[1]};
            int count = 0,pointsForBestValue = placeHolderValue[2];
            do
            {
                placeHolderValue = AllEndPositions[count];
                
                if (placeHolderValue[2] < pointsForBestValue)
                {
                    bestPositionDecidedCoords[0] = placeHolderValue[0];
                    bestPositionDecidedCoords[1] = placeHolderValue[1];
                    pointsForBestValue = placeHolderValue[2];
                }
                AllEndPositions.Remove(placeHolderValue);
            } while (AllEndPositions.Count > 0);
            return bestPositionDecidedCoords;
        }
        public string PathToDecision()
        {
            patternToReachBestPosition = "";//H=hardDrop L=left R=Right W=Spin
            int[] coords = PositionChosen();
            while (coords[0] != STARTINGPOSITIONX+1)
            {
                if (coords[0] > STARTINGPOSITIONX+1)
                {
                    coords[0]--;
                    patternToReachBestPosition += "R";
                }
                else 
                {
                    coords[0]++;
                    patternToReachBestPosition += "L";
                }
            }
            patternToReachBestPosition += "H";
            return patternToReachBestPosition;
        }
        private int HolesCreated(int OffsetX ,int OffsetY)
        {
            int numberOfHolesCreated=0;
            for (int i = 0; i < HEIGHT - 1; i++)
            {
                for (int j = 0; j < WIDTH-1; j++)
                {
                    if (CurrentGrid.Grid[j , i] == EMPTYSPACE && CurrentGrid.Grid[j,i+1] != 0)
                    {
                        numberOfHolesCreated++;
                    }
                }
            }
            return numberOfHolesCreated;
        }

        private int[] AllocatePointsToPosition(int OffsetX,int OffsetY)
        {
            int PointsAllocated = 0;
            PointsAllocated += HolesCreated(OffsetX,OffsetY);
            //PointsAllocated += OffsetY;
            int[] PositionXY = {OffsetX,OffsetY,PointsAllocated};
            //PositionXY[0] PositionXY[1] PositionXY[3]
            return PositionXY;
        }
        private void FindAllPossiblePositions()
        {
            //for (int NumberOfspins = 0; NumberOfspins <=4; NumberOfspins++)
            {
                //AIPlayerControls.SpinShape();
                for (int i = 0; i < WIDTH - 1; i++)
                {
                    CurrentShapeManaged.HorizontalMovement("L");
                }
                for (int PositionX = 0; PositionX < WIDTH - 1; PositionX++)
                {
                    CurrentShapeManaged.HardDrop();                    
                    AllEndPositions.Add(AllocatePointsToPosition(PositionX, CurrentShapeManaged.PosOffsetY));
                    //AllEndPositions(int[0,1,2])
                    CurrentShapeManaged.HorizontalMovement("R");
                    CurrentShapeManaged.InitialiseGridsShapeToZero(0);
                    CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONY;                    
                }                
            }
            CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONX;
            CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONY;
        }

    }
}
