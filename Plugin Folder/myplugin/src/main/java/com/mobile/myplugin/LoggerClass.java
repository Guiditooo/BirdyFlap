package com.mobile.myplugin;
import android.app.Activity;
import android.content.Context;
import android.util.Log;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;


public class LoggerClass
{
    public static final String TAG = "Tello>>>";

    private LoggerClass() { DebugMessage("Created plugin object"); } //CONSTRUCTOR

    public static final LoggerClass ourInstance = new LoggerClass();
    public static Context context = null;

    public static LoggerClass GetInstance(Context newContext)
    {
        context = newContext;
        return ourInstance;
    } //FUNCION

    public static OutputStreamWriter logReader;
    public static OutputStreamWriter saveFileReader;
    static public String FILE_NAME = "Logs.txt";

    public void DebugMessage(String msg)
    {
        Log.i(TAG, msg);
    }

    public void ShowMessage(String msg)
    {
        DebugMessage(msg);
        AddToFile(msg);
    } //FUNCION

    public void AddToFile(String data)
    {
        CreateFile();
        WriteToFile(data);
        //ShowMessage("Lo que hay en el Log es: " + ReadFromFile());
        CloseFile();
    }

    public void CreateFile()
    {
        try
        {
            logReader = new OutputStreamWriter(context.openFileOutput(FILE_NAME, Context.MODE_APPEND));
            //DebugMessage("Se creó el Log");
        }
        catch (IOException e)
        {
            DebugMessage("No se creó el Log");
        }
    }

    public void WriteToFile(String data)
    {
        try
        {
            logReader.write(data+"\n");
        }
        catch (IOException e)
        {
            DebugMessage("No se pudo escribir en el Log :c");
        }
    }

    public String ReadFromFile()
    {

        String ret = "";

        try
        {
            InputStream inputStream = context.openFileInput(FILE_NAME);

            if ( inputStream != null )
            {
                InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
                String receiveString = "";
                StringBuilder stringBuilder = new StringBuilder();

                while ( (receiveString = bufferedReader.readLine()) != null )
                {
                    stringBuilder.append(receiveString).append("\n");
                }

                inputStream.close();
                ret = stringBuilder.toString();
            }
        }
        catch (FileNotFoundException e)
        {
            DebugMessage("El Log no existe");
        }
        catch (IOException e)
        {
            DebugMessage("Perdon Rey, pero no puedo leer el Log");
        }

        return ret;
    }

    public void CloseFile()
    {
        try
        {
            logReader.close();
        }
        catch (IOException e)
        {
            DebugMessage("No se pudo ... cerrar el Log?");
        }
    }

    public void ClearFile()
    {
        context.deleteFile(FILE_NAME);
        CreateFile();
    }

}
