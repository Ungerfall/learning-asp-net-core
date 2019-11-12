using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NorthwindStore.Conventions
{
    public class ImageRequestPatterns : IEnumerable<Regex>
    {
        public static IReadOnlyCollection<Regex> Collection = new List<Regex>
        {
            new Regex(@"^.+/images/\d+", RegexOptions.Compiled),
            new Regex($@"^.+\.({ImageFormats.AsRegexpOrSubexpression})", RegexOptions.Compiled)
        };

        public IEnumerator<Regex> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
