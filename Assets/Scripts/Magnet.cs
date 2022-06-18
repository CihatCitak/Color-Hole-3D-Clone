using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton
    public static Magnet Instance { get => instance; }
    private static Magnet instance;

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

    [SerializeField] float magnetForce;
    [SerializeField] string objectTag = "Object";
    [SerializeField] string obstacleTag = "Obstacle";

    private bool IsGameContinue() => GameHandler.Instance.IsGameContinue();
    private bool IsObjectsCollision(Collider other) => (other.CompareTag(objectTag) || other.CompareTag(obstacleTag));

    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    private Transform magnet;

    void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
    }

    private void FixedUpdate()
    {
        if (IsGameContinue())
        {
            foreach (Rigidbody rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsGameContinue() && IsObjectsCollision(other))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGameContinue() && IsObjectsCollision(other))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }

    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }

    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }
}
