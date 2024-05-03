using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth, maxThirst, maxHunger;
    public float thirstDecreaseRate, hungerDecreaseRate;

    [Header("Player Stats")]
    public float health;
    public float thirst;
    public float hunger;

    void Start()
    {
        health = maxHealth;
        thirst = maxThirst;
        hunger = maxHunger;
    }

    void Update()
    {
        if(thirst > 0)
        {
            thirst -= thirstDecreaseRate * Time.deltaTime;
        }
        else if(thirst < 0)
        {
            thirst = 0;
        }

        if(hunger > 0)
        {
            hunger -= hungerDecreaseRate * Time.deltaTime;
        }
        else if(hunger < 0)
        {
            hunger = 0;
        }
    }
}
