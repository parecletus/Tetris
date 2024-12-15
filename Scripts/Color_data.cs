namespace Color_Data;
using Raylib_cs;

public struct Color_Data
{
    public Dictionary<int, Color> dictionary;
    public Color Darkgrey = new Color(26, 31, 40, 255);
    public Color green = new Color(47, 230, 23, 255);
    public Color red = new Color(232, 18, 18, 255);
    public Color orange = new Color(226, 116, 17, 255);
    public Color yellow = new Color(237, 234, 4, 255);
    public Color purple = new Color(166, 0, 247, 255);
    public Color cyan = new Color(21, 204, 209, 255);
    public Color blue = new Color(13, 64, 216, 255);
    public Color_Data()
    {
        // couldnt find any better way of doing 
        dictionary = new Dictionary<int, Color>
        {
            { 0, Darkgrey },
            { 1, green },
            { 2, red },
            { 3, orange },
            { 4, cyan },
            { 5, blue },
            { 6, purple },
            { 7, cyan },
            { 8, blue }
        };

    }
}