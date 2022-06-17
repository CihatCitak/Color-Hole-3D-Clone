using UnityEngine;

namespace Inputs
{
    [CreateAssetMenu(menuName = "Hole/Inputs/Input Settings")]
    public class InputSettings : ScriptableObject
    {
        public Vector2 InputDrag { get { return inputDrag; } set { inputDrag = value; } }
        [SerializeField] private Vector2 inputDrag;
    }
}
