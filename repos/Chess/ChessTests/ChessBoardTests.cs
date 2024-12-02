using Games;
using Xunit;

namespace ChessTests
{
    public class ChessBoardTests
    {
        [Fact]
        public void Board_CreatesCorrectSize()
        {
            var board = new ChessBoard(8, 8);

            Assert.False(board.HasMine("A1"));
        }


    }

    public class PlayerTests
    {
        [Fact]
        public void Player_Should_Start_At_Specified_Position()
        {
            // Arrange
            var player = new ChessPlayer("A1");

            // Act
            var position = player.Position;

            // Assert
            Assert.Equal("A1", position);
        }

        [Fact]
        public void Player_Should_Move_Right()
        {
            // Arrange
            var player = new ChessPlayer("A1");

            // Act
            player.Move("right");

            // Assert
            Assert.Equal("B1", player.Position);
        }
    }

}