using MudBlazor;

namespace BlazorLaboratory.Shared
{
    public static class Themes
    {
        public static MudTheme OrangeTheme = new()
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.Red.Lighten3,
                Secondary = Colors.Red.Lighten1,
                AppbarBackground = Colors.Red.Default,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.Red.Lighten1,
                Secondary = Colors.Red.Lighten1,
                AppbarBackground = Colors.Red.Darken4,
            },
            LayoutProperties = new LayoutProperties
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };

        public static MudTheme DarkForest = new()
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.Green.Lighten3,
                Secondary = Colors.Green.Lighten1,
                AppbarBackground = Colors.Green.Darken3,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.Green.Lighten1,
                Secondary = Colors.Green.Lighten1,
                AppbarBackground = Colors.Green.Darken4,
            },
            LayoutProperties = new LayoutProperties
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px"
            }
        };
    }
}
