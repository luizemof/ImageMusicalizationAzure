using System;

namespace Models.StateMachine
{
    public class LinkedStateMachineModel
    {
        public Guid ParentId { get; }
        public StateMachineModel NextState { get; }
        public int Probability { get; }

        public LinkedStateMachineModel(Guid parentId, StateMachineModel nextState, int probability)
        {
            ParentId = parentId;
            NextState = nextState;
            Probability = probability;
        }
    }
}