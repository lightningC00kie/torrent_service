namespace Core.Services.Interfaces;

public interface ITorrentFileService
{
    public byte[] GetFileData(string path);
    public byte[] GetInfoHash(string path);
}