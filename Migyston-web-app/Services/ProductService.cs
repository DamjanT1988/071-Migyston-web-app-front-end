using Migyston_web_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;
using System.Collections.Specialized;
using System.Net;
namespace Migyston_web_app.Services
{

    public static class ProductService
    {
//---START FIELDS
        //initiate list
        static List<Product> Products { get; }

        //constructor
        static ProductService()
        {
            //initiate list
            Products = new List<Product> { };
            //get products from API
            GetProducts();
        }
        

//---CRUD FUNCTIONALITY
        public static List<Product> GetAll()
        {
            return Products;
        }

        public static Product? Get(int id) => Products.FirstOrDefault(p => p.id == id);

        public static void Add(Product product)
        {
            product.id = Products.Count() + 1;
            Products.Insert(0, product);
            SaveProduct(product);
        }

        public static void Delete(int id)
        {
            var product = Get(id);
            if (product is null)
                return;

            Products.Remove(product);
            DeleteProduct(product);
        }

        
        public static void Update(Product product)
        {
            UpdateProduct(product);
            var index = Products.FindIndex(p => p.id == product.id);
            if (index == -1)
                return;

            Products[index] = product;
        }
        

        //---OWN METHODS
        public static async void SaveProduct(Product product)
        {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:5014/Product";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
        }

        public static async void DeleteProduct(Product product)
        {
            var url = "http://localhost:5014/Product/" + product.id;
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;
        }
        public static async void UpdateProduct(Product product)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5014/Product/" + product.id;
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
        }


        public static async void GetProducts()
        {
            string baseURL = "https://localhost:7014/Product";


            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage res = await client.GetAsync(baseURL))
                {
                    using (HttpContent content = res.Content)
                    {

                        string data = await content.ReadAsStringAsync();


                        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data);


                        for (int i = obj.Count-1; i>=0; i--)
                        {

                            Products.Add(new Product
                            {
                                id = obj[i].id,
                                product_title = obj[i].product_title,
                                ean_number = obj[i].ean_number,
                                product_description = obj[i].product_description,
                                price = obj[i].price,
                                amount_storage = obj[i].amount_storage,
                                expiration_date = obj[i].expiration_date,
                                category = obj[i].category,
                                isSwedish = obj[i].isSwedish
                            });
                        }
                    }
                }
            }
        }
        }
    }


