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
        decimal Mp;
        decimal[] Oi;
        decimal[] MR; // Мат ожидание
        decimal[] standDev; //стандартное отклонение
        (decimal[], decimal, decimal, decimal, decimal, StringBuilder) answer;
        bool gridFlag = false;
        StringBuilder solutionText = new StringBuilder("Математическая модель задачи:\r\n \r\nΣθi*σi^2 --->min\r\n\r\nСистема уравнений:\r\nΣθi=1\r\nΣθi*MRi=mp\r\nθi >= 0\r\n\r\nФункция Лагранжа имеет вид:\r\nF(θi,λ1,λ2)=Σθi*σi^2+λ1*(1-Σθi)+λ2*(mp-Σθi*MRi)\r\n\r\n");

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out N))
            {

                MR = new decimal[N];
                standDev = new decimal[N];
                dataGridView1.Rows.Clear();

                for (int i = 0; i < N; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = i + 1;
                }
            }
			else
			{
                MessageBox.Show("Введите целое число");
			}
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    
                    decimal tmpval;
                    if (decimal.TryParse(dataGridView1[i, j].Value.ToString(),out tmpval))
					{
                        gridFlag = true;

                    }
					else
					{
                        gridFlag = false;
                        break;

                    }

                }
            }
            if (int.TryParse(textBox1.Text, out N)
                && decimal.TryParse(textBox1.Text, out Mp)
                && gridFlag)
            {

                {                                      
                    Mp = Convert.ToDecimal(textBox2.Text);
                    for (int i = 0; i < N; i++)
                    {
                        MR[i] = Convert.ToDecimal(dataGridView1[1, i].Value);
                        standDev[i] = Convert.ToDecimal(dataGridView1[2, i].Value);
                    }

                    decimal[] dispersion = new decimal[N];

                    for (int i = 0; i < N; i++)
                    {
                        dispersion[i] = standDev[i] * standDev[i];
                        solutionText.Append( "dF/dθ" + (i + 1).ToString() + "=" + (dispersion[i] * 2).ToString() + "*θ" + (i + 1).ToString() + "-λ1-" + MR[i].ToString() + "λ2=0\r\n");
                    }
                    solutionText.Append("dF/dλ1=1-Σθi\r\n");
                    solutionText.Append("dF/dλ2=mp-Σθi*MRi\r\n\r\n");
                    
                    answer = MatModel.Solution(Mp, N, MR, standDev);

                    Oi = answer.Item1;                                      
                    solutionText.Append(answer.Item6);
                    textBox7.Text = solutionText.ToString();
                   
                }
            }
			else
			{
                MessageBox.Show("Введите коректные значения");
			}
        }

		private void label11_Click(object sender, EventArgs e)
		{

		}

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
