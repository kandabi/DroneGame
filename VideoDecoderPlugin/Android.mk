include $(CLEAR_VARS)

# override strip command to strip all symbols from output library; no need to ship with those..
# cmd-strip = $(TOOLCHAIN_PREFIX)strip $1

#LOCAL_ARM_MODE  := arm
LOCAL_PATH      := $(NDK_PROJECT_PATH)
LOCAL_MODULE    := libTelloVideoDecoder

FFMPEG_LIB_PATH := $(LOCAL_PATH)/TelloVideoDecoder/ffmpeg-android-prebuilt/lib

LOCAL_C_INCLUDES := $(LOCAL_PATH)/TelloVideoDecoder/ffmpeg-android-prebuilt/include
LOCAL_SRC_FILES := TelloVideoDecoder/RenderAPI.cpp TelloVideoDecoder/RenderAPI_OpenGLCoreES.cpp TelloVideoDecoder/RenderingPlugin.cpp TelloVideoDecoder/TelloVideoDecoder.cpp

LOCAL_LDFLAGS 	:= -L$(FFMPEG_LIB_PATH) \
		   -LE:\ffmpeg\bin\lib \
		   -LE:\ffmpeg\libx264\lib\x64

LOCAL_LDLIBS    := -Wl,--no-warn-shared-textrel \
		   -llog -lEGL -lGLESv1_CM -lGLESv2 -lz \
		   -lavformat -lavcodec -lswresample -lswscale -lavutil -lx264

LOCAL_STATIC_LIBRARIES := avcodec avformat avutil swscale x264

APP_ALLOW_MISSING_DEPS=true

ifeq ($(NDK_DEBUG), 0)
	LOCAL_CFLAGS += -DNDEBUG
endif

include $(BUILD_SHARED_LIBRARY)
#$(call import-add-path, $(FFMPEG_LIB_PATH))
