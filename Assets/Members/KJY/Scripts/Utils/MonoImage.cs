using UnityEngine;
using UnityEngine.UI;

namespace Members.KJY.Scripts.Utils
{
    public class MonoImage : MonoBehaviour
    {
        [field:SerializeField] public Image Image {get; private set;}
        
        public void SetImage(Sprite sprite) => Image.sprite = sprite;
        public void SetColor(Color color) => Image.color = color;
        public void SetEnable(bool enable) => Image.enabled = enable;
    }
}