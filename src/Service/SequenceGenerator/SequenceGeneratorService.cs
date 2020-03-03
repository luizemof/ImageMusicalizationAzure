using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Models.NoteGeneration;
using Models.SequenceGenerator;
using Models.StateMachine;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.SequenceGenerator
{
    internal class SequenceGeneratorService : ISequenceGeneratorService
    {
        private class Range : IEquatable<Range>
        {
            public int Min { get; set; }
            public int Max { get; set; }

            public bool Equals([AllowNull] Range other)
            {
                return
                this != null
                &&
                other != null
                &&
                this.Min == other.Min
                &&
                this.Max == other.Max;
            }

            public bool IsInRange(int value)
            {
                return Min >= value && Max >= value;
            }
        }

        private const int DEFAULT_TOTAL_NOTES = 30;
        public IEnumerable<SequenceGeneratorModel> GenerateSequence(StateMachineModel startMachineModel)
        {
            if (startMachineModel == null)
                throw new ArgumentException();

            var currentState = startMachineModel;
            var sequenceGeneratorModels = new List<SequenceGeneratorModel>();
            for (int i = 0; i < DEFAULT_TOTAL_NOTES; i++)
            {
                currentState = GetNextState(currentState);
                sequenceGeneratorModels.Add(new SequenceGeneratorModel() { Note = currentState.StateElement.Note });
            }
            return sequenceGeneratorModels;
        }

        private StateMachineModel GetNextState(StateMachineModel stateModel)
        {
            Dictionary<Range, StateMachineModel> notesRange = GetNotesRange(stateModel.LinkedStates);
            var randomValue = new Random().Next(100);
            return notesRange.FirstOrDefault(pairValue => pairValue.Key.IsInRange(randomValue)).Value ?? stateModel;
        }

        private Dictionary<Range, StateMachineModel> GetNotesRange(IEnumerable<LinkedStateMachineModel> linkedStates)
        {
            int startRange = 0;
            int endRange = -1;
            var notesRange = new Dictionary<Range, StateMachineModel>();
            foreach (var linkedState in linkedStates)
            {
                startRange = endRange + 1;
                endRange = startRange + (int)(linkedState.Probability * 100);
                var range = new Range()
                {
                    Min = startRange,
                    Max = endRange
                };

                notesRange.Add(range, linkedState.NextState);
            }

            return notesRange;
        }
    }
}