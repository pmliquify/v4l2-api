#pragma once

#include <v4l2videodevice.hpp>
#include <string>

#ifdef __cplusplus
extern "C" {
#endif

// Function prototypes for V4L2VideoDevice
V4L2VideoDevice *V4L2VideoDevice_create();
void V4L2VideoDevice_destroy(V4L2VideoDevice *video_device_ptr);

int V4L2VideoDevice_open(V4L2VideoDevice *video_device_ptr, const char *devicePath_c, const char *subDevicePath_c);
int V4L2VideoDevice_close(V4L2VideoDevice *video_device_ptr);
int V4L2VideoDevice_getFormat(V4L2VideoDevice *video_device_ptr);
int V4L2VideoDevice_setFormat(V4L2VideoDevice *video_device_ptr, const char *format_c = "GREY");
int V4L2VideoDevice_printFormat(V4L2VideoDevice *video_device_ptr);
int V4L2VideoDevice_setSelection(V4L2VideoDevice *video_device_ptr, int left, int top, int width, int height);

int V4L2VideoDevice_streamOn(V4L2VideoDevice *video_device_ptr, int bufferCount = 3);
int V4L2VideoDevice_streamOff(V4L2VideoDevice *video_device_ptr);
int V4L2VideoDevice_getNextImage(V4L2VideoDevice *video_device_ptr, Image *&image, int timeout, bool lastImage = true);
int V4L2VideoDevice_releaseImage(V4L2VideoDevice *video_device_ptr, Image *image);
int V4L2VideoDevice_getImage(V4L2VideoDevice *video_device_ptr, Image *&image, int timeout, bool lastImage = true);

int V4L2VideoDevice_setExposure(V4L2VideoDevice *video_device_ptr, int exposure);
int V4L2VideoDevice_setGain(V4L2VideoDevice *video_device_ptr, int gain);
int V4L2VideoDevice_setBlackLevel(V4L2VideoDevice *video_device_ptr, int blackLevel);
int V4L2VideoDevice_setBinning(V4L2VideoDevice *video_device_ptr, int binning);
int V4L2VideoDevice_setTriggerMode(V4L2VideoDevice *video_device_ptr, int triggerMode);
int V4L2VideoDevice_setIOMode(V4L2VideoDevice *video_device_ptr, int ioMode);
int V4L2VideoDevice_setFrameRate(V4L2VideoDevice *video_device_ptr, int frameRate);

int V4L2VideoDevice_setControlById(V4L2VideoDevice *video_device_ptr, unsigned int id, int value);
int V4L2VideoDevice_setControlByName(V4L2VideoDevice *video_device_ptr, const char *name, int value);

#ifdef __cplusplus
}
#endif