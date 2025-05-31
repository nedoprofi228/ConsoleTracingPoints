using System.Collections.Concurrent;
using MoreLinq;
using MyConsoleTracing.Entity;
using MyConsoleTracing.Exceptions;


namespace MyConsoleTracing.Service;

public class MatrixService
{
    private LiAlgoritm _liAlgorithm = new LiAlgoritm();
    private Random _random = new Random();
    private List<(Node start, Node end)> _nodeSets = new List<(Node, Node)>();
    
    public Node[,] CreateMatrix(int rows, int columns)
    {
        Node[,] matrix = new Node[rows, columns];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = new Node(' ', j, i);
            }
        }
        
        return matrix;
    }

    public void SetElementsPare(Node[,] matrix, List<(Node, Node)> elementsPare)
    {
        _nodeSets = elementsPare;
    }

    public bool AddElement(Node[,] matrix, Node node)
    {
        try
        {
            matrix[node.Y, node.X] = node;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public List<List<Node>> GetConnections(Node[,] matrix)
    {
        Console.CursorVisible = false;
        var permutations = _nodeSets.Permutations().ToList();
        int i = 1;
        int allCount = permutations.Count();
        foreach (var permutation in permutations)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Вариант {i} из {allCount} возможных");
            i++;
            
            List<List<Node>> connections = new List<List<Node>>();
            Node[,] matrixCopy = _CreateMatrixCopy(matrix);
            
            foreach (var nodeSet in permutation)
            {
                if (!_liAlgorithm.Wave(matrixCopy, nodeSet.start, nodeSet.end))
                {
                    break;
                }
                
                connections.Add(_liAlgorithm.GetConnection(matrixCopy, nodeSet.start, nodeSet.end));
            }
            
            if (connections.Count == _nodeSets.Count)
            {
                Console.Clear();
                return connections;
            }
        }
        
        throw new NotFoundConnectionsException("не существует возможных соединений");
    }

    private Node[,] _CreateMatrixCopy(Node[,] original)
    {
        var copy = new Node[original.GetLength(0), original.GetLength(1)];
        for (int i = 0; i < original.GetLength(0); i++)
        {
            for (int j = 0; j < original.GetLength(1); j++)
            {
                   copy[i, j] = new Node(original[i, j]); 
            }
        }
        
        return copy;
    }

    public void DrawMatrix(Node[,] matrix, List<List<Node>> connections, int milliseconds)
    {
        Console.WriteLine(new string('-', matrix.GetLength(1) + 2));
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j == 0 )
                {
                    Console.Write("|" + matrix[i, j].Value);
                }
                else if (j == matrix.GetLength(1) - 1)
                {
                    Console.Write(matrix[i, j].Value + "|");
                }
                else if (matrix[i, j].Value != '@')
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(matrix[i, j].Value);
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
        
        Console.WriteLine(new string('-', matrix.GetLength(1) + 2));
        
        foreach (var connection in connections)
        {
            foreach (var node in connection)
            {
                Console.SetCursorPosition(node.X + 1, node.Y + 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(node.Value);
                Console.ResetColor();
                
                Task.Delay(milliseconds).Wait();
            }
        }
        
        Console.SetCursorPosition(0, matrix.GetLength(0) + 2);
    }

    private void PrintMatrixValue(Node[,] matrix)
    {
        Console.Clear();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j].Value);
            }
            
            Console.WriteLine();
        }
    }
    
    private void PrintMatrixSteps(Node[,] matrix)
    {
        Console.Clear();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j].Step);
            }
            
            Console.WriteLine();
        }
    }
}