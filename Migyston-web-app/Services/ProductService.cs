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
        
        static List<Product> Products { get; }
        static int nextId = 0;

        //constructor
        static ProductService()
        {
            Products = new List<Product> { };
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
            product.id = ++nextId;
            Products.Add(product);
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
            Console.WriteLine(result);
        }
        public static async void UpdateProduct(Product product)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5014/Product";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }



        //Define your static method which will make the method become part of the class
        //Also make it asynchronous meaning it is retrieving data from a api.
        //Have it void since your are logging the result into the console.
        //Which would take a integar as a argument.
        public static async void GetProducts()
        {
            //Define your base url
            string baseURL = "https://localhost:7014/Product";
            //Have your api call in try/catch block.

            //Now we will have our using directives which would have a HttpClient 
            using (HttpClient client = new HttpClient())
            {
                //Now get your rsponse from the client from get request to baseurl.
                //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                using (HttpResponseMessage res = await client.GetAsync(baseURL))
                {
                    //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                    using (HttpContent content = res.Content)
                    {
                        //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                        string data = await content.ReadAsStringAsync();



                        dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data);


                        for (int i = 0; i < obj.Count; i++)
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




/*
        public static async void SaveList()
        {
            //serialize the list to a variable
            //string json = JsonSerializer.Serialize(Products);

            for (int i = 0; i < Products.Count; i++)
            {
                var product = new Product();
                product.product_title = Products[i].product_title;
                product.ean_number = Products[i].ean_number;
                product.product_description = Products[i].product_description;
                product.amount_storage = Products[i].amount_storage;
                product.price = Products[i].price;
                product.expiration_date = Products[i].expiration_date;
                product.category = Products[i].category;
                product.isSwedish = Products[i].isSwedish;

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:5014/Product";
                using var client = new HttpClient();

                client.DefaultRequestHeaders.ConnectionClose = true;

                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

            }
        }
*/



/*
            System.Net.ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12 |
            SecurityProtocolType.Tls11 |
            SecurityProtocolType.Tls;
*/

/*
    using HttpClient client = new ();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");


var repositories = await ProcessRepositoriesAsync(client);

foreach (var repo in repositories)
{
    Console.WriteLine($"ID: {repo.Id}");
    Console.WriteLine($"Name: {repo.Product_title}");
    Console.WriteLine($"EAN: {repo.Ean_number}");
    Console.WriteLine($"Description: {repo.Product_description}");
    Console.WriteLine($"Price: {repo.Price}");
    Console.WriteLine($"Amount: {repo.Amount_storage}");
    Console.WriteLine($"Expiration: {repo.Expiration_date}");
    Console.WriteLine($"Category: {repo.Category}");
    Console.WriteLine($"Swedish?: {repo.IsSwedish}");
    Console.WriteLine($"Date: {repo.LastPushUtc}");

    Console.WriteLine();
}*/




/*
var pairs = new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("login", "abc")
};

var content = new FormUrlEncodedContent(pairs);

var client = new HttpClient { BaseAddress = new Uri("http://localhost:7014") };

// call sync
var response = client.PostAsync("/Product", content).Result;
if (response.IsSuccessStatusCode)
{
}
*/

/*
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("http://localhost:7014");
    var content = new FormUrlEncodedContent(new[]
    {
    new KeyValuePair<string, string>("price", "666")
});
    var result = await client.PostAsync("/Product", content);
    string resultContent = await result.Content.ReadAsStringAsync();
    Console.WriteLine(resultContent);
}
*/




//static HttpClient client = new HttpClient();
//public s
//tatic string path = "https://localhost:7014/Product";



/*
        static async Task OKOO()
        {
            HttpClient httpClient = new HttpClient();

            Console.WriteLine("HTTP.. Method");

            /*
            // Obtain a JWT token.
            StringContent httpContent = new StringContent(@"{ ""Product_title"": ""Sam"", ""Ean_number"": ""123"" }", Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:44319/api/Authentication/Authenticate", httpContent);

            // Save the token for further requests.
            var token = await response.Content.ReadAsStringAsync();

            Console.WriteLine(token);

            // Set the authentication header. 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            */

/*        // Send a request to fetch data.
          string requestAddress = "https://localhost:7014/Product";
          var employees = await httpClient.GetAsync(requestAddress);
          Console.WriteLine("aaaaaaaa" + employees);
      }
*/

/*

static async Task<Product> GetProductAsync()
{
    Product product = null;
    HttpResponseMessage response = await client.GetAsync("https://localhost:7014/Product");
    if (response.IsSuccessStatusCode)
    {
        product = await response.Content.ReadAsStringAsync<Product>();
    }
    return product;
}
*/
