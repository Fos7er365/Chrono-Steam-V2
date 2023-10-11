using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void SceneHandlerButton(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
