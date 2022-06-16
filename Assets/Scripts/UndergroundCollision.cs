using UnityEngine;
using UnityEngine.SceneManagement;

public class UndergroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();

                Destroy(other.gameObject);
            }

            if(tag.Equals("Obstacle"))
            {
                Debug.Log("Obstacle");
            }
        }
    }
}
