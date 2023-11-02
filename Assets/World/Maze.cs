using System.Collections.Generic;
using System;

namespace World
{
    public class Cell
    {
        public bool Visited = false;
        public bool TopWall = true;
        public bool LeftWall = true;
    }
    enum Direction
    {
        North,
        East,
        South,
        West,
    }

    public class Maze
    {
        double m_breakProbability = 0.10;
        int m_maxDim = 20;
        public Cell[,] Cells;
        public Maze(int maxDim, double breakProbability)
        {
            m_maxDim = maxDim;
            m_breakProbability = breakProbability;
            Cells = new Cell[m_maxDim, m_maxDim];
        }
        public void Generate(int seed)
        {
            Random rand = new Random(seed);
            for (int i = 0; i < m_maxDim; i++)
            {
                for (int j = 0; j < m_maxDim; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

            Stack<(int x, int y)> stack = new();
            Cells[0, 0].Visited = true;
            stack.Push((0, 0));
            while (stack.Count > 0)
            {
                var (x, y) = stack.Peek();
                if ((0 <= x && x <= m_maxDim) && (0 <= y && y <= m_maxDim))
                {
                    Cell cell = Cells[x, y];
                    var checks = new Direction[]
                    {
                        Direction.North,
                        Direction.East,
                        Direction.South,
                        Direction.West
                    };

                    // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
                    for (int i = 0; i < checks.Length - 1; ++i)
                    {
                        var j = rand.Next(0, checks.Length);
                        var tmp = checks[i];
                        checks[i] = checks[j];
                        checks[j] = tmp;
                    }

                    foreach (var check in checks)
                    {
                        var direction = check;
                        int checkX = x;
                        int checkY = y;
                        switch (direction)
                        {
                            case Direction.North:
                                --checkY;
                                break;
                            case Direction.East:
                                ++checkX;
                                break;
                            case Direction.South:
                                ++checkY;
                                break;
                            case Direction.West:
                                --checkX;
                                break;
                        }

                        if ((0 <= checkX && checkX < m_maxDim) && (0 <= checkY && checkY < m_maxDim))
                        {
                            var checkCell = Cells[checkX, checkY];
                            if (!checkCell.Visited)
                            {
                                checkCell.Visited = true;
                                switch (direction)
                                {
                                    case Direction.North:
                                        cell.TopWall = false;
                                        break;
                                    case Direction.South:
                                        checkCell.TopWall = false;
                                        break;
                                    case Direction.West:
                                        cell.LeftWall = false;
                                        break;
                                    case Direction.East:
                                        checkCell.LeftWall = false;
                                        break;
                                }
                                stack.Push((checkX, checkY));
                                goto CONTINUE_OUTER;
                            }
                        }
                    }
                }

                stack.Pop();
            CONTINUE_OUTER: continue;
            }

            // Break random holes to make it less constricted
            for (int i = 0; i < m_maxDim; i++)
            {
                for (int j = 0; j < m_maxDim; j++)
                {
                    if (Cells[i, j].TopWall && rand.NextDouble() < m_breakProbability)
                        Cells[i, j].TopWall = false;
                    if (Cells[i, j].LeftWall && rand.NextDouble() < m_breakProbability)
                        Cells[i, j].LeftWall = false;
                }
            }
        }
        public void Output()
        {
            for (int wallY = 0; wallY < (m_maxDim + 1); wallY++)
            {
                for (int wallX = 0; wallX < (m_maxDim + 1); wallX++)
                {
                    bool North = false;
                    bool East = false;
                    bool South = false;
                    bool West = false;
                    if (wallX == 0)
                    {
                        if (wallY == 0)
                        {
                            North = false;
                            South = true;

                            East = true;
                            West = false;
                        }
                        else if (wallY == m_maxDim)
                        {
                            North = true;
                            South = false;

                            East = true;
                            West = false;
                        }
                        else
                        {
                            North = true;
                            South = true;
                            East = Cells[wallX, wallY].TopWall;
                        }
                    }
                    else if (wallX == m_maxDim)
                    {
                        if (wallY == 0)
                        {
                            North = false;
                            South = true;

                            East = false;
                            West = true;
                        }
                        else if (wallY == m_maxDim)
                        {
                            North = true;
                            South = false;

                            East = false;
                            West = true;
                        }
                        else
                        {
                            North = true;
                            South = true;

                            West = Cells[wallX - 1, wallY].TopWall;
                        }
                    }
                    else if (wallY == 0)
                    {
                        East = true;
                        West = true;
                        // X is for sure not on a boundary.
                        South = Cells[wallX, wallY].LeftWall;
                    }
                    else if (wallY == m_maxDim)
                    {
                        East = true;
                        West = true;

                        North = Cells[wallX, wallY - 1].LeftWall;
                    }
                    else
                    {
                        North = Cells[wallX, wallY - 1].LeftWall;
                        South = Cells[wallX, wallY].LeftWall;

                        East = Cells[wallX, wallY].TopWall;
                        West = Cells[wallX - 1, wallY].TopWall;
                    }

                    if (North && South && East && West)
                    {
                        Console.Write("┼─");
                    }
                    else if (North && South && East)
                    {
                        Console.Write("├─");
                    }
                    else if (North && South && West)
                    {
                        Console.Write("┤ ");
                    }
                    else if (North && East && West)
                    {
                        Console.Write("┴─");
                    }
                    else if (South && East && West)
                    {
                        Console.Write("┬─");
                    }
                    else if (North && South)
                    {
                        Console.Write("│ ");
                    }
                    else if (North && East)
                    {
                        Console.Write("└─");
                    }
                    else if (North && West)
                    {
                        Console.Write("┘ ");
                    }
                    else if (South && East)
                    {
                        Console.Write("┌─");
                    }
                    else if (South && West)
                    {
                        Console.Write("┐ ");
                    }
                    else if (East && West)
                    {
                        Console.Write("──");
                    }
                    else if (East)
                    {
                        Console.Write("╶─");
                    }
                    else if (West)
                    {
                        Console.Write("╴ ");
                    }
                    else if (North)
                    {
                        Console.Write("╵ ");
                    }
                    else if (South)
                    {
                        Console.Write("╷ ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
