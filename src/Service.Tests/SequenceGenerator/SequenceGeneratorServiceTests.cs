using System;
using System.Collections.Generic;
using System.Linq;
using Models.NoteGeneration;
using Models.StateMachine;
using NUnit.Framework;
using Service.SequenceGenerator;
using SkiaSharp;

namespace Service.Tests.SequenceGenerator
{
    public class SequenceGeneratorServiceTests
    {
        [Test]
        public void GivenIHaveAStateMachineModel_WhenICallSequenceGenerator_ThenShouldReturnACollectionOfNotes()
        {
            // Given
            var stateMachineModel = GetStateMachineModel();

            // When
            var sequenceGeneratorService = new SequenceGeneratorService();
            var result = sequenceGeneratorService.GenerateSequence(stateMachineModel);

            // Then
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: 30, result.Count());
        }

        private StateMachineModel GetStateMachineModel()
        {
            var random = new Random();
            var stateMachineModels = new List<StateMachineModel>();
            var hexCode = "X2";
            foreach (ENote note in Enum.GetValues(typeof(ENote)))
            {
                if (note != ENote.Unknow)
                {
                    int r = random.Next(255);
                    int g = random.Next(255);
                    int b = random.Next(255);
                    var pixel = SKColor.Parse($"{r.ToString(hexCode)}{g.ToString(hexCode)}{b.ToString(hexCode)}");
                    var stateElement = new StateElementModel(note, pixel, random.Next(10000));
                    var model = new StateMachineModel(stateElement);
                    stateMachineModels.Add(model);
                }
            }

            foreach (var item in stateMachineModels)
            {
                var linkedState = CreateLinkedState(item, stateMachineModels);
                item.AddLinkedStates(linkedState);
            }

            return stateMachineModels.FirstOrDefault();
        }

        private IEnumerable<LinkedStateMachineModel> CreateLinkedState(StateMachineModel stateMachineModel, IEnumerable<StateMachineModel> stateMachineModels)
        {
            var linkedStateMachineModels = new List<LinkedStateMachineModel>();
            double probability = 1.0d / stateMachineModels.Count();
            foreach (var item in stateMachineModels)
            {
                if (item.Id != stateMachineModel.Id)
                {
                    linkedStateMachineModels.Add(new LinkedStateMachineModel(stateMachineModel.Id, item)
                    {
                        Probability = probability
                    });
                }
            }

            return linkedStateMachineModels;
        }
    }
}