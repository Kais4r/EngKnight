using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalUIController : MonoBehaviour
{
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /*public void LoadLevelScene(string sceneName)
    {

    }*/

    /*public void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameHall")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }*/
}
