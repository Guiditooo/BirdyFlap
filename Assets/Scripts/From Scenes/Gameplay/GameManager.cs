using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    bool paused = false;

    [SerializeField] private TMP_Text txtPointsOnGame;
    [SerializeField] private TMP_Text txtPointsOnUI;
    [SerializeField] private TMP_Text txtPointsTotal;

    [SerializeField] private TMP_Text txtHighPoints;

    [SerializeField] private CanvasGroup endScreen;
    public float UIshowSpeed = 1;
    private bool shownEndScreen = false;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] obstacles;
    private GameObject[] coins;
    [SerializeField] private float distanceBetweenObstacles = 1;
    [SerializeField] private float initialPosX = 3.5f;
    
    private const float MaxHeight = 3.7f;
    private const float MinHeight = -2.6f;
    private const float LimitLeft = -3.5f;
    private const float LimitRight = 3.5f;

    public float maxHeight = MaxHeight;
    public float minHeight = MinHeight;

    private float distanceToReset = 0;
    private bool[] justPassed = null;
    private bool[] justChecked = null;

    private int pointsInGame = 0;
    private int storedPoints = 0;

    private bool playerAlive = false;

    private int maxPointsReached;

    [SerializeField] private float tubeSpeed = 1;
    [SerializeField] private float tubeAugmentCoef = 1.000001f;

    private float speedMultiplier = 1;
    private float timer = 0;

    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject beak;
    [SerializeField] private GameObject eyes;

    private SpriteRenderer hatSkin;
    private SpriteRenderer beakSkin;
    private SpriteRenderer eyesSkin;

    private void Awake()
    {

        playerAlive = player.GetComponent<Player>().alive;
        Player.onPlayerCollision += StopMovement;

        storedPoints = ScorePrefs.GetStoredPoints();

        maxPointsReached = ScorePrefs.GetMaxPointsReached();

        txtHighPoints.text = maxPointsReached.ToString();

        hatSkin = hat.GetComponent<SpriteRenderer>();
        beakSkin = beak.GetComponent<SpriteRenderer>();
        eyesSkin = eyes.GetComponent<SpriteRenderer>();

        Debug.Log(hatSkin);

        GetBirdSkin();

    }

    private void OnDestroy()
    {
        Player.onPlayerCollision -= StopMovement;
    }

    public void Start()
    {
        UnloadEndScreen(endScreen);
        justPassed = new bool[obstacles.Length];
        justChecked = new bool[obstacles.Length];
        SetTubesPosition();

        paused = false;
        playerAlive = true;
        shownEndScreen = false;

        maxPointsReached = ScorePrefs.GetMaxPointsReached();

        pointsInGame = 0;
    }

    void Update()
    {
        if (playerAlive)
        {
            MoveTubes();
            CheckTubes();
            CheckScore();
        }
        else
        {
            if(!shownEndScreen)
            {
                txtPointsOnGame.text = "";
                txtPointsOnUI.text = pointsInGame.ToString();

                storedPoints += pointsInGame;

                Debug.Log("Se ha terminado la partida con " + pointsInGame + " puntos.");
                Debug.Log("Se tienen un total de " + storedPoints + " puntos.");
                
                txtPointsTotal.text = GetTotalCurrency(storedPoints);

                if(HigherThanPrev(pointsInGame,maxPointsReached))
                {
                    txtHighPoints.text = pointsInGame.ToString();
                    maxPointsReached = pointsInGame;
                }

                SaveCurrency();

                Manager.CheckPointAchievement(pointsInGame);
                Manager.CheckAccumultarionAchievement(storedPoints);

                LoadEndScreen(endScreen);
                shownEndScreen = true;
            }
        }
    }
    void StopMovement() => playerAlive = false;
    void SetTubesPosition()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = new Vector3(initialPosX + distanceBetweenObstacles * i, Random.Range(minHeight, maxHeight));
            justPassed[i] = false;
            justChecked[i] = false;
        }
        distanceToReset = obstacles[obstacles.Length - 1].transform.position.x + distanceBetweenObstacles;
    }
    void SetNewObstaclePos(ref GameObject o, int actualPos)
    {
        int lastObstacle = 0;
        switch (actualPos)
        {
            case 0:
                lastObstacle = 2;
                break;
            case 1:
                lastObstacle = 0;
                break;
            case 2:
                lastObstacle = 1;
                break;
            default:
                break;
        }
        o.transform.position = new Vector3(obstacles[lastObstacle].transform.position.x + distanceBetweenObstacles, Random.Range(minHeight, maxHeight));
        
        justPassed[actualPos] = false;
        justChecked[actualPos] = false;
    }
    void CheckTubes()//Se fija cual ha llegado al final y lo resetea a una Y distinta
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (player.transform.position.x > obstacles[i].transform.position.x && !justPassed[i])
            {
                justPassed[i] = true;
            }
            if (obstacles[i].transform.position.x < LimitLeft)
            {
                SetNewObstaclePos(ref obstacles[i], i);

                Debug.Log("Reseteado el obstaculo " + i );
            }
        }
    }
    void CheckScore()
    {
        for (int i = 0; i < justPassed.Length; i++)
        {
            if (justPassed[i])
            {
                if (!justChecked[i])
                {
                    pointsInGame++;
                    txtPointsOnGame.text = pointsInGame.ToString();
                    Debug.Log("Contador de puntos: " + pointsInGame);
                    justChecked[i] = true;
                }
            }
        }
    }
    void MoveTubes()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = new Vector3(obstacles[i].transform.position.x - tubeSpeed * Time.deltaTime * speedMultiplier, obstacles[i].transform.position.y);
        }
        if (timer > 1)
        {
            timer = 0;
            speedMultiplier *= tubeAugmentCoef;
        }
        timer += Time.deltaTime;
    }
    bool HigherThanPrev(int actual, int prev)
    {
        if(actual>prev)
            Debug.Log("El puntaje alcanzado es mayor que el maximo alcanzado antes.");
        else
            Debug.Log("El puntaje anterior es el maximo alcanzado.");

        return actual > prev;
    }
    void LoadEndScreen(CanvasGroup panel)
    {
        StartCoroutine(LoadPanelCoroutine(panel));
    }
    void UnloadEndScreen(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }
    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime * UIshowSpeed;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
    string GetTotalCurrency(int c)
    {
        string txt = "(";
        txt += c.ToString();
        txt += ")";
        return txt;
    }
    public void BackToMenu()
    {
        Debug.Log("Volviendo al MENU. Desde el GAMEPLAY.");
        SceneManager.LoadScene("MainMenu");
    }
    public void EnterStore()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
           // Logger.SendFilePath();
        }

        Debug.Log("Yendo a la STORE, desde el GAMEPLAY");
        SceneManager.LoadScene("Store");
    }
    private void SaveCurrency()
    {
        ScorePrefs.SaveActualScore(storedPoints, maxPointsReached);
    }
    void GetBirdSkin()
    {
        hatSkin.sprite = Manager.GetHatSkins()[CosmeticPrefs.GetActualHatSkinID()].GetSprite();
        beakSkin.sprite = Manager.GetBeakSkins()[CosmeticPrefs.GetActualBeakSkinID()].GetSprite();
        eyesSkin.sprite = Manager.GetEyesSkins()[CosmeticPrefs.GetActualEyesSkinID()].GetSprite();
    }
    

    public void ShowAchievements()
    {
        Auth.ShowAchievements();
    }

}
