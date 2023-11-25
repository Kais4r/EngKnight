using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawner : MonoBehaviour
{
    SOActorModel actorModel;
    GameObject enemy;
    GameObject enemyBattleStation;
    List<string> enemyNames = new List<string>();

    void Start()
    {
        CreateBattleStation();
        CreateEnemyList();
        CreateEnemy();
    }

    void CreateBattleStation()
    {
        GameObject enemyBattleStationPreb = Resources.Load("GamePrefabs/EnemyBattleStation") as GameObject;
/*        if (enemyBattleStationPreb == null )
        {
            Debug.Log("there is no battle station");
        }*/
        enemyBattleStation = GameObject.Instantiate(enemyBattleStationPreb, Vector3.zero, Quaternion.identity);
        enemyBattleStation.transform.position = new Vector3(-3.56f, 2.8f, 0);
        enemyBattleStation.transform.SetParent(this.transform);
        enemyBattleStation.name = enemyBattleStationPreb.name;
    }

    void CreateEnemyList()
    {
        enemyNames.Clear();
        enemyNames.Add("Enemy");
        enemyNames.Add("Enemy2");
        enemyNames.Add("Enemy3");
        enemyNames.Add("Enemy4");
        enemyNames.Add("Boss");

    }

    void CreateEnemy()
    {
        int randomNum = Random.Range(0,4); //return a value between 0 and 3
        actorModel = Object.Instantiate(Resources.Load("ScriptableObject/Enemy/" + enemyNames[randomNum])) as SOActorModel;

        //Test Boss
        //actorModel = Object.Instantiate(Resources.Load("ScriptableObject/Enemy/" + enemyNames[randomNum])) as SOActorModel;

        //Debug.Log(randomNum);
        //Debug.Log(actorModel.description);

        enemy = GameObject.Instantiate(actorModel.actor);

        enemy.transform.position = new Vector3(enemyBattleStation.transform.position.x+10f, enemyBattleStation.transform.position.y + 1f, 0);
        enemy.name = "Enemy";
        enemy.transform.SetParent(this.transform);
        enemy.GetComponent<Enemy>().ActorStats(actorModel);
    }

    public void SpawnEnemy()
    {
        CreateEnemy();
    }

    public void SpawnBoss()
    {
        actorModel = Object.Instantiate(Resources.Load("ScriptableObject/Enemy/" + enemyNames[4])) as SOActorModel;
        enemy = GameObject.Instantiate(actorModel.actor);

        enemy.transform.position = new Vector3(enemyBattleStation.transform.position.x + 10f, enemyBattleStation.transform.position.y + 1f, 0);
        enemy.name = "Enemy";
        enemy.transform.SetParent(this.transform);
        enemy.GetComponent<Enemy>().ActorStats(actorModel);
    }

}
