using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace NorthwindStore.Data.Models
{
    public partial class Categories
    {
        public const int OLE_HEADER_OFFSET = 78;

        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        /// <summary>
        /// Gets aligned <see cref="Picture"/>. Northwind images were created
        /// with Microsoft Access so they have a 78 byte OLE header.
        /// </summary>
        [NotMapped]
        public byte[] AlignedPicture
        {
            get
            {
                if (Picture == null)
                    return new byte[0];

                using var ms = new MemoryStream();
                ms.Write(Picture, OLE_HEADER_OFFSET, Picture.Length - OLE_HEADER_OFFSET);

                return ms.ToArray();
            }
        }

        public virtual ICollection<Products> Products { get; set; }

        public void UploadPicture(byte[] picture)
        {
            var len = picture.Length;
            Picture = new byte[len + OLE_HEADER_OFFSET];
            Array.Copy(picture, 0, Picture, OLE_HEADER_OFFSET, len);
        }
    }
}
