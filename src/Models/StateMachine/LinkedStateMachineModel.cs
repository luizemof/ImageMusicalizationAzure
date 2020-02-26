using System;

namespace Models.StateMachine
{
    public class LinkedStateMachineModel
    {
        public Guid ParentId { get; set; }
        public StateMachineModel NextState { get; set; }
        public int Probability { get; set; }
    }
}