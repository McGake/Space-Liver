using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor {

    private SoundManager _soundManager;

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        _soundManager = (SoundManager)target;

        _soundManager.sfxSource = EditorHelper.ObjectField<AudioSource>("SFX Source", _soundManager.sfxSource, true);
        _soundManager.loopedSource = EditorHelper.ObjectField<AudioSource>("Looped Source", _soundManager.loopedSource, true);

        EditorGUILayout.Separator();

        _soundManager.snapshots = EditorHelper.DrawExtendedList("Snapshots", _soundManager.snapshots,"", DrawSnapshotHolder);

        EditorGUILayout.Separator();

        _soundManager.sounds = EditorHelper.DrawExtendedList("Sounds", _soundManager.sounds, "", DrawSoundEntry);


        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }


    private SoundManager.AudioSnapShotHolder DrawSnapshotHolder(SoundManager.AudioSnapShotHolder entry) {
        entry.audioName = EditorGUILayout.TextField("Audio Name", entry.audioName);
        entry.snapshot = EditorHelper.ObjectField<UnityEngine.Audio.AudioMixerSnapshot>("Snapshot", entry.snapshot);
        entry.source = EditorHelper.ObjectField<AudioSource>("Source", entry.source, true);

        return entry;
    }

    private SoundManager.SoundEntry DrawSoundEntry(SoundManager.SoundEntry entry) {
        entry.soundName = EditorGUILayout.TextField("Sound Name", entry.soundName);
        entry.clip = EditorHelper.ObjectField<AudioClip>("Audio Clip", entry.clip);


        return entry;
    }

}
