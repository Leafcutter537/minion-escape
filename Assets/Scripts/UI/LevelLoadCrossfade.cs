using System.Collections.Generic;
using Assets.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCrossfade : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private NextLevelLoadEvent nextLevelLoadEvent;
    [SerializeField] private LevelReloadEvent levelReloadEvent;
    [Header("Crossfade")]
    [SerializeField] private List<CrossfadeElement> crossfadeElements;
    [SerializeField] private float waitTime;
    private bool isReloading;
    private float timeToLoad;
    private bool loadNextLevel;

    private void Awake()
    {
        NextFadeIn();
    }
    private void OnEnable()
    {
        nextLevelLoadEvent.AddListener(LoadNextLevel);
        levelReloadEvent.AddListener(ReloadLevel);
    }
    private void OnDisable()
    {
        nextLevelLoadEvent.RemoveListener(LoadNextLevel);
        levelReloadEvent.RemoveListener(ReloadLevel);
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

    private void ReloadLevel(object sender, EventParameters args)
    {
        NextFadeOut();
    }

    private void LoadNextLevel(object sender, EventParameters args)
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
