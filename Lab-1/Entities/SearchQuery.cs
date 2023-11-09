using OOP_labs.Abstractions;

namespace OOP_labs.Entities;

/// <summary>
///     Builder product
/// </summary>
public class SearchQuery
{
    private List<Artist> Artists { get; set; } = new();

    private List<Piece> Songs { get; set; } = new();

    private List<PieceCollection> Albums { get; set; } = new();
    private string? ArtistFilter { get; set; }
    private string? AlbumFilter { get; set; }
    private string? GenreFilter { get; set; }
    private int? LengthFilter { get; set; }

    private string? PieceNameFilter { get; set; }

    public void SetSearchData(List<Artist> artists)
    {
        Artists = artists;
        Songs = Artists.SelectMany(a => a.ReleasedTracks).ToList();
        Albums = Artists.SelectMany(a => a.ReleasedAlbums).ToList();
    }

    public void SetArtistFilter(string? filter)
    {
        ArtistFilter = !string.IsNullOrEmpty(filter) ? filter.ToLower() : null;
    }

    public void SetAlbumFilter(string? filter)
    {
        AlbumFilter = !string.IsNullOrEmpty(filter) ? filter.ToLower() : null;
    }

    public void SetGenreFilter(string? filter)
    {
        GenreFilter = !string.IsNullOrEmpty(filter) ? filter.ToLower() : null;
    }

    public void SetLengthFilter(int filter)
    {
        LengthFilter = filter;
    }

    public void SetPieceNameFilter(string? filter)
    {
        PieceNameFilter = !string.IsNullOrEmpty(filter) ? filter.ToLower() : null;
    }

    public List<Piece> GetFilteredPieces()
    {
        var result = Songs;

        if (!string.IsNullOrEmpty(ArtistFilter))
            result = result.Where(s => s.Artists.Any(a => a.FullName.ToLower().Contains(ArtistFilter))).ToList();

        if (!string.IsNullOrEmpty(AlbumFilter))
            result = result
                .Where(s => Albums.Any(a => a.Name.ToLower().Contains(AlbumFilter) && a.TrackList.Contains(s)))
                .ToList();

        if (!string.IsNullOrEmpty(PieceNameFilter))
            result = result.Where(s => s.Name.ToLower().Contains(PieceNameFilter)).ToList();

        if (LengthFilter is > 0) result = result.Where(s => s.Length <= LengthFilter).ToList();

        if (!string.IsNullOrEmpty(GenreFilter))
            result = result.Where(s => s.Genres.ConvertAll(g => g.ToLower()).Contains(GenreFilter)).ToList();

        return result;
    }

    public List<Artist> GetFilteredArtists()
    {
        var result = Artists;

        if (!string.IsNullOrEmpty(ArtistFilter))
            result = result.Where(a => a.FullName.ToLower().Contains(ArtistFilter)).ToList();

        if (!string.IsNullOrEmpty(AlbumFilter))
            result = result.Where(a => a.ReleasedAlbums.Any(ra => ra.Name.ToLower().Contains(AlbumFilter))).ToList();

        if (!string.IsNullOrEmpty(GenreFilter))
            result = result.Where(a => a.TrackGenres.ConvertAll(s => s.ToLower()).Contains(GenreFilter)).ToList();

        if (!string.IsNullOrEmpty(PieceNameFilter))
            result = Artists.Where(a =>
                Songs.Any(s => s.Name.ToLower().Contains(PieceNameFilter) && s.Artists.Contains(a))).ToList();

        return result;
    }

    public List<PieceCollection> GetFilteredPieceCollections()
    {
        var result = Albums;

        if (!string.IsNullOrEmpty(ArtistFilter))
            result = result.Where(c => c.Artists.Any(a => a.FullName.ToLower().Contains(ArtistFilter))).ToList();

        if (!string.IsNullOrEmpty(AlbumFilter))
            result = result.Where(c => c.Name.ToLower().Contains(AlbumFilter)).ToList();

        if (!string.IsNullOrEmpty(GenreFilter))
            result = result.Where(c => c.Genres.ConvertAll(s => s.ToLower()).Contains(GenreFilter)).ToList();

        if (!string.IsNullOrEmpty(PieceNameFilter))
            result = result.Where(c => c.TrackList.Any(s => s.Name.ToLower().Contains(PieceNameFilter))).ToList();

        return result;
    }
}