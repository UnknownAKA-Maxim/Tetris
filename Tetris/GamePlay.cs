using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GamePlay : Form
    {
        const int WIDTH = 10, HEIGHT = 23;
        private Tile[,] Board2 = new Tile[WIDTH, HEIGHT];
        private Tile[,] Board1 = new Tile[WIDTH, HEIGHT];
        private Tile[,] HeldValue1 = new Tile[4, 4];
        private Tile[,] HeldValue2 = new Tile[4, 4];
        static Controller PlayerOne = new Controller();
        static Controller PlayerTwo = new Controller();
        static TetrisAIProcess AIPlayer = new TetrisAIProcess(PlayerTwo);
        private int GarbageSent1 = 0, GarbageSent2 = 0;
        private bool ButtonHeld = false;
        private string LastButtonPressed = "";
        private string aIDecision = "";
        private bool StartGame = false;
        private int DelayAutoShift = 4/*20ms*/,DASTimeElapsed = 0;
        private int SoftDropRepeatRate = 1/*10ms*/, SFDTimeElapsed = 0;
        private int AutoRepeatRate = 2, AutoRepeatRateElapsed = 0;
        MenusScreen Menu;
        private bool MenuHasBeenDisplayed = false;
        private int DifficultyMode = 0,DifficultyModeTickDelay;
        private void GarbageBufferTick(object sender, EventArgs e)
        {
            if (StartGame)
            {
                GarbageSent2 = PlayerOne.NumberOfGarbageSent(PlayerTwo.GarbageCanBeReceived);
                GarbageSent1 = PlayerTwo.NumberOfGarbageSent(PlayerOne.GarbageCanBeReceived);
                PlayerOne.AddGarbageToBoard(GarbageSent1);
                if (GarbageSent1 == 0)
                {
                    PlayerOne.SoftDrop();
                }
                else
                {
                    GarbageSent1 = 0;
                    PlayerOne.GarbageCanBeReceived = false;
                }
                PlayerTwo.AddGarbageToBoard(GarbageSent2);
                if (GarbageSent2 == 0)
                {
                    PlayerTwo.SoftDrop();
                }
                else
                {
                    GarbageSent2 = 0;
                    PlayerTwo.GarbageCanBeReceived = false;
                }
                ProcessBoard();
            }
        }
        private void ProcessBoard()
        {
            //PlayerOne.DisplayPieceShadow();
            Board1 = PlayerOne.GridDrawer(ref StartGame);
            HeldValue1 = PlayerOne.DrawHeldShape();
            Board2 = PlayerTwo.GridDrawer(ref StartGame);
            HeldValue2 = PlayerTwo.DrawHeldShape();
            this.Invalidate();
            this.Update();
        }

        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartGame = true;
                PlayerOne.Run();
                PlayerTwo.Run();
                AutoRepeatRate = Menu.ARR;
                DelayAutoShift = Menu.DAS;
                SoftDropRepeatRate = Menu.SFD;
                DifficultyMode = Menu.DifficultyMode*2;
                Menu.Close();
            }
            if (StartGame)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        PlayerOne.SpinShape();
                        break;
                    case Keys.Down:
                        LastButtonPressed = "down";
                        ButtonHeld = true;
                        break;
                    case Keys.Left:
                        if (!ButtonHeld)
                        {
                            PlayerOne.IncrementLeft();
                        }
                        LastButtonPressed = "left";
                        ButtonHeld = true;
                        break;
                    case Keys.Right:
                        if (!ButtonHeld)
                        {
                            PlayerOne.IncrementRight();
                        }
                        LastButtonPressed = "right";
                        ButtonHeld = true;
                        break;
                    case Keys.Space:
                        if (!ButtonHeld)
                        {
                            PlayerOne.HardDrop();
                        }
                        LastButtonPressed = "space";
                        ButtonHeld = true;
                        break;
                    case Keys.ShiftKey:
                        PlayerOne.Hold();
                        break;
                }
                ProcessBoard();
            }
        }

        private void KeyUpEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Space:
                    ButtonHeld = false;
                    break;
                case Keys.Left:
                case Keys.Right:
                    ButtonHeld = false;
                    DASTimeElapsed = 0;
                    break;
            }
        }
        public GamePlay()
        {
            this.BackColor = System.Drawing.Color.White;
            PlayerOne.Run();
            PlayerTwo.Run();//AI
            InitializeComponent();
        }

        private void AIPathFindingTickEvent(object sender, EventArgs e)
        {
            if(StartGame)
            {
                if (DifficultyModeTickDelay == 0)
                {
                    if (aIDecision == "")
                    {
                        AIPlayer.UpdateControllerClass(PlayerTwo);
                        aIDecision = AIPlayer.PathToDecision();
                    }
                    if (aIDecision[0] == 'H')
                    {
                        PlayerTwo.HardDrop();
                        aIDecision = "";
                    }
                    else
                    {
                        if (aIDecision[0] == 'W')
                        {
                            PlayerTwo.SpinShape();
                            aIDecision = aIDecision.Substring(1, aIDecision.Length - 1);
                        }
                        if (aIDecision[0] == 'L')
                        {
                            PlayerTwo.IncrementLeft();
                            aIDecision = aIDecision.Substring(1, aIDecision.Length - 1);
                        }
                        if (aIDecision[0] == 'R')
                        {
                            PlayerTwo.IncrementRight();
                            aIDecision = aIDecision.Substring(1, aIDecision.Length - 1);
                        }
                    }
                    DifficultyModeTickDelay = DifficultyMode;
                }
                else
                {
                    DifficultyModeTickDelay--;
                }
            }
        }

        private void PlayerSensitivityTick(object sender, EventArgs e)
        {
            if (ButtonHeld)
            {
                if (DelayAutoShift == DASTimeElapsed)
                {
                    if (AutoRepeatRate == AutoRepeatRateElapsed)
                    {
                        switch (LastButtonPressed)
                        {
                            case "left":
                                PlayerOne.IncrementLeft();
                                ProcessBoard();
                                break;
                            case "right":
                                PlayerOne.IncrementRight();
                                ProcessBoard();
                                break;
                        }
                        AutoRepeatRateElapsed = 0;
                    }
                    else AutoRepeatRateElapsed++;
                }
                else DASTimeElapsed++;
                if (SoftDropRepeatRate == SFDTimeElapsed)
                {
                    if (LastButtonPressed == "down")
                    {
                        PlayerOne.SoftDrop();
                        ProcessBoard();
                        SFDTimeElapsed = 0;
                    }
                }
                else SFDTimeElapsed++;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DoubleBuffered = true;
            const int SPACEBETWEENSQUARES = 6;
            const int SQUARESIZE = 20;
            const int BOARDOFFSET1X = 128, BOARDOFFSET1Y = 5, HELD1OFFSETX = -30, HELD1OFFSETY = 5, NEXTSHAPEDISPLAYX = 388,NEXTSHAPEDISPLAYY = 5;
            const int BOARDOFFSET2X = 720, BOARDOFFSET2Y = 5, HELD2OFFSETX = 560, HELD2OFFSETY = 5;
            //BoardOne Stuff
            int board1PositionX = BOARDOFFSET1X;
            int board1PositionY = BOARDOFFSET1Y;
            int held1PositionX = HELD1OFFSETX;
            int held1PositionY = HELD1OFFSETY;
            int NextShapeDisplayX = NEXTSHAPEDISPLAYX;
            int NextShapeDisplayY = NEXTSHAPEDISPLAYY;
            //BoardTwo Stuff
            int board2PositionX = BOARDOFFSET2X;
            int board2PositionY = BOARDOFFSET2Y;
            int held2PositionX = HELD2OFFSETX;
            int held2PositionY = HELD2OFFSETY;
            Rectangle[,] square = new Rectangle[WIDTH, HEIGHT];
            Rectangle[,] heldSquare = new Rectangle[4, 4];
            for (int i = HEIGHT - 1; i >= 0; i--)
            {
                for (int j = WIDTH - 1; j >= 0; j--)
                {
                    //BoardOne
                    Brush b = new SolidBrush(GetColour(Board1[j, i]));
                    board1PositionX += SPACEBETWEENSQUARES + SQUARESIZE;
                    square[j, i] = new Rectangle(board1PositionX, board1PositionY, SQUARESIZE, SQUARESIZE);
                    e.Graphics.FillRectangle(b, square[j, i]);

                    //BoardTwo
                    Brush c = new SolidBrush(GetColour(Board2[j, i]));
                    board2PositionX += SPACEBETWEENSQUARES + SQUARESIZE;
                    square[j, i] = new Rectangle(board2PositionX, board2PositionY, SQUARESIZE, SQUARESIZE);
                    e.Graphics.FillRectangle(c, square[j, i]);
                }
                //BoardOne
                board1PositionY += SPACEBETWEENSQUARES + SQUARESIZE;
                board1PositionX = BOARDOFFSET1X;
                //BoardTwo
                board2PositionY += SPACEBETWEENSQUARES + SQUARESIZE;
                board2PositionX = BOARDOFFSET2X;
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //
                    Brush b = new SolidBrush(GetColour(HeldValue1[j, i]));
                    held1PositionX += SQUARESIZE + SPACEBETWEENSQUARES;
                    heldSquare[j, i] = new Rectangle(held1PositionX, held1PositionY, SQUARESIZE, SQUARESIZE);
                    e.Graphics.FillRectangle(b, heldSquare[j, i]);
                    //
                    Brush c = new SolidBrush(GetColour(HeldValue2[j, i]));
                    held2PositionX += SQUARESIZE + SPACEBETWEENSQUARES;
                    heldSquare[j, i] = new Rectangle(held2PositionX, held2PositionY, SQUARESIZE, SQUARESIZE);
                    e.Graphics.FillRectangle(c, heldSquare[j, i]);
                }
                held1PositionY += SPACEBETWEENSQUARES + SQUARESIZE;
                held1PositionX = HELD1OFFSETX;
                held2PositionY += SPACEBETWEENSQUARES + SQUARESIZE;
                held2PositionX = HELD2OFFSETX;
            }
            if (!StartGame && !MenuHasBeenDisplayed)
            {
                Menu = new MenusScreen();
                Menu.Show();
                MenuHasBeenDisplayed = true;
            }
        }

        private static Color GetColour(Tile shape)
        {
            switch (shape)
            {
                case Tile.I: return Color.FromArgb(255, 145, 180);
                case Tile.O: return Color.FromArgb(255, 217, 118);
                case Tile.S: return Color.FromArgb(150, 144, 255);
                case Tile.Z: return Color.LightBlue;
                case Tile.L: return Color.FromArgb(150, 211, 236);
                case Tile.J: return Color.Chartreuse;
                case Tile.T: return Color.DarkOrchid;
                    //placed Value Colours
                case Tile.PlacedI: return Color.FromArgb(255, 145, 180);
                case Tile.PlacedO: return Color.FromArgb(255, 217, 118);
                case Tile.PlacedS: return Color.FromArgb(150, 144, 255);
                case Tile.PlacedZ: return Color.LightBlue;
                case Tile.PlacedL: return Color.FromArgb(150, 211, 236);
                case Tile.PlacedJ: return Color.Chartreuse;
                case Tile.PlacedT: return Color.DarkOrchid;
                case Tile.Shadow: return Color.Gray;
                    //Emptyspace
                default:
                    return Color.Black;
            }
        }
    }
}