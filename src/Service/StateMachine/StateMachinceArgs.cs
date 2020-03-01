using Models.NoteGeneration;
using SkiaSharp;

namespace Service.StateMachine
{
    public class StateMachinceArgs
    {
        public ENote Note { get; private set; }
        public SKColor Pixel { get; private set; }
        public int NumberOfElements { get; private set; }

        public StateMachinceArgs(ENote note, SKColor pixel, int numberOfElements)
        {
            Note = note;
            Pixel = pixel;
            NumberOfElements = numberOfElements;
        }
    }
}