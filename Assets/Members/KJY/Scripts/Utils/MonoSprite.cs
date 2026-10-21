using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class MonoSprite : MonoBehaviour
    {
        [field:SerializeField] public SpriteRenderer SpriteRenderer {get; private set;}
        [SerializeField] private bool changeWithIndex;
        [ShowIf("changeWithIndex")]
        [SerializeField] private Sprite[] sprites;
        
        public void SetSprite(Sprite sprite) =>  SpriteRenderer.sprite = sprite;
        public void SetColor(Color color) =>  SpriteRenderer.color = color;
        public void SetFlip(bool flip) =>  SpriteRenderer.flipX = flip;
        public void SetSpriteWithIndex(int index)
        {
            if (!changeWithIndex) return;
            SpriteRenderer.sprite = sprites[index];
        }
        public void SetEnable(bool enable) =>  SpriteRenderer.enabled = enable;
    }
}