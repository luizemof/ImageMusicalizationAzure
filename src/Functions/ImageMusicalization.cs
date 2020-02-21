using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Extraction.KMeans;
using Models.Extraction.KMeans;
using Models.Functions;

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
                KMeansExtractionResult result;
                var imageByte = Convert.FromBase64String(data.ImageBase64);
                using (var extraction = new KMeansExtraction(imageByte, data.Seed))
                {
                    result = extraction.Run();
                }

                return new OkObjectResult(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
