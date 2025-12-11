using System.Reflection;

namespace JustTip.Application;

/// <summary>
/// Class for finding assembly in tests
/// </summary>
public static class JtApplicationeAssemblyReference
{
    /// <summary>
    /// Project Assembly
    /// </summary>
    public static readonly Assembly Assembly = typeof(JtApplicationeAssemblyReference).Assembly;
}
