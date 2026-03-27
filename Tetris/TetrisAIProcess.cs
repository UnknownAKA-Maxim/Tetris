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
        private const int STARTINGPOSITIONX = 3, MAXIMUMAMOUNTOFMOVES = 40, STARTINGPOSITIONY = 19;
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
            int count = 0,pointsForBestValue = placeHolderValue[2],AmountOfTurns = 0;
            do
            {
                placeHolderValue = AllEndPositions[0];
                
                if (placeHolderValue[2] < pointsForBestValue)
                {
                    patternToReachBestPosition = "";
                    bestPositionDecidedCoords[0] = placeHolderValue[0];
                    bestPositionDecidedCoords[1] = placeHolderValue[1];
                    pointsForBestValue = placeHolderValue[2];
                    AmountOfTurns = placeHolderValue[3];
                }
                AllEndPositions.Remove(placeHolderValue);
            } while (AllEndPositions.Count > 0);
            for (int i = 0; i < AmountOfTurns; i++)            {
                patternToReachBestPosition += "W";
            }
            return bestPositionDecidedCoords;
        }
        public string PathToDecision()
        {
            patternToReachBestPosition = "";//H=hardDrop L=left R=Right W=Spin
            int[] coords = PositionChosen();
            while (coords[0] != STARTINGPOSITIONX)
            {
                if (coords[0] > STARTINGPOSITIONX)
                {
                    coords[0]--;
                    patternToReachBestPosition += "L";
                }
                else if (coords[0] != STARTINGPOSITIONX)
                {
                    coords[0]++;
                    patternToReachBestPosition += "R";
                }
            }
            patternToReachBestPosition += "H";
            return patternToReachBestPosition;
        }
        private int HolesCreated()
        {
            int numberOfHoles=0;
            for (int i = 0; i < HEIGHT - 1; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (CurrentGrid.Grid[j , i] == Tile.Empty && CurrentGrid.Grid[j,i+1] != Tile.Empty)
                    {
                        numberOfHoles++;
                    }
                }
            }
            return numberOfHoles;
        }
        private int PointsAllocatedForHeight(int OffsetX, int OffsetY)
        {
            int PointsAcredited = 0;
            for (int i = HEIGHT-1; i >= 0; i--)
            {
                for (int j = WIDTH-1; j >= 0; j--)
                {
                    if (CurrentGrid.Grid[j,i] == CurrentShapeManaged.Colour)
                    {
                        for (int y = 0; y < HEIGHT; y++)
                        {
                            if (CurrentGrid.LineIsComplete(y)) PointsAcredited--;
                        }
                        PointsAcredited += i + 1;
                        return PointsAcredited;
                    }
                }
            }
            return 0;//This needs to be here but wont ever be called
        }
        private int[] AllocatePointsToPosition(int OffsetX,int OffsetY,int AmountOfTimesTurned)
        {
            int PointsAllocated = 0;
            PointsAllocated += HolesCreated();
            PointsAllocated += PointsAllocatedForHeight(OffsetX,OffsetY);
            int[] PositionXY = {OffsetX,OffsetY,PointsAllocated,AmountOfTimesTurned};
            //PositionXY[0] PositionXY[1] PositionXY[2] PositionXY[3]
            return PositionXY;
        }
        private void FindAllPossiblePositions()
        {
            int amountOfTurns = 0;
            for (int NumberOfspins = 0; NumberOfspins <=3; NumberOfspins++)
            {
                int offsetPositionX = WIDTH;
                while (CurrentShapeManaged.ValidHorizontalMovement("L"))
                {
                    CurrentShapeManaged.HorizontalMovement("L");
                }
                while (CurrentShapeManaged.ValidHorizontalMovement("R"))
                {
                    offsetPositionX = CurrentShapeManaged.PosOffsetX;
                    CurrentShapeManaged.HardDrop(true);
                    AllEndPositions.Add(AllocatePointsToPosition(offsetPositionX, CurrentShapeManaged.PosOffsetY,amountOfTurns));
                    //AllEndPositions(int[0,1,2])
                    CurrentShapeManaged.InitialiseGridsShapeToZero(0);
                    CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONY;
                    CurrentShapeManaged.MapShapeToArray();
                    CurrentShapeManaged.HorizontalMovement("R");
                }
                offsetPositionX = CurrentShapeManaged.PosOffsetX;
                CurrentShapeManaged.HardDrop(true);
                AllEndPositions.Add(AllocatePointsToPosition(offsetPositionX, CurrentShapeManaged.PosOffsetY, amountOfTurns));
                CurrentShapeManaged.InitialiseGridsShapeToZero(0);
                CurrentShapeManaged.PosOffsetX = STARTINGPOSITIONX;
                CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONY;
                CurrentShapeManaged.MapShapeToArray();
                amountOfTurns++;
                CurrentShapeManaged.TurnShape();
            }
            CurrentShapeManaged.InitialiseGridsShapeToZero(0);
            CurrentShapeManaged.PosOffsetX = STARTINGPOSITIONX;
            CurrentShapeManaged.PosOffsetY = STARTINGPOSITIONY;
            CurrentShapeManaged.MapShapeToArray();
        }
    }
}