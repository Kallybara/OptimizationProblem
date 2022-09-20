using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizationProblem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static int N;
        double Mp;
        double[] Oi;
        double[] MR; // Мат ожидание
        double[] standDev; //стандартное отклонение
        (double[], double, double, double, double) answer;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            N = Convert.ToInt32(textBox1.Text);
            MR = new double[N];
            standDev = new double[N];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows.Clear();
            }

            for (int i = 0; i < N; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i+1;
            }

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < N - 1; i++)
            {
                dataGridView2.Rows.Add();
            }

            Mp = Convert.ToDouble(textBox2.Text);
            for (int i = 0; i < N; i++)
            {
                MR[i] = Convert.ToDouble(dataGridView1[1, i].Value);
                standDev[i] = Convert.ToDouble(dataGridView1[2, i].Value);
            }

            MessageBox.Show(Mp.ToString());
            MessageBox.Show(N.ToString());
            for (int i = 0; i < N; i++)
            {
                MessageBox.Show(MR[i].ToString());
            }
            for (int i = 0; i < N; i++)
            {
                MessageBox.Show(standDev[i].ToString());
            }
            answer = MatModel.Solution(Mp, N, MR, standDev);

            Oi = answer.Item1;
            textBox3.Text = answer.Item2.ToString();
            textBox4.Text = answer.Item3.ToString();
            textBox5.Text = answer.Item4.ToString();
            textBox6.Text = answer.Item5.ToString();
            for (int i = 0; i < N; i++)
            {
                dataGridView2[1, i].Value = Oi[i].ToString();
            }

        }
    }
}
