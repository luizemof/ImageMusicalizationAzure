using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Models.StateMachine;

[assembly: InternalsVisibleTo("Service.Tests")]
namespace Service.StateMachine
{
    internal class StateMachineService : IStateMachineService
    {
        public IEnumerable<StateMachineModel> CreateStateMachine(IEnumerable<StateMachinceArgs> args)
        {
            throw new System.NotImplementedException();
        }
    }
}