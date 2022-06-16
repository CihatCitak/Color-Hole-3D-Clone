using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get => instance; }
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

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

    public void UpdateLevelProgress()
    {
        float val = 1f - (float)Level.Instance.objectsInScene / Level.Instance.totalObjects;
        progressFillImage.DOFillAmount(val, 0.4f);
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteText.DOFade(1f, 0.6f).From(0f);
    }

    public void FadeAtStart()
    {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }

}
