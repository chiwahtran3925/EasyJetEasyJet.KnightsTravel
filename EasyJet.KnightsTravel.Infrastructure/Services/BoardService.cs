using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;

namespace EasyJet.KnightsTravel.Infrastructure.Services;

public class BoardService : IBoardService
{
    private readonly Board _board;

    public BoardService(Board board)
    {
        _board = board;
    }
    public bool IsMarked(Position position) => _board.IsMarked(position);
    public void Mark(Position position, int move) => _board.Mark(position, move);
    public void UnMark(Position position) => _board.UnMark(position);
    public int GetMoveCount() => _board.GetMoveCount();
    public Board GetBoard() => _board;
}
