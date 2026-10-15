using System;
using Members.KJY.Scripts.Utils.Interface;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class MonoRenderer : MonoBehaviour , ISetColorable
    {
        public SpriteRenderer SpriteRenderer {get; private set;}

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void SetColor(Color color) => SpriteRenderer.color = color;
        public void SetColor(ColorSO color) => SetColor(color.Color);
        
        public void SetSprite(Sprite sprite) => SpriteRenderer.sprite = sprite;
        public void SetFlipX(bool flipX) => SpriteRenderer.flipX = flipX;
        public void SetFlipY(bool flipY) => SpriteRenderer.flipY = flipY;
    }
}