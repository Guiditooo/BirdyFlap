using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    const string PACK_NAME = "com.mobile.unity";

    const string LOGGER_CLASS_NAME = "MyPlugin";

    static AndroidJavaClass LoggerClass = null;

    static AndroidJavaObject LoggerInstance = null;

    static AndroidJavaClass unityPlayer = null;
    static AndroidJavaObject activity = null;
    static AndroidJavaObject context = null;

    static void init()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            LoggerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
            LoggerInstance = LoggerClass.CallStatic<AndroidJavaObject>("GetInstance");
        }
    }
    static void initContext()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = activity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }
    public static void SendLog(string msg)
    {
        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        LoggerInstance.Call("ShowMessage", msg);
    }

    public static void WriteInContextFile(string content)
    {

        if (LoggerInstance == null)
        {
            init();
        }

        if(context == null)
        {
            initContext();
        }

        LoggerInstance?.Call("addToFile", content, context);

        Debug.Log("Creado un nuevo Archivo o se actualizo uno viejo.\n");

    }

    public static void CleanLog()
    {
        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        LoggerInstance?.Call("cleanFile", context);

        Debug.Log("Archivos Reseteados.\n");
    }

}