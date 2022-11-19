using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallPlugin : MonoBehaviour
{
    [SerializeField] private TMP_Text test; 

    public void CallThePlugin()
    {

        if (Application.platform == RuntimePlatform.Android)
            test.text = Logger.DebugReadedFile();
        //Debug.Log("Se han modificado cosas en el archivo de logs");
    }
    public void EraseFile()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Logger.CleanFile();
            test.text = Logger.DebugReadedFile();
        }

        Debug.Log("Se ha limpiado el archivo de logs");
    }
}
