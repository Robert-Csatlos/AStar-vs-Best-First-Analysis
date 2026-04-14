using System;

namespace AStar_vs_Best_First_Analysis {
    public class Grid
    {
        public int Size;
        public bool[,] IsWall;
        public Grid(int size) { Size = size; IsWall = new bool[size, size]; }
    }

    public class LabyrinthGenerator
    {
        private Random _rnd = new Random();

        public Grid Generate(int size, int wallPercent, out bool intentionalFail)
        {
            var grid = new Grid(size);
            intentionalFail = _rnd.Next(100) < 15; // 15% chance to force unsolvable

            if (intentionalFail)
            {
                if (size > 1) {
                    grid.IsWall[size - 2, size - 1] = true;
                    grid.IsWall[size - 1, size - 2] = true;
                }
            }

            int targetWalls = (int)(size * size * (wallPercent / 100.0));
            int placed = 0;
            while (placed < targetWalls)
            {
                int x = _rnd.Next(size), y = _rnd.Next(size);
                if (!grid.IsWall[x, y] && !(x == 0 && y == 0) && !(x == size - 1 && y == size - 1))
                {
                    grid.IsWall[x, y] = true;
                    placed++;
                }
            }
            return grid;
        }
    }
}