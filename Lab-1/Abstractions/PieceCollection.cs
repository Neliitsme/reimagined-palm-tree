using OOP_labs.Entities;
using OOP_labs.Interfaces;

namespace OOP_labs.Abstractions;

/// <summary>
///     Some kinda collection, either for an album or a compilation.
/// </summary>
public abstract class PieceCollection : INamed
{
    protected PieceCollection(string name, List<Piece> trackList)
    {
        Name = name;
        TrackList = trackList;
    }

    /// <summary>
    ///     Included tracks
    /// </summary>
    public List<Piece> TrackList { get; set; }

    /// <summary>
    ///     A list of Authors, co-authors, guest-artists, whatever
    /// </summary>
    public List<Artist> Artists => TrackList.SelectMany(t => t.Artists).Distinct().ToList();

    /// <summary>
    ///     Genres of the songs in the album
    /// </summary>
    public List<string> Genres => TrackList.SelectMany(t => t.Genres).Distinct().ToList();

    public string Name { get; set; }
}