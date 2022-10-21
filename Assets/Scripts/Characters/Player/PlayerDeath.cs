using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : CharacterAction
{

    protected override void EndAction()
    {
        LevelLoadCrossfade levelLoad = FindObjectOfType<LevelLoadCrossfade>();
        if (levelLoad != null)
        {
            levelLoad.ReloadLevel();
        }
    }
}
