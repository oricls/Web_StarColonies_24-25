using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace StarColonies.Web.Constraints;

/// <summary>
/// Gère la contrainte sur les slugs et la mise en forme de données vers ce format.
/// </summary>
public class SlugConstraint : IRouteConstraint, IOutboundParameterTransformer
{
    private static readonly Regex SlugRegex = new(@"[a-z0-9][a-z0-9\-]*");
    
    public bool Match(HttpContext? httpContext, 
        IRouter? route, string routeKey, 
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey))
        {
            return false;
        }
        if (values[routeKey] is not string candidate)
        {
            return false;
        }
        return SlugRegex.IsMatch(candidate);
    }
    
    public string? TransformOutbound(object? value) 
        => value?.ToString()?.ToKebab() ?? null;
}