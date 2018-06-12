using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataLayer
{
    public class DLProducts
    {
      public int ProductId { get; set; }
      public string  Name{ get; set; }
       
        public string Description { get; set; }
   
        public decimal Price { get; set; }

        public string SKU { get; set; }
        public string UPC { get; set; }
        public int TotalQty { get; set; }
        public string BrandName { get; set; }
     
        public string IsOutOfStock { get; set; }
        public string IsFeaturedProduct { get; set; }
        public string IsLatestProduct { get; set; }
        public string IsPopular { get; set; }
        public string Remarks { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string FashionFor { get; set; }
        public DLProducts Products { get; set; }
        public int OrderCartItemId { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CartItemId { get; set; }
        public List<Images> Image { get; set; }
    }
}
