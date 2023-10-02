using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private UsingDatabase _usingDatabase;
    [SerializeField] private TextMeshProUGUI _buttonWordMeaning;
    [SerializeField] private GameObject _playerActionPanel;
    [SerializeField] private GameObject _selectWordMeaningPanel;
    [SerializeField] private BattleSystem _battleSystem;

    private Image _buttonImage;
    Color myRed = new Color(0.941f, 0.561f, 0.561f);
    Color myGreen= new Color(0.561f, 0.949f, 0.545f);

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        if(_buttonImage == null)
        {
            Debug.LogError("button image is null in ActionButton.cs");
        }
    }

    public void CheckButtonMeaning()
    {
        if (_battleSystem.isPlayerChooseMeaningYet == true)
        {
            return;
        }
        else
        {
            _battleSystem.isPlayerChooseMeaningYet = true;
            if (_buttonWordMeaning.text == _usingDatabase.enemyMeaningText)
            {
                //Debug.Log("Correct choice");
                _buttonImage.color = myGreen;
                _battleSystem.actionResult = true;
            }
            else
            {
                _buttonImage.color = myRed;
            }

            if(_battleSystem.actionNum == 0)
            {
                Invoke("DeactivatePanelAfterDelay", _battleSystem.playerAttackAnimTime);
            }
            else if(_battleSystem.actionNum == 1)
            {
                Invoke("DeactivatePanelAfterDelay", _battleSystem.enemyAttackAnimTime);
            }
            else if(_battleSystem.actionNum == 2)
            {
                Invoke("DeactivatePanelAfterDelay", 0f);
            }
        }
    }

    private void DeactivatePanelAfterDelay()
    {
        _battleSystem.isPlayerChooseMeaningYet = false;
        _buttonImage.color = Color.white;

        // Execute the desired code
        _selectWordMeaningPanel.SetActive(false);
    }
}
