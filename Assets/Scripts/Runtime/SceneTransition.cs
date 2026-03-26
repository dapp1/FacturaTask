using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    void Awake()
    {
        // Этот объект выживет при загрузке MainScene
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}
