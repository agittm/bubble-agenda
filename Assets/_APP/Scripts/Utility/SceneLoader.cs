using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneField scene;

    public void Load()
    {
        SceneController.LoadScene(scene.SceneName);
    }

    public void LoadAdditive()
    {
        SceneController.LoadScene(scene.SceneName, LoadSceneMode.Additive);
    }

    public AsyncOperation LoadAsync()
    {
        return SceneController.LoadSceneAsync(scene.SceneName);
    }

    public AsyncOperation LoadAsyncAdditive()
    {
        return SceneController.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
    }
}