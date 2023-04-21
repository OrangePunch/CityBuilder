using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private float timer;
    private bool isPause = false;
    private bool guiPause; 

    private void Update()
    {
        Time.timeScale = timer;

        if (Input.GetKeyDown(KeyCode.Escape) && isPause == false)
        {
            isPause = true;
            ActivatePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause == true)
        {
            isPause = false;
            ExitPauseMenu();
        }

        if (isPause == true)
        {
            timer = 0;
            guiPause = true;
        }
        else if (isPause == false)
        {
            timer = 1f;
            guiPause = false;
        }
    }
    public void ActivatePauseMenu()
    {
        _pauseMenu.gameObject.SetActive(true);
    }

    public void ExitPauseMenu()
    {
        _pauseMenu.gameObject.SetActive(false);
    }

    public void ContinueButton()
    {
        isPause = false;
        ExitPauseMenu();
    }
}
