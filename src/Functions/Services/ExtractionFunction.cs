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
using System.Linq;

namespace Functions
{
    public static class ExtractionFunction
    {
        [FunctionName("ExtractionFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Received message");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ExtractionFunctionModel>(requestBody);

            try
            {
                var extractionService = ServiceFactory.CreateExtractionService();
                var kMeansExtractionResult = extractionService.KMeansExtraction(data.ImageBase64, data.Seed);

                return new OkObjectResult(JsonConvert.SerializeObject(kMeansExtractionResult.ToList()));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
