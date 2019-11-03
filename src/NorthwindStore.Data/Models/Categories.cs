using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace NorthwindStore.Data.Models
{
    public partial class Categories
    {
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
                using var ms = new MemoryStream();
                const int offset = 78;
                ms.Write(Picture, offset, Picture.Length - offset);

                return ms.ToArray();
            }
        }

        public virtual ICollection<Products> Products { get; set; }
    }
}
