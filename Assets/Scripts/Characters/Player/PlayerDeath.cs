using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : CharacterAction
{
    [SerializeField] private LevelReloadEvent levelReloadEvent;
    protected override void EndAction()
    {
        LevelLoadCrossfade levelLoad = FindObjectOfType<LevelLoadCrossfade>();
        if (levelLoad != null)
        {
            levelReloadEvent.Raise(this, null);
        }
    }
}
