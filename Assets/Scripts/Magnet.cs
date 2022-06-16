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

    List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    Transform magnet;

    // Start is called before the first frame update
    void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
    }

    private void FixedUpdate()
    {
        if(!Game.isGameOver && Game.isMoving)
        {
            foreach (Rigidbody rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if(!Game.isGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (!Game.isGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")))
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
