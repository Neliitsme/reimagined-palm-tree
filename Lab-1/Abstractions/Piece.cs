using OOP_labs.Entities;
using OOP_labs.Interfaces;

namespace OOP_labs.Abstractions;

/// <summary>
///     A musical piece. Could be a song or a track. I don't wanna separate the two in my scenario, but it could be easily
///     done.
/// </summary>
public abstract class Piece : INamed
{
    protected Piece(string name, Artist author, List<Artist> artists, List<string> genres, int length)
    {
        Name = name;
        Author = author;
        Artists = artists;
        Genres = genres;
        Length = length;
    }

    /// <summary>
    ///     The one who released the track
    /// </summary>
    public Artist Author { get; set; }

    /// <summary>
    ///     A list of guest-artists, co-authors, etc. Includes author.
    /// </summary>
    public List<Artist> Artists { get; set; }

    /// <summary>
    ///     Self-explanatory, genres of the track
    /// </summary>
    public List<string> Genres { get; set; }

    /// <summary>
    ///     Self-explanatory, the length of the track. In seconds
    /// </summary>
    public int Length { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Author.Alias} - {Name}";
    }
}