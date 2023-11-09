using OOP_labs.Abstractions;
using OOP_labs.Entities;
using OOP_labs.Entities.Collections;
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

        _artists = new List<Artist>
        {
            new("G Jones", "Greg", "Jones", new List<PieceCollection>(), new List<Piece>()),
            new("Haywyre", "Martin", "Vogt", new List<PieceCollection>(), new List<Piece>()),
            new("Swardy", "Benjamin", "Swardlick", new List<PieceCollection>(), new List<Piece>()),
            new("Culprate", "John", "Hislop", new List<PieceCollection>(), new List<Piece>()),
            new("Zes", "", "", new List<PieceCollection>(), new List<Piece>()),
            new("Maksim MC", "", "", new List<PieceCollection>(), new List<Piece>())
        };

        var albumsGjones = new List<PieceCollection>
        {
            new Single("Operator",
                new List<Piece>
                {
                    new Track("Operator", _artists[0], new List<Artist> { _artists[0] },
                        new List<string> { "Dance", "Electronic" }, 173)
                }),
            new Album("Paths", new List<Piece>
            {
                new Track("Familiar Frontiers", _artists[0], new List<Artist> { _artists[0] },
                    new List<string> { "Dance", "Electronic" }, 141),
                new Track("Too Far Gone", _artists[0], new List<Artist> { _artists[0] },
                    new List<string> { "Dance", "Electronic" }, 172),
                new Track("Which Way", _artists[0], new List<Artist> { _artists[0] },
                    new List<string> { "Dance", "Electronic" }, 178)
            })
        };
        _artists[0].ReleasedAlbums = albumsGjones;
        _artists[0].ReleasedTracks = albumsGjones.SelectMany(a => a.TrackList).ToList();

        var albumsHaywyre = new List<PieceCollection>
        {
            new Album("Two Fold Pt. 1", new List<Piece>
            {
                new Track("Prologue (Part One)", _artists[1], new List<Artist> { _artists[1] },
                    new List<string> { "Jazz", "House", "Chillout" }, 130),
                new Track("The Schism", _artists[1], new List<Artist> { _artists[1] },
                    new List<string> { "Electronic", "Glitch Hop", "Future Bass", "Chillout" }, 320),
                new Track("Dichotomy (Soft Mix)", _artists[1], new List<Artist> { _artists[1] },
                    new List<string> { "Electronic", "Glitch Hop", "Future Bass", "Chillout" }, 276)
            }),
            new Album("Two Fold Pt. 2", new List<Piece>
            {
                new Song("I Am Me", _artists[1], new List<Artist> { _artists[1] },
                    new List<string> { "Electronic", "Future Bass" }, 129),
                new Song("I Am You", _artists[1], new List<Artist> { _artists[1] },
                    new List<string> { "Electronic", "Future Bass", "House" }, 231)
            })
        };
        _artists[1].ReleasedAlbums = albumsHaywyre;
        _artists[1].ReleasedTracks = albumsHaywyre.SelectMany(a => a.TrackList).ToList();

        var albumsSwardy = new List<PieceCollection>
        {
            new Single("Compact Objects", new List<Piece>
            {
                new Song("Compact Objects", _artists[2], new List<Artist> { _artists[2] },
                    new List<string> { "Dance", "Electronic" }, 282)
            })
        };
        _artists[2].ReleasedAlbums = albumsSwardy;
        _artists[2].ReleasedTracks = albumsSwardy.SelectMany(a => a.TrackList).ToList();

        var albumsCulprate = new List<PieceCollection>
        {
            new Album("Deliverance", new List<Piece>
            {
                new Song("Whispers", _artists[3], new List<Artist> { _artists[3] },
                    new List<string> { "Electronic", "Dance" }, 356),
                new Track("Acid Rain", _artists[3], new List<Artist> { _artists[3] },
                    new List<string> { "Electronic", "Dubstep", "Intelligent Dance Music" }, 215),
                new Track("Relucent", _artists[3], new List<Artist> { _artists[3], _artists[4] },
                    new List<string> { "Electronic", "Intelligent Dance Music" }, 440)
            }),
            new Album("Dawn", new List<Piece>
            {
                new Track("Dawn", _artists[3], new List<Artist> { _artists[3] },
                    new List<string> { "Electronic", "Dubstep" }, 249),
                new Song("Nightmares Vip", _artists[3], new List<Artist> { _artists[3], _artists[5] },
                    new List<string> { "Electronic", "Dubstep" }, 187)
            })
        };
        _artists[3].ReleasedAlbums = albumsCulprate;
        _artists[3].ReleasedTracks = albumsCulprate.SelectMany(a => a.TrackList).ToList();

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