using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pancor_cw2_v1
{
    public partial class Form1 : Form
    {
        private double aRange = 0;
        private double bRange = 0;
        private double accuarcy = 0.0000001;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            resultBtn.Click += new EventHandler(ResultBtn_Click);
            newtonMethodRB.Click += new EventHandler(NewtonMethodRB_Click);
            falsiMethodRB.Click += new EventHandler(OtherMethodRB_Click);
            bisectionMethodRB.Click += new EventHandler(OtherMethodRB_Click);
            aRangeBar.ValueChanged += new EventHandler(aRangeBar_ValueChanged);
            bRangeBar.ValueChanged += new EventHandler(bRangeBar_ValueChanged);
            clearBtn.Click += new EventHandler(ClearBtn_Click);
        }

        private void ResultBtn_Click(object sender, EventArgs e)
        {
            resultView.Clear();
            if (newtonMethodRB.Checked)
            {
                FindResultByNewtonMethod();
            }
            else if (falsiMethodRB.Checked)
            {
                FindResultByFalsiMethod();
            }
            else if (bisectionMethodRB.Checked)
            {
                FindResultByBisectionMethod();
            }
        }

        private void NewtonMethodRB_Click(object sender, EventArgs e)
        {
            resultView.Clear();
            newtonObjectsView.Visible = true;
            otherObjectsView.Visible = false;
        }

        private void OtherMethodRB_Click(object sender, EventArgs e)
        {
            resultView.Clear();
            newtonObjectsView.Visible = false;
            otherObjectsView.Visible = true;
        }

        private void aRangeBar_ValueChanged(object sender, EventArgs e)
        {
            aRange = aRangeBar.Value;
            aRangeView.Text = "a = " + aRange;
        }

        private void bRangeBar_ValueChanged(object sender, EventArgs e)
        {
            bRange = bRangeBar.Value;
            bRangeView.Text = "b = " + bRange;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            resultView.Clear();
        }

        private void FindResultByNewtonMethod()
        {
            double result = 0;
            double previousResult = double.Parse(x0View.Text);
            int iterationNumber = 0;
            do {
                result = previousResult - 
                    (getMainFunctionResult(previousResult) / getDerivativeFunctionResult(previousResult));
                previousResult = result;
                resultView.Text += GetFormatedResult(iterationNumber, result, getMainFunctionResult(result));
                iterationNumber++;
            } while (Math.Abs(getMainFunctionResult(result)) > accuarcy);
        }

        private void FindResultByFalsiMethod()
        {
            double a = aRange, b = bRange, result;
            int iterationNumber = 0;
            do
            {
                if (!IsRangeOk(a, b)) { return; }
                result = a - (getMainFunctionResult(a) * 
                    ((b - a) / (getMainFunctionResult(b) - getMainFunctionResult(a))));
                if (getMainFunctionResult(a) * getMainFunctionResult(result) < 0)
                {
                    b = result;
                }
                else
                {
                    a = result;
                }
                resultView.Text += GetFormatedResult(iterationNumber, result, getMainFunctionResult(result));
                iterationNumber++;
            } while (Math.Abs(getMainFunctionResult(result)) > accuarcy);
        }

        private void FindResultByBisectionMethod()
        {
            double a = aRange, b = bRange;
            double result, avarage;
            int iterationNumber = 0;
            do
            {
                if (!IsRangeOk(a, b)){ return; }
                avarage = (a + b) / 2;
                result = getMainFunctionResult(avarage);
                if (result * getMainFunctionResult(a) < 0)
                {
                    b = avarage;
                } else {
                    a = avarage;
                }
                resultView.Text += GetFormatedResult(iterationNumber, avarage, result);
                iterationNumber++;
            } while (Math.Abs(a - b) > accuarcy);
        }

        private double getMainFunctionResult(double x)
        {
            double A = double.Parse(AView.Text);
            double B = double.Parse(BView.Text);
            double C = double.Parse(CView.Text);
            double D = double.Parse(DView.Text);
            return A * Math.Pow(x, 3) + B * Math.Pow(x, 2) + C * x + D;
        }

        private double getDerivativeFunctionResult(double x)
        {
            double A = double.Parse(AView.Text);
            double B = double.Parse(BView.Text);
            double C = double.Parse(CView.Text);
            double D = double.Parse(DView.Text);
            return 3 * A * Math.Pow(x, 2) + 2 * B * x  + C;
        }

        private String GetFormatedResult(int iterationNumber, double zeroPlace, double functionResult)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Iteracja nr: " + iterationNumber);
            result.Append(" Miejsce zerowe: " + zeroPlace);
            result.AppendLine();
            result.Append("                      Wartość funkcji f(x)=" + functionResult);
            result.AppendLine();
            result.AppendLine();
            return result.ToString();
        }

        private bool IsRangeOk(double a, double b)
        {
            double aResult = getMainFunctionResult(a);
            double bResult = getMainFunctionResult(b);
            if (aResult * bResult > 0)
            {
                MessageBox.Show("f(a) * f(b) > 0");
                return false;
            }
            return true;
        }
    }
}
