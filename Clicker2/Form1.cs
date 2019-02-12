using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Clicker2
{
    // Ethan Shipley
    // December 7, 2018
    // Simple auto clicker for user
    // program does not contain catches for input verification
    // created for fun use case of the timer
    // use at own risk
    // Inputs:
    // Accepts a timed delay before starting
    // how long the program is to run for
    // interval time between clicks
    public partial class frmClicker : Form
    {
        // set the mouse variables, counters and limits
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        int counterTime = 0;
        int timeRun;
        int counterDelay = 0;
        int delayTime;

        public frmClicker()
        {
            InitializeComponent();
        }

        // perform the mouse click on the existing mouse location
        public void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        // starts the clicker upon start button click
        private void btnStart_Click(object sender, EventArgs e)
        {
            int tmr;
            // gets the length of time program is to run for
            timeRun = Convert.ToInt32(txtTime.Text);
            // gets the delay before the program is to begin clicking
            delayTime = Convert.ToInt32(txtDelay.Text);
            //  gets the interval between left button clicks
            tmrInterval.Interval = Convert.ToInt32(txtInterval.Text);
            // delays the program based on the delay limit and enables the timers
            Thread.Sleep(delayTime * 1000);
            tmrTime.Enabled = true;
            tmrInterval.Enabled = true;
            
            tmr = tmrInterval.Interval;
            
        }

        // upon each interval timer tick perform the mouse click
        private void tmrInterval_Tick(object sender, EventArgs e)
        {
            tmrInterval.Interval = Convert.ToInt32(txtInterval.Text);
            DoMouseClick();
        }

        // upon Time timer tick increase the Time counter. When counterTime equals timer run the program stops clicking
        private void tmrTime_Tick(object sender, EventArgs e)
        {
            counterTime++;
            // if counterTime equals timeRun then clicking is stopped 
            if (counterTime == timeRun)
            {
                counterTime = 0;
                tmrInterval.Enabled = false;
            }
        }

        //// upon Delay timer tick the counter delay is increased and when it equals delay time it is then reset to zero
        //private void tmrDelay_Tick(object sender, EventArgs e)
        //{
        //    counterDelay++;
        //    if (counterDelay == delayTime)
        //    {
        //        counterDelay = 0;
        //    }
        //}

        // ends the program from running when clicked
        private void btnEnd_Click(object sender, EventArgs e)
        {
            tmrInterval.Enabled = false;
            tmrTime.Enabled = false;
            tmrTime.Enabled = false;
        }
    }
}
