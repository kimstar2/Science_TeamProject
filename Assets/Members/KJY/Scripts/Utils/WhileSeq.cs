using System;
using System.Collections.Generic;
using System.Threading;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.TimeService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Utils
{

    [Serializable]
    public struct WaitAndPlay
    {
        [field:SerializeField] public float Wait {get;private set;}
        [field: SerializeField] public UnityEvent Play {get;private set;}
    }
    public class WhileSeq : MonoBehaviour
    {
        [SerializeField] private List<WaitAndPlay> waitAndPlayList;
        private CancellationTokenSource _cts = new();

        public void Play()
        {
            UTaskUtil.KillTask(ref _cts);
            While();
        }

        public void Stop()
        {
            UTaskUtil.KillTask(ref _cts);
        }
        
        private void While()
        {
            _cts = new CancellationTokenSource();
            Sequence(_cts.Token).Forget();
        }

        private async UniTask Sequence(CancellationToken token)
        {
            try
            {
                foreach (WaitAndPlay waitAndPlay in waitAndPlayList)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(waitAndPlay.Wait), cancellationToken: token);
                    waitAndPlay.Play?.Invoke();
                }

                While();
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}