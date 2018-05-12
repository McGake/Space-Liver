﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager soundManager;
    public AudioSource sfxSource;
    public AudioSource loopedSource;
    //public AudioSource musicSource;
    [Header("Mixer Snapshots")]

    public List<AudioSnapShotHolder> snapshots = new List<AudioSnapShotHolder>();

    public AudioMixerSnapshot CurrentSnapshot { get; private set; }
    public AudioMixerSnapshot PreviousSnapshot { get; private set; }

    [Header("Sounds")]
    public List<SoundEntry> sounds = new List<SoundEntry>();
    [Header("Pitch Range")]
    public static float lowPitchRange = 0.95f;              //The lowest a sound effect will be randomly pitched.
    public static float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    private void Awake() {
        if (soundManager == null)
            soundManager = this;
        else if (soundManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //if(sfxSource == null)
        //    sfxSource = GetComponent<AudioSource>();
    }

    private void Start() {
        SwapMusic("Level", 0f);
    }

    public static bool IsSoundPlaying() {
        return soundManager.sfxSource.isPlaying;
    }

    public static void StopSound() {
        if (soundManager.sfxSource.isPlaying)
            soundManager.sfxSource.Stop();
    }


    public static void SwapMusic(string targetMusic, float time = 1f, bool previous = false) {
        if (previous) {
            if (soundManager.PreviousSnapshot != null) {
                soundManager.PreviousSnapshot.TransitionTo(time);

                AudioMixerSnapshot tempPrev = soundManager.PreviousSnapshot;
                AudioMixerSnapshot tempCurrent = soundManager.CurrentSnapshot;

                soundManager.CurrentSnapshot = tempPrev;
                soundManager.PreviousSnapshot = tempCurrent;
                return;
            }
        }

        AudioMixerSnapshot targetShot = null;

        for (int i = 0; i < soundManager.snapshots.Count; i++) {
            if (soundManager.snapshots[i].audioName == targetMusic) {
                targetShot = soundManager.snapshots[i].snapshot;

                break;
            }
        }

        if (targetShot != null) {
            targetShot.TransitionTo(time);
            soundManager.PreviousSnapshot = soundManager.CurrentSnapshot;
            soundManager.CurrentSnapshot = targetShot;
        }

    }

    public static void RestartMusic(string targetMusic) {
        AudioSnapShotHolder target = GetSnapshotHolder(targetMusic);

        if (target != null) {
            target.source.Stop();
            target.source.Play();
        }

    }

    public static void StopMusic() {
        int count = soundManager.snapshots.Count;

        for (int i = 0; i < count; i++) {
            soundManager.snapshots[i].source.Stop();
        }
    }


    public static void PlaySound(string soundName, float volume = 1f, bool variance = true, float pitchShift = 0f, bool looped = false) {
        AudioClip targetClip = GetClip(soundName);


        if (looped) {
            PlayLoopedSound(targetClip, volume);
            return;
        }

        //for (int i = 0; i < soundManager.sounds.Count; i++) {
        //    if (soundManager.sounds[i].soundName == soundName) {
        //        targetClip = soundManager.sounds[i].clip;
        //        break;
        //    }
        //}
        if (targetClip != null) {
            //soundManager.sfxSource.pitch = 1f;

            if (variance && pitchShift == 0) {
                RandomizeSfx(soundManager.sfxSource).PlayOneShot(targetClip, volume);
                return;
            }

            if (!variance && pitchShift != 0f) {
                AlterPitch(soundManager.sfxSource, pitchShift).PlayOneShot(targetClip, volume);
                return;
            }

            soundManager.sfxSource.PlayOneShot(targetClip, volume);
        }
    }

    public static void PlayLoopedSound(AudioClip targetClip, float volume) {
        soundManager.loopedSource.PlayOneShot(targetClip, volume);
    }

    public static void StopLoopedSound() {
        soundManager.loopedSource.Stop();
    }

    public static bool IsLoopedSoundPlaying() {
        return soundManager.loopedSource.isPlaying;
    }

    private static AudioClip GetClip(string clipName) {
        AudioClip targetClip = null;

        for (int i = 0; i < soundManager.sounds.Count; i++) {
            if (soundManager.sounds[i].soundName == clipName) {
                targetClip = soundManager.sounds[i].clip;
                break;
            }
        }


        return targetClip;
    }

    private static AudioSource RandomizeSfx(AudioSource source) {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        source.pitch = randomPitch;
        return source;
    }

    private static AudioSource AlterPitch(AudioSource source, float pitchMod) {
        source.pitch = 1f;


        source.pitch += pitchMod;
        //float p = source.pitch;

        //p += pitchMod;

        //source.pitch = p;

        return source;
    }

    private static AudioSnapShotHolder GetSnapshotHolder(string holderName) {
        AudioSnapShotHolder result = null;

        for (int i = 0; i < soundManager.snapshots.Count; i++) {
            if (soundManager.snapshots[i].audioName == holderName) {
                result = soundManager.snapshots[i];

                break;
            }
        }


        return result;
    }


    [System.Serializable]
    public class SoundEntry {
        public string soundName;
        public AudioClip clip;
    }

    [System.Serializable]
    public class AudioSnapShotHolder {
        public AudioMixerSnapshot snapshot;
        public AudioSource source;
        public string audioName;
    }

}
