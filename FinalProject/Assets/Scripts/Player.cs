using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 1f;
        maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
