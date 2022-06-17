using UnityEngine;

public class Ground : MonoBehaviour
{
    #region Singleton
    public static Ground Instance { get => instance; }
    private static Ground instance;

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

    public Transform groundMiddle;
    [Header("X Axis Limit Transforms")]
    public Transform xLeftLimit;
    public Transform xRightLimit;
    [Header("Y Axis Limit Transforms")]
    public Transform zDownLimit;
    public Transform zUpLimit;
}
