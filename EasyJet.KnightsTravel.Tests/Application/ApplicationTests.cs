using EasyJet.KnightsTravel.Application.Exceptions;
using EasyJet.KnightsTravel.Application.Services;
using EasyJet.KnightsTravel.Application.Services.Helpers;
using EasyJet.KnightsTravel.Application.Validators;
using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using Moq;
using Xunit;

namespace EasyJet.KnightsTravel.Tests.Application
{
    public class ApplicationTests
    {
        [Fact]
        public void PlayGame_ReturnsTrue_WhenFinderCompletes()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            //Setup
            var mockBoardService = new Mock<IBoardService>();
            mockBoardService.Setup(b=>b.GetBoard()).Returns(board);

            var mockPathFinderService = new Mock<IPathFinderService>();
            mockPathFinderService.Setup(b => b.FindMatch(board)).Returns(true);

            var mockLogger = new Mock<ILogger>();

            var gameService = new GameService(mockBoardService.Object, mockPathFinderService.Object, mockLogger.Object);

            //Act
            var result = gameService.PlayGame();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void PlayGame_ReturnsFalse_WhenFinderFails()
        {
            //Arrange
            var board = new Board(4, new Position(0, 0));

            //Setup
            var mockBoardService = new Mock<IBoardService>();
            mockBoardService.Setup(b => b.GetBoard()).Returns(board);

            var mockPathFinderService = new Mock<IPathFinderService>();
            mockPathFinderService.Setup(b => b.FindMatch(board)).Returns(false);

            var mockLogger = new Mock<ILogger>();

            var gameService = new GameService(mockBoardService.Object, mockPathFinderService.Object, mockLogger.Object);

            //Act
            var result = gameService.PlayGame();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void FindMatch_ReturnsFalse_WhenNoValidMoveExists()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var validPaths = new List<Position>();

            //Setup
            var mockChessMovementService = new Mock<IChessMovementService>();
            mockChessMovementService.Setup(b => b.GetValidMoves(board, board.GetPosition())).Returns(validPaths);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(mockChessMovementService.Object, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void FindMatch_ReturnsTrue_On8x8BoardValid()
        {
            //Arrange
            var board = new Board(8, new Position(0, 0));

            //Setup
            var knightMovement = new KnightMovement();
            var chessMovementService = new ChessMovementService(knightMovement);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(chessMovementService, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void FindMatch_ReturnsTrue_On5x5BoardValid()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));

            //Setup
            var knightMovement = new KnightMovement();
            var chessMovementService = new ChessMovementService(knightMovement);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(chessMovementService, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void FindMatch_ReturnsFalse_On4x4BoardNotValid()
        {
            //Arrange
            var board = new Board(4, new Position(0, 0));

            var knightMovement = new KnightMovement();
            var chessMovementService = new ChessMovementService(knightMovement);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(chessMovementService, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void FindMatch_ReturnsFalse_On1x1BoardNotValid()
        {
            //Arrange
            var board = new Board(1, new Position(0, 0));

            var knightMovement = new KnightMovement();
            var chessMovementService = new ChessMovementService(knightMovement);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(chessMovementService, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void GetValidMoves_ReturnsPositions_WhenValidMoveExists()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            var values = new Movement[]
            {
                new Movement(2,1),
                new Movement(1,2)
            };

            //Setup
            var mockPieceMovement = new Mock<IPieceMovement>();
            mockPieceMovement.Setup(b => b.GetPossibleMoves()).Returns(values);
            var pathFinderService = new ChessMovementService(mockPieceMovement.Object);

            //Act
            var result = pathFinderService.GetValidMoves(board,board.GetPosition());

            //Assert
            Assert.Equal(result.Count, values.Length);
        }

        [Fact]
        public void GetValidMoves_ReturnsNoPositions_WhenNoValidMoveExists()
        {
            //Arrange
            var board = new Board(2, new Position(0, 0));
            var values = new Movement[]
            {
                new Movement(2,1),
                new Movement(1,2)
            };

            //Setup
            var mockPieceMovement = new Mock<IPieceMovement>();
            mockPieceMovement.Setup(b => b.GetPossibleMoves()).Returns(values);
            var pathFinderService = new ChessMovementService(mockPieceMovement.Object);

            //Act
            var result = pathFinderService.GetValidMoves(board, board.GetPosition());

            //Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void ValidateBoardSize_DoesNotThrow_WhenValidBoard(int size)
        {
            //Setup
            var gameValidator = new GameValidator();

            //Act
            var ex = Record.Exception(()=>gameValidator.ValidateBoardSize(size));

            //Assert
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(9)]
        [InlineData(10)]
        public void ValidateBoardSize_Throws_WhenInvalidBoard(int size)
        {
            //Setup
            var gameValidator = new GameValidator();

            //Act and Assert
            Assert.Throws<InvalidBoardSizeException>(() => gameValidator.ValidateBoardSize(size));
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 5)]
        [InlineData(4, 5)]
        [InlineData(1, 7)]
        [InlineData(5, 7)]
        [InlineData(6, 7)]
        public void ValidatePositionValue_DoesNotThrow_WhenValidPosition(int positionValue, int boardSize)
        {
            //Setup
            var gameValidator = new GameValidator();

            //Act
            var ex = Record.Exception(() => gameValidator.ValidatePositionValue(positionValue,boardSize));

            //Assert
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(5,5)]
        [InlineData(8,5)]
        [InlineData(-1,5)]
        [InlineData(8, 8)]
        [InlineData(-10, 8)]
        [InlineData(10, 8)]
        public void ValidatePositionValue_Throws_WhenInvalidPosition(int positionValue, int boardSize)
        {
            //Setup
            var gameValidator = new GameValidator();

            //Act and Assert
            Assert.Throws<InvalidPositionException>(() => gameValidator.ValidatePositionValue(positionValue,boardSize));
        }


        [Fact]
        public void PlayGame_ReturnsTrue_WhenFinderCompletes_FullTest()
        {
            //Arrange
            var board = new Board(5, new Position(0, 0));
            //Setup
            var gameService = GameServiceBuilder.Build(board);

            //Act
            var result = gameService.PlayGame();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void PlayGame_ReturnsFalse_WhenFinderFails_FullTest()
        {
            //Arrange
            var board = new Board(4, new Position(0, 0));
            //Setup
            var gameService = GameServiceBuilder.Build(board);

            //Act
            var result = gameService.PlayGame();

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(4,4)]
        [InlineData(0,4)]
        [InlineData(4,0)]
        public void FindMatch_ReturnsTrue_OnEdgePositionsBoardValid(int x, int y)
        {
            //Arrange
            var board = new Board(5, new Position(x, y));

            //Setup
            var knightMovement = new KnightMovement();
            var chessMovementService = new ChessMovementService(knightMovement);

            var mockLogger = new Mock<ILogger>();

            var pathFinderService = new PathFinderService(chessMovementService, mockLogger.Object);

            //Act
            var result = pathFinderService.FindMatch(board);

            //Assert
            Assert.True(result);
        }

    }
}