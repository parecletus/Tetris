namespace Grid;

using System.Numerics;
using System.Reflection;
using Raylib_cs;
public class Grid
{
    public int row;
    public int column;
    public int[] grid;
    public int cellsize = 20;
    public Grid(int _row, int _column)
    {
        row = _row;
        column = _column;
        grid = new int[row * column];
        CreateGrid(row, column);
    }
    public void CreateGrid(int _row, int _column)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = 0;
        }
    }

    public int GetCell(int x, int y)
    {
        return grid[x + y * column];
    }
    public void ChangeCell(int x, int y, int changedto)
    {
        grid[x + y * column] = changedto;
    }
}

