using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HSH
{
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        private HashSet<AudioSource> _busyAdioSources = new HashSet<AudioSource>();
        private HashSet<AudioSource> _freeAdioSources = new HashSet<AudioSource>();


        public static AudioSource Play(AudioClip clip, float vol = 1f, float pitch = 1f)
        {
            foreach (var b in Instance._busyAdioSources.ToArray())
            {
                if (!b.isPlaying)
                {
                    Instance._busyAdioSources.Remove(b);
                    Instance._freeAdioSources.Add(b);
                }
            }

            var source = Instance._freeAdioSources.FirstOrDefault();
            if (source == null)
            {
                source = new GameObject($"SoundSrc_{Instance._busyAdioSources.Count + Instance._freeAdioSources.Count}", typeof(AudioSource)).GetComponent<AudioSource>();
                Instance._busyAdioSources.Add(source);
            }

            source.volume = vol;
            source.pitch = pitch;
            source.clip = clip;
            source.Play();

            return source;
        }
    }
}
