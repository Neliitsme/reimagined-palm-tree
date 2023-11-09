using OOP_labs.Abstractions;

namespace OOP_labs.Entities;

public class Song : Piece
{
    public Song(string name, Artist author, List<Artist> artists, List<string> genres, int length) : base(name, author,
        artists, genres, length)
    {
    }
}