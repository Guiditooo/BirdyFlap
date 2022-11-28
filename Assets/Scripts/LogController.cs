using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogController : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;

    private void Start()
    {
        logText.text = Logger.ReadLogFile();
    }

}
