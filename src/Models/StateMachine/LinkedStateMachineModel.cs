using System;

namespace Models.StateMachine
{
    public class LinkedStateMachineModel
    {
        public Guid ParentId { get; }
        public StateMachineModel NextState { get; }
        public double Probability { get; set; }

        public LinkedStateMachineModel(Guid parentId, StateMachineModel nextState)
        {
            ParentId = parentId;
            NextState = nextState;
        }
    }
}