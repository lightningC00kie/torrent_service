using Core.Models;

namespace Core.Interfaces;

public interface ITorrentFileService
{
    public byte[] GetFileData(string path);
    TorrentMetadata Parse(string filePath);
    TorrentMetadata Parse(byte[] bencodedData);
}