using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCanvasController : MonoBehaviour
{
    public RectTransform CanvasTransform;
    public Slider HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanvasTransform.rotation = Camera.main.transform.rotation;
    }

    public void OnHealthChanged(float health, float maxHealth)
    {
        HealthSlider.value = health / maxHealth;
    }

    public void OnDeath()
    {
        HealthSlider.enabled = false;
    }
}
