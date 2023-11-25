using UnityEngine;
[CreateAssetMenu (fileName = "Create Actor", menuName ="Create Actor")]

public class SOActorModel : ScriptableObject
{
    //This is a model for every unit in the game.
    public string actorName;
    public string description;
    public int health;
    public int maxHealth;
    public int power;
    public GameObject actor; //the object that gonna represents this.
    public Sprite actorSprite;
}
