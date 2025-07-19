using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Core.Interfaces;
using Core.Models;
using MonoTorrent.BEncoding;

namespace Infrastructure.Services;

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

    private static BEncodedValue? GetValueFromRoot(BEncodedDictionary root, string key)
    {
        return !root.TryGetValue(key, out var val) ? null : val;
    }
    
    private static BEncodedDictionary GetInfoDict(BEncodedDictionary root)
    {
        var infoDict = GetValueFromRoot(root, "info");
        if (infoDict == null)
            throw new ValidationException("Invalid torrent info.");

        return (BEncodedDictionary)infoDict;
    }
    
    

    private static string? GetAnnounce(BEncodedDictionary root)
    {
        var announceAddress = GetValueFromRoot(root, "announce");
        if (announceAddress == null)
            return null;

        return announceAddress.ToString()!;
    }
    
    private static List<string>? GetAnnounceList(BEncodedDictionary root)
    {
        var announceList = GetValueFromRoot(root, "announce_list");
        if (announceList == null)
            return null;
        
        var parsedAnnounceList = new List<string>();
        foreach (var tier in (BEncodedList)announceList)
        {
            var innerList = (BEncodedList) tier;
            parsedAnnounceList.AddRange(innerList.Select(announceAddress => 
                ((BEncodedString)announceAddress).ToString()));
        }
        return parsedAnnounceList;
    }

    private static string GetName(BEncodedDictionary infoDict)
    {
        var name = (BEncodedString) infoDict["name"];
        return name.ToString();
    }

    private static long GetPieceLength(BEncodedDictionary infoDict)
    {
        var pieceLength = (BEncodedNumber) infoDict["piece length"];
        return pieceLength.Number;
    }

    private static byte[] GetPieceHash(BEncodedDictionary infoDict)
    {
        var byteString = GetValueFromRoot(infoDict, "pieces");
        if (byteString == null)
            throw new ValidationException("Invalid piece hash.");
        var piecesBytes = byteString.Encode();
        return piecesBytes;
    }

    private static bool IsPrivate(BEncodedDictionary infoDict)
    {
        var privateBool =  GetValueFromRoot(infoDict, "private");
        if (privateBool == null)
            return false;
        var privateVal = ((BEncodedNumber)privateBool).Number;
        return privateVal == 1;
    }

    private static List<TorrentFile>? GetTorrentFiles(BEncodedDictionary infoDict)
    {
        var torrentFiles = new List<TorrentFile>();
        var torrentFilesBencodedList = GetValueFromRoot(infoDict, "files");
        if (torrentFilesBencodedList == null)
            return null;
        foreach (var bEncodedValue in (BEncodedList)torrentFilesBencodedList)
        {
            var file = (BEncodedDictionary) bEncodedValue;
            var fileLength = (BEncodedNumber) file["length"];
            var filePath = (BEncodedString) file["path"];
            torrentFiles.Add(new TorrentFile
            {
                Length = fileLength.Number,
                Path = filePath.ToString()
            });
        }
        
        return torrentFiles;
    }
    
    private byte[] GetInfoHash(BEncodedDictionary root)
    {
        var infoDict = GetInfoDict(root);
        var encodedInfo = bencoderService.Encode(infoDict);
        return SHA1.HashData(encodedInfo);
    }
    
    public TorrentMetadata Parse(byte[] bencodedData)
    {
        var root = (BEncodedDictionary)bencoderService.Decode(bencodedData);
        var infoDict = GetInfoDict(root);
      
        var metadata = new TorrentMetadata
        {
            Announce = GetAnnounce(root),
            AnnounceList = GetAnnounceList(root),
            Name = GetName(infoDict),
            InfoHash = GetInfoHash(root),
            PieceLength = GetPieceLength(infoDict),
            PiecesHash = GetPieceHash(infoDict),
            IsPrivate = IsPrivate(infoDict),
            Files = GetTorrentFiles(infoDict),
        };
        return metadata;
    }
    
    public TorrentMetadata Parse(string path)
    {
        if (!IsTorrentFile(path)) throw new ValidationException("Invalid torrent file");
        var fileBytes = File.ReadAllBytes(path);
        return Parse(fileBytes);
    }
}