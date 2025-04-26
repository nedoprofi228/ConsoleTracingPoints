namespace MyConsoleTracing.Entity;

public class Node
{
    public char Value { get; set; } = ' ';
    public int Step { get; set; } = 0;
    public int X {get; set; } = 0;
    public int Y {get; set; } = 0;
    

    public Node(Node node)
    {
        Value = node.Value;
        Step = node.Step;
        X = node.X;
        Y = node.Y;
    }

    public Node(char value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"x:{X} y:{Y} value:{Value}";
    }
}
