#include "RenderAPI.h"
#include "PlatformBase.h"
#include "Unity/IUnityGraphics.h"

#if defined(UNITY_ANDROID)
	#include <android/log.h>
#endif


RenderAPI* CreateRenderAPI(UnityGfxRenderer apiType)
{
#	if SUPPORT_D3D11
	if (apiType == kUnityGfxRendererD3D11)
	{
		extern RenderAPI* CreateRenderAPI_D3D11();
		return CreateRenderAPI_D3D11();
	}
#	endif // if SUPPORT_D3D11

#	if SUPPORT_OPENGL_UNIFIED
	if (apiType == kUnityGfxRendererOpenGLCore || apiType == kUnityGfxRendererOpenGLES20 || apiType == kUnityGfxRendererOpenGLES30)
	{
		extern RenderAPI* CreateRenderAPI_OpenGLCoreES(UnityGfxRenderer apiType);
		return CreateRenderAPI_OpenGLCoreES(apiType);
	}
#	endif // if SUPPORT_OPENGL_UNIFIED

	// Unknown or unsupported graphics API
	return NULL;
}


void debug_log(const char* msg)
{
#if defined(UNITY_WIN)
	LogToFile(msg);
#elif defined (UNITY_ANDROID)
	// #if !defined(NDEBUG)
		__android_log_print(ANDROID_LOG_VERBOSE, "TelloVideoDecoder.cpp", "%s\n", msg);
	// #endif
#else
	#if !defined(NDEBUG)
		std::cout << msg << std::endl;
	#endif
#endif
}

void LogToFile(const std::string& text)
{
	std::ofstream log_file("log_file.txt", std::ios_base::out | std::ios_base::app);
	log_file << text << std::endl;
}