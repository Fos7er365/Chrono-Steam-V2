using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip audio;

    public void HoverSound()
    {
        audioSrc.PlayOneShot(audio);
    }

    public void SceneHandlerButton(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
