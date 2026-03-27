using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenusScreen : Form
    {
        private int dAS = 1,sFD = 1,aRR = 1;
        private int difficultyMode = 0;
        public MenusScreen()
        {
            InitializeComponent();
        }
        public int DifficultyMode
        {
            get { return difficultyMode; }
            set { difficultyMode = value; }
        }
        public int DAS
        {
            get { return dAS; }
            set { dAS = value; }
        }
        public int SFD
        {
            get { return sFD; }
            set { sFD = value; }
        }
        public int ARR
        {
            get { return aRR; }
            set { aRR = value; }
        }
        private void DASSensitivity(object sender, EventArgs e)
        {
            DAS = DelayAutoShiftTrackBar.Value;//DelayAutoShiftTrackBar
            label5.Text = $"DAS:{dAS}0ms ARR:{aRR}0ms SFD:{sFD}0ms";
            this.Invalidate();
            this.Update();
        }
        private void StartButton(object sender, EventArgs e)
        { 
            this.Close();
        }

        private void DefaultSettings(object sender, EventArgs e)
        {
            ARR = 3;
            DAS = 5;
            SFD = 2;
            label5.Text = $"DAS:{dAS}0ms ARR:{aRR}0ms SFD:{sFD}0ms";
        }

        private void MenusScreen_Load(object sender, EventArgs e)
        {

        }

        private void NoviceMode(object sender, EventArgs e)
        {
            DifficultyMode = 3;
        }

        private void MediumMode(object sender, EventArgs e)
        {
            DifficultyMode = 2;
        }

        private void HardMode(object sender, EventArgs e)
        {
            DifficultyMode = 0;
        }

        private void ARRSensitivity(object sender, EventArgs e)
        {
            ARR = AutoRepeatRateTrackBar.Value;//AutoRepeatRateTrackBar
            label5.Text = $"DAS:{dAS}0ms ARR:{aRR}0ms SFD:{sFD}0ms";
            this.Invalidate();
            this.Update();
        }
        private void SFDSensitivity(object sender, EventArgs e)
        {
            SFD =SoftDropTrackBar.Value;
            label5.Text = $"DAS:{dAS}0ms ARR:{aRR}0ms SFD:{sFD}0ms";
            this.Invalidate();
            this.Update();
        }
    }
}
