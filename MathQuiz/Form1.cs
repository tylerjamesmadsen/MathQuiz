using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random randomizer = new Random();

        // addition variables
        int addend1;
        int addend2;

        // subtraction variables
        int minuend;
        int subtrahend;

        // multiplication variables
        int multiplicand;
        int multiplier;

        // division variables
        int dividend;
        int divisor;

        // timer variable
        int timeLeft;

        /// <summary>
        /// Check the answer
        /// </summary>
        /// <returns>True if correct, false if incorrect.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Start the quiz: fill in all the problems
        /// and start the timer.
        /// </summary>
        public void StartTheQuiz()
        {
            // initialize addition problem
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // initialize subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // initialize multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // initialize division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();

            // initialize timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                timeLabel.BackColor = Color.LightGreen;
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                timeLabel.BackColor = Color.Empty;
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft == 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                timeLabel.BackColor = Color.Empty;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Windows\media\tada.wav");

        void playSumHint(object sender, EventArgs e)
        {
            if (addend1 + addend2 == sum.Value)
            {
                player.Play();
            }
        }

        void playDifferenceHint(object sender, EventArgs e)
        {
            if (minuend - subtrahend == difference.Value)
            {
                player.Play();
            }
        }

        void playProductHint(object sender, EventArgs e)
        {
            if (multiplicand * multiplier == product.Value)
            {
                player.Play();
            }
        }

        void playQuotientHint(object sender, EventArgs e)
        {
            if (dividend / divisor == quotient.Value)
            {
                player.Play();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }
    }
}




