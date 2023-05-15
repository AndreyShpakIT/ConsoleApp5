using System;

class Cholesky
{
    static double[] Solve(double[,] A, double[] b)
    {
        int n = A.GetLength(0);
        double[,] L = new double[n, n];
        double[] y = new double[n];
        double[] x = new double[n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                double s = 0;
                for (int k = 0; k < j; k++)
                    s += L[i, k] * L[j, k];
                if (i == j)
                    L[i, i] = Math.Sqrt(A[i, i] - s);
                else
                    L[i, j] = (A[i, j] - s) / L[j, j];
            }
        }

        // Solve Ly = b
        for (int i = 0; i < n; i++)
        {
            double s = 0;
            for (int j = 0; j < i; j++)
                s += L[i, j] * y[j];
            y[i] = (b[i] - s) / L[i, i];
        }

        // Solve L'x = y
        for (int i = n - 1; i >= 0; i--)
        {
            double s = 0;
            for (int j = i + 1; j < n; j++)
                s += L[j, i] * x[j];
            x[i] = (y[i] - s) / L[i, i];
        }

        return x;
    }

    static void Main()
    {
        Console.Write("Enter the size of the matrix: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
            Console.Write("Enter the size of the matrix: ");
        }

        double[,] a = new double[n, n];

        Console.WriteLine("Enter the matrix elements:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double element;
                while (!double.TryParse(Console.ReadLine(), out element) || element <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.Write("Enter element [{0},{1}]: ", i, j);
                }
                a[i, j] = element;
            }
        }

        double[] b = new double[n];
        Console.WriteLine("Enter the vector elements:");
        for (int i = 0; i < n; i++)
        {
            double element;
            while (!double.TryParse(Console.ReadLine(), out element) || element <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter element b[{0}]: ", i);
            }
            b[i] = element;
        }

        double[] x = Solve(a, b);

        Console.WriteLine("Solution:");
        for (int i = 0; i < x.Length; i++)
        {
            Console.WriteLine("x[{0}] = {1}", i, x[i]);
        }
    }
}