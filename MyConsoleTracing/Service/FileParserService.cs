using MoreLinq.Extensions;
using MyConsoleTracing.Entity;

namespace MyConsoleTracing.Service;

public class FileParserService
{
    public int sizeX = 26;
    public int sizeY = 11;
    public List<(Node startNode, Node endNode)> ListNodesPare { get; private set; }= new List<(Node, Node)>();
    public List<Node> ListNodes { get; private set; } = new List<Node>();
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
            
            if (lineData[0].Contains("элемент"))
            {
                string[] posData = lineData[1].Split(',');
                string nodeName = lineData[0].Split(' ')[1];

                Node newNode =  new Node('@', int.Parse(posData[0].Trim()), int.Parse(posData[1].Trim()));
                _nodeDict[nodeName] = newNode;
                ListNodes.Add(newNode);
            }

            else if (lineData[0].Contains("злучыць"))
            {
                string[] pathData = lineData[1].Split(',');

                Node startNode;
                Node endNode;

                if (_nodeDict.TryGetValue(pathData[0].Trim(), out startNode)
                    && _nodeDict.TryGetValue(pathData[1].Trim(), out endNode))
                {
                    ListNodesPare.Add((startNode, endNode));
                }

            }
        }
        
        streamReader.Close();
    }
}