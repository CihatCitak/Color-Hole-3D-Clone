using UnityEngine;

[CreateAssetMenu(menuName = "Hole/Hole Data", fileName = "Hole Data")]
public class HoleData : ScriptableObject
{
    [SerializeField] float moveSpeed;
    [SerializeField] float radius;

    public float MoveSpeed { get => moveSpeed; }
    public float Radius { get => radius; }
}
