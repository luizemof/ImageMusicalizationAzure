using Models.SequenceGenerator;

namespace Service.SequenceGenerator
{
    public interface ISequenceGeneratorService
    {
        SequenceGeneratorModel GenerateSequence();
    }
}