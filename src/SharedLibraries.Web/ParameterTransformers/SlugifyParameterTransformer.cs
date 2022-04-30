namespace SharedLibraries.Web.ParameterTransformers;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;


internal class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        // Slugify value
        return value == null ? null : Regex.Replace(input: value!.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
