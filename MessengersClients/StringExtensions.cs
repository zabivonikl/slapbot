namespace MessengersClients;

public static class StringExtensions
{
    public static string EscapeSymbols(this string str) => str
        .Replace("_", "\\_")
        .Replace("*", "\\*")
        .Replace("[", "\\[")
        .Replace("]", "\\]")
        .Replace("(", "\\(")
        .Replace(")", "\\)")
        .Replace("~", "\\~")
        .Replace("`", "\\`")
        .Replace(">", "\\>")
        .Replace("#", "\\#")
        .Replace("#", "\\+")
        .Replace("#", "\\-")
        .Replace("#", "\\=")
        .Replace("#", "\\|")
        .Replace("{", "\\{")
        .Replace("}", "\\}")
        .Replace(".", "\\.")
        .Replace("!", "\\!");
}