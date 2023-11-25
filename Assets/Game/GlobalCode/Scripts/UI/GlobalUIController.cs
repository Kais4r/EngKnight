using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalUIController : MonoBehaviour
{
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
