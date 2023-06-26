
using Assets.EventSystem;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GamePauseEvent pauseEvent;
    [SerializeField] private GameUnpauseEvent unpauseEvent;
    [SerializeField] private LevelReloadEvent levelReloadEvent;
    [SerializeField] private PauseController pauseController;
    [SerializeField] private GameObject pauseTint;

    private void OnEnable()
    {
        pauseEvent.AddListener(OnGamePause);
        unpauseEvent.AddListener(OnGameUnpause);
    }
    private void OnDisable()
    {
        pauseEvent.RemoveListener(OnGamePause);
        unpauseEvent.RemoveListener(OnGameUnpause);
    }
    private void OnGamePause(object sender, EventParameters args)
    {
        pauseTint.SetActive(true);
    }
    private void OnGameUnpause(object sender, EventParameters args)
    {
        pauseTint.SetActive(false);
    }
    public void ReloadLevel()
    {
        levelReloadEvent.Raise(this, null);
        pauseController.PauseGame();
    }
}
