using System.Collections.Generic;
using Models.SequenceGenerator;
using Models.StateMachine;

namespace Service.SequenceGenerator
{
    public interface ISequenceGeneratorService
    {
        IEnumerable<SequenceGeneratorModel> GenerateSequence(StateMachineModel startMachineModel);
    }
}