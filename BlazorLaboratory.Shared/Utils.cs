using System.Reflection;
using System.Resources;

namespace BlazorLaboratory.Shared;

public static class Utils
{
    /// <remarks>
    /// This is a fragile workaround for accessing string localizer from not referenced project.
    /// </remarks>
    public static string GetLocalizedString(string key)
    {
        var assembly = GetAssemblyByName("BlazorLaboratory.BlazorServer");

        ResourceManager rm = new ResourceManager("BlazorLaboratory.BlazorServer.Resources.App", assembly);

        var value = rm.GetString(key);
        return value ?? key;
    }

    private static Assembly GetAssemblyByName(string name)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().
            SingleOrDefault(assembly => assembly.GetName().Name == name);

        return assembly ?? throw new Exception($"{name} assembly not found");
    }
}
