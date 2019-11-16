using System.Collections;
using System.Collections.Generic;

namespace NorthwindStore.Conventions
{
    public class ImageFormats : IEnumerable<string>
    {
        public static IReadOnlyCollection<string> Collection = new List<string>
        {
            "png",
            "jpg",
            "jpeg",
            "bmp"
        };

        public static string AsRegexpOrSubexpression => string.Join('|', Collection);

        public IEnumerator<string> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
