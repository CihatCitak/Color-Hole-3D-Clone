using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IObjectSwallowed, IGameWin
{
    [Header("Level Progress UI")]
    [SerializeField] int sceneOffset;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] Image progressFillImage;

    [Space]
    [SerializeField] TextMeshProUGUI levelCompleteText;

    [Space]
    [SerializeField] Image fadePanel;


    private void Start()
    {
        FadeAtStart();

        progressFillImage.fillAmount = 0;
        SetLevelProgressText();
    }

    private void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;

        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    private void UpdateLevelProgress()
    {
        float val = 1f - GameHandler.Instance.GetLevelProgress();
        progressFillImage.DOFillAmount(val, 0.4f);
    }

    private void ShowLevelCompleteUI()
    {
        levelCompleteText.DOFade(1f, 0.6f).From(0f);
    }

    private void FadeAtStart()
    {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }

    #region Event
    private void OnEnable()
    {
        EventManager.OnObjectSwallowed += OnObjectSwallowed;
        EventManager.OnGameWin += OnGameWin;
    }

    private void OnDisable()
    {
        EventManager.OnObjectSwallowed -= OnObjectSwallowed;
        EventManager.OnGameWin -= OnGameWin;
    }

    public void OnObjectSwallowed()
    {
        UpdateLevelProgress();
    }

    public void OnGameWin()
    {
        ShowLevelCompleteUI();
    }

    #endregion
}
