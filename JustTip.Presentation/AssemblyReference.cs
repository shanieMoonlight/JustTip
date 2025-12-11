using System.Reflection;

namespace JustTip.Presentation;

/// <summary>
/// Class for finding assembly in tests
/// </summary>
public static class JtPresentationAssemblyReference
{
    /// <summary>
    /// Project Assembly
    /// </summary>
    public static readonly Assembly Assembly = typeof(JtPresentationAssemblyReference).Assembly;
}
