using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour, IGameLose
{
    private void OnEnable()
    {
        EventManager.OnGameLose += OnGameLose;
    }

    private void OnDisable()
    {
        EventManager.OnGameLose -= OnGameLose;
    }

    public void OnGameLose()
    {
        transform
            .DOShakePosition(1f, 0.2f, 20, 90f)
            .OnComplete(() => Level.Instance.RestartLevel());
    }
}
