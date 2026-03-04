using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class TetrisAIProcess
    {
        private readonly int HEIGHT = 23, WIDTH = 10;
        private Controller AIPlayerControls;
        private ShapeProcess CurrentShapeManaged;
        private List<int[]> AllEndPositions = new List<int[]>();
        private const int STARTINGPOSITIONX = 3;
        private int PointsAllocated = 0;
        public TetrisAIProcess(Controller PlayerTwo) 
        {
            AIPlayerControls = PlayerTwo;
            CurrentShapeManaged = AIPlayerControls.Shape;
        }
        public void UpdateGrid(Controller UpdatedGrid)
        {
            AIPlayerControls = UpdatedGrid;
        }
        private int HolesCreated()
        {

            return 0;
        }
        private int[] PositonIsPossible()
        {
            int[] PositonXY = {CurrentShapeManaged.PosOffsetX, CurrentShapeManaged.PosOffsetY,0};

            return null;
        }
        private void FindAllPossiblePositions()
        {
            int posXOffset = STARTINGPOSITIONX,posYOffset;
            for (int i = 0; i < WIDTH - 1; i++)
            {
                AIPlayerControls.IncrementLeft();
            }
            for (int i = 0; i < WIDTH - 1; i++)
            {
                AIPlayerControls.HardDrop();
                AllEndPositions.Add(PositonIsPossible());
                AIPlayerControls.IncrementRight();
            }
        }

    }
}
