
using MyConsoleTracing.Entity;
using MyConsoleTracing.Exceptions;
using MyConsoleTracing.Service;

class Program
{
    public static void Main(string[] args)
    {
        MatrixService matrixService = new MatrixService();
        FileParserService fileParserService = new FileParserService("Data.txt");

        fileParserService.Parse();
        Node[,] matrix = matrixService.CreateMatrix(fileParserService.sizeY, fileParserService.sizeX);

        matrixService.SetElementsPare(matrix, fileParserService.ListNodesPare);
        foreach (var node in fileParserService.ListNodes)
        {
            
            if (!matrixService.AddElement(matrix, node))
            {
                Console.WriteLine($"не возможно разместить на плате эти элемент \n {node.ToString()}");
                Console.ReadKey();
            }
        }

        try
        {
            if (fileParserService.ListNodesPare.Count == 0)
            {
                throw new NodesIsEmptyException("на плате отсутствуют элементы");
            }
    
            List<List<Node>> connections = matrixService.GetConnections(matrix);
            matrixService.DrawMatrix(matrix, connections, 10);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("нажмите на клавишу чтобы завершить программу");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("программа успешно завершена");
        Console.WriteLine("нажмите на клавишу чтобы завершить программу");
        Console.ReadKey();
    }
}
