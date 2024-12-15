namespace MusicPlayer;

using Raylib_cs;

public class MusicPlayer
{
    private Playlist playlist;
    private int id;
    public int music_volume = 3;
    public Songs currentSong;
    public double timer;
    public MusicPlayer()
    {
        this.playlist = new Playlist();
        id = 0;
        Raylib.SetMusicVolume(currentSong.music, music_volume);
        currentSong = playlist.dict[id];
    }
    public void Update()
    {
        Raylib.UpdateMusicStream(currentSong.music);
        timer = Raylib.GetMusicTimePlayed(currentSong.music);
        NextSong();
        CheckInput();
    }
    public void ChangeVolume(int volume)
    {
        int temp = music_volume + volume;
        if (temp >= 0 && temp <= 5)
        {
            music_volume += volume;
            Raylib.SetMusicVolume(currentSong.music, music_volume);
        }
    }
    public void Mute()
    {
        music_volume = 0;
        Raylib.SetMusicVolume(currentSong.music, music_volume);
    }
    public void CheckInput()
    {
        int key = Raylib.GetKeyPressed();
        switch (key)
        {
            case (int)KeyboardKey.M: Mute(); break;
            case (int)KeyboardKey.Left: ChangeVolume(-1); break;
            case (int)KeyboardKey.Right: ChangeVolume(1); break;
            case (int)KeyboardKey.P: Pause_Play(); break;
            case (int)KeyboardKey.N: NextSong(true); break;
        }
    }
    public void NextSong(bool forced = false)
    {
        if (timer == Raylib.GetMusicTimeLength(currentSong.music) || forced)
        {
            id = id == 0 ? 1 : 0;
            currentSong = playlist.dict[id];
            Raylib.PlayMusicStream(currentSong.music);
        }
    }
    public void Pause_Play()
    {
        // I should use LoadMusicStream but i dont understand sbyte
        // so Whenever someone pause music restarts. ://
        if (Raylib.IsMusicStreamPlaying(currentSong.music))
        {
            Raylib.StopMusicStream(currentSong.music);
        }
        else Raylib.PlayMusicStream(currentSong.music);
    }
    public void OnDestroy()
    {
        Raylib.UnloadMusicStream(currentSong.music);
        NextSong();
    }
}
public struct Playlist
{
    public Dictionary<int, Songs> dict;
    public Playlist()
    {
        dict = new Dictionary<int, Songs>() { { 0, song1 }, { 1, song2 } };
    }
    private Songs song1 = new Songs()
    {
        name = "Song 1",
        music = Raylib.LoadMusicStream("/Tetris/Songs/Song1.mp3")
    };
    private Songs song2 = new Songs()
    {
        name = "Song 2 ",
        music = Raylib.LoadMusicStream("/Tetris/Songs/Song2.mp3")
    };
}
public struct Songs
{
    public string name;
    public Music music;
}