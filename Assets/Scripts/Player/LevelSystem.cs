using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("Boosts")]
    [SerializeField] private float SpeedRatio;
    [SerializeField] private float WeightEnduranceRatio;

    // Trackers/Counters
    private int levelCounter = 1;
    private bool isSpeedBoosted;
    private bool isWeightEnduranceBoosted;

    // Constants (read-only)
    private const int MaxLevel = 10;

    // Static constructor
    public static LevelSystem Instance;

    private void LevelUp() {
        levelCounter++;
    }

    private void OnPlayerOilAbsorb() { 
        LevelUp();
    }

    private void OnPlayerDodgePerfectly() { 
    }
}