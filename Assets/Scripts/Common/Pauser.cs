using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool IsPause => isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnPuase();
    }

    public void ChangePauseState()
    {
        if (IsPause == true)
            UnPuase();
        else
            Pause();
    }
    public void Pause()
    {
        if (isPause == true) return;

        Time.timeScale = 0;
        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }

    public void UnPuase()
    {
        if (isPause == false) return;

        Time.timeScale = 1;
        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }
}
