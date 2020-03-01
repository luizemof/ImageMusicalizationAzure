using Models.NoteGeneration;
using Service.StateMachine;
using SkiaSharp;
using NUnit.Framework;
using System.Linq;

namespace Service.Tests.StateMachine
{
    public class StateMachineTests
    {
        [Test]
        public void GivenIHaveASequenceOfNotesAndColor_WhenICallCreateStateMachine_ThenShouldReturnStatesBasedOnNotes()
        {
            // Given
            var args = new[]
            { 
                new StateMachinceArgs(ENote.C, SKColors.Black, 1000),
                new StateMachinceArgs(ENote.D, SKColors.Red, 1000),
                new StateMachinceArgs(ENote.E, SKColors.Green, 1000),
                new StateMachinceArgs(ENote.F, SKColors.Blue, 1000),
                new StateMachinceArgs(ENote.G, SKColors.Yellow, 1000),
                new StateMachinceArgs(ENote.A, SKColors.Magenta, 1000),
                new StateMachinceArgs(ENote.B, SKColors.Cyan, 1000),
                new StateMachinceArgs(ENote.C_8, SKColors.White, 1000)
            };

            // When
            var stateMachineService = new StateMachineService();
            var result = stateMachineService.CreateStateMachine(args);

            // Then
            Assert.IsNotNull(result);
            CollectionAssert.IsNotEmpty(result);
            Assert.AreEqual(expected: 8, actual: result.Count());
            Assert.IsTrue(result.All(r => r.LinkedStates.Count() == 7));
        }
        
        [Test]
        public void GivenTheArgsIsNull_WhenICallCreateStateMachine_ThenShouldReturnEmpty()
        {
            // Given
            StateMachinceArgs[] args = null;

            // When
            var stateMachineService = new StateMachineService();
            var result = stateMachineService.CreateStateMachine(args);

            // Then
            CollectionAssert.IsEmpty(result);
        }
    } 
}