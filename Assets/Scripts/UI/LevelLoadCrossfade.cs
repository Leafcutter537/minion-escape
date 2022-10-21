using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCrossfade : MonoBehaviour
{
    [SerializeField] private List<CrossfadeElement> crossfadeElements;
    [SerializeField] private float waitTime;
    private bool isReloading;
    private float timeToLoad;
    private bool loadNextLevel;

    private void Awake()
    {
        NextFadeIn();
    }

    private void Update()
    {
        if (timeToLoad < 0)
        {
            if (!loadNextLevel)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else if (isReloading)
        {
            timeToLoad -= Time.deltaTime;
        }
    }

    public void NextFadeIn()
    {
        foreach (CrossfadeElement element in crossfadeElements)
        {
            if (!element.isFadingIn)
            {
                element.FadeIn();
                return;
            }
        }
    }

    public void ReloadLevel()
    {
        NextFadeOut();
    }

    public void LoadNextLevel()
    {
        loadNextLevel = true;
        NextFadeOut();
    }

    public void NextFadeOut()
    {
        for (int i = crossfadeElements.Count - 1; i >= 0; i--)
        {
            CrossfadeElement element = crossfadeElements[i];
            if (!element.isFadingOut)
            {
                element.FadeOut();
                return;
            }
        }
        isReloading = true;
        timeToLoad = waitTime;
    }
}
