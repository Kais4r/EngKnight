using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorTemplate 
{
    //Basic functions for all the unit in the game.
    int SendDamage();
    void TakeDamage(int incomingDamage);
    void Die();
    void ActorStats(SOActorModel actorModel);
}
