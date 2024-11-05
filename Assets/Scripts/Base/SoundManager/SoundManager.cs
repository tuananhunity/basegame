using Anh.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Base
{
    public class SoundManager : Singleton<SoundManager>
    {
        private const string NAME_MUSIC = "GO_Music";
        private const string NAME_SFX = "GO_Sfx";
        private const string NAME_ONESHOT = "GO_OneShot";
        private AudioSource _sourceFx, _sourceMusic, _sourceFxOneShot;
        public GameObject gOMusic, gOFx, gOOneShot;
        public AudioSource SourceMusic
        {
            get
            {
                if (_sourceMusic == null)
                {
                    if (gOMusic != null && gOMusic.TryGetComponent<AudioSource>(out _sourceMusic) == false)
                        _sourceMusic = gOMusic.AddComponent<AudioSource>();
                    else if (gOMusic == null)
                    {
                        GameObject go = new ();
                        go.transform.SetParent(transform);
                        go.name = NAME_MUSIC;
                        gOMusic = go;
                        gOMusic.transform.position = Vector2.zero;
                        _sourceMusic = gOMusic.AddComponent<AudioSource>();
                    }
                }
                return _sourceMusic;
            }
        }
        public AudioSource SourceFx
        {
            get
            {
                if (_sourceFx == null)
                {
                    if (gOFx != null && gOFx.TryGetComponent<AudioSource>(out _sourceFx) == false)
                        _sourceFx = gOFx.AddComponent<AudioSource>();
                    else if(gOFx == null)
                    {
                        GameObject go = new();
                        go.transform.SetParent(transform);
                        go.name = NAME_SFX;
                        gOFx = go;
                        gOFx.transform.position = Vector2.zero;
                        _sourceFx = gOFx.AddComponent<AudioSource>();
                    }
                }
                return _sourceFx;
            }
        }
        public AudioSource SourceOneShot
        {
            get
            {
                if (_sourceFxOneShot == null)
                {
                    if (gOOneShot != null && gOOneShot.TryGetComponent<AudioSource>(out _sourceFxOneShot) == false)
                        _sourceFxOneShot = gOOneShot.AddComponent<AudioSource>();
                    else if (gOFx == null)
                    {
                        GameObject go = new();
                        go.transform.SetParent(transform);
                        go.name = NAME_ONESHOT;
                        gOOneShot = go;
                        gOOneShot.transform.position = Vector2.zero;
                        _sourceFxOneShot = gOOneShot.AddComponent<AudioSource>();
                    }
                }
                return _sourceFxOneShot;
            }
        }
        public void PlayFx(AudioClip clip, Action on_done = null, bool is_loop = false, float volume = 1)
        {
            if (SourceFx == null || clip == null)
            {
                on_done?.Invoke();
                return;
            }
            SourceFx.Stop();
            SourceFx.volume = volume;
            SourceFx.clip = clip;
            SourceFx.loop = is_loop;
            SourceFx.Play();
            if (on_done != null)
                StartCoroutine(IEYield(clip, on_done));
        }
        public void PlayMusic(AudioClip clip, Action on_done = null, float volume = 1, bool is_loop = true)
        {
            if (SourceMusic == null || clip == null)
            {
                on_done?.Invoke();
                return;
            }
            if (SourceMusic.clip != null && SourceMusic.clip.name.Equals(clip.name) && SourceMusic.isPlaying)
            {
                on_done?.Invoke();
                return;
            }
            SourceMusic.Stop();
            SourceMusic.clip = clip;
            SourceMusic.volume = volume;
            SourceMusic.loop = is_loop;
            SourceMusic.Play();
            if (on_done != null)
                StartCoroutine(IEYield(clip, on_done));
        }
        public void PlayFxOneShot(AudioClip clip, Action on_done = null, Action on_start = null, float volume = 1)
        {
            if (SourceOneShot == null || clip == null)
            {
                on_done?.Invoke();
                return;
            }
            on_start?.Invoke();
            SourceOneShot.volume = volume;
            SourceOneShot.PlayOneShot(clip);
            if (on_done != null)
                StartCoroutine(IEYield(clip, on_done));
        }
        private IEnumerator IEYield(AudioClip clip, Action on_done)
        {
            if (clip == null)
            {
                on_done?.Invoke();
                yield break;
            }
            float duration = clip.length;
            yield return new WaitForSeconds(duration);
            on_done?.Invoke();
        }
        public bool IsMusicPlaying()
        {
            if (SourceMusic == null) return false;
            return SourceMusic.isPlaying;
        }

        public bool IsFxPlaying()
        {
            if (SourceFx == null) return false;
            return SourceFx.isPlaying;
        }

        public bool IsFxOneShotPlaying()
        {
            if (SourceOneShot == null) return false;
            return SourceOneShot.isPlaying;
        }
        public void StopMusic()
        {
            if (SourceMusic == null)
            {
                return;
            }
            SourceMusic.Stop();
        }

        public void StopFx()
        {
            if (SourceFx == null) return;
            SourceFx.Stop();
        }
        public void StopOneShot()
        {
            if (SourceOneShot != null)
            {
                SourceOneShot.Stop();
            }
        }

        public void StopAll()
        {
            StopMusic();
            StopFx();
            StopOneShot();
        }
    }
}
