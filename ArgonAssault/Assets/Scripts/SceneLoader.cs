using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float transitionDelay = 2f;

    private static SceneLoader instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            this.gameObject.SetActive(false);
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        setTimedTransition();
    }

    private void setTimedTransition()
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 0)
        {
            LoadNextSceneDelayed(transitionDelay);
        }
    }

    public void LoadNextSceneDelayed(float transitionDelay)
    {
        this.transitionDelay = transitionDelay;
        StartCoroutine(DelayedSceneLoad());
    }

    public void ReloadScene(float transitionDelay)
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        this.transitionDelay = transitionDelay;
        StartCoroutine(DelayedSceneLoad(currentLevel));
    }

    private IEnumerator DelayedSceneLoad(int sceneIndexToLoad = 1)
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(1);
    }
}
