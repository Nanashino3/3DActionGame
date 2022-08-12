using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
