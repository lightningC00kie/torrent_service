using Core.Models;

namespace Core.Services.Interfaces;

public interface ITorrentFileService
{
    public byte[] GetFileData(string path);
    //public byte[] GetInfoHash(string path);
    
    //TorrentMetadata Parse(Stream stream);
    TorrentMetadata Parse(string filePath);
    TorrentMetadata Parse(byte[] bencodedData);
}