set ANDROID_NDK_ROOT=E:\Android_SDK\ndk\21.0.6113669
%USERPROFILE%\AppData\Local\Android\sdk\ndk-bundle


call %ANDROID_NDK_ROOT%\ndk-build NDK_PROJECT_PATH=. NDK_APPLICATION_MK=Application.mk clean

call %ANDROID_NDK_ROOT%\ndk-build NDK_PROJECT_PATH=. NDK_APPLICATION_MK=Application.mk %1
