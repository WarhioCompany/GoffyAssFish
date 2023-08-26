using UnityEngine;
using UnityEngine.UI;
using LevelSystem;
using TMPro;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public Slider ExperienceSlider;
    [SerializeField] public TMP_Text levelDisplay;

    [Header("Settings")]
    [SerializeField] private float UISpeed = 0.3f;

    public Animator uiAnim;

    // Static constructor
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSlider(ExperienceSlider, 0, PlayerStats.Instance.MinExpValue, PlayerStats.Instance.MaxExpValue);
    }

    #region experienceSlider

    // ~ Utilites for easy workflow
    public void UpdateSlider(Slider _slider, float target, float _speed) { 
     
        _slider.value = Mathf.Lerp(_slider.value, target, _speed);
    }

    public void SetSlider(Slider _slider, float value, float _min, float _max) {
        _slider.maxValue = _max;
        _slider.minValue = _min;
        _slider.value = value;
    }

    // ~ 

    public void OnLevelUp()
    {
        SetSlider(ExperienceSlider, 0, PlayerStats.Instance.MinExpValue, PlayerStats.Instance.MaxExpValue);
        levelDisplay.text = (GetComponentInParent<PlayerStats>().Level + 1).ToString();
    }

    public void UpdateUI() {

        // Update experience slider
        if(ExperienceSlider.value < PlayerStats.Instance.ExperiencePoints)
            UpdateSlider(ExperienceSlider, PlayerStats.Instance.ExperiencePoints, UISpeed);

    }

    #endregion

    public void UIFadeIn()
    {
        uiAnim.SetBool("active", true);
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Destroy(GameObject.FindWithTag("AudioManager"));
    }

    public void Update(){
        UpdateUI(); 
    }
}