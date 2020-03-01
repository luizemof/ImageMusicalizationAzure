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
using System.Collections.Generic;
using System.Linq;
using Service.StateMachine;
using Models.NoteGeneration;
using SkiaSharp;

namespace Services
{
    public static class StateMachineFunction
    {
        [FunctionName("StateMachineFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Message received in StateMachineFunction");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<StateMachineFunctionModel>>(requestBody);
                var args = ConvertToArgs(data);
                var stateMachineService = ServiceFactory.CreateStateMachineService();
                var stateMachines = stateMachineService.CreateStateMachine(args);

                return (ActionResult)new OkObjectResult(stateMachines);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        private static IEnumerable<StateMachinceArgs> ConvertToArgs(List<StateMachineFunctionModel> datas)
        {
            var stateMachineArgs = new List<StateMachinceArgs>();
            foreach (var data in datas)
            {
                if (!Enum.TryParse<ENote>(data.Note, out ENote note))
                {
                    throw new Exception($"BAD REQUEST - Invalid Note {JsonConvert.SerializeObject(data)}");
                }

                if (!SKColor.TryParse(data.ColorHex, out SKColor pixelColor))
                {
                    throw new Exception($"BAD REQUEST - Invalid Color {JsonConvert.SerializeObject(data)}");
                }

                var stateMachineArg = new StateMachinceArgs(note, pixelColor, data.NumberOfElements);
                stateMachineArgs.Add(stateMachineArg);

            }

            return stateMachineArgs;
        }
    }
}
