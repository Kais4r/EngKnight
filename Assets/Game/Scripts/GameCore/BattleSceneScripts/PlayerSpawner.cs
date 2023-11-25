using DG.Tweening;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    SOActorModel actorModel;
    GameObject player;

    GameObject playerBattleStation;

    public CharacterDatabase characterDB;
    private int selectedOption = 0;


    void Start()
    {
        //GetReferences();
        CreateBattleStation();
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
        CreatePlayer();
    }

    void CreateBattleStation()
    {
        GameObject playerBattleStationPreb = Resources.Load("GamePrefabs/PlayerBattleStation") as GameObject;
/*        if(playerBattleStationPreb == null )
        {
            Debug.Log("there is no player battle station");
        }*/
        playerBattleStation = GameObject.Instantiate(playerBattleStationPreb, Vector3.zero, Quaternion.identity);
        playerBattleStation.transform.position = new Vector3(-6.18f, 2.8f, 0);
        playerBattleStation.transform.SetParent(this.transform);
        playerBattleStation.name = playerBattleStationPreb.name;
    }

    void CreatePlayer() 
    {
        //Create player
        //actorModel = Object.Instantiate(Resources.Load("ScriptableObject/Player_Default")) as SOActorModel;
        player = GameObject.Instantiate(actorModel.actor);

        //Setup player
        player.transform.position = new Vector3(playerBattleStation.transform.position.x - 10f, playerBattleStation.transform.position.y + 1f, 0);
        player.name = "Player";
        player.transform.SetParent(this.transform);
        player.GetComponent<Player>().ActorStats(actorModel);
    }

    void UpdateCharacter(int selectedOption)
    {
        actorModel = characterDB.GetCharacter(selectedOption);
    }

    void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
