using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Color", menuName = "KJY/Utils/Color", order = 0)]
    public class ColorSO : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public List<Color> Colors { get; private set; }

        public Color GetRandomColor()
        {
            int r = Random.Range(0, Colors.Count);
            return Colors[r];
        }
    }
}