using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) =>
        SceneManager.LoadScene(sceneName, mode);

    public static void LoadScene(int sceneIndex, LoadSceneMode mode = LoadSceneMode.Single) =>
        LoadScene(SceneManager.GetSceneByBuildIndex(sceneIndex).name, mode);

    public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive) =>
        SceneManager.LoadSceneAsync(sceneName, mode);

    public static AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode mode = LoadSceneMode.Additive) =>
        LoadSceneAsync(SceneManager.GetSceneByBuildIndex(sceneIndex).name, mode);

    public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters) =>
        SceneManager.LoadSceneAsync(sceneName, parameters);

    public static AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneParameters parameters) =>
        LoadSceneAsync(SceneManager.GetSceneByBuildIndex(sceneIndex).name, parameters);

    public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options = UnloadSceneOptions.None) =>
        SceneManager.UnloadSceneAsync(sceneName, options);

    public static AsyncOperation UnloadSceneAsync(int sceneIndex, UnloadSceneOptions options = UnloadSceneOptions.None) =>
        UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(sceneIndex).name, options);

    public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options = UnloadSceneOptions.None) =>
        UnloadSceneAsync(scene.name, options);

    public static void SetActiveScene(Scene scene) =>
        SceneManager.SetActiveScene(scene);

    public static void SetActiveScene(string sceneName) =>
        SetActiveScene(SceneManager.GetSceneByName(sceneName));

    public static void SetActiveScene(int sceneIndex) =>
        SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));

    public static Scene GetActiveScene() =>
        SceneManager.GetActiveScene();

    public static void ReloadScene() =>
        SceneManager.LoadScene(GetActiveScene().name);

    public static void MergeScene(Scene source, Scene destination) =>
        SceneManager.MergeScenes(source, destination);

    public static void MoveGameObjectToScene(GameObject go, Scene scene) =>
        SceneManager.MoveGameObjectToScene(go, scene);

    public static float GetSceneTime() =>
        Time.timeSinceLevelLoad;
}