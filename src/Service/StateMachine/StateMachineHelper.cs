using System;
using Models.StateMachine;
using SkiaSharp;

namespace Service.StateMachine
{
    public static class StateMachineHelper
    {
        public static double CalculateProbability(this StateMachineModel model, LinkedStateMachineModel linkedStateMachineModel)
        {
            var totalOfSetValue = CalculateTotalSetValue(model);
            var distance = model.StateElement.Pixel.CalculateDistance(linkedStateMachineModel.NextState.StateElement.Pixel);
            return (linkedStateMachineModel.NextState.StateElement.NumberOfElements + distance) / totalOfSetValue;
        }

        private static double CalculateTotalSetValue(StateMachineModel model)
        {
            double total = model.StateElement.NumberOfElements;
            foreach (var linkedState in model.LinkedStates)
            {
                total += linkedState.NextState.StateElement.NumberOfElements;
                total += model.StateElement.Pixel.CalculateDistance(linkedState.NextState.StateElement.Pixel);
            }

            return total;
        }
    }
}