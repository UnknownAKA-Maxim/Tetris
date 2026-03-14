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
        private int dAS,sFD,aRR;
        public MenusScreen()
        {
            InitializeComponent();
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
            aRR = AutoRepeatRateTrackBar.Value;
        }
        private void StartButton(object sender, EventArgs e)
        { 
            this.Close();
        }
        private void ARRSensitivity(object sender, EventArgs e)
        {
            dAS = DelayAutoShiftTrackBar.Value;
        }
        private void SFDSensitivity(object sender, EventArgs e)
        {
            sFD =SoftDropTrackBar.Value;
        }
    }
}
