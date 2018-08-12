using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator anim;
    string sceneName;

    void Start()
    {
        anim.SetTrigger("out");
    }
    public void SetSceneName(string nameS)
    {
        sceneName = nameS;
        anim.SetTrigger("in");
    }
    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
