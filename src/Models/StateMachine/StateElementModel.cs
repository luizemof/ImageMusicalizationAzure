using Models.NoteGeneration;
using SkiaSharp;

namespace Models.StateMachine
{
    public class StateElementModel
    {
        public ENote Note { get; private set; }
        public SKColor Pixel { get; private set; }

        public StateElementModel(ENote note, SKColor pixel)
        {
            Note = note;
            Pixel = pixel;
        }
    }
}