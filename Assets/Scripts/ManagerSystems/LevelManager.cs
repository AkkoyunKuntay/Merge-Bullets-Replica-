using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public enum GamePhases { Merge, BlockRace, Shooting }
public class LevelManager : CustomSingleton<LevelManager>
{
    
    public GamePhases gamePhases;

    public event System.Action BlockRaceIsOnEvent;
    public event System.Action ShootingIsOnEvent;

    public void OnPlayButtonClicked()
    {
        GameManager.instance.isLevelActive = true;
        gamePhases = GamePhases.BlockRace;
        BlockRaceIsOnEvent?.Invoke();
    }

    public void SetGamePhase(GamePhases desiredPhase)
    {
        gamePhases = desiredPhase;
        if(desiredPhase == GamePhases.Shooting) ShootingIsOnEvent?.Invoke();

    }
}
