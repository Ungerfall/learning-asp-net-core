using System.Collections;
using System.Collections.Generic;

namespace NorthwindStore.Conventions
{
    public sealed class ImageContentType : IEnumerable<string>
    {
        public static readonly IReadOnlyCollection<string> Collection = new[]
        {
            "image/aces",
            "image/avci",
            "image/avcs",
            "image/bmp",
            "image/cgm",
            "image/dicom",
            "image/emf",
            "image/example",
            "image/fits",
            "image/g3fax",
            "image/heic",
            "image/heif",
            "image/hej2k",
            "image/hsj2",
            "image/jls",
            "image/jp2",
            "image/jph",
            "image/jphc",
            "image/jpm",
            "image/jpx",
            "image/jxr",
            "image/jxrA",
            "image/jxrS",
            "image/jxs",
            "image/jxsc",
            "image/jxsi",
            "image/jxss",
            "image/naplps",
            "image/png",
            "image/prs",
            "image/prs",
            "image/pwg",
            "image/t38",
            "image/tiff",
            "image/vnd",
            "image/emf",
            "image/wmf"
        };

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
