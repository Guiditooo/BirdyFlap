using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup creditPanel;
    [SerializeField] private CanvasGroup logPanel;

    [SerializeField] private Image hatSkin;
    [SerializeField] private Image beakSkin;
    [SerializeField] private Image eyesSkin;

    void Awake()
    {
        GetBirdSkin();
    }


    public float showSpeed = 2;
    public void Start()
    {
        if (mainMenuPanel.alpha != 1)
            LoadMainMenuPanel();

    }
    public void LoadMainMenuPanel()
    {
        StopAllCoroutines();
        UnloadPanel(creditPanel);
        UnloadPanel(logPanel);
        LoadPanel(mainMenuPanel);
        Logger.SendLog("Inicia el panel de menu");
    }
    public void LoadCreditsPanel()
    {
        StopAllCoroutines();
        UnloadPanel(mainMenuPanel);
        LoadPanel(creditPanel);
        Logger.SendLog("Inicia el panel de creditos");
    }
    public void LoadLogsPanel()
    {
        StopAllCoroutines();
        UnloadPanel(mainMenuPanel);
        LoadPanel(logPanel);
        Logger.SendLog("Inicia el panel de logs");
    }

    public void LoadStoreScene()
    {
        Logger.SendLog("Deja el menu, y se va a la tienda");
        SceneManager.LoadScene("Store");
    }
    private void UnloadPanel(CanvasGroup panel)
    {
        StartCoroutine(UnloadPanelCoroutine(panel));
    }
    private void LoadPanel(CanvasGroup panel)
    {
        StartCoroutine(LoadPanelCoroutine(panel));
    }
    IEnumerator UnloadPanelCoroutine(CanvasGroup panel)
    {
        if (panel.alpha > 0)
        {
            float t = 1;
            while (t > 0)
            {
                panel.alpha = Mathf.Lerp(0, 1, t);
                t -= Time.deltaTime * showSpeed;
                yield return null;
            }
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
    }
    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime * showSpeed;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
    public void LoadGame(string scene)
    {
        Logger.SendLog("Deja el menu, y se va al juego"); 
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Logger.SendLog("Saliendo del juego.");
        Application.Quit();
    }
    void GetBirdSkin()
    {
        hatSkin.sprite = Manager.GetHatSkins()[CosmeticPrefs.GetEquippedHatSkinID()].GetSprite();
        beakSkin.sprite = Manager.GetBeakSkins()[CosmeticPrefs.GetEquippedBeakSkinID()].GetSprite();
        eyesSkin.sprite = Manager.GetEyesSkins()[CosmeticPrefs.GetEquippedEyesSkinID()].GetSprite();
    }
    public void ShowAchievements()
    {
        Auth.ShowAchievements();
    }

}
