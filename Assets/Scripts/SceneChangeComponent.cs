using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeComponent : MonoBehaviour
{
    [SerializeField]
    private int targetSceneIndex;

    public void GoToScene()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
