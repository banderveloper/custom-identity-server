using System.Reflection;
using AutoMapper;

namespace IdentityServer.Application.Common.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
        => ApplyMappingsFromAssembly(assembly);

    // Method invoked at MAIN, applies mapping profiles (calls Mapping method) for each type that inherits from IMappable
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        // Get all classes than implements IMappable interface
        var mappableTypes = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i == typeof(IMappable)))
            .ToList();

        // run above each class that implements IMappable interface
        foreach (var mappableType in mappableTypes)
        {
            // locally creates instance of class than inherits from IMappable
            var instance = Activator.CreateInstance(mappableType);

            // get MAPPING method, got from IMappable
            var method = mappableType.GetMethod("Mapping");

            // invokes method Mapping at given instance at pass AssemblyMappingProfile there
            method?.Invoke(instance, new object?[] { this });
        }
    }
}