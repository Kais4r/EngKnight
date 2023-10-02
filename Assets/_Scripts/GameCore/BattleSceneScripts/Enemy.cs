using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IActorTemplate
{
    int health;
    int maxHealth;
    int power;
    GameObject actor;
    float damageMultiplier = 2f;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public void ActorStats(SOActorModel actorModel)
    {
        health = actorModel.health;
        maxHealth = actorModel.maxHealth;
        power = actorModel.power;
        actor = actorModel.actor;
        //Debug.Log("Enemy" + health.ToString() + " " + maxHealth.ToString() + " " + power.ToString());
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public int SendDamage()
    {
        return power;
    }

    public int PowerAttack()
    {
        return Convert.ToInt32(power*damageMultiplier);
    }

    public void TakeDamage(int incomingDamage)
    {
        if (health > 0)
        {
            health -= incomingDamage;
            if(health <= 0)
            {
                Die();
            }
        }
    }

}
