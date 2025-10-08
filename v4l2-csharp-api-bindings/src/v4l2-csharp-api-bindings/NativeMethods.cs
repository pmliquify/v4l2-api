using System;
using System.Runtime.InteropServices;

namespace v4l2_csharp_api_bindings;
public partial class V4L2ImageInterop : IDisposable
{
    public readonly IntPtr _handle;

    public ushort Width => width(_handle);
    public ushort Height => height(_handle);
    public uint SizeInBytes => imageSize(_handle);
    public ushort BytesPerLine => bytesPerLine(_handle);
    public uint PixelFormat => pixelformat(_handle);

    public uint BytesUsed => bytesUsed(_handle);
    public uint PlanesCount => planesCount(_handle);
    public ushort Shift => shift(_handle);
    public ushort BufferIndex => bufferIndex(_handle);

    public IntPtr PlaneAt(uint index = 0) => planeAt(_handle, index);

    public uint ImageSize => imageSize(_handle);

    public V4L2ImageInterop(IntPtr _handle)
    {
        this._handle = _handle;
    }

    public void Print()
    {
        Console.WriteLine($"#########################################");
        Console.WriteLine($"width: {Width}");
        Console.WriteLine($"height: {Height}");
        Console.WriteLine($"sizeInBytes: {SizeInBytes}");
        Console.WriteLine($"bytesPerLine: {BytesPerLine}");
        Console.WriteLine($"pixelFormat: {PixelFormat}");
        Console.WriteLine($"bytesUsed: {BytesUsed}");
        Console.WriteLine($"planeCount: {PlanesCount}");
        Console.WriteLine($"shift: {Shift}");
    }

    public void Dispose()
    {
        destroy(_handle);
    }

    #region NativeMethods
    private const string LibraryName = "libv4l2-c-api.so";
    [LibraryImport(LibraryName, EntryPoint = "Image_create")]
    private static partial IntPtr create();

