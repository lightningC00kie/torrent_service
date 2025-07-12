using Core.Services.Interfaces;
using MonoTorrent.BEncoding;

namespace Core.Services;

public class BEncoderService : IBEncoderService
{
    public BEncodedValue Decode(byte[] data)
    {
        var value = BEncodedValue.Decode(data);
        return value;
    }

    public byte[] Encode(BEncodedValue value)
    {
        var buffer = new byte[value.LengthInBytes()];
        value.Encode(buffer);  // No need to store return value
        return buffer;
    }
    
}