#pragma once

#include <image.hpp>

#ifdef __cplusplus
extern "C"
{
#endif

    // Function to create an Image object
    Image *Image_create();

    // Function to destroy an Image object
    void Image_destroy(Image *image_ptr);

    // Function to get pixel value
    unsigned short Image_pixelValue(Image *image_ptr, unsigned short x, unsigned short y);

    // Function to initialize the Image object
    void Image_init(Image *image_ptr, unsigned short width, unsigned short height, unsigned short bytesPerLine, unsigned int imageSize,
                    unsigned int bytesUsed, unsigned int pixelformat, unsigned int sequence, unsigned long timestamp);

    // Function to get the width of the Image
    unsigned short Image_width(Image *image_ptr);

    // Function to get the height of the Image
    unsigned short Image_height(Image *image_ptr);

    // Function to get the bytes per line of the Image
    unsigned short Image_bytesPerLine(Image *image_ptr);

    // Function to get the image size of the Image
    unsigned int Image_imageSize(Image *image_ptr);

    // Function to set the image size of the Image
    void Image_setImageSize(Image *image_ptr, unsigned int size);

    // Function to get the bytes used of the Image
    unsigned int Image_bytesUsed(Image *image_ptr);

    // Function to get the pixel format of the Image
    unsigned int Image_pixelformat(Image *image_ptr);

    // Function to get the sequence of the Image
    unsigned int Image_sequence(Image *image_ptr);

    // Function to get the timestamp of the Image
    unsigned long Image_timestamp(Image *image_ptr);

    // Function to get the shift of the Image
    unsigned short Image_shift(Image *image_ptr);

    // Function to set the shift of the Image
    void Image_setShift(Image *image_ptr, unsigned short shift);

    // Function to get the amount of planes of the Image
    unsigned int Image_planesCount(Image *image_ptr);

    // Function to get a plane by index
    unsigned const char *Image_planeAt(Image *image_ptr, unsigned int index);

#ifdef __cplusplus
}
#endif