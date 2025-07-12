using System.ComponentModel.DataAnnotations;
using Core.Services.Interfaces;
using MonoTorrent.BEncoding;
using System.Security.Cryptography;

namespace Core.Services;

public class TorrentFileService(IBEncoderService bencoderService) : ITorrentFileService
{
    private static bool IsTorrentFile(string path)
    {
        return path.EndsWith(".torrent");
    }
    
    public byte[] GetFileData(string path)
    {
        if (IsTorrentFile(path))
        {
            return File.ReadAllBytes(path);   
        }

        throw new ValidationException("Invalid torrent file");
    }

    public byte[] GetInfoHash(string path)
    {
        var data = GetFileData(path);
        var root = (BEncodedDictionary)bencoderService.Decode(data);
        if (!root.TryGetValue("info", out var infoVal) || infoVal is not BEncodedDictionary infoDict)
            throw new Exception("Invalid torrent file");
        var encodedInfo = bencoderService.Encode(infoDict);
        using var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(encodedInfo);
        return hash;
    }
}