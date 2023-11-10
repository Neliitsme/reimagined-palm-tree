// See https://aka.ms/new-console-template for more information

// Unused because project is test-driven. Check Tests.cs
// Used OOP principles: Encapsulation, Inheritance, Polymorphism, Abstraction
// Used Design Patterns: Builder

using OOP_labs.Entities;
using OOP_labs.Helpers;

Console.WriteLine("Hello, World!");

var artists = DataLoader.GetArtists();
var builder = new SearchEngine().ResetQuery(artists);

Console.WriteLine(
    $"Loaded data: Artists - {artists.Count}, Albums - {artists.Sum(a => a.ReleasedAlbums.Count)}, Songs - {artists.Sum(a => a.ReleasedTracks.Count)}");
Console.WriteLine("To set filters, type commands in the following format: \"filter: input\"");
Console.WriteLine("Available filters: artist, album, genre, length, song name");
Console.WriteLine("To get filtered results, type one of these.");
Console.WriteLine("Available processes: get artists, get songs, get albums");
Console.WriteLine("Type \"reset\" to reset the filters.");
Console.WriteLine("C-v to exit.");

while (true)
{
    Console.WriteLine("Awaiting input.");
    var userInput = Console.ReadLine()?.ToLower().Split(':', 2).Select(s => s.Trim()).ToList();
    if (userInput is null || userInput.Any(string.IsNullOrEmpty))
    {
        Console.WriteLine("Try again.");
        continue;
    }

    switch (userInput[0])
    {
        case "reset":
        {
            builder.ResetQuery(artists);
            Console.WriteLine("Reset successfully.");
            break;
        }
        case "artist":
        {
            builder.AddArtistFilter(userInput[1]);
            Console.WriteLine("Set artist filter successfully.");
            break;
        }
        case "album":
        {
            builder.AddAlbumFilter(userInput[1]);
            Console.WriteLine("Set album filter successfully.");
            break;
        }
        case "genre":
        {
            builder.AddGenreFilter(userInput[1]);
            Console.WriteLine("Set genre filter successfully.");
            break;
        }
        case "length":
        {
            if (!int.TryParse(userInput[1], out var length))
            {
                Console.WriteLine("Could not parse length.");
                break;
            }

            builder.AddLengthFilter(length);
            Console.WriteLine("Set length filter successfully.");
            break;
        }
        case "song name":
        {
            builder.AddPieceNameFilter(userInput[1]);
            Console.WriteLine("Set song name filter successfully.");
            break;
        }
        case "get artists":
        {
            var result = builder.GetQuery().GetFilteredArtists();
            Console.WriteLine("Got artists successfully.");
            Console.WriteLine(string.Join(", ", result));
            Console.WriteLine("End.");
            break;
        }
        case "get songs":
        {
            var result = builder.GetQuery().GetFilteredPieces();
            Console.WriteLine("Got songs successfully.");
            result.ForEach(Console.WriteLine);
            Console.WriteLine("End.");
            break;
        }
        case "get albums":
        {
            var result = builder.GetQuery().GetFilteredPieceCollections();
            Console.WriteLine("Got albums successfully.");
            Console.WriteLine(string.Join(", ", result));
            Console.WriteLine("End.");
            break;
        }
        default:
        {
            Console.WriteLine("Incorrect command.");
            break;
        }
    }
}