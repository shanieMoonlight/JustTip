using System.Reflection;

namespace JustTip.Infrastructure;

/// <summary>
/// Class for finding assembly in tests
/// </summary>
public static class JtInfrastructureAssemblyReference
{
    /// <summary>
    /// Project Assembly
    /// </summary>
    public static readonly Assembly Assembly = typeof(JtInfrastructureAssemblyReference).Assembly;
}
