using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World
{
    class Cell
    {
        public bool Visited;
        public bool TopWall;
        public bool LeftWall;
    }
    class Maze
    {
        Cell[,] m_Cells;
        void Generate(int seed)
        {
            const int maxdim = 100;
            m_Cells = new Cell[maxdim, maxdim];
            Stack<(int x, int y)> stack = new();
            stack.Push((0, 0));
            while (stack.Count > 0)
            {
                var (x, y) = stack.Peek();
                if ((0 <= x && x <= maxdim) && (0 <= y && y <= maxdim))
                {
                    Cell cell = m_Cells[x, y];
                    cell.Visited = true;
                    if (x < maxdim && !m_Cells[x + 1, y].Visited)
                    {
                        m_Cells[x + 1, y].LeftWall = false;
                        stack.Push((x + 1, y));
                        continue;
                    }
                    if (x > 0 && !m_Cells[x - 1, y].Visited)
                    {
                        cell.LeftWall = false;
                        stack.Push((x - 1, y));
                        continue;
                    }
                    if (y < maxdim && !m_Cells[x, y + 1].Visited)
                    {
                        m_Cells[x, y + 1].TopWall = false;
                        stack.Push((x, y + 1));
                        continue;
                    }
                    if (y > 0 && !m_Cells[x, y - 1].Visited)
                    {
                        cell.TopWall = false;
                        stack.Push((x, y - 1));
                        continue;
                    }
                }

                stack.Pop();
            }
            for (int wallX = 0; wallX < (maxdim + 1); wallX++)
            {
                for (int wallY = 0; wallY < (maxdim + 1);)
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
                        else if (wallY == maxdim)
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
                            East = m_Cells[wallX, wallY].TopWall;
                        }
                    }
                    else if (wallX == maxdim)
                    {
                        if (wallY == 0)
                        {
                            North = false;
                            South = true;

                            East = false;
                            West = true;
                        }
                        else if (wallY == maxdim)
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

                            West = m_Cells[wallX - 1, wallY].TopWall;
                        }
                    }
                    else if (wallY == 0)
                    {
                        East = true;
                        West = true;
                        // X is for sure not on a boundary.
                        South = m_Cells[wallX, wallY].LeftWall;
                    }
                    else if (wallY == maxdim)
                    {
                        East = true;
                        West = true;

                        North = m_Cells[wallX, wallY - 1].LeftWall;
                    }
                    else
                    {
                        North = m_Cells[wallX, wallY - 1].LeftWall;
                        South = m_Cells[wallX, wallY].LeftWall;

                        East = m_Cells[wallX, wallY].TopWall;
                        West = m_Cells[wallX - 1, wallY].TopWall;
                    }
                }
            }
        }
    }
}
