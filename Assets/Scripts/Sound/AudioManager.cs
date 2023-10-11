
/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using System.Collections;
After:
using System;
using System.Collections;
*/
using System;
using UnityEngine;
/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using UnityEngine.Audio;
using System;
After:
using UnityEngine.Audio;
*/


public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {

        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            //s.Source.dopplerLevel = s.DopplerLevel;
            s.Source.spatialBlend = s.SpatialBlend;

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            return;
        }
        s.Source.Play();
    }
}
