using CRUD_with_consuming_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CRUD_with_consuming_API.Controllers
{
    public class ProductsController : Controller
    {
        HttpClientHandler handler = new HttpClientHandler();
        Product product = new Product();
        List<Product> products = new List<Product>();

        public ProductsController()
        {
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Product>> GetAllProducts()
        {
            products = new List<Product>();
            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.GetAsync("https://fakestoreapi.com/products?limit=5"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return products;
        }

        [HttpGet]
        public async Task<Product> GetById(int Id)
        {
            product = new Product();
            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.GetAsync("https://fakestoreapi.com/products?limit=5" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return product;
        }

        [HttpPost]
        public async Task<Product> AddUpdateProduct()
        {
            product = new Product();
            using (var httpClient = new HttpClient(handler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://fakestoreapi.com/products?limit=5", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return product;
        }

        [HttpDelete]
        public async Task<string> Delete(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(handler))
            {
                using (var response = await httpClient.DeleteAsync("https://fakestoreapi.com/products?limit=5" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
