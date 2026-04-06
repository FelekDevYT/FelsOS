using IL2CPU.API.Attribs;

namespace FenixOS.System.utils;

public class ResourceManager
{
    [ManifestResourceStream(ResourceName = "FenixOS.assets.cursor.bmp")]
    public static byte[] cursorIcon;

    [ManifestResourceStream(ResourceName = "FenixOS.assets.logo.bmp")]
    public static byte[] logoIcon;
}