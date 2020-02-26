using System.Collections.Generic;
using Models.StateMachine;

namespace Service.StateMachine
{
    public interface IStateMachineService
    {
        IEnumerable<StateMachineModel> CreateStateMachine(IEnumerable<StateMachinceArgs> args);
    }
}