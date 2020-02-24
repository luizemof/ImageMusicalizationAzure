using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Models.Functions;
using Service;

namespace Functions
{
    public static class ImageMusicalization
    {
        [FunctionName("ImageMusicalization")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Received message");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ImageMusicalizationModel>(requestBody);

            try
            {
                var KMeansService = ServiceFactory.CreateKMeansService();
                var result = KMeansService.KMeansExtraction(data.ImageBase64, data.Seed);

                return new OkObjectResult(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
