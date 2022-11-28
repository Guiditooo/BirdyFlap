using UnityEngine;

public class Logger
{
    
    private const string PACK_NAME = "com.mobile.myplugin";

    private const string LOGGER_CLASS_NAME = "LoggerClass";

    private static AndroidJavaClass LoggerClass = null;
    private static AndroidJavaObject LoggerInstance = null;

    private static AndroidJavaClass unityPlayer = null;
    private static AndroidJavaObject activity = null;
    private static AndroidJavaObject context = null;

    static void init()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.LogWarning("\nAntes de crear el objeto de la clase.\n");

            if (LoggerClass == null)
            {
                LoggerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
            }

            Debug.LogWarning("\nAntes de crear contexto.\n");

            if (context == null)
            {
                initContext();
            }

            Debug.LogWarning("\nAntes de obtener la instancia de la clase Logger.\n");

            if (LoggerInstance == null)
            {
                LoggerInstance = LoggerClass.CallStatic<AndroidJavaObject>("GetInstance", context);
            }

            Debug.LogWarning("\nDespues de obtener la instancia de la clase Logger.\n");

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

        if (LoggerInstance == null || LoggerClass == null || context == null)
        {
            init();
        }

        LoggerInstance.Call("ShowMessage", msg);
        Debug.Log(msg);
    }

    public static void WriteInFile(string content)
    {

        if (LoggerInstance == null)
        {
            init();
        }

        if(context == null)
        {
            initContext();
        }

        LoggerInstance?.Call("AddToFile", content);

        SendLog("Creado un nuevo Archivo o se actualizo uno viejo.\n");

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

        LoggerInstance?.Call("ClearFile");

        SendLog("Archivo Reseteado.\n");

    }

    public static string ReadLogFile()
    {
        string logText = "";

        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        logText = LoggerInstance.Call<string>("ReadFromFile");

        SendLog("Leyendo el archivo.");

        return logText;
    }

}