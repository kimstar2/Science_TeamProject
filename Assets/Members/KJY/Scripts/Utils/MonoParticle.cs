using System;
using Members.KJY.Scripts.Utils.Interface;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class MonoParticle : MonoBehaviour , ISetColorable
    {
        public ParticleSystem ParticleSystem {get; private set;}
        public ParticleSystem.MainModule Main {get; private set;}
        
        private void Awake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            Main = ParticleSystem.main;
        }

        public void SetLoop(bool loop)
        {
            var temp = ParticleSystem.main;
            temp.loop = loop;
            Main = temp;
        }

        public void SetLoopAndPlay(bool loop)
        {
            SetLoop(loop);
            Play();
        }
        
        public void Play() 
            => ParticleSystem.Play();
        public void Stop()
            => ParticleSystem.Stop();

        public void SetColor(Color color)
        {
            var temp = ParticleSystem.main;
            temp.startColor = color;
            Main = temp;
        }
        
        public void SetColor(ColorSO color) => SetColor(color.Color);
    }
}