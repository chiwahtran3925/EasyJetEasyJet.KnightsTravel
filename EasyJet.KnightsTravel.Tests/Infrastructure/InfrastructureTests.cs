using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Infrastructure.Services;
using Xunit;

namespace EasyJet.KnightsTravel.Tests.Infrastructure
{
    public class InfrastructureTests
    {
        [Fact]
        public void MarkPosition_ReturnsTrue_CheckIsMarked()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var markedPosition = new Position(2, 1);

            //Setup
            var boardService = new BoardService(board);

            //Act
            boardService.Mark(markedPosition, 1);
            var result = boardService.IsMarked(markedPosition);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void UnMarkPosition_ReturnsFalse_CheckIsMarked()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var unMarkedPosition = new Position(2, 1);

            //Setup
            var boardService = new BoardService(board);

            //Act
            var result = boardService.IsMarked(unMarkedPosition);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void MarkPosition_Throws_WhenPositionOutsideBoard()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var markedPosition = new Position(6, 6);

            //Setup
            var boardService = new BoardService(board);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=>boardService.Mark(markedPosition, 1));
        }
        [Fact]
        public void UnMarkPosition_Throws_WhenPositionOutsideBoard()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var unMarkedPosition = new Position(6, 6);

            //Setup
            var boardService = new BoardService(board);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => boardService.UnMark(unMarkedPosition));
        }

        [Fact]
        public void MarkPosition_Twice_ReturnsMoveCount()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var markedPosition = new Position(2, 1);

            //Setup
            var boardService = new BoardService(board);

            //Act
            boardService.Mark(markedPosition, 1);
            boardService.Mark(markedPosition, 1);
            var moveCount = boardService.GetMoveCount();
            //Assert
            Assert.Equal(2, moveCount);
        }

        [Fact]
        public void UnMarkPosition_Twice_ReturnsMoveCount()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var unMarkedPosition = new Position(2, 1);

            //Setup
            var boardService = new BoardService(board);

            //Act
            boardService.UnMark(unMarkedPosition);
            boardService.UnMark(unMarkedPosition);
            var moveCount = boardService.GetMoveCount();
            //Assert
            Assert.Equal(1,moveCount);
        }
    }
}