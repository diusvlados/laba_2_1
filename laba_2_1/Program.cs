using System;

namespace MyMatrixLab
{
    class Program
    {
        static int ReadNumber(string message, int min, int max)
        {
            int number = 0;
            Console.Write(message);
            bool valid = int.TryParse(Console.ReadLine(), out number);

            while (!valid || number < min || number > max)
            {
                Console.WriteLine("Введіть число від " + min + " до " + max);
                Console.Write(message);
                valid = int.TryParse(Console.ReadLine(), out number);
            }

            return number;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("1 - Сапер");
            Console.WriteLine("2 - Перевірка матриць");
            int choice = ReadNumber("Ваш вибір: ", 1, 2);

            if (choice == 2)
            {
                MatrixDemo.Run();
                return;
            }

            int rows = ReadNumber("Рядки (2-15): ", 2, 15);
            int cols = ReadNumber("Стовпчики (2-15): ", 2, 15);
            int mines = ReadNumber("Кількість мін: ", 1, rows * cols - 1);

            Minesweeper game = new Minesweeper(rows, cols, mines);
            bool playing = true;

            while (playing)
            {
                Console.WriteLine();
                game.Show();

                int row = ReadNumber("Номер рядка: ", 1, rows) - 1;
                int col = ReadNumber("Номер стовпчика: ", 1, cols) - 1;

                if (!game.Open(row, col))
                {
                    Console.WriteLine("Міна! Ви програли.");
                    playing = false;
                }
                else if (game.HasWon())
                {
                    Console.WriteLine("Перемога! Ви відкрили всі безпечні клітинки.");
                    playing = false;
                }

            }

            Console.WriteLine();
            game.Show(true);
        }
    }
}
