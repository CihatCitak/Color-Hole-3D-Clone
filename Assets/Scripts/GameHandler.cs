using UnityEngine;

public class GameHandler : MonoBehaviour, IGameWin, IGameLose
{
    #region Singleton
    public static GameHandler Instance { get => instance; }

    private static GameHandler instance;

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

    public GameStates GameState { get => gameState; }
    private GameStates gameState;

    private void Start()
    {
        gameState = GameStates.START;
    }

    public bool IsGameContinue()
    {
        if (GameState == GameStates.START)
            return true;
        else
            return false;
    }

    #region Events
    private void OnEnable()
    {
        EventManager.OnGameWin += OnGameWin;
        EventManager.OnGameLose += OnGameLose;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= OnGameWin;
        EventManager.OnGameLose -= OnGameLose;
    }

    public void OnGameWin()
    {
        gameState = GameStates.WIN;
    }

    public void OnGameLose()
    {
        gameState = GameStates.LOSE;
    }
    #endregion
}

public enum GameStates
{
    START,
    WIN,
    LOSE
}
