namespace Game;
using Grid;
using Blocks;
using Raylib_cs;
using Color_Data;
using Helpers;
using System.Numerics;

public class Game
{

    public Grid grid;
    public int row;
    public int column;
    public Blocks selected;
    public Blocks nextBlock;
    public Color_Data colors;
    public List<Blocks> temp_list = [];
    private bool game_ended;
    public int destroyedrow = 0;
    public double lastupdate;
    public void Tick(double interval)
    {
        double currenttime = Raylib.GetTime();
        if (currenttime - lastupdate > interval - destroyedrow / 1000)
        {
            lastupdate = currenttime;
            MoveBlocks_Down(0, 1);
        }
    }
    public Game()
    {
        Raylib.InitAudioDevice();
        selected = SelectRandom();
        nextBlock = SelectRandom();
        colors = new Color_Data();
        row = 15;
        column = 10;
        grid = new Grid(row, column);
        Console.WriteLine(grid.row + "  " + grid.column);
    }
    public void Update()
    {
        Check_Input();
        DrawGrid();
        DrawNextBlock(colors.dictionary[nextBlock.id]);
        DrawBlock(11, 11, colors.dictionary[selected.id]);
        DrawText();
        Tick(0.3);
    }
    #region Draw

    public void DrawGrid()
    {
        for (int i = 0; i < grid.row; i++)
        {
            for (int j = 0; j < grid.column; j++)
            {
                int cellvalue = grid.GetCell(j, i);
                Raylib.DrawRectangle(j * grid.cellsize + 11, i * grid.cellsize + 11, grid.cellsize - 1, grid.cellsize - 1, colors.dictionary[cellvalue]);
            }
        }
    }
    public void DrawBlock(int x, int y, Color color)
    {
        foreach (var i in selected.Position_EachCell(selected.rot_id))
        {
            Raylib.DrawRectangle((int)(i.X * 20 + x), (int)(i.Y * 20 + y), 19, 19, color);
        }
    }
    public void DrawNextBlock(Color color)
    {
        Raylib.DrawRectangle(234, 10, 6 * grid.cellsize, 6 * grid.cellsize, colors.dictionary[0]);
        foreach (var i in nextBlock.positions[0])
        {
            Raylib.DrawRectangle((int)i.X * 20 + 274, (int)i.Y * 20 + 40, 19, 19, color);
        }
    }
    public void DrawText()
    {
        Raylib.DrawTextEx(Raylib.GetFontDefault(), "Score :", new Vector2(220, 140), 16, 4, Color.White);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), (destroyedrow * 10).ToString(), new Vector2(220, 170), 16, 4, Color.White);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), "Press R :", new Vector2(220, 200), 16, 4, Color.White);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), "To Go Menu", new Vector2(220, 230), 16, 4, Color.White);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), "Press Q :", new Vector2(220, 260), 16, 4, Color.White);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), "To Restart", new Vector2(220, 290), 16, 4, Color.White);
    }
    #endregion 

    #region Inputs
    public void Check_Input()
    {
        int key = Raylib.GetKeyPressed();
        switch (key)
        {
            case (int)KeyboardKey.Left:
                {
                    MoveBlocks(-1, 0);
                    break;
                }

            case (int)KeyboardKey.Right:
                {
                    MoveBlocks(1, 0);
                    break;
                }
            case (int)KeyboardKey.Down:
                {
                    MoveBlocks_Down(0, 1);
                    lastupdate = (double)Raylib.GetTime();
                    break;
                }
            case (int)KeyboardKey.E:
                {
                    Rotate();
                    break;
                }
            case (int)KeyboardKey.Q:
                {
                    if (game_ended)
                    {
                        grid = new Grid(row, column);
                        game_ended = false;
                        SelectRandom(reset: true);
                    }
                    break;
                }
        }
    }
    public void MoveBlocks_Down(int x, int y)
    {
        if (IsValid_Down(x, y, selected.rot_id))
        {
            selected.Move(x, y);
        }
    }
    public void MoveBlocks(int x, int y)
    {
        if (IsValid_Move(x, y, selected.rot_id))
        {
            selected.Move(x, y);
        }
    }
    public void Rotate()
    {
        int temp = (selected.rot_id + 1) % 4 == 0 ? 0 : selected.rot_id + 1;
        if (IsValid_Move(0, 0, temp))
        {
            if (selected.rot_id == 3) selected.rot_id = 0;
            selected.rot_id += 1;
        }
    }
    #endregion

    #region isValid's
    public bool IsValid_Move(int x, int y, int rot_id)
    {
        bool moving = true;
        foreach (var i in selected.Position_EachCell(rot_id))
        {
            int newposx = x + (int)i.X;
            int newposy = y + (int)i.Y;

            if (newposx >= 0 && newposy >= 0 && newposx < column && newposy < row) // inside grid ?
            {
                if (grid.GetCell(newposx, newposy) != 0) // is there a block?
                {
                    moving = false;
                }
            }
            else moving = false;
        }
        return moving;
    }
    public bool IsValid_Down(int x, int y, int rot_id)
    {
        bool moving = true;
        bool locking = false;
        foreach (var i in selected.Position_EachCell(rot_id))
        {
            int newposx = x + (int)i.X;
            int newposy = y + (int)i.Y;

            if (newposx >= 0 && newposy >= 0 && newposx < column && newposy < row) // inside grid
            {
                if (grid.GetCell(newposx, newposy) != 0) // down is blocked
                {
                    moving = false;
                    locking = true;
                }
            }
            else moving = false;
            if (newposy == row) locking = true; // hit bottom 
        }
        if (locking && !game_ended) LockBlock();
        return moving;
    }
    #endregion

    #region Random Blocks
    public List<Blocks> GetBlocks()
    {
        return new List<Blocks>(){
        new IBlock(),
        new JBlock(),
        new SBlock(),
        new LBlock(),
        new OBlock(),
        new TBlock(),
        new ZBlock()
        };
    }
    public Blocks SelectRandom(bool reset = false)
    {
        if (temp_list.Count == 0 || reset) temp_list = GetBlocks();
        int random = new Random().Next(0, temp_list.Count); // select random number
        var temp = temp_list[random];  // select a random block
        temp_list.Remove(temp); // remove that block from temp list
        return temp; // return that block
    }

    private void LockBlock()
    {
        List<int> searchin = new List<int>();
        foreach (var i in selected.Position_EachCell(selected.rot_id))
        {
            grid.ChangeCell((int)i.X, (int)i.Y, selected.id);
            if (!searchin.Contains((int)i.Y))
            {
                searchin.Add((int)i.Y);
            }
        }
        CheckRow(searchin);
        if (selected.offset_positions.Y == nextBlock.offset_positions.Y) game_ended = true;
        selected = nextBlock;
        nextBlock = SelectRandom();
    }
    #endregion

    #region Destroy Row
    private void CheckRow(List<int> searchin)
    {
        foreach (var y in searchin)
        {
            bool found = true;
            for (int x = 0; x < column; x++)// check every row 
            {
                if (grid.GetCell(x, y) == 0) // return false if found a gap
                {
                    found = false;
                }
                else continue;
            }
            if (found) // if no gap , destroy
            {
                DestroyRow(y);
                destroyedrow++;
            }
        }
    }

    private void DestroyRow(int _column)
    {
        for (int j = _column; j != 1; j--)
        {
            for (int i = 0; i < column; i++)
            {
                grid.ChangeCell(i, j, grid.GetCell(i, j - 1));
            }
        }
    }
    #endregion
}