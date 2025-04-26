using MoreLinq.Extensions;
using MyConsoleTracing.Entity;

namespace MyConsoleTracing.Service;

public class FileParserService
{
    public int sizeX = 10;
    public int sizeY = 10;
    public List<(Node startNode, Node endNode)> ListNodes { get; private set; }= new List<(Node, Node)>();
    
    private string _filePath;
    private Dictionary<string, Node> _nodeDict = new Dictionary<string, Node>();

    public FileParserService(string filePath)
    {
        this._filePath = filePath;
    }

    public void Parse()
    {
        StreamReader streamReader = new StreamReader(this._filePath);

        while (!streamReader.EndOfStream)
        {
            string[] lineData = streamReader.ReadLine().Split(':');

            if (lineData[0].Contains("size"))
            {
                string[] sizeData = lineData[1].Split(',');
                this.sizeX = int.Parse(sizeData[0]);
                this.sizeY = int.Parse(sizeData[1]);
            }

            else if (lineData[0].Contains("node"))
            {
                string[] posData = lineData[1].Split(',');
                string nodeName = lineData[0].Split(' ')[1];

                _nodeDict[nodeName] = new Node('@', int.Parse(posData[0]), int.Parse(posData[1]));
            }

            else if (lineData[0].Contains("path"))
            {
                string[] pathData = lineData[1].Split(',');

                Node startNode;
                Node endNode;

                if (_nodeDict.TryGetValue(pathData[0].Trim(), out startNode)
                    && _nodeDict.TryGetValue(pathData[1].Trim(), out endNode))
                {
                    ListNodes.Add((startNode, endNode));
                }

            }
        }
    }
}