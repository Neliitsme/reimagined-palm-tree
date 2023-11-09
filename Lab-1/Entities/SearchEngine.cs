namespace OOP_labs.Entities;

/// <summary>
///     Glorified query builder
/// </summary>
public class SearchEngine
{
    private SearchQuery _query = new();

    public SearchEngine ResetQuery(List<Artist> artists)
    {
        _query = new SearchQuery();
        _query.SetSearchData(artists);
        return this;
    }

    public SearchEngine AddArtistFilter(string? input)
    {
        _query.SetArtistFilter(input);
        return this;
    }

    public SearchEngine AddAlbumFilter(string? input)
    {
        _query.SetAlbumFilter(input);
        return this;
    }

    public SearchEngine AddGenreFilter(string? input)
    {
        _query.SetGenreFilter(input);
        return this;
    }

    public SearchEngine AddLengthFilter(int input)
    {
        _query.SetLengthFilter(input);
        return this;
    }

    public SearchEngine AddPieceNameFilter(string? input)
    {
        _query.SetPieceNameFilter(input);
        return this;
    }

    public SearchQuery GetQuery()
    {
        var result = _query;
        return result;
    }
}