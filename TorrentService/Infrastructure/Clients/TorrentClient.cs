using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Clients;

public class TorrentClient : IClient
{
    private ITorrentFileService _torrentFileService;
    
    public TorrentClient(ITorrentFileService torrentFileService)
    {
        _torrentFileService = torrentFileService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddTorrentAsync(TorrentMetadata metadata)
    {
        throw new NotImplementedException();
    }

    public Task<List<TorrentStatus>> GetStatusAsync()
    {
        throw new NotImplementedException();
    }
}