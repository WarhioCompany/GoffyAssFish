using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("Boosts")]
    [SerializeField] private float SpeedBoost;
    [SerializeField] private float WeightEnduranceBoost;

    // Trackers/Counters
    private int levelCounter = 1;
    private bool isSpeedBoosted;
    private bool isWeightEnduranceBoosted;

    // Current Temporary Boosts
    public List<Booster> CurrentTempBoosters = new List<Booster>();

    // Constants (read-only)
    private const int MaxLevel = 10;

    // Static constructor
    public static LevelSystem Instance;

    private void LevelUp() {
        levelCounter++;
    }

    // ~[ Temporary Boosts ]~

    // Boosts a player's abiltiy for specific time(s)
    private void BoostFor(float seconds, ref float abilityToBoost) {
        Booster booster = new Booster();
        booster.Setup(abilityToBoost, ref abilityToBoost, seconds);

        CurrentTempBoosters.Add(booster);   
    }

    public void StopBoostWhere(float seconds, float boostedAbility) {
        Booster booster = CurrentTempBoosters.Where
            (y => y.CurrentValue == boostedAbility && y.BoosterTime == seconds).First();
        CurrentTempBoosters.Remove(booster);
    }

    // ~

    private void OnPlayerOilAbsorb() { 
        LevelUp();
    }

    private void OnPlayerDodgePerfectly() { 
    }
}