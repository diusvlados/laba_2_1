using System;

namespace MyMatrixLab
{
    static class MatrixDemo
    {
        static void Show(string title, MyMatrix m)
        {
            Console.WriteLine(title);
            Console.WriteLine(m);
            Console.WriteLine();
        }

        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створюємо матриці різними способами
            MyMatrix a = new MyMatrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            MyMatrix b = new MyMatrix(new double[][]
            {
                new double[] { 1, 0 },
                new double[] { 0, 1 },
                new double[] { 2, 2 }
            });
            MyMatrix c = new MyMatrix(new string[] { "1 2 3", "4 5 6" });
            MyMatrix d = new MyMatrix("7 8 9\n10 11 12");
            MyMatrix copy = new MyMatrix(a);
            MyMatrix again = new MyMatrix(a.ToString());

            Show("A (з двовимірного масиву):", a);
            Show("B (з зубчастого масиву):", b);
            Show("C (з масиву рядків):", c);
            Show("D (з одного рядка):", d);
            Show("Матриця, створена з a.ToString():", again);

            // Розміри
            Console.WriteLine("A:Height =" + a.Height + ", Width =" + a.Width);
            Console.WriteLine("A:getHeight() =" + a.getHeight() + ", getWidth() =" + a.getWidth());
            Console.WriteLine();

            // зміна копії, оригінал лишаєм
            copy[0, 0] = 10;
            copy.setElement(1, 2, 50);
            Console.WriteLine("copy[0, 0] =" + copy[0, 0] + ", copy.getElement(1, 2) =" + copy.getElement(1, 2));
            Show("Копія після змін:", copy);
            Show("A не змінилась:", a);

            // Додавання й множення
            Show("A + D:", a + d);
            Show("A * B:", a * b);

            // Транспонування
            Show("Транспонована копія A:", a.GetTransponedCopy());
            Show("A після копії (та сама):", a);
            a.TransponeMe();
            Show("A після TransponeMe():", a);

            //  визначник до та після зміни матриці
            MyMatrix square = new MyMatrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Console.WriteLine("Визначник:" + square.CalcDeterminant()); // -2
            Console.WriteLine("Повторний виклик:" + square.CalcDeterminant()); 
            square[0, 0] = 2;
            Console.WriteLine("Після зміни:" + square.CalcDeterminant()); // 2
            Console.WriteLine();

            // Перевірка помилок
            Console.WriteLine("Помилки");

            try
            {
                new MyMatrix(new double[][] { new double[] { 1, 2 }, new double[] { 3 } });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не прямокутний масив: " + ex.Message);
            }

            try
            {
                new MyMatrix(new string[] { "1 2", "3 abc" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не числа: " + ex.Message);
            }

            try
            {
                Console.WriteLine(c[5, 5]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неіснуючий елемент: " + ex.Message);
            }

            try
            {
                Console.WriteLine(c + b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Додавання різних розмірів: " + ex.Message);
            }

            try
            {
                Console.WriteLine(b * b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Множення з невідповідними розмірами: " + ex.Message);
            }
        }
    }
}