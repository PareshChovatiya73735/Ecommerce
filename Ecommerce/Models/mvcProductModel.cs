using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class mvcProductModel
    {
        public long ProductId { get; set; }
        public int ProdCatId { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProdName { get; set; }
        [DisplayName("Description")]
        public string ProdDescription { get; set; }
        public List<mvcCategoryModel> CategoryList { get; set; }
        public List<mvcAttributeModel> AttributeList { get; set; }

        public mvcProductModel()
        {
            CategoryList = new List<mvcCategoryModel>();
            AttributeList = new List<mvcAttributeModel>();
        }
    }




}