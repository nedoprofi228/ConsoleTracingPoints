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
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                matrix[i, j] = new Node(' ', j, i);
            }
        }
        
        return matrix;
    }

    public bool AddElements(Node[,] matrix, Node startElement, Node endElement)
    {
        try
        {
            _nodeSets.Add((startElement, endElement));
            matrix[startElement.Y, startElement.X] = startElement;
            matrix[endElement.Y, endElement.X] = endElement;
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

        List<List<Node>> connections = new List<List<Node>>();
        List<List<List<Node>>> connectionsVariants = new List<List<List<Node>>>();
        var permutationsSets = _nodeSets.Permutations();
        
        foreach (var permutation in permutationsSets)
        {
            foreach (var nodeSet in permutation)
            {
                List<Node> connection = new List<Node>();
                
                if(!_liAlgorithm.Wave(matrix, nodeSet.start, nodeSet.end))
                {
                    break;
                }
                
                if ((connection = _liAlgorithm.GetConnection(matrix, nodeSet.start, nodeSet.end)).Count > 0)
                {
                    connections.Add(connection);
                }
                else
                {
                    _ClearMatrix(matrix);
                    break;
                }
            }
            
            if(connections.Count == _nodeSets.Count)
            {
                connectionsVariants.Add(connections);
            }
            
            connections = new List<List<Node>>();
            _ClearMatrix(matrix);
            
        }
        if (connectionsVariants.Count > 0)
        {
            return connectionsVariants.OrderBy(var => var.Sum(con => con.Count)).First();
        }
        
        throw new NotFoundConnectionsException("не существует возможных соединений");
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
            ConsoleColor color = ConsoleColor.DarkBlue + _random.Next(14);
            foreach (var node in connection)
            {
                Console.SetCursorPosition(node.X + 1, node.Y + 1);

                Console.ForegroundColor = color;
                Console.Write(node.Value);
                Console.ResetColor();
                
                Task.Delay(milliseconds).Wait();
            }
        }
    }

    private void _ClearMatrix(Node[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j].Value != '@')
                {
                    matrix[i, j].Value = ' ';
                    matrix[i, j].Step = 0;
                }
            }
        }
    }
}