using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStateController 
{
    private ISceneState m_State;
    public bool m_bRunBegin = false;
    public AsyncOperation loadingScene;

    public SceneStateController() { }

    public void SetState(ISceneState State, string LoadSceneName)
    {
        m_bRunBegin = false;
        LoadScene(LoadSceneName);
        if (m_State != null)
        {
            m_State.StateEnd();
        }
        m_State = State;
    }

    private void LoadScene(string LoadSceneName) {
        if (LoadSceneName == null || LoadSceneName.Length == 0) {
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadingScene = SceneManager.LoadSceneAsync(LoadSceneName);
        loadingScene.allowSceneActivation = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        m_bRunBegin = true;
        m_State.StateBegin();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
