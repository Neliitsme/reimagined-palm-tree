using OOP_labs.Abstractions;

namespace OOP_labs.Entities.Collections;

public class Single : PieceCollection
{
    public Single(string name, List<Piece> trackList) : base(name, trackList)
    {
    }
}