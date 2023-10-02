using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IActorTemplate
{
    int health;
    int maxHealth;
    int power;
    GameObject actor;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    
    public int MaxHealth
    { 
        get { return maxHealth; } 
        set {  maxHealth = value; }
    }

    public void ActorStats(SOActorModel actorModel)
    {
        health = actorModel.health;
        maxHealth = actorModel.maxHealth;
        power = actorModel.power;
        actor = actorModel.actor;
        //Debug.Log("Player" + health.ToString() + " " + maxHealth.ToString() + " " + power.ToString());
    }


    public void Die()
    {
        Destroy(this.gameObject);
       
    }

    public int SendDamage()
    {
        return power;
    }

    public void TakeDamage(int incomingDamage)
    {
        if(health > 0)
        {
            health -= incomingDamage;
            if (health <= 0)
            {
                Die();
            }
        }
    }


}
