using OpenCvSharp;
using System.Runtime.InteropServices;

namespace v4l2_csharp_api_bindings
{
    /// <summary>
    /// This is an example implementation of a v4l2 camera using the v4l2-api.
    /// It is tested on a VC IMX568.
    /// </summary>
    public sealed class V4L2Camera
    {
        private readonly V4L2VideoDeviceInterop videoDevice = new();
        private CancellationTokenSource cancellationTokenSource = new();

        private bool started = false;

        public readonly BoundedQueue<MemoryMat> Images = new(3);
        private Task? task;

        public int Start(bool printFps = false)
        {
            if (started)
                return -1;
            started = true;
            cancellationTokenSource = new();

            var ret = videoDevice.Open("/dev/video0", "/dev/v4l-subdev1");
            // Console.WriteLine($"V4L2VideoDeviceInterop.open returned {ret}");

            // Setting horizontal_flip to 1
            // ret = videoDevice.SetControl(0x00980914, 1);
            // Console.WriteLine($"V4L2VideoDeviceInterop.setControl returned {ret}");

            // ret = V4L2VideoDeviceInterop.getFormat();
            // Console.WriteLine($"V4L2VideoDeviceInterop.getFormat returned {ret}");

            ret = videoDevice.SetFormat();
            // ret = videoDevice.SetFormat("Y10 ");
            // Console.WriteLine($"V4L2VideoDeviceInterop.setFormat returned {ret}");


            // Try max by passing 0
            // ret = V4L2VideoDeviceInterop.setFrameRate(0);

            // ret = V4L2VideoDeviceInterop.setBlackLevel(60);


            ret = videoDevice.SetTriggerMode(0);
            // 0 = DISABLED lanes: 2, format: GREY, type: STREAM
            // 1 = EXTERNAL lanes: 2, format: GREY, type: EXT.TRG -> Not working with this loop
            // 2 = PULSEWIDTH lanes: 2, format: GREY, type: EXT.TRG -> Not working with this loop
            // 3 = SELF lanes: 2, format: GREY, type: EXT.TRG -> Working but not in sync
            // 4 = SINGLE -> Not working with this loop
            // 5 = Trigger mode 5 not supported! lanes: 2, format: GREY, type: STREAM
            // 6 = Trigger mode 6 not supported! lanes: 2, format: GREY, type: STREAM
            // Console.WriteLine($"V4L2VideoDeviceInterop.setTriggerMode returned {ret}");

            // Setting flash_mode to 1 for streaming mode with leds
            // This needs to be 0 when using trigger mode.
            ret = videoDevice.SetIOMode(1);

            ret = videoDevice.PrintFormat();
            // Console.WriteLine($"V4L2VideoDeviceInterop.printFormat returned {ret}");

            ret = videoDevice.StreamOn();
            // Console.WriteLine($"V4L2VideoDeviceInterop.streamOn returned {ret}");

            task = Task.Run(() =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    // the timeout is 1000000 microseconds = 1 second
                    int ret = videoDevice.GetNextImage(out IntPtr imagePtr, 1000000);
                    V4L2ImageInterop image = new(imagePtr);
                    // int ret = V4L2VideoDeviceInterop.getNextOpenCvImage(out imagePtr, 1000000);
                    if (ret < 0)
                    {
                        Console.WriteLine($"V4L2Camera could not get imagePtr. {ret}");
                        break;
                    }
                    if (cancellationTokenSource.IsCancellationRequested) break;
                    if (imagePtr != IntPtr.Zero)
                    {
                        IntPtr combinedPlanePtr = Marshal.AllocHGlobal((int)image.SizeInBytes);
                        try
                        {
                            if (image.PlanesCount == 1)
                            {
                                IntPtr planePtr = image.PlaneAt();
                                if (planePtr == IntPtr.Zero)
                                    throw new InvalidOperationException("Plane pointer is null");

                                if (image.BytesPerLine <= 0)
                                    throw new ArgumentException("BytesPerLine must be greater than 0");

                                unsafe
                                {
                                    var srcSpan = new Span<byte>(planePtr.ToPointer(), (int)image.SizeInBytes);
                                    var dstSpan = new Span<byte>(combinedPlanePtr.ToPointer(), (int)image.SizeInBytes);
                                    srcSpan.CopyTo(dstSpan);
                                }

                            }
                            else
                            {
                                throw new ArgumentException("Cannot convert more than 1 plane");
                            }
                            // ########################################################################
                            Mat mat8 = new(image.Height, image.Width, MatType.CV_8UC1, combinedPlanePtr);
                            // ########################################################################
                            // Convert Y10 to GREY.
                            // ########################################################################
                            // Mat mat = new(height, width, MatType.CV_16UC1, combinedPlanePtr);
                            // Mat mat8 = new(height, width, MatType.CV_8UC1, new Scalar(200, 0, 0));
                            // mat.ConvertTo(mat8, MatType.CV_8UC1, 255.0 / 1023 / (1 << shift));
                            // ########################################################################
                            Images.Enqueue(new(mat8, combinedPlanePtr));
                            ret = videoDevice.ReleaseImage(image);
                            if (ret < 0)
                            {
                                Console.WriteLine($"V4L2Camera could not release imagePtr");
                            }
                        }
                        catch (Exception e)
                        {
                            ret = videoDevice.ReleaseImage(image);
                            if (ret < 0)
                            {
                                Console.WriteLine($"V4L2Camera could not release imagePtr");
                            }
                            Marshal.FreeHGlobal(combinedPlanePtr);
                            Console.WriteLine($"V4L2Camera loop failed badly: {e}");
                        }
                    }
                }
                // Maybe move to stop???
                // This results in error but otherwise the task blocks infinitely in some situations
                ret = videoDevice.StreamOff();
                // Console.WriteLine($"V4L2VideoDeviceInterop.streamOff returned {ret}");
            }, cancellationTokenSource.Token);
            return 0;
        }

        public int Stop()
        {
            try
            {
                if (!started)
                    return -1;
                started = false;
                cancellationTokenSource.Cancel();
                task?.GetAwaiter().GetResult();
                Console.WriteLine("Stopped V4L2Camera.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Stopping V4L2Camera failed:" + e);
            }
            finally
            {
                started = false;
            }
            return 0;
        }

        public int SetRoi(int x, int y, int width, int height)
        {
            return videoDevice.SetSelection(x, y, width, height);
        }

        public int SetFps(int fps)
        {
            return videoDevice.SetFrameRate(fps);
        }

        public int SetShutter(int shutter)
        {
            return videoDevice.SetExposure(shutter);
        }

        public int SetGain(int gain)
        {
            return videoDevice.SetGain(gain);
        }

        public int SetBlackLevel(int level)
        {
            return videoDevice.SetBlackLevel(level);
        }

        public void Dispose()
        {
            try
            {
                Stop();
                videoDevice.Dispose();
                Console.WriteLine("Disposed V4L2Camera.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Disposing V4L2Camera failed: " + e);
            }
        }
    }

}
