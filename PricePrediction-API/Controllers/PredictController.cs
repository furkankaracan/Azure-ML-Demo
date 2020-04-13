using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PricePrediction_API.Models;
using Newtonsoft.Json;
using PricePrediction_API.Helpers;

namespace PricePrediction_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        readonly string[] columnNames = new string[] { "make", "body-style", "wheel-base", "engine-size", "horsepower", "peak-rpm", "highway-mpg" };


        [HttpPost]
        public async Task<IActionResult> PredictPrice(CarModel car)
        {
            string output = "";
            var outcome = new List<String>();
            CarModel model = null;

            if (car.selectedModel != null)
                output = await InvokeRequestResponseService(car);

            var previousPredictions = new List<CarModel>();

            if (output != null && !string.IsNullOrEmpty(output))
            {

                var predicted = PredictResultHelper.FromJson(output);
                var result = predicted.Results.output.value.Values[0];

                if (result != null && result.Length > 0)
                    outcome = result.ToList();

                model = new CarModel()
                {
                    selectedModel = outcome[0],
                    selectedBody = outcome[1],
                    wheelBase = Convert.ToInt32(outcome[2]),
                    engineSize = Convert.ToInt32(outcome[3]),
                    horsePower = Convert.ToInt32(outcome[4]),
                    peakRpm = Convert.ToInt32(outcome[5]),
                    highwayMpg = Convert.ToInt32(outcome[6]),
                    price = outcome[7].Substring(0, (outcome[7].IndexOf('.') + 3)),
                };
            }

            return Ok(model);
        }

        private async Task<string> InvokeRequestResponseService(CarModel car)
        {
            const string apiKey = "RKjkbFBXz5yOJ2SvLM4d1N0UjSjZnJO3D0FsXFxfefqE/my6pyOTu8qI/DGc99EvqntZyNPwvCiL7rObYlsAaw==";
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, ArrayPairs>() {
                        {
                            "input1",
                            new ArrayPairs() {
                                ColumnNames = columnNames,
                                Values = new string[,] {
                                    {
                                        car.selectedModel,
                                        car.selectedBody,
                                        car.wheelBase.ToString(),
                                        car.engineSize.ToString(),
                                        car.horsePower.ToString(),
                                        car.peakRpm.ToString(),
                                        car.highwayMpg.ToString()
                                    },
                                    {
                                           car.selectedModel,
                                        car.selectedBody,
                                        car.wheelBase.ToString(),
                                        car.engineSize.ToString(),
                                        car.horsePower.ToString(),
                                        car.peakRpm.ToString(),
                                        car.highwayMpg.ToString()
                                    }
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                };



                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                try
                {
                    // var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://ussouthcentral.services.azureml.net/workspaces/9a262786c5e24526898e2b6de0f4bd6e/services/9209f59e7d0b46d3a5d1f500181a1323/execute?api-version=2.0&details=true"));
                    // request.Content = new StringContent(JsonConvert.SerializeObject(car));
                    // request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/9a262786c5e24526898e2b6de0f4bd6e/services/9209f59e7d0b46d3a5d1f500181a1323/execute?api-version=2.0&details=true");

                    var content = JsonConvert.SerializeObject(scoreRequest);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync("", byteContent).Result;

                    string res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return res;

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }

    public class ArrayPairs
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
}