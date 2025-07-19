namespace Core.Models;

public class TorrentFile
{
    public required string Path { get; init; }
    public long Length { get; init; }
}
