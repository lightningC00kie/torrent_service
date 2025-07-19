using MonoTorrent.BEncoding;

namespace Core.Interfaces;
public interface IBEncoderService
{
    public BEncodedValue Decode(byte[] data);
    public byte[] Encode(BEncodedValue value);
}