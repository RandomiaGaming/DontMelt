using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class Audio_Manager : MonoBehaviour
{
    private int CurrentSong = 0;
    private AudioSource AS;
    public List<AudioClip> Songs;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        List<AudioClip> Playlist = new List<AudioClip>();
        System.Random rnd = new System.Random();
        while (Songs.Count > 0)
        {
            int index = rnd.Next(0, Songs.Count - 1);
            Playlist.Add(Songs[index]);
            Songs.RemoveAt(index);
        }

        Songs = new List<AudioClip>(Playlist);
        AS.clip = Songs[0];
        AS.Play();
    }

    void Update()
    {
        AS.volume = Save_Data_Manager.Current_Settings.GameVolume / 100f;
        if (!AS.isPlaying)
        {
            CurrentSong++;
            if (CurrentSong >= Songs.Count)
            {
                CurrentSong = 0;
            }

            AS.clip = Songs[CurrentSong];
            AS.Play();
        }
    }
}