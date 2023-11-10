using OOP_labs.Abstractions;
using OOP_labs.Entities;
using OOP_labs.Entities.Collections;
using Single = OOP_labs.Entities.Collections.Single;

namespace OOP_labs.Helpers;

public static class DataLoader
{
    public static List<Artist> GetArtists()
    {
        List<Artist> artists = new List<Artist>
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
                    new Track("Operator", artists[0], new List<Artist> { artists[0] },
                        new List<string> { "Dance", "Electronic" }, 173)
                }),
            new Album("Paths", new List<Piece>
            {
                new Track("Familiar Frontiers", artists[0], new List<Artist> { artists[0] },
                    new List<string> { "Dance", "Electronic" }, 141),
                new Track("Too Far Gone", artists[0], new List<Artist> { artists[0] },
                    new List<string> { "Dance", "Electronic" }, 172),
                new Track("Which Way", artists[0], new List<Artist> { artists[0] },
                    new List<string> { "Dance", "Electronic" }, 178)
            })
        };
        artists[0].ReleasedAlbums = albumsGjones;
        artists[0].ReleasedTracks = albumsGjones.SelectMany(a => a.TrackList).ToList();

        var albumsHaywyre = new List<PieceCollection>
        {
            new Album("Two Fold Pt. 1", new List<Piece>
            {
                new Track("Prologue (Part One)", artists[1], new List<Artist> { artists[1] },
                    new List<string> { "Jazz", "House", "Chillout" }, 130),
                new Track("The Schism", artists[1], new List<Artist> { artists[1] },
                    new List<string> { "Electronic", "Glitch Hop", "Future Bass", "Chillout" }, 320),
                new Track("Dichotomy (Soft Mix)", artists[1], new List<Artist> { artists[1] },
                    new List<string> { "Electronic", "Glitch Hop", "Future Bass", "Chillout" }, 276)
            }),
            new Album("Two Fold Pt. 2", new List<Piece>
            {
                new Song("I Am Me", artists[1], new List<Artist> { artists[1] },
                    new List<string> { "Electronic", "Future Bass" }, 129),
                new Song("I Am You", artists[1], new List<Artist> { artists[1] },
                    new List<string> { "Electronic", "Future Bass", "House" }, 231)
            })
        };
        artists[1].ReleasedAlbums = albumsHaywyre;
        artists[1].ReleasedTracks = albumsHaywyre.SelectMany(a => a.TrackList).ToList();

        var albumsSwardy = new List<PieceCollection>
        {
            new Single("Compact Objects", new List<Piece>
            {
                new Song("Compact Objects", artists[2], new List<Artist> { artists[2] },
                    new List<string> { "Dance", "Electronic" }, 282)
            })
        };
        artists[2].ReleasedAlbums = albumsSwardy;
        artists[2].ReleasedTracks = albumsSwardy.SelectMany(a => a.TrackList).ToList();

        var albumsCulprate = new List<PieceCollection>
        {
            new Album("Deliverance", new List<Piece>
            {
                new Song("Whispers", artists[3], new List<Artist> { artists[3] },
                    new List<string> { "Electronic", "Dance" }, 356),
                new Track("Acid Rain", artists[3], new List<Artist> { artists[3] },
                    new List<string> { "Electronic", "Dubstep", "Intelligent Dance Music" }, 215),
                new Track("Relucent", artists[3], new List<Artist> { artists[3], artists[4] },
                    new List<string> { "Electronic", "Intelligent Dance Music" }, 440)
            }),
            new Album("Dawn", new List<Piece>
            {
                new Track("Dawn", artists[3], new List<Artist> { artists[3] },
                    new List<string> { "Electronic", "Dubstep" }, 249),
                new Song("Nightmares Vip", artists[3], new List<Artist> { artists[3], artists[5] },
                    new List<string> { "Electronic", "Dubstep" }, 187)
            })
        };
        artists[3].ReleasedAlbums = albumsCulprate;
        artists[3].ReleasedTracks = albumsCulprate.SelectMany(a => a.TrackList).ToList();

        return artists;
    }
}