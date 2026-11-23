using System;

namespace MyMatrixLab
{

    internal partial class MyMatrix
    {
        private double[,] elements;

        // Скидання кешу після зміни елемента
        partial void InvalidateDeterminant();

        // З двовимірного масиву
        public MyMatrix(double[,] array)
        {
            elements = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    elements[i, j] = array[i, j];
        }

        // Копія
        public MyMatrix(MyMatrix other)
        {
            elements = (double[,])other.elements.Clone();
        }

        // Із зубчастого масиву
        public MyMatrix(double[][] rows)
        {
            if (rows == null || rows.Length == 0 || rows[0] == null)
                throw new ArgumentException("Потрібен непорожній прямокутний масив");
            elements = new double[rows.Length, rows[0].Length];
            for (int i = 0; i < Height; i++)
            {
                if (rows[i] == null || rows[i].Length != Width)
                    throw new ArgumentException("Масив не прямокутний: рядки різної довжини");
                for (int j = 0; j < Width; j++)
                    elements[i, j] = rows[i][j];
            }
        }

        // З масиву рядків
        public MyMatrix(string[] lines)
        {
            if (lines == null || lines.Length == 0)
                throw new ArgumentException("Матриця не може бути порожньою");
            int width = Numbers(lines[0]).Length;
            if (width == 0)
                throw new ArgumentException("У першому рядку немає чисел");

            elements = new double[lines.Length, width];
            for (int i = 0; i < Height; i++)
            {
                string[] numbers = Numbers(lines[i]);
                if (numbers.Length != Width)
                    throw new ArgumentException("У рядках різна кількість чисел");
                for (int j = 0; j < Width; j++)
                    elements[i, j] = double.Parse(numbers[j]);
            }
        }

        // Із тексту з кількома рядками
        public MyMatrix(string text) : this(SplitLines(text))
        {
        }
        private static string[] SplitLines(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Матриця не може бути порожньою");
            return text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string[] Numbers(string line)
        {
            return line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Висота й ширина
        public int Height { get { return elements.GetLength(0); } }
        public int Width { get { return elements.GetLength(1); } }

        public int getHeight()
        {
            return Height;
        }

        public int getWidth()
        {
            return Width;
        }

        // Індексатор
        public double this[int row, int col]
        {
            get { return elements[row, col]; }
            set { elements[row, col] = value; InvalidateDeterminant(); }
        }

        public double getElement(int row, int col)
        {
            return elements[row, col];
        }

        public void setElement(int row, int col, double value)
        {
            elements[row, col] = value;
            InvalidateDeterminant();
        }

        public override string ToString()
        {
            string[] lines = new string[Height];
            for (int i = 0; i < Height; i++)
            {
                string[] numbers = new string[Width];
                for (int j = 0; j < Width; j++)
                    numbers[j] = elements[i, j].ToString();
                lines[i] = string.Join("\t", numbers);
            }
            return string.Join("\n", lines);
        }
    }
}