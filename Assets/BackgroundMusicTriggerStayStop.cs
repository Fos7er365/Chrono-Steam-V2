using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicTriggerStayStop : MonoBehaviour
{
    [SerializeField] int buildIndex;
    [SerializeField] string audioToPlay;
    LevelManager lvlMgr;
    AudioManager audioMgr;
    GameObject boss;

    private void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>();
        audioMgr = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if(GameObject.FindWithTag("Final_Boss") != null) boss = GameObject.FindWithTag("Final_Boss");
    }

    private void OnTriggerStay(Collider other)
    {
        if(boss != null)
        {
            if (boss.GetComponent<Combat>().EnemyModel.EnemyHealthController.CurrentLife <= 0)
            {
                CheckDisable(other);
            }
        }
            //CheckEnabling(other);
    }

    void CheckDisable(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(WaitToStopMusic());
            audioMgr.Stop(audioToPlay);
        }
    }

    IEnumerator CheckGameWin()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Win Screen");
    }

    IEnumerator WaitToStopMusic()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Win Screen");
    }

}
