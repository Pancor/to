using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pancor_cw5_v1
{
    public partial class Form1 : Form
    {

        private int minValue=-10, maxValue=10;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            aBar.ValueChanged += new EventHandler(aBar_ValueChanged);
            bBar.ValueChanged += new EventHandler(bBar_ValueChanged);
            checkAllCB.CheckedChanged += new EventHandler(checkAllCB_CheckedChanged);
            countBtn.Click += new EventHandler(countBtn_Click);
            firstFuncRB.CheckedChanged += new EventHandler(otherFunctionRB_CheckedChanged);
            secondFuncRB.CheckedChanged += new EventHandler(secondFuncRB_CheckedChanged);
            thirdFuncRB.CheckedChanged += new EventHandler(otherFunctionRB_CheckedChanged);
        }

        private void aBar_ValueChanged(object sender, System.EventArgs e)
        {
            minValue = aBar.Value;
            minText.Text = minValue.ToString();
        }

        private void bBar_ValueChanged(object sender, System.EventArgs e)
        {
            maxValue = bBar.Value;
            maxText.Text = maxValue.ToString();
        }

        private void checkAllCB_CheckedChanged(Object sender, EventArgs e)
        {
            if (checkAllCB.Checked)
            {
                rectangleCB.Checked = true;
                trapezeCB.Checked = true;
                simpsonCB.Checked = true;
            } else {
                rectangleCB.Checked = false;
                trapezeCB.Checked = false;
                simpsonCB.Checked = false;
            }
        }

        private void secondFuncRB_CheckedChanged(Object sender, EventArgs e)
        {
            setupValues(180, 360);
        }

        private void otherFunctionRB_CheckedChanged(Object sender, EventArgs e)
        {
            setupValues(10, 20);
        }

        private void countBtn_Click(Object sender, EventArgs e)
        {
            rectangleResultText.Clear();
            trapezeResultText.Clear();
            simpsonResultText.Clear();
            functionChart.Series.Clear();

            Func<double, double> f = firstFunction;
            if (secondFuncRB.Checked)
            {
                f = secondFunction;
            } else if (thirdFuncRB.Checked)
            {
                f = thirdFunction;
            }

            drawChart(f);

            if (rectangleCB.Checked)
            {
                countByRectangleMethod(f);
            }
            if (trapezeCB.Checked)
            {
                countByTrapezeMethod(f);
            } 
            if (simpsonCB.Checked)
            {
                countBySimpsonsMethod(f);
            }   
        }

        private void countByRectangleMethod(Func<double, double> f)
        {
            double m = double.Parse(mText.Text);
            double h = (maxValue - minValue) / m;
            double result = 0;

            for (int i = 0; i < m; i++)
            {
                result += h * f(minValue + i*h);
            }
            rectangleResultText.Text = result.ToString();
        }

        private void countByTrapezeMethod(Func<double, double> f)
        {
            double m = double.Parse(mText.Text);
            double h = (maxValue - minValue) / m;
            double result = 0;

            for (int i = 0; i < m-1; i++)
            {
                result += (f(minValue + i * h) + f(minValue + i * h + h)) * h / 2;
            }
            trapezeResultText.Text = result.ToString();
        }

        private void countBySimpsonsMethod(Func<double, double> f)
        {
            double m = double.Parse(mText.Text);
            double h = (maxValue - minValue) / m;
            double result = 0;

            for (int i = 0; i < m/2 - 1; i++)
            {
                result += (h/3) * (f(minValue + 2*i*h) + 4 * f(minValue + h + 2*i*h) +
                    f(minValue + 2*h + 2*i*h));
            }

            simpsonResultText.Text = result.ToString();
        }

        private double firstFunction(double x)
        {
            return -5 * Math.Pow(x, 4) + 3 * Math.Pow(x, 3) - 3 * Math.Pow(x, 2) + 2;
        }

        private double secondFunction(double fi)
        {
            double rad = fi * Math.PI / 180;
            return 5 * Math.Sin(rad) * Math.Cos(rad) - Math.Cos(rad);
        }

        private double thirdFunction(double x)
        {
            return Math.Pow(Math.E, -x) + Math.Pow(x, 5);
        }

        private void drawChart(Func<double, double> f)
        {
            double m = double.Parse(mText.Text);
            double h = (maxValue - minValue) / m;

            Series series = functionChart.Series.Add("");
            series.ChartType = SeriesChartType.RangeColumn;
            series.IsVisibleInLegend = false;

            for (double i = minValue; i < maxValue; i += h)
            {
                series.Points.AddXY(i, f(i));
            }
        }

        private void setupValues(int value, int range)
        {
            aBar.Minimum = -range;
            aBar.Maximum = range;
            bBar.Minimum = -range;
            bBar.Maximum = range;
            minValue = -value;
            maxValue = value;
            aBar.Value = -value;
            bBar.Value = value;
            minText.Text = minValue.ToString();
            maxText.Text = maxValue.ToString();
        }
    }
}
