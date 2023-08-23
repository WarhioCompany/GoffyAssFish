using UnityEngine;

namespace LevelSystem
{
    // Class to hold player's evolution data
    public class PlayerStats : MonoBehaviour
    {
        // Player's level
        public int Level;

        // Player's experience
        public float ExperiencePoints;

        // ~ change in the inspector ~

        [Header("Settings")]
        [SerializeField] public float MinExpValue = 0f;
        [SerializeField] public float MaxExpValue = 100f;
        [SerializeField] public float LevelUpExpRatio = 1.25f;

        [SerializeField] public int MinLevel = 1;
        [SerializeField] public int MaxLevel = 10; 

        // ~ 

        // Static Constructor
        public static PlayerStats Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Level = MinLevel;
        }

        public bool IsMaxExp() { 
            return ExperiencePoints >= MaxExpValue;
        }

        public void OnLevelUp() {
            MaxExpValue *= LevelUpExpRatio;
            ExperiencePoints = 0;
            UIManager.Instance.OnLevelUp();
            Level++;
        }
    }
}
