using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar_vs_Best_First_Analysis {
    public class Node : IComparable<Node>
    {
        public int X, Y, G, H;
        public int F => G + H;
        public Node Parent; // Added to track the winning path
        public int CompareTo(Node other) => F.CompareTo(other.F);
    }

    public abstract class PathfinderBase
    {
        protected readonly (int x, int y)[] Directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };
        protected int Manhattan(int x1, int y1, int x2, int y2) => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        // Updated to accept a UI callback
        public abstract SearchResult Search(Grid grid, Action<int, int, int> updateUI = null);
    }

    public class AStar : PathfinderBase
    {
        public override SearchResult Search(Grid grid, Action<int, int, int> updateUI = null)
        {
            var openList = new List<Node>();
            var closedSet = new HashSet<string>();
            int visited = 0;

            openList.Add(new Node { X = 0, Y = 0, G = 0, H = Manhattan(0, 0, grid.Size - 1, grid.Size - 1) });

            while (openList.Count > 0)
            {
                var current = openList.OrderBy(n => n.F).First();
                openList.Remove(current);

                string key = $"{current.X},{current.Y}";
                if (closedSet.Contains(key)) continue;
                closedSet.Add(key);
                visited++;

                // Notify UI: Cell is being visited (State 1)
                updateUI?.Invoke(current.X, current.Y, 1);

                if (current.X == grid.Size - 1 && current.Y == grid.Size - 1) {
                    // Backtrack to draw final path (State 2)
                    var temp = current;
                    while (temp != null) {
                        updateUI?.Invoke(temp.X, temp.Y, 2);
                        temp = temp.Parent;
                    }
                    return new SearchResult { PathFound = true, VisitedCells = visited };
                }

                foreach (var d in Directions)
                {
                    int nx = current.X + d.x, ny = current.Y + d.y;
                    if (nx >= 0 && nx < grid.Size && ny >= 0 && ny < grid.Size && !grid.IsWall[nx, ny])
                    {
                        if (!closedSet.Contains($"{nx},{ny}")) {
                            openList.Add(new Node { X = nx, Y = ny, G = current.G + 1, H = Manhattan(nx, ny, grid.Size - 1, grid.Size - 1), Parent = current });
                            updateUI?.Invoke(nx, ny, 0); // Cell discovered (State 0)
                        }
                    }
                }
            }
            return new SearchResult { PathFound = false, VisitedCells = visited };
        }
    }

    public class BestFirstSearch : PathfinderBase
    {
        public override SearchResult Search(Grid grid, Action<int, int, int> updateUI = null)
        {
            var openList = new List<Node>();
            var closedSet = new HashSet<string>();
            int visited = 0;

            openList.Add(new Node { X = 0, Y = 0, H = Manhattan(0, 0, grid.Size - 1, grid.Size - 1) });

            while (openList.Count > 0)
            {
                var current = openList.OrderBy(n => n.H).First();
                openList.Remove(current);

                string key = $"{current.X},{current.Y}";
                if (closedSet.Contains(key)) continue;
                closedSet.Add(key);
                visited++;

                updateUI?.Invoke(current.X, current.Y, 1);

                if (current.X == grid.Size - 1 && current.Y == grid.Size - 1) {
                    var temp = current;
                    while (temp != null) {
                        updateUI?.Invoke(temp.X, temp.Y, 2);
                        temp = temp.Parent;
                    }
                    return new SearchResult { PathFound = true, VisitedCells = visited };
                }

                foreach (var d in Directions)
                {
                    int nx = current.X + d.x, ny = current.Y + d.y;
                    if (nx >= 0 && nx < grid.Size && ny >= 0 && ny < grid.Size && !grid.IsWall[nx, ny])
                    {
                        if (!closedSet.Contains($"{nx},{ny}")) {
                            openList.Add(new Node { X = nx, Y = ny, H = Manhattan(nx, ny, grid.Size - 1, grid.Size - 1), Parent = current });
                            updateUI?.Invoke(nx, ny, 0);
                        }
                    }
                }
            }
            return new SearchResult { PathFound = false, VisitedCells = visited };
        }
    }
}