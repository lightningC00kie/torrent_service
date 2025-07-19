using Core.Models;

namespace Core.Interfaces;

public interface IClient
{
    /// <summary>
    /// Starts the torrent client.
    /// </summary>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops all activity and closes the client.
    /// </summary>
    Task StopAsync();

    /// <summary>
    /// Adds a torrent to download by file or magnet.
    /// </summary>
    Task AddTorrentAsync(TorrentMetadata metadata);

    /// <summary>
    /// Returns current status of all active torrents.
    /// </summary>
    Task<List<TorrentStatus>> GetStatusAsync();
}