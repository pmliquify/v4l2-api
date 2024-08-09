using System;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace v4l2_csharp_api_bindings;

/// <summary>
/// This is a wrapper for an OpenCv Mat, where the memeory lives in a pointer which needs to be freed manually.
/// </summary>
public sealed class MemoryMat : IDisposable
{
    public Mat Mat { private set; get; }
    public IntPtr Ptr { private set; get; }

    public bool IsDisposed { get; private set; } = false;

    private readonly object _lock = new();

    private readonly Action? dispose;

    public MemoryMat(Mat mat, IntPtr? ptr = null, Action? dispose = null)
    {
        Mat = mat;
        Ptr = ptr ?? IntPtr.Zero;
        this.dispose = dispose;
    }

    public void Dispose()
    {
        lock (_lock)
        {
            if (IsDisposed) return;
            IsDisposed = true;
            Mat.Dispose();
            if (dispose != null)
            {
                dispose();
                return;
            }
            if (Ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Ptr);
            }
        }
    }
}
