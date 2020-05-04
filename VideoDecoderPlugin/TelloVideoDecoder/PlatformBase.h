#pragma once

// Standard base includes, defines that indicate our current platform, etc.

#include <stddef.h>


// Which platform we are on?
// UNITY_WIN - Windows (regular win32)
// UNITY_OSX - Mac OS X
// UNITY_LINUX - Linux
// UNITY_IOS - iOS
// UNITY_TVOS - tvOS
// UNITY_ANDROID - Android
// UNITY_METRO - WSA or UWP
// UNITY_WEBGL - WebGL


#if _MSC_VER
	#define UNITY_WIN 1
#elif defined(__ANDROID__)
	#define UNITY_ANDROID 1
#else
	#error "Unknown platform!"
#endif



// Which graphics device APIs we possibly support?
#if UNITY_WIN
	//#define SUPPORT_D3D11 1 // comment this out if you don't have D3D11 header/library files

	#define SUPPORT_OPENGL_UNIFIED 1
	#define SUPPORT_OPENGL_CORE 1
#endif


// COM-like Release macro
#ifndef SAFE_RELEASE
#define SAFE_RELEASE(a) if (a) { a->Release(); a = NULL; }
#endif













//#pragma once
//
//// Standard base includes, defines that indicate our current platform, etc.
//
//#include <stddef.h>
//
//// Which platform we are on?
//// UNITY_WIN - Windows (regular win32)
//// UNITY_OSX - Mac OS X
//// UNITY_LINUX - Linux
//// UNITY_IPHONE - iOS
//// UNITY_ANDROID - Android
//// UNITY_METRO - WSA or UWP
//// UNITY_WEBGL - WebGL
//
//#if __ANDROID__ 
//	#define UNITY_ANDROID 1
//#endif
//
//#if _MSC_VER
//	#define UNITY_WIN 1
//#elif defined(__APPLE__)
//	#if defined(__arm__) || defined(__arm64__)
//		#define UNITY_IPHONE 1
//	#else
//		#define UNITY_OSX 1
//	#endif
//#elif defined(UNITY_METRO) || defined(UNITY_ANDROID) || defined(UNITY_LINUX) || defined(UNITY_WEBGL)
//	// these are defined externally
//#elif defined(__EMSCRIPTEN__)
//	// this is already defined in Unity 5.6
//	#define UNITY_WEBGL 1
//#else
//	#error "Unknown platform!"
//#endif
//
//
//
//// Which graphics device APIs we possibly support?
//#if UNITY_METRO
//	#define SUPPORT_D3D11 1
//	#if WINDOWS_UWP
//		#define SUPPORT_D3D12 1
//	#endif
//#elif UNITY_WIN
//	//#define SUPPORT_D3D11 1 // comment this out if you don't have D3D11 header/library files
//
//	#define SUPPORT_OPENGL_CORE 1
//	#define SUPPORT_OPENGL_UNIFIED 1
//	//#define SUPPORT_OPENGL_ES 1 //*** Android Rendering API
//#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WEBGL
//	#define SUPPORT_OPENGL_UNIFIED 1
//	#define SUPPORT_OPENGL_ES 1
//#elif UNITY_OSX || UNITY_LINUX
//	#define SUPPORT_OPENGL_UNIFIED 1
//	#define SUPPORT_OPENGL_CORE 1
//#endif
//
//// COM-like Release macro
//#ifndef SAFE_RELEASE
//	#define SAFE_RELEASE(a) if (a) { a->Release(); a = NULL; }
//#endif
//
