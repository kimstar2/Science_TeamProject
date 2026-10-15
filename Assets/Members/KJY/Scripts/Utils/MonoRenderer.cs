using System;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class MonoRenderer : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer {get; private set;}

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void SetColor(Color color) => SpriteRenderer.color = color;
        public void SetSprite(Sprite sprite) => SpriteRenderer.sprite = sprite;
        public void SetFlipX(bool flipX) => SpriteRenderer.flipX = flipX;
        public void SetFlipY(bool flipY) => SpriteRenderer.flipY = flipY;
    }
}