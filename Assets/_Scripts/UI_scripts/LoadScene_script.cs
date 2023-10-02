using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene_script : MonoBehaviour
{
    //[SerializeField] private Image _img;
    //[SerializeField] private Sprite _default,_pressed;
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
