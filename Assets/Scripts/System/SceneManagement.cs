using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit()
    {
        Application.Quit();
    }
    
    public void setting()
    {
        SceneManager.LoadScene("Setting");
    }
    
    public void back()
    {
        SceneManager.LoadScene("Menu");
    }

}
