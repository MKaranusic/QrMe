using System.Globalization;
using System.Linq;
using System.Text;

namespace Virgin.MSGraph.Helpers
{
    internal static class UsernameHelper
    {
        public static string CreateUsername(string name, string surname)
        {
            var originalName = (name + "-" + surname).ToLower();

            var decomposed = originalName.Normalize(NormalizationForm.FormD);
            char[] filtered = decomposed
                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            var username = ReplaceExceptionalCharachter(filtered).Replace(" ", string.Empty);
            return username;
        }

        private static string ReplaceExceptionalCharachter(char[] username)
        {
            for (int i = 0; i < username.Length; i++)
            {
                if (username[i] == 'đ')
                    username[i] = 'd';
            }

            return new string(username);
        }
    }
}
