using System;

namespace MyMatrixLab
{
    class Minesweeper
    {
        private MyMatrix field;
        private bool[,] opened;
        private int mines;
        private int safeOpened;

        public Minesweeper(int rows, int cols, int mineCount)
        {
            field = new MyMatrix(new double[rows, cols]);
            opened = new bool[rows, cols];
            mines = mineCount;
            safeOpened = 0;

            Random random = new Random();
            int placed = 0;

            // Ставимо міни у випадкові клітинки
            while (placed < mines)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);

                if (field[row, col] != -1)
                {
                    field[row, col] = -1;
                    placed++;
                }
            }

            // Записуємо числа біля мін
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (field[row, col] != -1)
                        field[row, col] = CountMines(row, col);
                }
            }
        }

        private int CountMines(int row, int col)
        {
            int count = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < field.Height && j >= 0 && j < field.Width)
                    {
                        if (field[i, j] == -1)
                            count++;
                    }
                }
            }

            return count;
        }
        // символ конкретної клітинки.
        private string GetCell(int row, int col, bool allMines)
        {
            if (!opened[row, col] && !(allMines && field[row, col] == -1))
                return "#";

            if (field[row, col] == -1)
                return "*";

            if (field[row, col] == 0)
                return ".";

            return field[row, col].ToString();
        }
        //показ усього поля
        public void Show(bool allMines = false)
        {
            Console.Write("    ");
            for (int col = 0; col < field.Width; col++)
                Console.Write((col + 1).ToString().PadLeft(3));
            Console.WriteLine();

            for (int row = 0; row < field.Height; row++)
            {
                Console.Write((row + 1).ToString().PadLeft(3) + " ");
                for (int col = 0; col < field.Width; col++)
                    Console.Write(GetCell(row, col, allMines).PadLeft(3));
                Console.WriteLine();
            }
        }

        public bool Open(int row, int col)
        {
            if (field[row, col] == -1)
            {
                opened[row, col] = true;
                return false;
            }

            OpenSafeCells(row, col);
            return true;
        }
        //рекурсія самовиклик для сусідніх клітинок
        private void OpenSafeCells(int row, int col)
        {
            if (row < 0 || row >= field.Height || col < 0 || col >= field.Width)
                return;//перевірка меж

            if (opened[row, col] || field[row, col] == -1)
                return;

            opened[row, col] = true;
            safeOpened++;

            if (field[row, col] != 0)
                return;

            for (int i = row - 1; i <= row + 1; i++)
                for (int j = col - 1; j <= col + 1; j++)
                    OpenSafeCells(i, j);
        }

        public bool HasWon()
        {
            int safeCells = field.Height * field.Width - mines;
            return safeOpened == safeCells;
        }
    }
}
