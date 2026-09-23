using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject Option_canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainUiToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();

    }
    public void Option()
    {
        Option_canvas.SetActive(true);
    }
    public void CanvasToBack()
    {
        Option_canvas.SetActive(false);
    }
    public void GameToMainUi()
    {
        SceneManager.LoadScene(0);
    }
}
