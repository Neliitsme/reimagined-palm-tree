using OOP_labs.Abstractions;

namespace OOP_labs.Entities;

public class Track : Piece
{
    public Track(string name, Artist author, List<Artist> artists, List<string> genres, int length) : base(name, author,
        artists, genres, length)
    {
    }
}