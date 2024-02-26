using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.MyScripts.UtilClasses;
using DG.Tweening;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts.AudioRelated
{
    public class AudioManager : Singleton<AudioManager>,IHasResourcesToLoad
    {
        public AudioSource SfxAudioSource;
        public AudioSource BgmAudioSource;

        public List<AudioClip> SfxClips = new();


        private void Awake()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MessageBroker.Default.Receive<PlaySfxEvent>().Subscribe(_ =>
            {
                PlaySfx(_.AudioName);

            }).AddTo(this);
            MessageBroker.Default.Receive<UnitSpawnData>().Subscribe(_ =>
            {
                PlaySfx("AC_Cute_01v1");
                // DOVirtual.DelayedCall(0.6f, () => PlaySfx("LetsGo_Female1"));
            }).AddTo(this);
        }
        

        public void PlaySfx(string nameOfAudio)
        {
            var found = SfxClips.FirstOrDefault(x => x.name == nameOfAudio);
            if (found == null) return;
            SfxAudioSource.PlayOneShot(found);
        }

        [Button]
        public void LoadResources()
        {
            SfxClips = new();
            var audioClips = Resources.LoadAll<AudioClip>("Audios/");
            SfxClips.AddRange(audioClips);
        }
        
    }
}