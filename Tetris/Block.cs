using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal abstract class Block
    {
        private const int WIDTH = 3;
        private const int HEIGHT = 3;
        private readonly int[,] ShapeMappingDictionary0 = {//representation of the states of each shape
                                                    { 0, 1, 2, 3},  //0 0 1 0  
                                                    { 4, 5, 6, 7},  //0 0 1 0
                                                    { 8, 9,10,11},  //0 0 1 0
                                                    {12,13,14,15}   //0 0 1 0
                                                 };
        private readonly int[,] ShapeMappingDictionary90 = {          //X X X X 
                                                    {12, 8, 4, 0},    //0 0 0 0 Y
                                                    {13, 9, 5, 1},    //0 0 0 0 Y
                                                    {14,10, 6, 2},    //1 1 1 1 Y
                                                    {15,11, 7, 3}     //0 0 0 0 Y
                                                  };
        private readonly int[,] ShapeMappingDictionary180 = {
                                                    {15,14,13,12},    //0 1 0 0
                                                    {11,10, 9, 8},    //0 1 0 0
                                                    { 7, 6, 5, 4},    //0 1 0 0
                                                    { 3, 2, 1, 0}     //0 1 0 0
                                                  };
        private readonly int[,] ShapeMappingDictionary270 = {
                                                    { 3, 7,11,15},   //0 0 0 0
                                                    { 2, 6,10,14},   //1 1 1 1
                                                    { 1, 5, 9,13},   //0 0 0 0
                                                    { 0, 4, 8,12}    //0 0 0 0
                                                  };
        public int[] NinetyDegreeSpin(int PositionX, int PositionY, int CurrentRotation)
        {
            int FutureBlockPosition;
            int[] XYPOS = { 0, 0 };
            switch (CurrentRotation % 3)
            {
                case 0:
                    FutureBlockPosition = PositionY * 4 + PositionX;
                    for (int i = 0; i <= WIDTH; i++)
                    {
                        for (int j = 0; j <= HEIGHT; j++)
                        {
                            if (ShapeMappingDictionary90[j, i] == FutureBlockPosition)
                            {
                                XYPOS[0] = i;//column
                                XYPOS[1] = j;//row
                                return XYPOS;
                            }
                        }
                    }
                    break;
                case 1:
                    FutureBlockPosition = 12 + PositionY - (PositionX * 4);
                    for (int i = 0; i <= WIDTH; i++)
                    {
                        for (int j = 0; j <= HEIGHT; j++)
                        {
                            if (ShapeMappingDictionary180[j, i] == FutureBlockPosition)
                            {
                                XYPOS[0] = i;//column
                                XYPOS[1] = j;//row
                                return XYPOS;
                            }
                        }
                    }
                    break;
                case 2:
                    FutureBlockPosition = 15 - (PositionY * 4) - PositionX;
                    for (int i = 0; i <= WIDTH; i++)
                    {
                        for (int j = 0; j <= HEIGHT; j++)
                        {
                            if (ShapeMappingDictionary270[j, i] == FutureBlockPosition)
                            {
                                XYPOS[0] = i;//column
                                XYPOS[1] = j;//row
                                return XYPOS;
                            }
                        }
                    }
                    break;
                case 3:
                    FutureBlockPosition = 3 + PositionY + (PositionX * 4);
                    for (int i = 0; i <= WIDTH; i++)
                    {
                        for (int j = 0; j <= HEIGHT; j++)
                        {
                            if (ShapeMappingDictionary0[j, i] == FutureBlockPosition)
                            {
                                XYPOS[0] = i;//column
                                XYPOS[1] = j;//row
                                return XYPOS;
                            }
                        }
                    }
                    break;
            }
            return null;
        }
    }
}
