using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();

                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);

                Destroy(other.gameObject);

                if (Level.Instance.objectsInScene == 0)
                {
                    UIManager.Instance.ShowLevelCompleteUI();
                    Level.Instance.PlayWinFx();

                    Invoke("NextLevel", 2f);
                }
            }

            if (tag.Equals("Obstacle"))
            {
                Game.isGameOver = true;
                Camera.main.transform
                    .DOShakePosition(1f, 0.2f, 20, 90f)
                    .OnComplete(() => Level.Instance.RestartLevel());

            }
        }
    }

    private void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
