using AutoMapper;

namespace IdentityServer.Application.Common.Mappings;

// Interface created for auto mapping, each DTO/models inherits from this interface
// Method ApplyMappingsFromAssembly from AssemblyMappingProfile
// using reflections finds all classes than inherits this interface and call Mapping method in them

// this makes it unnecessary to write a large list of mapping profiles.
// Each dto will describe its own mapping config, and you won't have to look for it.
public interface IMappable
{
    // Method which calls in every instance inherited from IMappable (calls by assemblyMappingProfile)
    public void Mapping(Profile profile);
}