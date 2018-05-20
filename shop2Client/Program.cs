﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace shop2Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        public static async Task RunAsync()  // async methods return Task or Task<T>
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51931/shop2/");
                    //replace http://localhost:51931 with the address returned when you run with Microsoft Edge
                  

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Test get all Customers
                    Console.WriteLine("All Customers in database by descending Customer ID:");
                    HttpResponseMessage response = await client.GetAsync("customers/all");  //customers/all is the route defined to reach the method GetAllCustomers() in the Web Api project
                    if (response.IsSuccessStatusCode)                                    
                    {
                        // read results 
                        var customers = await response.Content.ReadAsAsync<IEnumerable<CustomerClient>>(); //GetAllCustomers() returns a list of Customers
                        foreach (var customer in customers)
                        {
                            Console.WriteLine(customer);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // Test get customer with ID 1
                    Console.WriteLine("Customer 1:");
                    response = await client.GetAsync("customer/ByCustomerId/1");
                    if (response.IsSuccessStatusCode)                                                   
                    {
                        // read results 
                        var customer = await response.Content.ReadAsAsync<CustomerClient>();
                        Console.WriteLine(customer);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // Test get Customer IDs and Name for specified keyword
                    Console.WriteLine("Customers containing Dublin in Address:");
                    response = await client.GetAsync("customer/ByKeyword/Dublin");
                    if (response.IsSuccessStatusCode)  
                    {
                        // read results 
                        var customers = await response.Content.ReadAsStringAsync();
                        foreach (var m in customers)
                        {
                            Console.WriteLine(m);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // Test POST
                    Console.WriteLine("Create new Customer");
                    CustomerClient newCustomer = new CustomerClient() { CustomerID = 6, CName = "Alex Dunne", CAddress = "26 Meadow View, Raheny, Dublin",  Phone = 0876589995  };
                    response = await client.PostAsJsonAsync("customer/New", newCustomer);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Customer {0} added", newCustomer.CName);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

    }

}
