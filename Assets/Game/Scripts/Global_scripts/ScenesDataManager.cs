using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesDataManager : MonoBehaviour
{
    public static ScenesDataManager instance;

    // create variable to pass data here
    public List<EnglishWord> englishWords = new List<EnglishWord>();
    public string levelToStart = "";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GetEnglishWordList(List<EnglishWord> temp)
    {
        englishWords = temp;
    }
    public void ClearGeneratedWordsList()
    {
        englishWords.Clear();
    }
    public int CheckIfWordExist(EnglishWord englishWord)
    {
        bool result = englishWords.Exists(eW => eW.Name == englishWord.Name);
        if (result)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
