package com.mobile.unity;
import android.app.Activity;
import android.content.Context;
import android.os.Environment;
import android.util.Log;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;

public class MyPlugin extends Activity
{
    static public String FILE_NAME = "config.txt";
    static public String SAVE_FILE_NAME = "save.txt";

    static public String DEFAULT_SAVE_FILE_STRING = "0_0_0_0-t_f_f_f_f_t_f_t_f-t_f_f_f_f_t_f_t_f";

    public static final MyPlugin ourInstance = new MyPlugin();
    public static final String TAG = "Tello";

    public static int coins = 0;
    public static int points = 0;

    public static MyPlugin GetInstance() { return ourInstance; }

    public static OutputStreamWriter logReader;
    public static OutputStreamWriter saveFileReader;

    private MyPlugin()
    {
        Log.i(TAG,"Opened Plugin");
    }

    public void GetCurrency(int p, int c, String from)
    {
        coins = c;
        points = p;
        Log.i(TAG, "Mandado desde: " + from);
        ChargeHighScore(points,coins);
    }

    public void ShowMessage(String msg)
    {
        Log.i(TAG, msg);
    }

    public void ChargeHighScore(int p, int c)
    {
        ShowMessage("Obtube " + p + " puntos");
        ShowMessage("Obtube " + c + " monedas");
        //CreateDirectory();
    }

    public void addToFile(String data, Context context)
    {
        createFile(context);
        writeToFile(data);
        ShowMessage("Lo que hay en el Log es: "+readFromFile(context));
        closeFile();
    }

    public void createFile(Context context)
    {
        try
        {
            logReader = new OutputStreamWriter(context.openFileOutput(FILE_NAME, Context.MODE_APPEND));
            ShowMessage("Se creó el Log");
        }
        catch (IOException e)
        {
            ShowMessage("No se creó el Log");
        }
    }

    public void writeToFile(String data)
    {
        try
        {
            logReader.write(data+"\n");
        }
        catch (IOException e)
        {
            ShowMessage("No se pudo escribir en el Log :c");
        }
    }

    public void closeFile()
    {
        try
        {
            logReader.close();
        }
        catch (IOException e)
        {
            ShowMessage("No se pudo ... cerrar el Log?");
        }
    }

    public void cleanFile(Context context)
    {
        context.deleteFile(FILE_NAME);
        createFile(context);
    }

    public String readFromFile(Context context)
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
            ShowMessage("El Log no existe");
        }
        catch (IOException e)
        {
            ShowMessage("Perdon Rey, pero no puedo leer el Log");
        }

        return ret;
    }
//------------------------------------------------------------------
//------------------------------------------------------------------
//------------------------------------------------------------------
//------------------------------------------------------------------
    public void saveCurrency(String data, Context context)
    {
        cleanSaveFile(context);
        writeToSaveFile(data);
        //ShowMessage("\nLo que hay en el SaveFile es: \n\n"+readFromSaveFile(context)+"\n");
        closeSaveFile();
    }

    public void createSaveFile(Context context)
    {
        try
        {
            saveFileReader = new OutputStreamWriter(context.openFileOutput(SAVE_FILE_NAME, Context.MODE_PRIVATE));
        }
        catch (IOException e)
        {
            ShowMessage("Crear: No se creó el SaveFile");
        }
    }

    public void writeToSaveFile(String data)
    {
        try
        {
            saveFileReader.write(data);
            closeSaveFile();
        }
        catch (IOException e)
        {
            ShowMessage("No se pudo escribir en el SaveFile :c");
        }
    }

    public void closeSaveFile()
    {
        try
        {
            saveFileReader.close();
        }
        catch (IOException e)
        {
            ShowMessage("Close: No se pudo cerrar el SaveFile?");
        }
    }

    public String readFromSaveFile(Context context)
    {

        String ret = "";

        try
        {
            InputStream inputStream = context.openFileInput(SAVE_FILE_NAME);

            if ( inputStream != null )
            {
                InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
                String receiveString = "";
                StringBuilder stringBuilder = new StringBuilder();

                while ( (receiveString = bufferedReader.readLine()) != null )
                {
                    stringBuilder.append(receiveString);
                }

                inputStream.close();
                ret = stringBuilder.toString();
            }
        }
        catch (FileNotFoundException e)
        {
            ShowMessage("Leer: El SaveFile no existe. Por defecto: 0_0_0_0-t_f_f_f_f_t_f_t_f-t_f_f_f_f_t_f_t_f");
            ret = DEFAULT_SAVE_FILE_STRING;
        }
        catch (IOException e)
        {
            ShowMessage("Leer: No se pudo leer el SaveFile. Por defecto: 0_0_0_0-t_f_f_f_f_t_f_t_f-t_f_f_f_f_t_f_t_f");
            ret = DEFAULT_SAVE_FILE_STRING;
        }

        return ret;
    }

    public void cleanSaveFile(Context context)
    {
        context.deleteFile(SAVE_FILE_NAME);
        createSaveFile(context);
    }

}
