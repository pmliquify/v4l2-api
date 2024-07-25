cmake_minimum_required(VERSION 3.5.0)

project(v4l2-capture
        VERSION 0.3.0
        DESCRIPTION "V4L2 C++ Capture Example"
        HOMEPAGE_URL "https://github.com/pmliquify/v4l2-api"
        LANGUAGES CXX)
configure_file (
    "${CMAKE_SOURCE_DIR}/include/version.h.in"
    "${PROJECT_BINARY_DIR}/include/version.h"
)

add_executable(v4l2-capture
        main.cpp)
target_include_directories(v4l2-capture PRIVATE ${PROJECT_BINARY_DIR}/include)
target_link_libraries(v4l2-capture PRIVATE v4l2-cxx-api)

add_custom_command(
        TARGET v4l2-capture POST_BUILD 
        COMMAND scp ARGS v4l2-capture root@verdin-imx8mp-06775457:/home/root
        VERBATIM)