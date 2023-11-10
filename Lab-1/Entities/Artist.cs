using OOP_labs.Abstractions;

namespace OOP_labs.Entities;

/// <summary>
///     Real human bean and a real hero
/// </summary>
public class Artist
{
    public Artist(string alias, string firstName, string lastName, List<PieceCollection> releasedAlbums,
        List<Piece> releasedTracks)
    {
        Alias = alias;
        FirstName = firstName;
        LastName = lastName;
        ReleasedAlbums = releasedAlbums;
        ReleasedTracks = releasedTracks;
    }

    public string Alias { get; set; }

    /// <summary>
    ///     First name of the bean.
    /// </summary>
    private string FirstName { get; }

    /// <summary>
    ///     Last name of the bean.
    /// </summary>
    private string LastName { get; }

    /// <summary>
    ///     Full name including the alias. For search optimization
    /// </summary>
    public string FullName => $"{FirstName} {LastName} {Alias}";

    public List<PieceCollection> ReleasedAlbums { get; set; }

    public List<Piece> ReleasedTracks { get; set; }

    public List<string> TrackGenres => ReleasedTracks.SelectMany(rt => rt.Genres).Distinct().ToList();

    public override string ToString()
    {
        return Alias;
    }
}