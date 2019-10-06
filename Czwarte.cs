using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pancor_cw4_v1
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
            StringBuilder matrix = new StringBuilder();
            matrix.AppendLine("3 1 -2 2 3");
            matrix.AppendLine("0 1 9 -3 1");
            matrix.AppendLine("1 2 0 2 3");
            matrix.AppendLine("0 9 1 -2 2");
            MatrixView.Text = matrix.ToString();

            CountBtn.Click += new EventHandler(CountBtn_Click);
            ClearBtn.Click += new EventHandler(ClearBtn_Click);
        }

        private void CountBtn_Click(object sender, EventArgs e)
        {
            ResultView.Clear();

            string[] vectors = MatrixView.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            double[,] matrixA = new double[vectors.Count(), vectors[0].Split(' ').Count()];
            int numberOfVariables = vectors.Count() - 1;

            for (int i = 0; i < numberOfVariables; i++)
            {
                string[] cells = vectors[i].Split(' ');
                for (int j = 0; j < cells.Count(); j++) 
                {
                    matrixA[i, j] = double.Parse(cells[j]);
                }
            }

            for (int i = 0; i < numberOfVariables - 1; i++)
            {
                for (int j = i + 1; j < numberOfVariables; j++)
                {
                    double m = 0;
                    if (matrixA[i, i] != 0)
                    {
                        m = (-1) * (matrixA[j, i] / matrixA[i, i]);
                    }
                    for (int k = i; k < numberOfVariables + 1; k++)
                    {
                        matrixA[j, k] = matrixA[j, k] + m*matrixA[i, k];
                    }
                }
            }

            double[] matrixResult = new double[numberOfVariables];
            for (int i = 0; i < numberOfVariables; i++)
            {
                matrixResult[i] = 1;
            }

            for (int i = numberOfVariables - 1; i > -1; i--)
            {
                double s = matrixA[i, numberOfVariables];
                for (int j = numberOfVariables - 1; j > i; j--)
                {
                    s = s - matrixA[i, j] * matrixResult[j];
                }
                matrixResult[i] = s / matrixA[i, i];
            }

            String result = "";
            for (int i = 0; i < matrixResult.Count(); i++)
            {
                result += "x" + (i+1).ToString() + " = " + matrixResult[i].ToString() + Environment.NewLine;
            }
            ResultView.Text += result;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResultView.Clear();
        }
    }
}
