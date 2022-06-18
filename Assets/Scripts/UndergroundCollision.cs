using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UndergroundCollision : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] string objectTag;
    [SerializeField] string obstacleTag;

    private void OnTriggerEnter(Collider other)
    {
        if (GameHandler.Instance.GameState == GameStates.START)
        {
            if (other.CompareTag(objectTag))
            {
                EventManager.InvokeOnObjectSwallowed();

                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
                Destroy(other.gameObject);
            }

            if (other.CompareTag(obstacleTag))
            {
                EventManager.InvokeOnGameLose();
            }
        }
    }
}
