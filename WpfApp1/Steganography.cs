using System.Drawing;
using System.Text;

public class Steganography
{

    public static Bitmap CodeText(string text, Bitmap bmp, bool isFile)
    {
        Color pixel = Color.Black;
        text = text.ToLower();
        int length = text.Length;
        bmp = encodeLength(bmp, length);
        int idx = 0;
        
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                if (i == 0 && j is 0 or 1 or 2 or 3) 
                {
                    continue;
                }
                if (idx == length)
                {
                    return bmp;
                }
                pixel = bmp.GetPixel(i, j);
                bmp.SetPixel(i, j, encode(text[idx], pixel));
                
                idx++;
            }
        }

        return bmp;
    }

    static Color encode(char symbol, Color pixel)
    {
        byte red = (byte) (pixel.R & 0xFC);
        byte green = (byte) (pixel.G & 0xF8);
        byte blue = (byte) (pixel.B & 0xF8);
        red += (byte) (symbol & 3);
        green += (byte) ((symbol & 28) >> 2);
        blue += (byte) ((symbol & 224) >> 5);
        Color color = Color.FromArgb(red, green, blue);
        return color;
    }

    static Bitmap encodeLength(Bitmap bmp, int length)
    {
        int a = 255;
        int part = 0;
        part = length & a;
        
        for (int y = 0; y < 4; y++)
        {
            Color color = bmp.GetPixel(0, y);
            Color newColor = encode((char) part, color);
            bmp.SetPixel(0, y, newColor);
            length = length >> 8;
            part = length & a;
        }

        return bmp;
    }
    
    static char decode(Color pixel)
    {
        byte red = (byte) (pixel.R & 0x3);
        byte green = (byte) (pixel.G & 0x7);
        byte blue = (byte) (pixel.B & 0x7);
        byte a = (byte) ((((blue << 3) + green) << 2) + red);
        return (char) a;
    }

    static int decodeLength(Bitmap bmp)
    {
        int a;
        int part = 0;
        int interval = 0;
        for (int y = 0; y < 4; y++)
        {
            Color color = bmp.GetPixel(0, y);
            a = decode(color);
            part = (a << interval) + part;
            interval += 8;
        }

        return part;
    }

    public static string DecodeText(Bitmap bmp)
    {
        Color color = Color.Aqua;
        int idx = 0;
        int length = decodeLength(bmp);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                if (i == 0 && j is 0 or 1 or 2 or 3)
                {
                    continue;
                }

                if (idx == length)
                {
                    return stringBuilder.ToString();
                }
                color = bmp.GetPixel(i, j);
                stringBuilder.Append(decode(color));
                idx++;
            }
        }

        return stringBuilder.ToString();
    }
}