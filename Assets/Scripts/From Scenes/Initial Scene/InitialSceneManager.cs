using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class InitialSceneManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup panel;
    [SerializeField] private string targetScene = "MainMenu";
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float timeToLoadTheOtherScene = 3.0f;

    private void Start()
    {
        LoadScene();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SkipCoroutine();
        }
    }

    private void SkipCoroutine()
    {
        StopAllCoroutines();
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
        SceneManager.LoadScene(targetScene);
    }

    private void LoadScene()
    {
        StartCoroutine(UnloadSceneCoroutine());
    }
    IEnumerator UnloadSceneCoroutine()
    {
        if (panel.alpha > 0)
        {
            float t = timeToLoadTheOtherScene;
            while (t > 0)
            {
                panel.alpha = Mathf.Lerp(0, 1, t);
                t -= Time.deltaTime * speed;
                yield return null;
            }
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
            SceneManager.LoadScene(targetScene);
        }
    }

}
