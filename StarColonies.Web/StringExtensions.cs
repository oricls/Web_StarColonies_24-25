using System.Text.RegularExpressions;

namespace StarColonies.Web;

public static class StringExtensions
{
    public static readonly Regex SepRegex = new Regex(@"(\p{P}|\s)+");

    public static string ToKebab(this string target)
        => SepRegex.Replace(target, "-").ToLower();
}