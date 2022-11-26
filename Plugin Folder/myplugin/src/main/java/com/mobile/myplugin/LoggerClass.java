package com.mobile.myplugin;
import android.app.Activity;
import android.util.Log;

public class LoggerClass extends Activity
{

    public static final String TAG = "Tello";
    public void ShowMessage(String msg)
    {
        Log.i(TAG, msg);
    }


}
