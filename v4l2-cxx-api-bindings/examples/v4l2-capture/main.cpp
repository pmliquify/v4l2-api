#include <version.h>
#include <v4l2videodevice.hpp>
#include <string>


int main(int argc, const char *argv[])
{
        V4L2VideoDevice device;

        char path[20] = "/dev/video0";
        if (argc > 1) {
                sprintf(path, "/dev/video%s", argv[1]);
        }
        printf("Open capture device %s\n", path);
        device.open(path, path);

        device.streamOn(3);
        Image *image = NULL;    
        int ret = device.getNextImage(image, 1000000);
        if(ret == 0) {
                printf("Image captured (size: %u bytes)\n", image->imageSize());
                device.releaseImage(image);
        } else {
                printf("Error while capturing! (error: %d)\n", ret);
        }
        device.streamOff();

        device.close();
        return 0;
}