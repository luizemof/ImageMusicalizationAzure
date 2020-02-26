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
                new StateMachinceArgs(ENote.C, SKColors.Black),
                new StateMachinceArgs(ENote.D, SKColors.Red),
                new StateMachinceArgs(ENote.E, SKColors.Green),
                new StateMachinceArgs(ENote.F, SKColors.Blue),
                new StateMachinceArgs(ENote.G, SKColors.Yellow),
                new StateMachinceArgs(ENote.A, SKColors.Magenta),
                new StateMachinceArgs(ENote.B, SKColors.Cyan),
                new StateMachinceArgs(ENote.C_8, SKColors.White)
            };

            // When
            var stateMachineService = new StateMachineService();
            var result = stateMachineService.CreateStateMachine(args);

            // Then
            Assert.IsNotNull(result);
            CollectionAssert.IsNotEmpty(result);
            Assert.AreEqual(expected: 8, actual: result.Count());
        }
    } 
}