using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}
