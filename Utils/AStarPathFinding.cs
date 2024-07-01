using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class AStarPathfinding
    {
        private int[,] grid;
        private int width;
        private  int height;
        private readonly List<Point> directions = new List<Point>
        {
            new Point(0, -1), // Up
            new Point(1, 0),  // Right
            new Point(0, 1),  // Down
            new Point(-1, 0)  // Left
        };

        public AStarPathfinding()
        {

        }

        public void Init()
        {
            int width = Globals.map.tiles.GetLength(0);
            int height = Globals.map.tiles.GetLength(1);
            int[,] grid = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = Globals.map.tiles[x, y].collision ? 1 : 0;
                }
            }

            this.grid = grid;
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);
        }

        public Queue<Point> FindPath(Point start, Point goal)
        {
            var closedSet = new HashSet<Point>();
            var openSet = new PriorityQueue<Point, float>();
            var cameFrom = new Dictionary<Point, Point>();

            var gScore = new Dictionary<Point, float>
            {
                [start] = 0
            };
            var fScore = new Dictionary<Point, float>
            {
                [start] = Heuristic(start, goal)
            };

            openSet.Enqueue(start, fScore[start]);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }

                closedSet.Add(current);

                foreach (var direction in directions)
                {
                    var neighbor = new Point(current.X + direction.X, current.Y + direction.Y);

                    if (!IsWithinBounds(neighbor) || grid[neighbor.X, neighbor.Y] != 0 || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    var tentativeGScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }

            return new Queue<Point>();
        }

        private Queue<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var path = new Stack<Point>();
            while (cameFrom.ContainsKey(current))
            {
                path.Push(current);
                current = cameFrom[current];
            }

            return new Queue<Point>(path);
        }

        private bool IsWithinBounds(Point point)
        {
            return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
        }

        private float Heuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }

    public class PriorityQueue<TElement, TPriority> where TElement : notnull
    {
        private readonly SortedList<TPriority, Queue<TElement>> list = new SortedList<TPriority, Queue<TElement>>();

        public int Count { get; private set; }

        public void Enqueue(TElement element, TPriority priority)
        {
            if (!list.ContainsKey(priority))
            {
                list[priority] = new Queue<TElement>();
            }

            list[priority].Enqueue(element);
            Count++;
        }

        public TElement Dequeue()
        {
            var pair = list.First();
            var element = pair.Value.Dequeue();
            if (pair.Value.Count == 0)
            {
                list.Remove(pair.Key);
            }
            Count--;
            return element;
        }

        public bool Contains(TElement element)
        {
            return list.Any(pair => pair.Value.Contains(element));
        }
    }
}
