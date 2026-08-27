// Copyright (c) 2026 Peter Martienssen
// SPDX-License-Identifier: MIT

#include <v4l2image.hpp>
#include <linux/videodev2.h>


V4L2Image::V4L2Image() :
        m_bufferIndex(0)
{
}