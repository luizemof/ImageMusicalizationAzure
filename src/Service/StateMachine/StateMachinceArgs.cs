using Models.NoteGeneration;
using SkiaSharp;

namespace Service.StateMachine
{
    public class StateMachinceArgs
    {
        public ENote Note { get; private set; }
        public SKColor Pixel { get; private set; }

        public StateMachinceArgs(ENote note, SKColor pixel)
        {
            Note = note;
            Pixel = pixel;
        }
    }
}