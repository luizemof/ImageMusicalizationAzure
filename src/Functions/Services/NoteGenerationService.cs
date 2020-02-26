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
    public static class NoteGenerationService
    {
        [FunctionName("NoteGenerationService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Message received in NoteGenerationService");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<NoteGenerationFunctionModel>(requestBody);
                var noteGenerationService = ServiceFactory.CreateNoteGenerationService();

                return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(noteGenerationService.Generate(data.ImageBase64)));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
