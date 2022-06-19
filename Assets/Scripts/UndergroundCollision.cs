using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UndergroundCollision : MonoBehaviour
{
    [Header("Tag Data")]
    [SerializeField] TagDatas tagDatas;

    private void OnTriggerEnter(Collider other)
    {
        if (GameHandler.Instance.GameState == GameStates.START)
        {
            if (other.CompareTag(tagDatas.ObjectTag))
            {
                EventManager.InvokeOnObjectSwallowed();

                Destroy(other.gameObject);
            }

            if (other.CompareTag(tagDatas.ObstacleTag))
            {
                EventManager.InvokeOnGameLose();
            }
        }
    }
}
