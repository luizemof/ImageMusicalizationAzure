using System;
using System.Collections.Generic;

namespace Models.StateMachine
{
    public class StateMachineModel
    {
        public Guid Id { get; set; }
        public StateElementModel StateElement { get; set; }
        public IEnumerable<LinkedStateMachineModel> LinkedStates { get; set; }
    }
}