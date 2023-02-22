using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Scene
{
    Title,
    Tutorial,
    Stage1,
    Stage2,
    Ending,
}

public class ScenesManager : MonoBehaviour
{
    #region Singletone

    private static ScenesManager instance = null;

    public static ScenesManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@ScenesManager");
            instance = go.AddComponent<ScenesManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    #region Scene Control
    public Scene currentScene;
    Scene curscene;
    public void ChangeScene(Scene scene)
    {

        UIManager.GetInstance().ClearList();
        ObjectManager.GetInstance().ClearList();

        currentScene = scene;
        SceneManager.LoadScene(scene.ToString());
    }
    public void ChangeSceneint(int aa)
    {

        UIManager.GetInstance().ClearList();
        ObjectManager.GetInstance().ClearList();

        SceneManager.LoadScene(aa);
    }

    #endregion

    public void SceneReLoad()
    {
        ChangeSceneint(SceneManager.GetActiveScene().buildIndex);

    }

}
