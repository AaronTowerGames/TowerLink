using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : GSC<SceneController>
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventBus.LoadScene.Subscribe(LoadScene);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventBus.LoadScene.Unsubscribe(LoadScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EventBus.OnLoadedScene.Invoke(scene.buildIndex);
    }

    private void LoadScene(int sceneId)
    {
        StartCoroutine(AsyncLoad(sceneId));
    }

    private IEnumerator AsyncLoad(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId); //, LoadSceneMode.Additive

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
