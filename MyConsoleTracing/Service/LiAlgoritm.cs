using System;
using System.Collections.Generic;
using MyConsoleTracing.Entity;

namespace MyConsoleTracing.Service
{
    public class LiAlgoritm
    {
        public bool Wave(Node[,] matrix, Node start, Node end)
        {
            List<Node> lastWaves = new List<Node> { start };
            List<Node> currentWaves = new List<Node>();

            bool isFounded = false;
            int step = 1;
            int height = matrix.GetLength(0);
            int width = matrix.GetLength(1);
            start.Step = 0;

            while (step < height * width && !isFounded)
            {
                foreach (var wave in lastWaves)
                {
                    int x = wave.X;
                    int y = wave.Y;

                    if (x + 1 < width)
                    {
                        Node neighbor = matrix[y, x + 1];
                        if (neighbor.X == end.X && neighbor.Y == end.Y)
                        {
                            end.Step = step;
                            isFounded = true;
                        }

                        if (neighbor.Value == ' ')
                        {
                            neighbor.Step = step;
                            neighbor.Value = '^';
                            currentWaves.Add(neighbor);
                        }
                        
                    }

                    if (x - 1 >= 0)
                    {
                        Node neighbor = matrix[y, x - 1];
                        if (neighbor.X == end.X && neighbor.Y == end.Y)
                        {
                            end.Step = step;
                            isFounded = true;
                        }

                        if (neighbor.Value == ' ')
                        {
                            neighbor.Step = step;
                            neighbor.Value = '^';
                            currentWaves.Add(neighbor);
                        }
                     
                    }

                    if (y + 1 < height)
                    {
                        Node neighbor = matrix[y + 1, x];
                        if (neighbor.X == end.X && neighbor.Y == end.Y)
                        {
                            end.Step = step;
                            isFounded = true;
                        }

                        if (neighbor.Value == ' ')
                        {
                            neighbor.Step = step;
                            neighbor.Value = '^';
                            currentWaves.Add(neighbor);
                        }
                        
                    }

                    if (y - 1 >= 0)
                    {
                        Node neighbor = matrix[y - 1, x];
                        if (neighbor.X == end.X && neighbor.Y == end.Y)
                        {
                            end.Step = step;
                            isFounded = true;
                        }

                        if (neighbor.Value == ' ')
                        {
                            neighbor.Step = step;
                            neighbor.Value = '^';
                            currentWaves.Add(neighbor);
                        }
                    
                    }
                    
                    
                }
                
                lastWaves = currentWaves;
                currentWaves = new List<Node>();
                step++;
            }


            return isFounded;
        }

        public List<Node> GetConnection(Node[,] matrix, Node start, Node end)
        {
            if (end.Step == 0 && end != start)
            {
                return new List<Node>();
            }

            List<Node> path = new List<Node>();
            Node current = end;

            while (current.Step > 0)
            {
                if (current.Step != end.Step)
                {
                    current.Value = '*';
                }

                path.Add(current);
                int cx = current.X;
                int cy = current.Y;
                int cstep = current.Step;

                if (cx + 1 < matrix.GetLength(1) && matrix[cy, cx + 1].Step == cstep - 1)
                {
                    current.Step = -1;
                    current = matrix[cy, cx + 1];
                }
                else if (cx - 1 >= 0 && matrix[cy, cx - 1].Step == cstep - 1)
                {
                    current.Step = -1;
                    current = matrix[cy, cx - 1];
                }
                else if (cy + 1 < matrix.GetLength(0) && matrix[cy + 1, cx].Step == cstep - 1)
                {
                    current.Step = -1;
                    current = matrix[cy + 1, cx];
                }
                else if (cy - 1 >= 0 && matrix[cy - 1, cx].Step == cstep - 1)
                {
                    current.Step = -1;
                    current = matrix[cy - 1, cx];
                }
            }

            path.Add(start);
            path.Reverse();
            _CleanMatrix(matrix);
            return path;
        }

        private void _CleanMatrix(Node[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].Value == '^')
                    {
                        matrix[i,j].Value = ' ';
                        matrix[i,j].Step = 0;
                    }

                }
            }
        }

        private void PrintMatrix(Node[,] matrix, Node curNode, Node startNode, Node endNode, bool isValue)
        {
            Console.Clear();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (isValue)
                    {
                        if ((matrix[i, j].X == startNode.X && matrix[i, j].Y == startNode.Y)|| (matrix[i, j].X == endNode.X && matrix[i, j].Y == endNode.Y))
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else if (matrix[i, j] == curNode)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }
                        
                        Console.Write(matrix[i, j].Value);
                        Console.ResetColor();
                    }

                    else
                    {
                        if (matrix[i, j] == startNode || matrix[i, j] == endNode)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else if (matrix[i, j] == curNode)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }
                        
                        Console.Write(matrix[i, j].Step + " ");
                        Console.ResetColor();
                    }
                }
                
                Console.WriteLine();
            }
        }
    }
}