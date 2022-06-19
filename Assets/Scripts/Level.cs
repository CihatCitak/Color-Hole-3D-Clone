using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-10)]
public class Level : MonoBehaviour, IObjectSwallowed, IGameWin, IGameLose
{
    [SerializeField] ParticleSystem winFx;

    [Space]
    [HideInInspector] public int objectsInScene;
    [HideInInspector] public int totalObjects;

    [SerializeField] Transform objectsParent;

    [Space]
    [Header("Materials & Sprites")]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] Image progressFillImage;

    [SerializeField] SpriteRenderer bgFadeSprite;

    [Space]
    [Header("Level Colors-------")]
    [Header("Ground")]
    [SerializeField] Color groundColor;
    [SerializeField] Color bordersColor;
    [SerializeField] Color sideColor;

    [Header("Objects & Obstacles")]
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;

    [Header("UI (progress)")]
    [SerializeField] Color progressFillColor;

    [Header("Background")]
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;


    void Start()
    {
        CountObjects();
        UpdateLevelColors();
    }

    private void CountObjects()
    {
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }

    private void PlayWinFx()
    {
        winFx.Play();
    }

    private IEnumerator LoadNextLevel(float duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator RestartLevel(float duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private float LevelProgressAmount()
    {
        return (float)objectsInScene / (float)totalObjects;
    }

    #region Event
    private void OnEnable()
    {
        EventManager.OnObjectSwallowed += OnObjectSwallowed;
        EventManager.OnGameWin += OnGameWin;
        EventManager.OnGameLose += OnGameLose;
    }

    private void OnDisable()
    {
        EventManager.OnObjectSwallowed -= OnObjectSwallowed;
        EventManager.OnGameWin -= OnGameWin;
        EventManager.OnGameLose -= OnGameLose;
    }

    public void OnObjectSwallowed()
    {
        objectsInScene--;

        GameHandler.Instance.SetLevelProgress(LevelProgressAmount());

        if (objectsInScene == 0)
        {
            EventManager.InvokeOnGameWin();
        }
    }

    public void OnGameWin()
    {
        PlayWinFx();

        StartCoroutine(LoadNextLevel(2f));
    }

    public void OnGameLose()
    {
        StartCoroutine(RestartLevel(2f));
    }
    #endregion

    #region Color Issues
    void UpdateLevelColors()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;

        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;

        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    private void OnValidate()
    {
        UpdateLevelColors();
    }
    #endregion
}
