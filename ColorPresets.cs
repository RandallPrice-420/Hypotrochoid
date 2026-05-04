using System;
using System.Drawing;
using System.Linq;


using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Spirograph_v1
{
    public struct ColorPreset
    {
        public string  Name;
        public Color[] Colors;

        private int intPreset = 0;

        // Parameterized constructor.
        public ColorPreset(string name, Color[] colors)
        {
            Name = name ?? $"Preset{intPreset++}";
            Colors = colors;
        }

    }   // struct ColorPreset


    public enum Color_Presets
    {
        Flag   = 0,
        Mix    = 1,
        Random = 2,
        VT     = 3
    }



    public class ColorPresets
    {
        private static Random random = new Random();

        private static readonly KnownColor[] allColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));



        public ColorPreset[] MyColors;



        public ColorPreset[] GetColorPresets()
        {
            Color[] presetFlag   = [Color.Red, Color.White, Color.Blue];
            Color[] presetMix    = [Color.Red, Color.Green, Color.Blue, Color.Orange, Color.Orchid];
            Color[] presetRandom = [GetRandomColor(), GetRandomColor(),GetRandomColor(),GetRandomColor()];
            Color[] presetVT     = [Color.Orange, Color.Maroon];

            MyColors =[
                new ColorPreset("Random", presetFlag),
                new ColorPreset("Mix",    presetMix),
                new ColorPreset("VT",     presetRandom),
                new ColorPreset("Flag",   presetVT)
            ];

            return MyColors;

        }   // GetColorPresets()


        public static string[] GetEnumNames()
        {
            string[] enumNames = Enum.GetValues(typeof(Color_Presets))
                                     .Cast<Color_Presets>()
                                     .Select(v => v.ToString())
                                     .ToArray();
            return enumNames;   

        }   // GetEnumNames()


        public static Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }


        public static Color GetRandomKnownColor()
        {
            int index = random.Next(allColors.Length);
            KnownColor randomColorName = allColors[index];

            return Color.FromKnownColor(randomColorName);
        }


    }   // class ColorPresets

}   // namespace Spirograph_v1


