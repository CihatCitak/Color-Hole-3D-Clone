using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    [SerializeField] float magnetForce;
    [SerializeField] TagDatas tagDatas;

    private bool IsGameContinue() => GameHandler.Instance.IsGameContinue();
    private bool IsObjectsCollision(Collider other) => (other.CompareTag(tagDatas.ObjectTag) || other.CompareTag(tagDatas.ObstacleTag));

    private void OnTriggerStay(Collider other)
    {
        if (IsGameContinue() && IsObjectsCollision(other))
        {
            var otherRb = other.attachedRigidbody.GetComponent<Rigidbody>();
            otherRb.AddForce((transform.position - other.transform.position) * magnetForce * Time.fixedDeltaTime);
        }
    }
}
