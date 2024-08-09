#include <interop/v4l2videodevice.hpp>
#include <string>

using namespace std;

// Function to create V4L2VideoDevice instance
V4L2VideoDevice *V4L2VideoDevice_create()
{
    return new V4L2VideoDevice();
}

// Function to destroy V4L2VideoDevice instance
void V4L2VideoDevice_destroy(V4L2VideoDevice *video_device_ptr)
{
    delete video_device_ptr;
}

// Function to open V4L2VideoDevice
int V4L2VideoDevice_open(V4L2VideoDevice *video_device_ptr, const char *devicePath_c, const char *subDevicePath_c)
{
    return video_device_ptr->open(std::string(devicePath_c), std::string(subDevicePath_c));
}

// Function to close V4L2VideoDevice
int V4L2VideoDevice_close(V4L2VideoDevice *video_device_ptr)
{
    return video_device_ptr->close();
}

// Function to get format from V4L2VideoDevice
int V4L2VideoDevice_getFormat(V4L2VideoDevice *video_device_ptr)
{
    return video_device_ptr->getFormat();
}

// Function to set format in V4L2VideoDevice
int V4L2VideoDevice_setFormat(V4L2VideoDevice *video_device_ptr, const char *format_c)
{

    std::string format = std::string(format_c);
    if (format.size() == 4)
    {
        return video_device_ptr->setFormat(*(int *)format.c_str());
    }
    else
    {
        return video_device_ptr->setFormat();
    }
}

// Function to print format of V4L2VideoDevice
int V4L2VideoDevice_printFormat(V4L2VideoDevice *video_device_ptr)
{
    return video_device_ptr->printFormat();
}

// Function to set selection in V4L2VideoDevice
int V4L2VideoDevice_setSelection(V4L2VideoDevice *video_device_ptr, int left, int top, int width, int height)
{
    return video_device_ptr->setSelection(left, top, width, height);
}

// Function to start streaming in V4L2VideoDevice
int V4L2VideoDevice_streamOn(V4L2VideoDevice *video_device_ptr, int bufferCount)
{
    return video_device_ptr->streamOn(bufferCount);
}

// Function to stop streaming in V4L2VideoDevice
int V4L2VideoDevice_streamOff(V4L2VideoDevice *video_device_ptr)
{
    return video_device_ptr->streamOff();
}

// Function to get next image from V4L2VideoDevice
int V4L2VideoDevice_getNextImage(V4L2VideoDevice *video_device_ptr, Image *&image, int timeout, bool lastImage)
{
    return video_device_ptr->getNextImage(image, timeout, lastImage);
}

// Function to release image in V4L2VideoDevice
int V4L2VideoDevice_releaseImage(V4L2VideoDevice *video_device_ptr, Image *image)
{
    return video_device_ptr->releaseImage(image);
}

// Function to get image from V4L2VideoDevice
int V4L2VideoDevice_getImage(V4L2VideoDevice *video_device_ptr, Image *&image, int timeout, bool lastImage)
{
    return video_device_ptr->getImage(image, timeout, lastImage);
}

// Function to set exposure in V4L2VideoDevice
int V4L2VideoDevice_setExposure(V4L2VideoDevice *video_device_ptr, int exposure)
{
    return video_device_ptr->setExposure(exposure);
}

// Function to set gain in V4L2VideoDevice
int V4L2VideoDevice_setGain(V4L2VideoDevice *video_device_ptr, int gain)
{
    return video_device_ptr->setGain(gain);
}

// Function to set black level in V4L2VideoDevice
int V4L2VideoDevice_setBlackLevel(V4L2VideoDevice *video_device_ptr, int blackLevel)
{
    return video_device_ptr->setBlackLevel(blackLevel);
}

// Function to set binning in V4L2VideoDevice
int V4L2VideoDevice_setBinning(V4L2VideoDevice *video_device_ptr, int binning)
{
    return video_device_ptr->setBinning(binning);
}

// Function to set trigger mode in V4L2VideoDevice
int V4L2VideoDevice_setTriggerMode(V4L2VideoDevice *video_device_ptr, int triggerMode)
{
    return video_device_ptr->setTriggerMode(triggerMode);
}

// Function to set IO mode in V4L2VideoDevice
int V4L2VideoDevice_setIOMode(V4L2VideoDevice *video_device_ptr, int ioMode)
{
    return video_device_ptr->setIOMode(ioMode);
}

// Function to set frame rate in V4L2VideoDevice
int V4L2VideoDevice_setFrameRate(V4L2VideoDevice *video_device_ptr, int frameRate)
{
    return video_device_ptr->setFrameRate(frameRate);
}

// Function to set a v4l2 control in V4L2VideoDevice
int V4L2VideoDevice_setControl(V4L2VideoDevice *video_device_ptr, unsigned int id, int value)
{
    return video_device_ptr->setControl(id, value);
}