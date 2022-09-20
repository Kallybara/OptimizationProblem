using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationProblem
{
    class MatModel
    {

		public static (decimal[], decimal, decimal, decimal, decimal, StringBuilder) Solution(decimal Mp,int N, decimal[] MR, decimal[] standDev)
        {

			StringBuilder solutionProblem = new StringBuilder();
			decimal[] dispersion = new decimal[N];
			//Вычилсяем дисперсию кажой доли
			for (int i = 0; i < N; i++)
			{
				dispersion[i] = standDev[i] * standDev[i];
			}

			decimal[] Oi = new decimal[N+2]; // Доли капитала по бумагам	
			decimal SumOi = 0;
			decimal delta2=0;   
			decimal delta1=0;
			decimal dispersionP = 0;

			

			decimal[] vector = new decimal[N + 2];
			decimal[,] Matrix = new decimal[N + 2, N + 2];
			decimal[,] TMatrix = new decimal[N + 2, N + 2];
			decimal[,] InvMatrix = new decimal[N + 2, N + 2];


			for (int i = 0; i < N + 2; i++)
			{
				for (int j = 0; j < N + 2; j++)
				{
					if (i == j && j < N)
						Matrix[i, j] = dispersion[i] * 2;
					else if (i < N && j == N)
						Matrix[i, j] = -1;
					else if (i < N && j == N + 1)
						Matrix[i, j] = -MR[i];
					else if (j < N && i == N)
						Matrix[i, j] = -1;
					else if (j < N && i == N + 1)
						Matrix[i, j] = -MR[j];
					else
						Matrix[i, j] = 0;
				}
			}

			for (int i = 0; i < N + 2; i++)
			{
				if (i == N)
					vector[i] = -1;
				else if (i == N + 1)
					vector[i] = -Mp;
				else
					vector[i] = 0;
			}

            



			decimal det = Det(Matrix, N + 2);

			for (int i = 0; i < N + 2; i++)
			{
				for (int j = 0; j < N + 2; j++)
				{
					int m = N + 2 - 1;
					decimal[,] temp_matr = new decimal[m, m];
					Get_matr(Matrix, N + 2, temp_matr, i, j);
					InvMatrix[i, j] = Convert.ToDecimal(Math.Pow(-1.0, i + j + 2)) * Det(temp_matr, m) / det;
				}
			}
			TransponMtx(InvMatrix, TMatrix, N + 2);

			for (int i = 0; i < N + 2; i++)
				for (int j = 0; j < N + 2; j++)
				{
					if (i == N)
						delta1 += TMatrix[i, j] * vector[j];
					if(i ==N+1)
						delta2 += TMatrix[i, j] * vector[j];				
					Oi[i] += TMatrix[i, j] * vector[j];
				}			
			for (int i = 0; i < N; i++)
			{
				SumOi += Oi[i];
			}

			for (int i = 0; i < N; i++)
			{
				dispersionP += Oi[i] * Oi[i] * dispersion[i];
			}


			//Matrix text 
			for (int i = 0; i < vector.Length; i++)
			{
				if (i < vector.Length - 2)
				{
					solutionProblem.Append(" |θ" + (i + 1).ToString() + "|   |");
				}
                else
                {
					solutionProblem.Append(" |σ" + (i + 1).ToString() + "|   |");
				}
				for (int j = 0; j < vector.Length; j++)
				{
					solutionProblem.Append(" " + Matrix[i, j]);
				}
				solutionProblem.Append(" |   |" + vector[i].ToString() + "|   |" + Oi[i].ToString() + "|\r\n");
			}
			solutionProblem.Append("\r\nДисперсия оптимального портфеля:\r\nD=Σθi^2*σi^2=" + dispersionP.ToString());
			return (Oi, SumOi, delta1, delta2, dispersionP, solutionProblem);
		}

		static void TransponMtx(decimal[,] matr, decimal[,] tMatr, int n)
		{
			for (int i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
					tMatr[j, i] = matr[i, j];
		}

		static decimal Det(decimal[,] matr, int n)
		{
			decimal temp = 0;   //временная переменная для хранения определителя
			int k = 1;      //степень
			if (n < 1)
			{

				return 0;
			}
			else if (n == 1)
				temp = matr[0, 0];
			else if (n == 2)
				temp = matr[0, 0] * matr[1, 1] - matr[1, 0] * matr[0, 1];
			else
			{
				for (int i = 0; i < n; i++)
				{
					int m = n - 1;
					decimal[,] temp_matr = new decimal[m, m];
					Get_matr(matr, n, temp_matr, 0, i);
					temp = temp + k * matr[0, i] * Det(temp_matr, m);

					k = -k;

				}
			}
			return temp;
		}
		static void Get_matr(decimal[,] matr, int n, decimal[,] temp_matr, int indRow, int indCol)
		{
			int ki = 0;
			for (int i = 0; i < n; i++)
			{
				if (i != indRow)
				{
					for (int j = 0, kj = 0; j < n; j++)
					{
						if (j != indCol)
						{
							temp_matr[ki, kj] = matr[i, j];
							kj++;
						}
					}
					ki++;
				}
			}
		}

		static decimal[] ArraysDivide(decimal[] arr1, decimal[] arr2)
		{
			decimal[] c = new decimal[arr1.Length];
			for (int i = 0; i < arr1.Length; i++)
			{
				c[i] = arr1[i] / (arr2[i] * 2);
			}
			return c;
		}
		static decimal[] ArraysDivide(decimal a1, decimal[] arr2)
		{
			decimal[] c = new decimal[arr2.Length];
			for (int i = 0; i < arr2.Length; i++)
			{
				c[i] = a1 / (arr2[i] * 2);
			}
			return c;
		}


		static double Sum(double[] arr)
		{
			double sum = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				sum += arr[i];
			}
			return sum;
		}

		static double[] Pow(double[] arr)
		{
			double[] c = new double[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				c[i] = arr[i] * arr[i];
			}
			return c;
		}

		
	}
}
