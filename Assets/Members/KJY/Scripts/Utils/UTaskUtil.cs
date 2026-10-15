using System.Threading;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public static class UTaskUtil
    {
        public static void KillTask(ref CancellationTokenSource _cts)
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}