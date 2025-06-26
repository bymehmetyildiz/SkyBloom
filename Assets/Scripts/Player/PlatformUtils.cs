using System.Runtime.InteropServices;
using UnityEngine;

public static class PlatformUtils
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern int IsMobileBrowser();
#endif

    public static bool IsWebGLMobile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobileBrowser() == 1;
#else
        return Application.isMobilePlatform;
#endif
    }
}
