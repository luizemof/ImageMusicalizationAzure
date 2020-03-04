using System;
using System.Collections.Generic;
using System.Linq;
using Models.NoteGeneration;
using Models.StateMachine;
using NUnit.Framework;
using Service.StateMachine;
using SkiaSharp;

namespace Service.Tests.StateMachine
{
    public class StateMachineHelperTests
    {
        [Test]
        public void GivenIHaveLinkedStatesIntoStateMachineModel_WhenICallCalculateAndSetProbability_ThenShouldSetCalculateIntoModel()
        {
            // Given
            var stateElementModel = new StateElementModel(ENote.C, SKColors.Black, 1000);
            var stateMachineModel = new StateMachineModel(stateElementModel);
            var linkedStates = CreateLinkedStates(stateMachineModel.Id);

            stateMachineModel.AddLinkedStates(linkedStates);

            // When
            StateMachineHelper.CalculateAndSetProbability(stateMachineModel);
            
            // Then
            Assert.IsTrue(stateMachineModel.LinkedStates.All(ls => ls.Probability > 0));
        }

        [Test]
        public void GivenIHaveLinkedStates_WhenICallCalculateProbability_ThenSouldReturnTheProbabilityValue()
        {
            // Given
            var stateElementModel = new StateElementModel(ENote.C, SKColors.Black, 1000);
            var stateMachineModel = new StateMachineModel(stateElementModel);
            var linkedStates = CreateLinkedStates(stateMachineModel.Id);

            stateMachineModel.AddLinkedStates(linkedStates);

            // When
            var probability = StateMachineHelper.CalculateProbability(stateMachineModel, linkedStates.First());

            // Then
            Assert.AreEqual(expected: 0.30, actual: Math.Round(probability, 2));
        }

        private IEnumerable<LinkedStateMachineModel> CreateLinkedStates(Guid id)
        {
            var dElement = new StateElementModel(ENote.D, SKColor.Parse("ff00ff00"), 500);
            var dStateMachine = new StateMachineModel(dElement);
            var d = new LinkedStateMachineModel(id, new StateMachineModel(dElement));

            var eElement = new StateElementModel(ENote.E, SKColor.Parse("ff0000ff"), 500);
            var eStateMachine = new StateMachineModel(eElement);
            var e = new LinkedStateMachineModel(id, new StateMachineModel(eElement));

            return new []
            {
                d, e
            };
        }
    }
}