using System.Numerics;

namespace Blocks;


public class Blocks
{
    public int id;
    public int rot_id = 0;  //rotation id
    public Vector2 offset_positions = new Vector2(0, 0);
    public Dictionary<int, List<Vector2>> positions = new Dictionary<int, List<Vector2>>();

    // just to write it easier
    protected List<Vector2> Position_List(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        return new List<Vector2>(){
            new Vector2(x1, y1),
            new Vector2(x2, y2),
            new Vector2(x3, y3),
            new Vector2(x4, y4)
        };
    }
    public List<Vector2> Position_EachCell(int _rot)
    {
        var templist = new List<Vector2>();
        foreach (Vector2 cell in positions[_rot])
        {
            Vector2 sa = new Vector2((int)(cell.X + offset_positions.X), (int)(cell.Y + offset_positions.Y));
            templist.Add(sa);
        }
        return templist;
    }
    public void Move(int x, int y)
    {
        offset_positions += new Vector2(x, y);
    }
}
public class LBlock : Blocks
{
    public LBlock()
    {
        id = 1;
        positions.Add(0, Position_List(0, 2, 1, 0, 1, 1, 1, 2));
        positions.Add(1, Position_List(0, 1, 1, 1, 2, 1, 2, 2));
        positions.Add(2, Position_List(1, 0, 1, 1, 1, 2, 2, 0));
        positions.Add(3, Position_List(0, 0, 0, 1, 1, 1, 2, 1));
        Move(4, 1);
    }
}
public class JBlock : Blocks
{
    public JBlock()
    {
        id = 2;
        positions.Add(0, Position_List(0, 0, 1, 0, 1, 1, 1, 2));
        positions.Add(1, Position_List(0, 1, 0, 2, 1, 1, 2, 1));
        positions.Add(2, Position_List(1, 0, 1, 1, 1, 2, 2, 2));
        positions.Add(3, Position_List(0, 1, 1, 1, 2, 0, 2, 1));
        Move(4, 1);
    }
}
public class IBlock : Blocks
{
    public IBlock()
    {
        id = 3;
        positions.Add(0, Position_List(1, 0, 1, 1, 1, 2, 1, 3));
        positions.Add(1, Position_List(0, 2, 1, 2, 2, 2, 3, 2));
        positions.Add(2, Position_List(2, 0, 2, 1, 2, 2, 2, 3));
        positions.Add(3, Position_List(0, 1, 1, 1, 2, 1, 3, 1));
        Move(4, 0);
    }
}
public class OBlock : Blocks
{
    public OBlock()
    {
        id = 4;
        positions.Add(0, Position_List(0, 0, 0, 1, 1, 0, 1, 1));
        positions.Add(1, Position_List(0, 0, 0, 1, 1, 0, 1, 1));
        positions.Add(2, Position_List(0, 0, 0, 1, 1, 0, 1, 1));
        positions.Add(3, Position_List(0, 0, 0, 1, 1, 0, 1, 1));
        Move(4, 1);
    }
}
public class SBlock : Blocks
{
    public SBlock()
    {
        id = 5;
        positions.Add(0, Position_List(0, 1, 0, 2, 1, 0, 1, 1));
        positions.Add(1, Position_List(0, 1, 1, 1, 1, 2, 2, 2));
        positions.Add(2, Position_List(1, 1, 1, 2, 2, 0, 2, 1));
        positions.Add(3, Position_List(0, 0, 1, 0, 1, 1, 2, 1));
        Move(4, 1);
    }
}
public class TBlock : Blocks
{
    public TBlock()
    {
        id = 6;
        positions.Add(0, Position_List(0, 1, 1, 0, 1, 1, 1, 2));
        positions.Add(1, Position_List(0, 1, 1, 1, 1, 2, 2, 1));
        positions.Add(2, Position_List(1, 0, 1, 1, 1, 2, 2, 1));
        positions.Add(3, Position_List(0, 1, 1, 0, 1, 1, 2, 1));
        Move(4, 1);
    }
}
public class ZBlock : Blocks
{
    public ZBlock()
    {
        id = 7;
        positions.Add(0, Position_List(0, 0, 0, 1, 1, 1, 1, 2));
        positions.Add(1, Position_List(0, 2, 1, 1, 1, 2, 2, 1));
        positions.Add(2, Position_List(1, 0, 1, 1, 2, 1, 2, 2));
        positions.Add(3, Position_List(0, 1, 1, 0, 1, 1, 2, 0));
        Move(4, 1);
    }
}