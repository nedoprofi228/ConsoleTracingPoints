
using MyConsoleTracing.Entity;
using MyConsoleTracing.Exceptions;
using MyConsoleTracing.Service;

MatrixService matrixService = new MatrixService();
FileParserService fileParserService = new FileParserService("Data.txt");

fileParserService.Parse();
Node[,] matrix = matrixService.CreateMatrix(fileParserService.sizeX, fileParserService.sizeY);

foreach (var nodes in fileParserService.ListNodes)
{
    if (!matrixService.AddElements(matrix, nodes.startNode, nodes.endNode))
    {
        Console.WriteLine($"не возможно разместить на плате эти элементы \n {nodes.startNode}\n {nodes.endNode}");
        Console.ReadKey();
    }
}

try
{
    if (fileParserService.ListNodes.Count == 0)
    {
        throw new NodesIsEmptyException("на плате отсутствуют элементы");
    }
    
    List<List<Node>> connections = matrixService.GetConnections(matrix);
    matrixService.DrawMatrix(matrix, connections, 200);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("нажмите на клавишу чтобы завершить программу");
    Console.ReadKey();
}
