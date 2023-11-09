using OOP_labs.Abstractions;

namespace OOP_labs.Entities.Collections;

public class Album : PieceCollection
{
    public Album(string name, List<Piece> trackList) : base(name, trackList)
    {
    }
}