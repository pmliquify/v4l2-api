#include <interop/image.hpp>
#include <string>

using namespace std;

// Function to create an Image object
Image *Image_create()
{
    return new Image();
}

// Function to destroy an Image object
void Image_destroy(Image *image_ptr)
{
    delete image_ptr;
}

// Function to get pixel value
unsigned short Image_pixelValue(Image *image_ptr, unsigned short x, unsigned short y)
{
    return image_ptr->pixelValue(x, y);
}

// Function to initialize the Image object
void Image_init(Image *image_ptr, unsigned short width, unsigned short height, unsigned short bytesPerLine, unsigned int imageSize,
                unsigned int bytesUsed, unsigned int pixelformat, unsigned int sequence, unsigned long timestamp)
{
    image_ptr->init(width, height, bytesPerLine, imageSize, bytesUsed, pixelformat, sequence, timestamp);
}

// Function to get the width of the Image
unsigned short Image_width(Image *image_ptr)
{
    return image_ptr->width();
}

// Function to get the height of the Image
unsigned short Image_height(Image *image_ptr)
{
    return image_ptr->height();
}

// Function to get the bytes per line of the Image
unsigned short Image_bytesPerLine(Image *image_ptr)
{
    return image_ptr->bytesPerLine();
}

// Function to get the image size of the Image
unsigned int Image_imageSize(Image *image_ptr)
{
    return image_ptr->imageSize();
}

// Function to set the image size of the Image
void Image_setImageSize(Image *image_ptr, unsigned int size)
{
    image_ptr->setImageSize(size);
}

// Function to get the bytes used of the Image
unsigned int Image_bytesUsed(Image *image_ptr)
{
    return image_ptr->bytesUsed();
}

// Function to get the pixel format of the Image
unsigned int Image_pixelformat(Image *image_ptr)
{
    return image_ptr->pixelformat();
}

// Function to get the sequence of the Image
unsigned int Image_sequence(Image *image_ptr)
{
    return image_ptr->sequence();
}

// Function to get the timestamp of the Image
unsigned long Image_timestamp(Image *image_ptr)
{
    return image_ptr->timestamp();
}

// Function to get the shift of the Image
unsigned short Image_shift(Image *image_ptr)
{
    return image_ptr->shift();
}

// Function to set the shift of the Image
void Image_setShift(Image *image_ptr, unsigned short shift)
{
    image_ptr->setShift(shift);
}

unsigned int Image_planesCount(Image *image_ptr)
{
    return static_cast<unsigned int>(image_ptr->planes().size());
}

unsigned const char *Image_planeAt(Image *image_ptr, unsigned int index)
{
    if (index < image_ptr->planes().size())
    {
        return image_ptr->plane(index);
    }
    return nullptr;
}

unsigned int Image_planeSizeAt(Image *image_ptr, unsigned int index)
{
    if (index < image_ptr->planes().size())
    {
        return sizeof(image_ptr->plane(index));
    }
    return 0;
}