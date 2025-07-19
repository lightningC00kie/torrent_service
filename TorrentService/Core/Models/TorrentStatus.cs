namespace Core.Models;

enum StateDescription
{
    Seeding,
    Downloading,
    Uploading,
    Paused
}

public class TorrentStatus
{
    /// <summary>
    /// Name of the torrent (from metadata).
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Total size of the torrent in bytes.
    /// </summary>
    public long TotalSizeBytes { get; set; }

    /// <summary>
    /// Number of bytes downloaded so far.
    /// </summary>
    public long DownloadedBytes { get; set; }

    /// <summary>
    /// Number of bytes uploaded so far.
    /// </summary>
    public long UploadedBytes { get; set; }

    /// <summary>
    /// Download progress in the range [0.0, 1.0].
    /// </summary>
    public double Progress => TotalSizeBytes == 0 ? 0 : (double)DownloadedBytes / TotalSizeBytes;

    /// <summary>
    /// Current download speed in bytes per second.
    /// </summary>
    public long DownloadSpeedBytesPerSec { get; set; }

    /// <summary>
    /// Current upload speed in bytes per second.
    /// </summary>
    public long UploadSpeedBytesPerSec { get; set; }

    /// <summary>
    /// Number of connected peers.
    /// </summary>
    public int ConnectedPeers { get; set; }

    /// <summary>
    /// Total number of known peers (from tracker/DHT).
    /// </summary>
    public int KnownPeers { get; set; }

    /// <summary>
    /// Indicates whether the torrent is currently active (downloading or seeding).
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Any human-readable status message (e.g., "Seeding", "Downloading", "Paused").
    /// </summary>
    public string? StateDescription { get; set; }

    /// <summary>
    /// Time since torrent started.
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Estimated time remaining (nullable if unknown).
    /// </summary>
    public TimeSpan? TimeRemaining { get; set; }
}