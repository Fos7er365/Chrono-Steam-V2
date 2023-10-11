using System.Collections;
using UnityEngine;

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
After:
using UnityEngine.Animations;
using UnityEngine.Events;
*/
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PortalToNextLevelController : MonoBehaviour
{
    GameObject MainCamera;
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
    }
    void loadWinLevelEvent()
    {
        UnityEvent @event = GameManager.Instance.LvlManager.GetComponent<LevelManager>().WinRoom;
        GameManager.Instance.EventQueue.Add(@event);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                MainCamera.GetComponent<CameraFollow>().enabled = false;
                other.gameObject.GetComponent<Player_Controller>().Rb.velocity = Vector3.zero;
                GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().Isleaving = true;
                SceneManager.LoadScene(GameManager.Instance.LvlToCharge);
            }
        }
    }

    public IEnumerable LoadNextLevel()
    {
        yield return new WaitForSeconds(.5f);
    }
}
