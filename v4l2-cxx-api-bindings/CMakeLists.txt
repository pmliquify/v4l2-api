cmake_minimum_required(VERSION 3.5.0)

project(v4l2-cxx-api 
        VERSION 0.3.0
        DESCRIPTION "V4L2 C++ API bindings"
        HOMEPAGE_URL "https://github.com/pmliquify/v4l2-api"
        LANGUAGES CXX)
configure_file (
    "${PROJECT_SOURCE_DIR}/include/version.h.in"
    "${PROJECT_BINARY_DIR}/include/version.h"
)

set(CMAKE_POSITION_INDEPENDENT_CODE ON)

add_library(v4l2-cxx-api
        STATIC
        src/errno.cpp
        src/image.cpp
        src/v4l2image.cpp
        src/v4l2videodevice.cpp)
target_include_directories(v4l2-cxx-api PUBLIC include)

# C Interop Library
add_library(v4l2-c-api
        SHARED
        src/interop/image.cpp
        src/interop/v4l2videodevice.cpp)
target_include_directories(v4l2-c-api PUBLIC include)
target_link_libraries(v4l2-c-api PRIVATE v4l2-cxx-api)

# add_subdirectory(examples)