using System;
using System.Runtime.InteropServices;

namespace v4l2_csharp_api_bindings;
public class V4L2ImageInterop : IDisposable
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
    [DllImport(LibraryName, EntryPoint = "Image_create")]
    private static extern IntPtr create();

    [DllImport(LibraryName, EntryPoint = "Image_destroy")]
    private static extern void destroy(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_pixelValue")]
    private static extern ushort pixelValue(IntPtr imagePtr, ushort x, ushort y);

    [DllImport(LibraryName, EntryPoint = "Image_init")]
    private static extern void init(IntPtr imagePtr, ushort width, ushort height, ushort bytesPerLine, uint imageSize,
                                         uint bytesUsed, uint pixelformat, uint sequence, ulong timestamp);

    [DllImport(LibraryName, EntryPoint = "Image_width")]
    private static extern ushort width(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_height")]
    private static extern ushort height(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_bytesPerLine")]
    private static extern ushort bytesPerLine(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_imageSize")]
    private static extern uint imageSize(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_setImageSize")]
    private static extern void setImageSize(IntPtr imagePtr, uint size);

    [DllImport(LibraryName, EntryPoint = "Image_bytesUsed")]
    private static extern uint bytesUsed(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_pixelformat")]
    private static extern uint pixelformat(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_sequence")]
    private static extern uint sequence(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_timestamp")]
    private static extern ulong timestamp(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_shift")]
    private static extern ushort shift(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_setShift")]
    private static extern void setShift(IntPtr imagePtr, ushort shift);

    [DllImport(LibraryName, EntryPoint = "Image_planesCount")]
    private static extern uint planesCount(IntPtr imagePtr);

    [DllImport(LibraryName, EntryPoint = "Image_planeAt")]
    private static extern IntPtr planeAt(IntPtr imagePtr, uint index);
    #endregion NativeMethods

}

public class V4L2VideoDeviceInterop : IDisposable
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

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_create")]
    private static extern IntPtr create();

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_destroy")]
    private static extern void destroy(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_open")]
    private static extern int open(IntPtr videoDevicePtr, string devicePath, string subDevicePath);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_close")]
    private static extern int close(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_getFormat")]
    private static extern int getFormat(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setFormat")]
    private static extern int setFormat(IntPtr videoDevicePtr, string pixelFormat);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_printFormat")]
    private static extern int printFormat(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setSelection")]
    private static extern int setSelection(IntPtr videoDevicePtr, int left, int top, int width, int height);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_streamOn")]
    private static extern int streamOn(IntPtr videoDevicePtr, int bufferCount);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_streamOff")]
    private static extern int streamOff(IntPtr videoDevicePtr);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_getNextImage")]
    private static extern int getNextImage(IntPtr videoDevicePtr, out IntPtr image, int timeout, bool lastImage = false);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_releaseImage")]
    private static extern int releaseImage(IntPtr videoDevicePtr, IntPtr image);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_getImage")]
    private static extern int getImage(IntPtr videoDevicePtr, out IntPtr image, int timeout, bool lastImage = false);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setExposure")]
    private static extern int setExposure(IntPtr videoDevicePtr, int exposure);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setGain")]
    private static extern int setGain(IntPtr videoDevicePtr, int gain);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setBlackLevel")]
    private static extern int setBlackLevel(IntPtr videoDevicePtr, int blackLevel);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setBinning")]
    private static extern int setBinning(IntPtr videoDevicePtr, int binning);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setTriggerMode")]
    private static extern int setTriggerMode(IntPtr videoDevicePtr, int triggerMode);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setIOMode")]
    private static extern int setIOMode(IntPtr videoDevicePtr, int ioMode);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setFrameRate")]
    private static extern int setFrameRate(IntPtr videoDevicePtr, int frameRate);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setControlById")]
    private static extern int setControl(IntPtr videoDevicePtr, uint id, int value);

    [DllImport(LibraryName, EntryPoint = "V4L2VideoDevice_setControlByName")]
    private static extern int setControl(IntPtr videoDevicePtr, string name, int value);
    #endregion NativeMethods

}
