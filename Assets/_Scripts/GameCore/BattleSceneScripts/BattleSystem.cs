using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening.Core.Easing;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    // Serialized fields - can be accessed and modified in Inspector
    [SerializeField] private GameObject _playerActionPanel;
    [SerializeField] private GameObject _selectionWordMeaningPanel;
    [SerializeField] private UsingDatabase _usingDatabase;
    [SerializeField] int damageReduction = 0;
    [SerializeField] int enemyCount = 4;
    [SerializeField] int enemyKill = 0;
    [SerializeField] bool instantKill = false;
    [SerializeField] bool instantKillPlayer = false;
    [SerializeField] float setupTime = 2f;
    [SerializeField] float playerTimeToChooseMeaning = 2f;
    [SerializeField] int healNumber = 1;

    // Public fields - can be accessed externally by other scripts or components
    public BattleState state;
    public bool actionResult = false;
    public bool isPlayerChooseMeaningYet = false;

    public int actionNum; //0: attack, 1: defend;
    public float enemyAttackAnimTime = 2f;
    public float playerAttackAnimTime = 2f;

    // Private fields - only useonce
    private LoadScene_script loadScene_Script;

    // Private fields - internal variables used within the script
    private BattleHUD playerHUD;
    private BattleHUD enemyHUD;
    private BattleUnit playerUnit;
    private BattleUnit enemyUnit;
    private TMP_Text dialogueText;
    private GameObject player;
    private GameObject enemy;
    private Timer timer;
    private EnemySpawner enemySpawner;

    // biến dùng kiểm soát hành động của player, nếu onGoingAction = 1 player không được tương tác
    private int onGoingAction = 0;

    void GetReferences()
    {
        loadScene_Script = GameObject.Find("PlayerActionPanel").GetComponent<LoadScene_script>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<BattleHUD>();
        enemyHUD = GameObject.Find("EnemyHUD").GetComponent<BattleHUD>();
        dialogueText = GameObject.Find("PlayerActionPanel").GetComponentInChildren<TMP_Text>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    IEnumerator SetupBattle()
    {
        onGoingAction = 1;
        float delayTime = 0f;

        player = GameObject.Find("Player");
        playerUnit = player.GetComponent<BattleUnit>();
        playerHUD.SetPlayerHUD(player.GetComponent<Player>());
        playerUnit.Setup();
        yield return new WaitForSeconds(delayTime);

        enemy = GameObject.Find("Enemy");
        enemyUnit = enemy.GetComponent<BattleUnit>();
        enemyHUD.SetEnemyHUD(enemy.GetComponent<Enemy>());
        enemyUnit.Setup();
        //this looks like a mess but trust me it works =))
        dialogueText.text = "Battle Begin";

        //onGoingAction = 1;
        yield return new WaitForSeconds(setupTime);

        //onGoingAction = 0;
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        GetReferences();

        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (timer.timeValue == 0)
        {
            /*dialogueText.text = "Why are you taking so long??????";
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());*/
            loadScene_Script.LoadScene(7);
        }
    }

    void SetUpNewEnemy()
    {
        dialogueText.text = "New enemy appeared!";
        if (enemyCount == enemyKill)
        {
            enemySpawner.SpawnBoss();
        }
        else
        {
            enemySpawner.SpawnEnemy();
        }
        enemy = GameObject.Find("Enemy");
        enemyHUD.SetEnemyHUD(enemy.GetComponent<Enemy>());
        enemyUnit = enemy.GetComponent<BattleUnit>();
        enemyUnit.Setup();
    }

    public void PlayerTurn()
    {
        onGoingAction = 0;
        ActivatePlayerActionPanel();
        dialogueText.text = "Choose an action: ";
    }

    public IEnumerator PlayerAttack()
    {
        if (instantKill)
        {
            Destroy(enemy);
        }
        //start player attack animation
        playerUnit.PlayAttackAnimation();
        enemyUnit.PlayHitAnimation();
        enemy.GetComponent<Enemy>().TakeDamage(player.GetComponent<Player>().SendDamage());
        enemyHUD.SetHP(enemy.GetComponent<Enemy>().Health);
        dialogueText.text = "Attack successful!";

        //player stop attacking enemy
        yield return new WaitForSeconds(playerAttackAnimTime);

        //check if enemy still alive
        if (enemy.IsDestroyed())
        {
            enemyKill++;
            if (enemyKill > enemyCount)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                SetUpNewEnemy();
                state = BattleState.PLAYERTURN;
                //Invoke("PlayerTurn", playerTurnTime); //wait 2 seconds to start next function
                PlayerTurn();
                timer.ResetTimer();
            }
        }
        else
        {
            state = BattleState.ENEMYTURN;
            timer.ResetTimer();
            StartCoroutine(EnemyTurn());
        }
    }

    public IEnumerator PlayerDefend()
    {
        damageReduction = 2;
        dialogueText.text = "Defend successful!";
        yield return new WaitForSeconds(enemyAttackAnimTime);

        state = BattleState.ENEMYTURN;
        timer.ResetTimer();
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator PlayerHeal()
    {
        if(healNumber > 0)
        {
            dialogueText.text = "You can heal: " + healNumber;
            player.GetComponent<Player>().Health += 10;
            if(player.GetComponent<Player>().Health > player.GetComponent<Player>().MaxHealth)
            {
                playerHUD.SetHP(player.GetComponent<Player>().MaxHealth);
            }
            else
            {
                playerHUD.SetHP(player.GetComponent<Player>().Health);
            }
            healNumber--;
        }
        yield return new WaitForSeconds(enemyAttackAnimTime);

        state = BattleState.ENEMYTURN;
        timer.ResetTimer();
        StartCoroutine (EnemyTurn());
    }

    void PlayerAction(int caseNum)
    {
        switch (caseNum)
        {
            case 0:
                StartCoroutine(PlayerAttack());
                return;
            case 1:
                StartCoroutine(PlayerDefend());
                return;
            case 2:
                StartCoroutine(PlayerHeal());
                return;
            case 3:
                return;
        }
    }

    void EnemyAction(int caseNum)
    {
        switch (caseNum)
        {
            case 0: //Normal Attack
                player.GetComponent<Player>().TakeDamage(enemy.GetComponent<Enemy>().SendDamage() - damageReduction);
                dialogueText.text = "Enemy attacks!";
                return;
            case 1://Power Attack
                player.GetComponent<Player>().TakeDamage(enemy.GetComponent<Enemy>().PowerAttack() - damageReduction);
                dialogueText.text = "Critical hit!";
                return;
        }
    }

    public IEnumerator EnemyTurn()
    {
        int randomNum = Random.Range(0, 2); //random enemy action
        if (instantKillPlayer)
        {
            Destroy(player);
        }

        //start the animation
        enemyUnit.PlayAttackAnimation();
        playerUnit.PlayHitAnimation();
        EnemyAction(randomNum);
        playerHUD.SetHP(player.GetComponent<Player>().Health);

        yield return new WaitForSeconds(enemyAttackAnimTime);

        //kiem tra nguoi choi da chet chua
        if (player.IsDestroyed())
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            damageReduction = 0;
            state = BattleState.PLAYERTURN;
            timer.ResetTimer();
            PlayerTurn();

            //allow user to choose action again
        }
    }

    void ActivateWordPanel()
    {
        _selectionWordMeaningPanel.SetActive(true);
    }

    void ActivatePlayerActionPanel()
    {
        _selectionWordMeaningPanel.SetActive(false);
    }

    public void OnDefendButton()
    {
        if (state == BattleState.ENEMYTURN || onGoingAction == 1)
        {
            return;
        }
        else
        {
            onGoingAction = 1;
            actionNum = 1;

            ActivateWordPanel();
            //create and assign word to button
            _usingDatabase.StartAction();
            StartCoroutine(PlayerChoosingMeaning());
        }
    }

    public void OnAttackButton()
    {
        if (state == BattleState.ENEMYTURN || onGoingAction == 1)
        {
            return;
        }
        else
        {
            onGoingAction = 1;

            //action num 0 = player attack
            actionNum = 0;

            //Open attack panel
            ActivateWordPanel();

            //create and assign word to button
            _usingDatabase.StartAction();

            //Check the Result
            StartCoroutine(PlayerChoosingMeaning());
        }
    }

    public void OnHealButton()
    {
        if (state == BattleState.ENEMYTURN || onGoingAction == 1)
        {
            return;
        }
        else
        {
            onGoingAction= 1;
            actionNum = 2;

            if (healNumber == 0)
            {
                PlayerTurn();
                dialogueText.text = "Out of healing";
                onGoingAction = 0;
                return;
            }
            else
            {
                ActivateWordPanel();
                _usingDatabase.StartAction();

                StartCoroutine(PlayerChoosingMeaning());
            }

        }
        
    }

    IEnumerator PlayerChoosingMeaning()
    {
        // check the result from StartAction function
        yield return new WaitForSeconds(playerTimeToChooseMeaning);

        if (actionResult == true)
        {
            actionResult = false;
            PlayerAction(actionNum);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            timer.ResetTimer();
            StartCoroutine(EnemyTurn());
        }

        onGoingAction = 0;
    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            timer.StopTimer();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated!";
            timer.StopTimer();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}
