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
        private readonly Controller AIPlayerControls;
        private List<int[]> PotentialMoves = new List<int[]>();
        public TetrisAIProcess(Controller PlayerTwo) 
        {
            AIPlayerControls = PlayerTwo;
        }
        public void UpdateGrid(Controller UpdatedGrid)
        {

        }
        private int[] PositonIsPossible()
        {

            return null;
        }
        private void FindAllPossiblePositions()
        {
            for (int i = 0; i < WIDTH - 1; i++)
            {

            }
        }

    }
}
