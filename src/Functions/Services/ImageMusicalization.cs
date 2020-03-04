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

namespace Services
{
    public static class ImageMusicalization
    {
        [FunctionName("ImageMusicalization")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Message Received in ImageMusicalization");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyData = JsonConvert.DeserializeObject<ImageMusicalizationFunctionModel>(requestBody);
            var imageMusicalizationService = ServiceFactory.CreateImageMusicalizationService();
            var result = imageMusicalizationService.CreateSongFromImage(bodyData.ImageBase64);
            return new OkObjectResult(result);
        }
    }
}
