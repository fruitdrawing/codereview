using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.MyScripts.TestRelated
{
    public class UniTaskWhileTest : MonoBehaviour
    {
        public CancellationTokenSource cts;

        private void Awake()
        {
        }

        [Button]
        public void CANCELTOKEN()
        {
            cts.Cancel();
        }
        [Button]
        public void Inovke_KeepDoing()
        {
            cts = new();
            KeepDoing(cts.Token);
        }
        public async UniTask KeepDoing(CancellationToken ct)
        {
            while (cts.IsCancellationRequested == false)
            {
                await DOOMOMO(cts.Token);
            }
        }

        private async UniTask DOOMOMO(CancellationToken ct)
        {
            Debug.Log(1);
            await UniTask.Delay(1000, cancellationToken: ct);
            Debug.Log(2);
            
        }
    }
}