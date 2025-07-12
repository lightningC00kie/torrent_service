using Core.Services;
using Core.Services.Interfaces;

namespace Unittests;

public class TorrentFileServiceTests
{
    private ITorrentFileService _fileService;
    private IBEncoderService _encoderService;
    
    [SetUp]
    public void Setup()
    {
        _encoderService = new BEncoderService();
        _fileService = new TorrentFileService(_encoderService);
    }

    [Test]
    public void TorrentFileService_ValidTorrentFile_ReturnsNotNull()
    {
        var fileData = _fileService.GetFileData("PATH_TO_TORRENT_FILE");
        Assert.That(fileData, Is.Not.Null);
    }
}