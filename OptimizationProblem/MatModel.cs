using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationProblem
{
    class MatModel
    {
		//Вводные данные
		// Дисперсия
		
		public static (double[], double, double, double, double) Solution(double Mp,int N, double[] MR, double[] standDev)
        {
			

			double[] dispersion = new double[N];
			//Вычилсяем дисперсию кажой доли
			for (int i = 0; i < N; i++)
			{
				dispersion[i] = standDev[i] * standDev[i];
			}

			double[] Oi = new double[N]; // Доли капитала по бумагам	
			double SumOi = 0;
			double delta2;
			double delta1;
			double dispersionP = 0;

			delta2 = 1.0 + ((((Mp * Sum(ArraysDivide(1.0, dispersion)) - (Sum(ArraysDivide(MR, dispersion)))) / (Math.Pow(Sum(ArraysDivide(MR, dispersion)), 2.0) * Sum(ArraysDivide(1.0, dispersion)) - Sum(ArraysDivide(Pow(MR), dispersion))))));
			delta1 = ((1.0 - ((delta2) * (Sum(ArraysDivide(MR, dispersion))))) / (Sum(ArraysDivide(1.0, dispersion))));

			for (int i = 0; i < Oi.Length; i++)
			{
				Oi[i] = (1.0 / (dispersion[i] * 2.0)) * (delta1 + delta2 * MR[i]);
				SumOi += Oi[i];
			}

			for (int i = 0; i < N; i++)
			{
				dispersionP += Oi[i] * Oi[i] * dispersion[i];

			}

			return (Oi, SumOi, delta1, delta2, dispersionP);
		}

		static double[] ArraysDivide(double[] arr1, double[] arr2)
		{
			double[] c = new double[arr1.Length];
			for (int i = 0; i < arr1.Length; i++)
			{
				c[i] = arr1[i] / (arr2[i] * 2);
			}
			return c;
		}
		static double[] ArraysDivide(double a1, double[] arr2)
		{
			double[] c = new double[arr2.Length];
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
