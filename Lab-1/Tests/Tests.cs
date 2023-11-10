using OOP_labs.Abstractions;
using OOP_labs.Entities;
using OOP_labs.Entities.Collections;
using OOP_labs.Helpers;
using Xunit;
using Xunit.Abstractions;
using Single = OOP_labs.Entities.Collections.Single;

namespace OOP_labs.Tests;

public class Tests
{
    private readonly List<Artist> _artists;
    private readonly Dictionary<string, Artist> _artistsDictionary;
    private readonly ITestOutputHelper _output;

    /// <summary>
    ///     Loads test data.
    /// </summary>
    public Tests(ITestOutputHelper output)
    {
        _output = output;

        _artists = DataLoader.GetArtists();
        _artistsDictionary = _artists.ToDictionary(a => a.Alias);
    }

    /// <summary>
    ///     To make sure it fails as it supposed to
    /// </summary>
    [Fact]
    public void FailingTest()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddArtistFilter("Gones").GetQuery();
        Assert.Contains(query.GetFilteredArtists().First(), _artists);
    }

    /// <summary>
    ///     Ensures that it returns everything if no filters are set up
    /// </summary>
    [Fact]
    public void GetAllSongs()
    {
        var query = new SearchEngine().ResetQuery(_artists).GetQuery();
        Assert.Equal(query.GetFilteredPieces().Count, _artists.SelectMany(a => a.ReleasedTracks).Count());
    }

    [Fact]
    public void FindingArtistByName()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddArtistFilter("G Jones").GetQuery();
        Assert.Equal(_artistsDictionary["G Jones"], query.GetFilteredArtists().First());
    }

    [Fact]
    public void FindingArtistByPartialName()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddArtistFilter("Sward").GetQuery();
        Assert.Equal(_artistsDictionary["Swardy"], query.GetFilteredArtists().First());
    }

    [Fact]
    public void FindingArtistByPieceName()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddPieceNameFilter("The Schism").GetQuery();
        Assert.Equal(_artistsDictionary["Haywyre"], query.GetFilteredArtists().First());
    }

    [Fact]
    public void FindingArtistsByGenre()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddGenreFilter("House").GetQuery();
        query.GetFilteredPieces().ForEach(i => _output.WriteLine($"{i.Author.Alias} - {i.Name}"));
        var result = query.GetFilteredArtists();
        Assert.Single(result);
        Assert.Equal(_artistsDictionary["Haywyre"], result.First());
    }

    [Fact]
    public void FindingAlbumByName()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddAlbumFilter("Dawn").GetQuery();
        Assert.Equal(
            _artistsDictionary["Culprate"].ReleasedAlbums.First(a => a.Name == "Dawn"),
            query.GetFilteredPieceCollections().First());
    }

    [Fact]
    public void FindingAlbumBySong()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddPieceNameFilter("Dichotomy").GetQuery();
        Assert.Equal(
            _artistsDictionary["Haywyre"].ReleasedAlbums.First(a => a.Name == "Two Fold Pt. 1"),
            query.GetFilteredPieceCollections().First());
    }

    [Fact]
    public void FindingSongsByGenre()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddGenreFilter("Dance").GetQuery();
        var result = query.GetFilteredPieces();
        Assert.Equal(6, result.Count);
        result.ForEach(i => _output.WriteLine(i.Name));
    }

    [Fact]
    public void FindingSongByLength()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddLengthFilter(150).GetQuery();
        var result = query.GetFilteredPieces();
        Assert.Equal(3, result.Count);
        result.ForEach(i => _output.WriteLine(i.Name));
    }

    /// <summary>
    ///     To test combined filters
    /// </summary>
    [Fact]
    public void FindingSongByGenreAndAlbum()
    {
        var query = new SearchEngine().ResetQuery(_artists).AddAlbumFilter("pt. 2").AddGenreFilter("house")
            .GetQuery();
        var result = query.GetFilteredPieces();
        result.ForEach(i => _output.WriteLine(i.Name));

        Assert.Single(result);
        Assert.Equal(result.First(), _artistsDictionary["Haywyre"].ReleasedTracks.First(rt => rt.Name == "I Am You"));
    }
}