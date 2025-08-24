namespace EasyJet.KnightsTravel.Domain.Entities;

public class Board
{
    private int[,] _cells;
    private int _size;
    private int _moveCount;
    private Position _current; 
    public Board(int size, Position position)
    {
        _size = size;
        _cells = new int[size, size];
        _moveCount = 0;
        _current = position;
        Mark(position, 1);
    }

    public bool IsMarked(Position position) => _cells[position.X, position.Y] != 0;
    public int NextStep() => _moveCount + 1;
    public bool IsInside(Position position) => position.X >= 0 && position.Y >= 0 && position.X < _size && position.Y < _size;
    public int[,] GetCells() => _cells;
    public int GetSize() => _size;
    public Position GetPosition() => _current;
    public int GetMoveCount() => _moveCount;
    public void Mark(Position position, int move) {
        if(!IsInside(position))
            throw new ArgumentOutOfRangeException(nameof(position), "Position is out of board bounds.");
        
        if (IsMarked(position))
            return;

        _cells[position.X, position.Y] = move;
        _moveCount++;
        _current = position;
    }

    public void UnMark(Position position)
    {
        if (!IsInside(position))
            throw new ArgumentOutOfRangeException(nameof(position), "Position is out of board bounds.");

        if (!IsMarked(position))
            return;

        _cells[position.X, position.Y] = 0;
        _moveCount--;
    }

    public bool IsCompleted() => _moveCount == _size * _size;

}