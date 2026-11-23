using System;

namespace MyMatrixLab
{
    internal partial class MyMatrix
    {
        // Зберігаємо вже обчислений визначник
        private bool hasCachedDeterminant = false;
        private double cachedDeterminant;

        // Додавання
        public static MyMatrix operator +(MyMatrix left, MyMatrix right)
        {
            if (left.Height != right.Height || left.Width != right.Width)
                throw new ArgumentException("Матриці різного розміру додати не можна");

            double[,] sum = new double[left.Height, left.Width];
            for (int i = 0; i < left.Height; i++)
                for (int j = 0; j < left.Width; j++)
                    sum[i, j] = left.elements[i, j] + right.elements[i, j];
            return new MyMatrix(sum);
        }

        // Множення
        public static MyMatrix operator *(MyMatrix left, MyMatrix right)
        {
            if (left.Width != right.Height)
                throw new ArgumentException("Кількість стовпчиків першої матриці не дорівнює кількості рядків другої");

            double[,] product = new double[left.Height, right.Width];
            for (int i = 0; i < left.Height; i++)
                for (int j = 0; j < right.Width; j++)
                    for (int k = 0; k < left.Width; k++)
                        product[i, j] += left.elements[i, k] * right.elements[k, j];
            return new MyMatrix(product);
        }
        private double[,] GetTransponedArray()
        {
            double[,] result = new double[Width, Height];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    result[j, i] = elements[i, j];
            return result;
        }
        public MyMatrix GetTransponedCopy()
        {
            return new MyMatrix(GetTransponedArray());
        }
        public void TransponeMe()
        {
            elements = GetTransponedArray();
            InvalidateDeterminant();
        }
///
        partial void InvalidateDeterminant()
        {
            hasCachedDeterminant = false;
        }

        public double CalcDeterminant()
        {
            if (Height != Width)
                throw new InvalidOperationException("Матриця повинна бути квадратною");

            if (hasCachedDeterminant)//чи збережений результат
                return cachedDeterminant;

            double[,] a = (double[,])elements.Clone();
            int n = Height;
            double det = 1;

            for (int i = 0; i < n; i++)
            {
                int maxRow = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(a[j, i]) > Math.Abs(a[maxRow, i]))
                        maxRow = j;
                }

                if (a[maxRow, i] == 0)
                {
                    cachedDeterminant = 0;
                    hasCachedDeterminant = true;
                    return 0;
                }

                if (maxRow != i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double temp = a[i, j];
                        a[i, j] = a[maxRow, j];
                        a[maxRow, j] = temp;
                    }
                    det = -det;
                }

                double diagonal = a[i, i];
                det *= diagonal;

                for (int j = i + 1; j < n; j++)
                {
                    double factor = a[j, i] / diagonal;
                    for (int col = i + 1; col < n; col++)
                        a[j, col] -= factor * a[i, col];
                    a[j, i] = 0;
                }
            }

            cachedDeterminant = det;
            hasCachedDeterminant = true;
            return det;
        }
    }
}
