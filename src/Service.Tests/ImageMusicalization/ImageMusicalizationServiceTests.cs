using System;
using System.Collections.Generic;
using System.Linq;
using Models.NoteGeneration;
using Models.SequenceGenerator;
using Models.StateMachine;
using Moq;
using NUnit.Framework;
using Service.ImageMusicalization;
using Service.NoteGeneration;
using Service.SequenceGenerator;
using Service.StateMachine;
using SkiaSharp;

namespace Service.Tests.ImageMusicalization
{
    public class ImageMusicalizationServiceTests
    {
        [Test]
        public void GivenIHaveAnImageInBase64_AndImageMusicalizationService_WhenICallCreateSongFromImage_ThenShouldReturnAListOfNotes()
        {
            // Given
            var imageBase64 = Constants.ImageBase64;
            var noteGenerationMock = new Mock<INoteGenerationService>();
            var stateMachineServiceMock = new Mock<IStateMachineService>();
            var sequenceGeneratorService = new Mock<ISequenceGeneratorService>();

            var imageMusicalizationService = new ImageMusicalizationService(noteGenerationMock.Object, stateMachineServiceMock.Object, sequenceGeneratorService.Object);

            // When
            var result = imageMusicalizationService.CreateSongFromImage(imageBase64);

            // Then
            noteGenerationMock.Verify(x => x.Generate(imageBase64), Times.Once);
            stateMachineServiceMock.Verify(x => x.CreateStateMachine(It.IsAny<IEnumerable<StateMachinceArgs>>()), Times.Once);
            sequenceGeneratorService.Verify(x => x.GenerateSequence(It.IsAny<StateMachineModel>()), Times.Once);
        }
    }
}