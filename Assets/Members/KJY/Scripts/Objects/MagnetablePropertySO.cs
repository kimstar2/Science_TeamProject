using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Objects
{
    [CreateAssetMenu(fileName = "Magnetable Prop", menuName = "KJY/Objects/Magnetable Prop", order = 0)]
    public class MagnetablePropertySO : ScriptableObject
    {
        [field: Header("Pull Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public float PullInstability { get; private set; } = 0.1f; // 불안정성 (자기장에 관여 받으면서 공중에 떠있을 때)
        [field: SerializeField] public float PullForce { get; private set; } = 0.05f;
        [field: SerializeField] public float PullOffRange { get; private set; } = 2f;

        [field: Header("Push Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public float MaxPushForce { get; private set; } = 30f;
    }
}