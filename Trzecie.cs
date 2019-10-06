using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pancor_cw3_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            clearBtn.Click += new EventHandler(clearBtn_Click);
            countBtn.Click += new EventHandler(countBtn_Click);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            resultChart.Series.Clear();
        }

        private void countBtn_Click(object sender, EventArgs e)
        {
            resultChart.Series.Clear();
            DoProperFunction();
            DoEuler();
            DoEulerCauchy();
        }

        private void DoProperFunction()
        {
            int i = GetNumberOfIterations();
            double h = double.Parse(hView.Text);
            double[] x = new double[i];
            double[] t = new double[i];
            Series series = resultChart.Series.Add("Main function");
            series.ChartType = SeriesChartType.Spline;

            x[0] = double.Parse(x0View.Text);
            t[0] = 0;
            series.Points.AddXY(t[0], x[0]);

            for (int j = 1; j < i; j++)
            {
                t[j] = t[j - 1] + h;
                x[j] = Math.Exp((-1) * Math.Pow(t[j], 2)) * (0.5 * Math.Pow(t[j], 2) + x[0]);
                series.Points.AddXY(t[j], x[j]);
            }
        }

        private void DoEuler()
        {
            int i = GetNumberOfIterations();
            double h = double.Parse(hView.Text);
            double[] x = new double[i];
            double[] t = new double[i];
            Series series = resultChart.Series.Add("Euler function");
            series.ChartType = SeriesChartType.Spline;

            x[0] = double.Parse(x0View.Text);
            t[0] = 0;
            series.Points.AddXY(t[0], x[0]);

            for (int j = 1; j < i; j++)
            {
                t[j] = t[j - 1] + h;
                x[j] = x[j - 1] + h * GetMainFunctionResult(x[j - 1], t[j - 1]);
                series.Points.AddXY(t[j], x[j]);
            }
        }

        private void DoEulerCauchy()
        {
            int i = GetNumberOfIterations();
            double h = double.Parse(hView.Text);
            double[] x = new double[i];
            double[] t = new double[i];
            Series series = resultChart.Series.Add("Euler-Cauchy function");
            series.ChartType = SeriesChartType.Spline;

            x[0] = double.Parse(x0View.Text);
            t[0] = 0;
            series.Points.AddXY(t[0], x[0]);

            for (int j = 1; j < i; j++)
            {
                t[j] = t[j - 1] + h;
                x[j] = x[j - 1] + h * GetMainFunctionResult(
                    x[j - 1] + (h / 2) * GetMainFunctionResult(x[j - 1], t[j - 1]), 
                    t[j - 1] + h / 2);
                series.Points.AddXY(t[j], x[j]);
            }
        }

        private double GetMainFunctionResult(double x, double t)
        {
            return t * Math.Exp((-1) * Math.Pow(t, 2)) - 2*t*x;
        }

        private int GetNumberOfIterations()
        {
            double tMax = double.Parse(tMaxView.Text);
            double h = double.Parse(hView.Text);
            return (int) Math.Round(tMax / h);
        }
    }
}
