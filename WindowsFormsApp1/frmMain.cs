using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class FrmMain : Form
    {
        //Global
        Random RanIcon = new Random();
        List<string> lisIcon = new List<string>
            {
                "!", "!",
                "N", "N",
                ",", ",",
                "k", "k",
                "b", "b",
                "v", "v",
                "w", "w",
                "z", "z",
            };
        List<Label> labels;
        Label firstSelected;
        Label secondSelected = null;
        int counter = 0;
        int correct = 0;

        public FrmMain()
        {
            InitializeComponent();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            labels = tblMain.Controls.OfType<Label>().ToList();
            foreach (var label in labels)
            {
                int At = RanIcon.Next(lisIcon.Count);
                label.Text = lisIcon[At];
                lisIcon.RemoveAt(At);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            //Avoid user to select third label
            if (timer.Interval == 750)
                return;

            Label Selected = (Label)sender;

            if (firstSelected != null)
            {
                timer.Interval = 750;
                secondSelected = Selected;
                Selected.BackColor = Color.Yellow;
                Selected.ForeColor = Color.Black;
            }
            // User doesnt select any label
            else if (firstSelected == null)
            {
                Selected.BackColor = Color.Yellow;
                Selected.ForeColor = Color.Black;
                timer.Start();
                timer.Interval = 2 * 1000;

                firstSelected = Selected;
                secondSelected = null;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Interval = 100;
            if (secondSelected != null)
            {
                counter++;

                if (secondSelected.Text == firstSelected.Text)
                {
                    correct++;

                    firstSelected.BackColor = Color.Green;
                    secondSelected.BackColor = Color.Green;

                    firstSelected.ForeColor = Color.Black;
                    secondSelected.ForeColor = /*Fore color should be as same as Back color*/ firstSelected.ForeColor;
                    if(correct == labels.Count /2)
                    {
                        MessageBox.Show($"You win \nwith {counter} try");
                    }
                }
                else
                {
                    firstSelected.BackColor = Color.PowderBlue;
                    secondSelected.BackColor = firstSelected.BackColor;

                    firstSelected.ForeColor = firstSelected.BackColor;
                    secondSelected.ForeColor = /*Fore color should be as same as Back color*/ firstSelected.ForeColor;
                }
            }
            else
            {
                firstSelected.BackColor = Color.PowderBlue;

                firstSelected.ForeColor = firstSelected.BackColor;
            }

            secondSelected = firstSelected = null;
        }
    }
}
