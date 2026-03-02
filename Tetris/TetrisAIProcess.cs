using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class TetrisAIProcess
    {
        private readonly Controller AIPlayerControls;
        private List<int[]> PotentialMoves = new List<int[]>();
        public TetrisAIProcess(Controller PlayerTwo) 
        {
            AIPlayerControls = PlayerTwo;
        }

    }
}
