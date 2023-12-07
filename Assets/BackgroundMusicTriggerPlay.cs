using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTriggerPlay : MonoBehaviour
{
    AudioManager audioMgr;
    [SerializeField] string audioToPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioMgr = GameObject.Find("AudioManager").GetComponent < AudioManager>() ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !audioMgr.GetAudio(audioToPlay).Source.isPlaying)
            audioMgr.Play(audioToPlay);
    }
}
