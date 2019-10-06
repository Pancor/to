using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pancor_cw1
{
    public partial class FirstEx : Form
    {
        double accuracy = 0.00000001;

        public FirstEx()
        {
            InitializeComponent();
            setupListeners();
        }

        private void setupListeners()
        {
            countBtn.Click += new EventHandler(countBtn_Click);
            clearBtn.Click += new EventHandler(clearBtn_Click);
        }

        private void clearBtn_Click(object sender, System.EventArgs e)
        {
            xView.Clear();
            dView.Clear();
            resultView.Clear();
            properResultView.Clear();
        }

        private void countBtn_Click(object sender, System.EventArgs e)
        {
            operateMathOperatorsGroup();
        }

        private void operateMathOperatorsGroup()
        {
            if (sqrtRB.Checked)
            {
                countProperSqrt();
                countSqrt();
            }
            else if (expRB.Checked)
            {
                countProperExp();
                countExp();
            }
            else if (sinRB.Checked)
            {
                countProperSin();
                countSin();
            }
            else if (cosRB.Checked)
            {
                countProperCos();
                countCos();
            }
        }

        private void countProperSqrt()
        {
            double x = Convert.ToDouble(xView.Text);
            double result = Math.Sqrt(x);
            properResultView.Text = result.ToString();
        }

        private void countSqrt()
        {
            resultView.Clear();
            double x = Convert.ToDouble(xView.Text);
            double d = Math.Floor(Math.Log10(x) + 1);
            double m = 0;
            if (d % 2 == 0) {
                m = (d - 2) / 2;
            } else {
                m = (d - 1) / 2;
            }
            double result = 2 * Math.Pow(10, m);
            double previousResult = 0;
            int iterationNumber = 0;
            do {
                previousResult = result;
                result = 0.5 * (x/result + result);
                resultView.Text += "Pętla nr " + iterationNumber + " :" + result + Environment.NewLine;
                iterationNumber++;
            } while (Math.Abs(result - previousResult) >= accuracy);
        }

        private void countProperExp()
        {
            double x = Convert.ToDouble(xView.Text);
            double result = Math.Exp(x);
            properResultView.Text = result.ToString();
        }

        private void countExp()
        {
            resultView.Clear();
            double x = Convert.ToDouble(xView.Text);
            double result = 1;
            double previousResult = 0;
            int iterationNumber = 0;
            do {
                if (iterationNumber != 0)
                {
                    previousResult = result;
                    result += Math.Pow(x, iterationNumber) / Tools.Instance.Factorial(iterationNumber);
                }
                resultView.Text += "Pętla nr " + iterationNumber + " :" + result + Environment.NewLine;
                iterationNumber++;
            } while (Math.Abs(result - previousResult) >= accuracy);
        }

        private void countProperSin()
        {
            double d = Convert.ToDouble(dView.Text);
            d = (d * Math.PI) / 180;
            double result = Math.Sin(d);
            properResultView.Text = result.ToString();
        }

        private void countSin()
        {
            resultView.Clear();
            double d = Convert.ToDouble(dView.Text);
            d = (d*Math.PI) / 180;
            while (d > Math.PI) {
                d -= 2 * Math.PI;
            }
            double result = 0;
            double previousResult = 0;
            int iterationNumber = 0;
            int doubleStep = 1;
            do {
                previousResult = result;
                if (iterationNumber % 2 == 0) {
                    result += Math.Pow(d, doubleStep) / Tools.Instance.Factorial(doubleStep);
                } else {
                    result -= Math.Pow(d, doubleStep) / Tools.Instance.Factorial(doubleStep);
                }
                resultView.Text += "Pętla nr " + iterationNumber + " :" + result + Environment.NewLine;
                doubleStep += 2;
                iterationNumber++;
            } while (Math.Abs(result - previousResult) >= accuracy);
        }

        private void countProperCos()
        {
            double d = Convert.ToDouble(dView.Text);
            d = (d * Math.PI) / 180;
            double result = Math.Cos(d);
            properResultView.Text = result.ToString();
        }

        private void countCos()
        {
            resultView.Clear();
            double d = Convert.ToDouble(dView.Text);
            d = (d * Math.PI) / 180;
            while (d > Math.PI) {
                d -= 2 * Math.PI;
            }
            double result = 0;
            double previousResult = 0;
            int iterationNumber = 0;
            int doubleStep = 0;
            do
            {
                previousResult = result;
                if (iterationNumber % 2 == 0)
                {
                    if (doubleStep != 0)
                    {
                        result += Math.Pow(d, doubleStep) / Tools.Instance.Factorial(doubleStep);
                    } else {
                        result = 1;
                    }
                }
                else
                {
                    result -= Math.Pow(d, doubleStep) / Tools.Instance.Factorial(doubleStep);
                }
                resultView.Text += "Pętla nr " + iterationNumber + " :" + result + Environment.NewLine;
                doubleStep += 2;
                iterationNumber++;
            } while (Math.Abs(result - previousResult) >= accuracy);
        }
    }
}
