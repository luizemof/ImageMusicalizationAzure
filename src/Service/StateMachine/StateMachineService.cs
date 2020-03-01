using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.StateMachine;
using System.Linq;
using System;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.StateMachine
{
    internal class StateMachineService : IStateMachineService
    {
        public IEnumerable<StateMachineModel> CreateStateMachine(IEnumerable<StateMachinceArgs> args)
        {
            var stateMachineModels = args?.Select(arg => CreateStateMachineModels(arg)).ToList();
            if (stateMachineModels != null)
            {
                foreach (var state in stateMachineModels)
                {
                    state.AddLinkedStates(CreateLinkedStateMachineModel(state, stateMachineModels));
                }
            }
            return stateMachineModels ?? Enumerable.Empty<StateMachineModel>();
        }

        private IEnumerable<LinkedStateMachineModel> CreateLinkedStateMachineModel(StateMachineModel currentState, IEnumerable<StateMachineModel> statesToLink)
        {
            var linkedStates = new List<LinkedStateMachineModel>();
            foreach (var link in statesToLink)
            {
                if (currentState.Id != link.Id)
                {
                    var parentId = currentState.Id;
                    var linkedStateMachineModel = new LinkedStateMachineModel(parentId, link);
                    linkedStates.Add(linkedStateMachineModel);
                }
            }

            return linkedStates;
        }

        private StateMachineModel CreateStateMachineModels(StateMachinceArgs arg)
        {
            var element = new StateElementModel(arg.Note, arg.Pixel, arg.NumberOfElements);
            return new StateMachineModel(element);
        }
    }
}