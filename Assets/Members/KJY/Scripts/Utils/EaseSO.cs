using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Ease Data", menuName = "KJY/Utils/Ease Data", order = 0)]
    public class EaseSO : ScriptableObject
    { 
        [field:SerializeField] public Ease Ease {get; private set;}
        [field:SerializeField] public AnimationCurve Curve {get; private set;}
    }
}