using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp_HttpExample;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace CreateRating.Function
{
    public static class CreateRating
    {

        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create and store a new Rating record in Azure Cosmos DB...");

            string userId = ""; //req.Query["userId"];
            string productId = "";
            string locationName = "";
            string rating = "";
            string userNotes = "";

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Rating deserializedRating = JsonConvert.DeserializeObject<Rating>(requestBody);
            
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            // Retrieve input payload data
            userId = data?.userId;
            productId = data?.productId;
            locationName = data?.locationName;
            rating = data?.rating;
            userNotes = data?.userNotes;
            


            // Validate user Id
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://serverlessohuser.trafficmanager.net/api/GetUser?userId=" + userId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(apiResponse);
                    dynamic data2 = JsonConvert.DeserializeObject(apiResponse);
                    
                    // Retrieve input payload data
                    string uid = data2?.userId;
                    string uname = data2?.userName;
                    string fullName = data2?.fullName;
                    user.SetUserId(uid);
                    user.SetUserName(uname);
                    user.SetFullName(fullName);
                }
            }
            if (userId == user.GetUserId())
            {
                //User ID is validated
            }
            userId = user.GetUserId();

            // Validate product Id
            Product product = new Product();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId=" + productId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(apiResponse);
                    dynamic data3 = JsonConvert.DeserializeObject(apiResponse);
                    
                    // Retrieve input payload data
                    string pid = data3?.productId;
                    string pname = data3?.productName;
                    string pdesc = data3?.productDescription;
                    product.SetProductId(pid);
                    product.SetProductName(pname);
                    product.SetProductDescription(pdesc);
                }
            }
            if (productId == product.GetProductId())
            {
                //Product ID is validated
            }    
            productId = product.GetProductId();        

            // Obtain a new GUID
            string g = Guid.NewGuid().ToString();
            
            // Create Rating record
            Rating ratingrec = new Rating();
            
            ratingrec.SetId(g);
            ratingrec.SetUserId(userId);
            ratingrec.SetProducId(productId);
            ratingrec.SetTimestamp(System.DateTime.Today.ToString());
            ratingrec.SetLocationName(locationName);
            ratingrec.SetRating(rating);
            ratingrec.SetUserNotes(userNotes);

            //string responseMessage = JsonConvert.SerializeObject(ratingrec);
            string responseMessage = "{\"id\":\"" + ratingrec.GetId() + "\",\"userId\":\"" + ratingrec.GetUserId() + "\",\"productId\":\"" + ratingrec.GetProductId() + "\",\"timestamp\":\"" + ratingrec.GetTimestamp()+ "\",\"locationName\":\"" + ratingrec.GetLocationName() + "\",\"rating\":\"" + ratingrec.GetRating() + "\",\"userNotes\":\"" + ratingrec.GetUserNotes() + "\"}";
        
            //Store Rating to Cosmos DB        
            string baseUri = "https://storerating-rilian.azurewebsites.net/api/StoreRating-rilian";
            //string key = "1ofQy8a49vopAlCYzd89edS0DPDJR1CUl9SagzNaz8EsbNZEXGMFaQ==";
            //string contentType = "application/json";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = responseMessage; //JsonConvert.SerializeObject(someObject);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(baseUri, httpContent);

                string resultContent = httpResponse.Content.ReadAsStringAsync().Result;

                responseMessage = "Save to Cosmos DB Status: " + httpResponse.StatusCode + ", Rating data: " + responseMessage;
            }

            return new OkObjectResult(responseMessage);
                
        }
    }


}