    [LibraryImport(LibraryName, EntryPoint = "Image_destroy")]
    private static partial void destroy(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_pixelValue")]
    private static partial ushort pixelValue(IntPtr imagePtr, ushort x, ushort y);

    [LibraryImport(LibraryName, EntryPoint = "Image_init")]
    private static partial void init(IntPtr imagePtr, ushort width, ushort height, ushort bytesPerLine, uint imageSize,
                                         uint bytesUsed, uint pixelformat, uint sequence, ulong timestamp);

    [LibraryImport(LibraryName, EntryPoint = "Image_width")]
    private static partial ushort width(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_height")]
    private static partial ushort height(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_bytesPerLine")]
    private static partial ushort bytesPerLine(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_imageSize")]
    private static partial uint imageSize(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_setImageSize")]
    private static partial void setImageSize(IntPtr imagePtr, uint size);

    [LibraryImport(LibraryName, EntryPoint = "Image_bytesUsed")]
    private static partial uint bytesUsed(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_pixelformat")]
    private static partial uint pixelformat(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_sequence")]
    private static partial uint sequence(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_timestamp")]
    private static partial ulong timestamp(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_shift")]
    private static partial ushort shift(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_setShift")]
    private static partial void setShift(IntPtr imagePtr, ushort shift);

    [LibraryImport(LibraryName, EntryPoint = "Image_planesCount")]
    private static partial uint planesCount(IntPtr imagePtr);

    [LibraryImport(LibraryName, EntryPoint = "Image_planeAt")]
    private static partial IntPtr planeAt(IntPtr imagePtr, uint index);

    [LibraryImport(LibraryName, EntryPoint = "Image_bufferIndex")]
    private static partial ushort bufferIndex(IntPtr imagePtr);
    #endregion NativeMethods

}

public partial class V4L2VideoDeviceInterop : IDisposable
{
    public readonly IntPtr _handle;
    public V4L2VideoDeviceInterop()
    {
        _handle = create();
    }

    public int Open(string devicePath, string subDevicePath) => open(_handle, devicePath, subDevicePath);

    public int Close() => close(_handle);

    public int GetFormat() => getFormat(_handle);

    public int SetFormat(string pixelFormat = "GREY") => setFormat(_handle, pixelFormat);

    public int PrintFormat() => printFormat(_handle);

    public int SetSelection(int left, int top, int width, int height) => setSelection(_handle, left, top, width, height);

    public int StreamOn(int bufferCount = 3) => streamOn(_handle, bufferCount);

    public int StreamOff() => streamOff(_handle);

    // the timeout is 1000000 microseconds = 1 second
    public int GetNextImage(out IntPtr imagePtr, int timeout = 1000000, bool lastImage = false) => getNextImage(_handle, out imagePtr, timeout, lastImage);

    public int ReleaseImage(V4L2ImageInterop image) => releaseImage(_handle, image._handle);

    public int ReleaseImageByIndex(int bufferIndex) => releaseImageByIndex(_handle, bufferIndex);

    public int GetImage(out IntPtr imagePtr, int timeout, bool lastImage = false) => getImage(_handle, out imagePtr, timeout, lastImage);

    public int SetExposure(int exposure) => setExposure(_handle, exposure);

    public int SetGain(int gain) => setGain(_handle, gain);

    public int SetBlackLevel(int blackLevel) => setBlackLevel(_handle, blackLevel);

    public int SetBinning(int binning) => setBinning(_handle, binning);

    public int SetTriggerMode(int triggerMode) => setTriggerMode(_handle, triggerMode);

    public int SetIOMode(int ioMode) => setIOMode(_handle, ioMode);

    public int SetFrameRate(int frameRate) => setFrameRate(_handle, frameRate);

    public int SetControl(uint id, int value) => setControl(_handle, id, value);

    public int SetControl(string name, int value) => setControl(_handle, name, value);

    public void Dispose()
    {
        destroy(_handle);
    }

    #region NativeMethods
    
    private const string LibraryName = "libv4l2-c-api.so";

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_create")]
    private static partial IntPtr create();

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_destroy")]
    private static partial void destroy(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_open")]
    private static extern int open(IntPtr videoDevicePtr, string devicePath, string subDevicePath);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_close")]
    private static partial int close(IntPtr videoDevicePtr);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_getFormat")]
    private static partial int getFormat(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setFormat")]
    private static extern int setFormat(IntPtr videoDevicePtr, string pixelFormat);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_printFormat")]
    private static partial int printFormat(IntPtr videoDevicePtr);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setSelection")]
    private static partial int setSelection(IntPtr videoDevicePtr, int left, int top, int width, int height);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_streamOn")]
    private static partial int streamOn(IntPtr videoDevicePtr, int bufferCount);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_streamOff")]
    private static partial int streamOff(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_getNextImage")]
    private static extern int getNextImage(IntPtr videoDevicePtr, out IntPtr image, int timeout, bool lastImage = false);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_releaseImage")]
    private static partial int releaseImage(IntPtr videoDevicePtr, IntPtr image);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_releaseImageByIndex")]
    private static partial int releaseImageByIndex(IntPtr videoDevicePtr, int bufferIndex);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_getImage")]
    private static extern int getImage(IntPtr videoDevicePtr, out IntPtr image, int timeout, bool lastImage = false);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setExposure")]
    private static partial int setExposure(IntPtr videoDevicePtr, int exposure);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setGain")]
    private static partial int setGain(IntPtr videoDevicePtr, int gain);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setBlackLevel")]
    private static partial int setBlackLevel(IntPtr videoDevicePtr, int blackLevel);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setBinning")]
    private static partial int setBinning(IntPtr videoDevicePtr, int binning);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setTriggerMode")]
    private static partial int setTriggerMode(IntPtr videoDevicePtr, int triggerMode);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setIOMode")]
    private static partial int setIOMode(IntPtr videoDevicePtr, int ioMode);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setFrameRate")]
    private static partial int setFrameRate(IntPtr videoDevicePtr, int frameRate);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setControlById")]
    private static partial int setControl(IntPtr videoDevicePtr, uint id, int value);

    [LibraryImport(LibraryName, EntryPoint = "V4L2VideoDevice_setControlByName", StringMarshalling = StringMarshalling.Utf8)]
    private static partial int setControl(IntPtr videoDevicePtr, string name, int value);
    
    #endregion NativeMethods

}
