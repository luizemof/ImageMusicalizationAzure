using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.StateMachine
{
    public class StateMachineModel
    {
        private List<LinkedStateMachineModel> _LinkedStates;

        public Guid Id { get; }
        public StateElementModel StateElement { get; }
        public IEnumerable<LinkedStateMachineModel> LinkedStates { get { return _LinkedStates; } }

        public StateMachineModel(StateElementModel element)
        {
            Id = Guid.NewGuid();
            StateElement = element;
            _LinkedStates = new List<LinkedStateMachineModel>();
        }

        public bool AddLinkedStates(IEnumerable<LinkedStateMachineModel> stateMachineModels)
        {
            var parentStateMachineModels = stateMachineModels.Where(state => state.ParentId == Id);
            if (parentStateMachineModels.Any())
            {
                _LinkedStates.AddRange(parentStateMachineModels);
                return true;
            }

            return false;
        }
    }
}