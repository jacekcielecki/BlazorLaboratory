using MudBlazor;

namespace BlazorLaboratory.Shared
{
    public static class Themes
    {
        public static MudTheme OrangeTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                Primary = Colors.Red.Lighten3,
                Secondary = Colors.Red.Lighten1,
                AppbarBackground = Colors.Red.Default,
            },
            PaletteDark = new Palette()
            {
                Primary = Colors.Red.Lighten1,
                Secondary = Colors.Red.Lighten1,
                AppbarBackground = Colors.Red.Darken4,
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };

        public static MudTheme DarkForest = new MudTheme()
        {
            Palette = new Palette()
            {
                Primary = Colors.Green.Lighten3,
                Secondary = Colors.Green.Lighten1,
                AppbarBackground = Colors.Green.Darken3,
            },
            PaletteDark = new Palette()
            {
                Primary = Colors.Green.Lighten1,
                Secondary = Colors.Green.Lighten1,
                AppbarBackground = Colors.Green.Darken4,
                //Background = Colors.Grey.Darken3,
                //TextPrimary = Colors.Shades.White
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };
    }
}
