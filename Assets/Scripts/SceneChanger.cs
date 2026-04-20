using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Make sure the name here matches your scene name in the Build Settings exactly
    public string nextSceneName = "Level2";

    public void MoveToLevel2()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}