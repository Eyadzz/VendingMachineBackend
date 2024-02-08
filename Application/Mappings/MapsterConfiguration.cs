using System.Reflection;

namespace Application.Mappings;

public static class MapsterConfiguration
{
    public static void AddMapster()
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssembly);
    }
}