using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductList(int? i)
        {
            List<mvcProductModel> productList;
            HttpResponseMessage responce = GlobaleVariables.webApiClient.GetAsync("Products").Result;
            productList = responce.Content.ReadAsAsync<List<mvcProductModel>>().Result;
            PagedList<mvcProductModel> products = (PagedList<mvcProductModel>)productList.ToList().ToPagedList(i ?? 1, 3);
            return View(products);
        }

        [HttpGet]
        public ActionResult AddOrEditProduct(int id = 0)
        {
            mvcProductModel product = new mvcProductModel();
            List<mvcCategoryModel> categoryList;
            List<mvcAttributeModel> attributeList;
            if (id == 0)
            {
                HttpResponseMessage responce = GlobaleVariables.webApiClient.GetAsync("Products/GetCategory").Result;
                categoryList = responce.Content.ReadAsAsync<List<mvcCategoryModel>>().Result;
                product.CategoryList = categoryList;

                HttpResponseMessage responceA = GlobaleVariables.webApiClient.GetAsync("Products/GetAttributeLookups/" + categoryList.First().ProdCatId).Result;
                attributeList = responceA.Content.ReadAsAsync<List<mvcAttributeModel>>().Result;
                product.AttributeList = attributeList;

            }
            else
            {
                HttpResponseMessage responce = GlobaleVariables.webApiClient.GetAsync("Products/" + id).Result;
                product = responce.Content.ReadAsAsync<mvcProductModel>().Result;

            }
            return View(product);
        }

        public ActionResult AttributeList(int id)
        {
            mvcProductModel product = new mvcProductModel();
            List<mvcAttributeModel> attributeList;
            HttpResponseMessage responceA = GlobaleVariables.webApiClient.GetAsync("Products/GetAttributeLookups/" + id).Result;
            attributeList = responceA.Content.ReadAsAsync<List<mvcAttributeModel>>().Result;
            product.AttributeList = attributeList;
            return PartialView("_AttributeList", product);
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(mvcProductModel product)
        {
            if (product.ProductId == 0)
            {
                HttpResponseMessage responce = GlobaleVariables.webApiClient.PostAsJsonAsync("Products", product).Result;
                TempData["successMessage"] = "Create Successfully";
                return RedirectToAction("ProductList");
            }
            else
            {
                HttpResponseMessage responce = GlobaleVariables.webApiClient.PutAsJsonAsync("Products/" + product.ProductId, product).Result;
                TempData["successMessage"] = "Update Successfully";
                return RedirectToAction("ProductList");
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            HttpResponseMessage responce = GlobaleVariables.webApiClient.DeleteAsync("Products/" + id.ToString()).Result;
            TempData["successMessage"] = "Delete Successfully";
            return RedirectToAction("ProductList");
        }
    }
}