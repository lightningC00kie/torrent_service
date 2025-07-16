using System.Text;

namespace Core.Models;

public class TorrentMetadata
{
    public required string? Announce { get; set; }
    public List<string>? AnnounceList { get; set; } = [];
    public required string Name { get; set; }
    public long PieceLength { get; set; }
    public required byte[] PiecesHash { get; set; } // Concatenated SHA1 hashes
    public bool IsPrivate { get; set; }

    public List<TorrentFile>? Files { get; set; } = []; // For multi-file torrents
    public required byte[] InfoHash { get; set; } // 20-byte SHA1 of bencoded "info" dict

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Name: {Name}");
        builder.AppendLine($"Announce: {Announce ?? "(none)"}");

        if (AnnounceList is { Count: > 0 })
        {
            builder.AppendLine("Announce List:");
            foreach (var url in AnnounceList)
                builder.AppendLine($"  - {url}");
        }

        builder.AppendLine($"Piece Length: {PieceLength} bytes");
        builder.AppendLine($"Is Private: {IsPrivate}");
        builder.AppendLine($"Info Hash: {BitConverter.ToString(InfoHash).Replace("-", "").ToLower()}");

        int pieceCount = PiecesHash.Length / 20;
        builder.AppendLine($"Number of Pieces: {pieceCount}");

        if (Files is { Count: > 0 })
        {
            builder.AppendLine("Files:");
            foreach (var file in Files)
                builder.AppendLine($"  - {file.Path} ({file.Length} bytes)");
        }
        else
        {
            builder.AppendLine("Single-file torrent");
        }

        return builder.ToString();
    }

}