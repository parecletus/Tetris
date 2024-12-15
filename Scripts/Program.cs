
namespace Program;
using Raylib_cs;
using Game;
using Menu;
using MusicPlayer;
using Helpers;
public class Program
{

    public static void Main(string[] args)
    {
        Raylib.InitWindow(364, 320, "Default");
        Raylib.SetTargetFPS(60);
        Game game = new Game();
        Menu menu = new Menu();
        MusicPlayer mp = new MusicPlayer();
        Color darkblue = new Color(44, 44, 127, 255);
        Color Black = new Color(0, 0, 0, 255);
        Raylib.PlayMusicStream(mp.currentSong.music);

        while (Raylib.WindowShouldClose() == false)
        {

            Raylib.BeginDrawing();
            if (Raylib.IsKeyPressed(KeyboardKey.R)) Helpers.Gameover();
            if (Helpers.GameIsOn)
            {
                Raylib.ClearBackground(darkblue);
                game.Update();
            }
            else
            {
                Raylib.ClearBackground(Black);
                menu.SongName(mp.currentSong, mp.music_volume);
                menu.Update();
            }
            mp.Update();

            Raylib.EndDrawing();
        }

        mp.OnDestroy();
        Raylib.CloseWindow();
    }
}