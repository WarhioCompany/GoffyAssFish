using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Player's level
    public int Level { get; set; }

    // Player's experience
    public float ExperiencePoints { get; set; }

    // Static Constructor
    public static PlayerStats Instance;

    private void Awake()
    {
        Instance = this;
    }
}