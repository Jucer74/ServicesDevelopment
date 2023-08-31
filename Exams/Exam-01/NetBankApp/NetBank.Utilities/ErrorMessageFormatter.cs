using System.Text.RegularExpressions;

namespace NetBank.Utilities;

public static class ErrorMessageFormatter
{
    public static string AddSpaceBetweenLowerAndCapitalLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        string pattern = @"(?=\p{Lu})(?<=\p{Ll})";

        string result = Regex.Replace(input, pattern, " ");

        return result;
    }
}