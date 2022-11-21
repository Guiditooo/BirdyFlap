using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallPlugin : MonoBehaviour
{
    [SerializeField] private TMP_Text test; 

    public void CallThePlugin()
    {
        //if (Application.platform == RuntimePlatform.Android)
            //test.text = Logger.DebugReadedFile();
        //Debug.Log("Se han modificado cosas en el archivo de logs");
    }
}
