using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public TMP_Text nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);

    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }        
        UpdateCharacter(selectedOption);
        Save();
    }

    void UpdateCharacter(int selectedOption)
    {
        SOActorModel character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.actorSprite;
        nameText.text = character.name;
    }

    void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("A1_battle_scene");
    }
}
