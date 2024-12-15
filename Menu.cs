namespace Menu;

using System.Numerics;
using Raylib_cs;
using MusicPlayer;
public class Menu
{
    public bool menu_over;
    public Font font = Raylib.GetFontDefault();
    public int offset;
    public Music music;
    public double lastupdate;
    private string song_name = "";
    private int volume = 3;
    public void Tick(double interval)
    {
        double currenttime = Raylib.GetTime();
        if (currenttime - lastupdate > interval)
        {
            lastupdate = currenttime;
            MoveFont();
        }
    }

    public Menu()
    {
        menu_over = true;
    }
    public void MoveFont()
    {
        offset += 1;
    }
    public void Draw_Font()
    {
        Raylib.GetFontDefault();
        Raylib.DrawTextEx(font, "Press R to Tetris", new Vector2(20, 20), 30, 2, Color.White);
        Raylib.DrawTextEx(font, "Current Song: " + song_name, new Vector2(20, 90), 16, 4, Color.White);
        Raylib.DrawTextEx(font, "Music Volume :" + volume, new Vector2(20, 120), 16, 4, Color.White);
        Raylib.DrawTextEx(font, "Press <- and -> to Change Volume", new Vector2(20, 150), 12, 4, Color.White);
        Raylib.DrawTextEx(font, "Press P Pause / Play", new Vector2(20, 180), 12, 4, Color.White);
        Raylib.DrawTextEx(font, "Press M to Mute", new Vector2(20, 210), 16, 4, Color.White);
        Raylib.DrawTextEx(font, "Press N to Play next", new Vector2(20, 240), 16, 4, Color.White);
        Raylib.DrawTextEx(font, "Press Esc to Close", new Vector2(20, 270), 16, 4, Color.White);
    }
    public void Update()
    {
        Tick(0.1);
        Input();
        Draw_Font();
    }
    public void Input()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            menu_over = true;
        }
    }
    public void SongName(Songs songs, int _volume)
    {
        volume = _volume;
        song_name = songs.name.ToString();
    }
    public void OpenMenu()
    {
        menu_over = false;
    }
}