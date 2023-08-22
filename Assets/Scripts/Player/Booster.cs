using UnityEngine;

public class Booster : MonoBehaviour
{
    public float BoosterTime { get; set; }
    public float DefaultValue { get; set; } // Value of the player's ability before boosting
    public float CurrentValue { get; set; }

    private float timer = 0.0f;

    public void Setup(float defValue, ref float currentValue, float time) { 
        DefaultValue = defValue;
        CurrentValue = currentValue;
        BoosterTime = time;
    }

    public void Revert() {
        CurrentValue = DefaultValue;
        LevelSystem.Instance.StopBoostWhere(BoosterTime, CurrentValue);
        Destroy(GetComponent<Booster>());
    }

    private void Update(){
        if (timer < BoosterTime)
        {
            if (timer < Time.time)
                timer = Time.time + BoosterTime;
        }
        else
        {
            Revert();
        }
    }
}