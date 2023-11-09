using OOP_labs.Abstractions;

namespace OOP_labs.Entities.Collections;

public class CompilationAlbum : PieceCollection
{
    public CompilationAlbum(string name, List<Piece> trackList) : base(name, trackList)
    {
    }
}