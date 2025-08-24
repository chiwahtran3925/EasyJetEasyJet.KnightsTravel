using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Presentation;
using EasyJet.KnightsTravel.Domain.Interfaces;
using Moq;
using EasyJet.KnightsTravel.Application.Validators;
using EasyJet.KnightsTravel.Application.Exceptions;
using EasyJet.KnightsTravel.Application.Services;
using Xunit;

namespace EasyJet.KnightsTravel.Tests.Presentation
{
    public class PresentationTests
    {

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void PrintBoard_CallsVisual_ForEachCell(int size)
        {
            //Arrange
            var board = new Board(size, new Position(0, 0));
            //Setup
            var mockLogger = new Mock<ILogger>();
            var gameValidator = new GameValidator();

            var boardVisualiser = new BoardVisualiser(mockLogger.Object, gameValidator);

            //Act
            boardVisualiser.PrintBoard(board);

            //Assert
            mockLogger.Verify(v => v.Visual(It.IsAny<string>(),It.IsAny<bool>()), Times.Exactly(size * size));
        }

        [Fact]
        public void PrintBoard_Throws_WhenInvalidBoard()
        {
            //Arrange
            var board = new Board(1, new Position(0, 0));
            //Setup
            var mockLogger = new Mock<ILogger>();
            var gameValidator = new GameValidator();

            var boardVisualiser = new BoardVisualiser(mockLogger.Object, gameValidator);

            //Act and Assert
            Assert.Throws<InvalidBoardSizeException>(() => boardVisualiser.PrintBoard(board));
        }

        [Fact]
        public void PlayGame_CallsLoggerInfo()
        {
            var board = new Board(5, new Position(0, 0));
            var mockLogger = new Mock<ILogger>();
            var pathFinder = new Mock<IPathFinderService>();
            var boardService = new Mock<IBoardService>();
            boardService.Setup(b => b.GetBoard()).Returns(board);
            pathFinder.Setup(p => p.FindMatch(board)).Returns(true);

            var gameService = new GameService(boardService.Object, pathFinder.Object, mockLogger.Object);
            gameService.PlayGame();

            mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}