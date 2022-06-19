using UnityEngine;

[CreateAssetMenu(menuName ="Hole/Tag Datas", fileName ="Tag Datas")]
public class TagDatas : ScriptableObject
{
    [SerializeField] string objectTag = "Object";
    [SerializeField] string obstacleTag = "Obstacle";

    public string ObjectTag { get => objectTag; }
    public string ObstacleTag { get => obstacleTag; }
}
