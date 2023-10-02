using UnityEngine;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class UsingDatabase : MonoBehaviour
{
    //Out put field
    [SerializeField]
    private string dataBaseName = "EnglishWords";

    ScenesDataManager scenesDataManager;
    public void Awake()
    {
        if (!GameObject.Find("ScenesDataManager").TryGetComponent<ScenesDataManager>(out scenesDataManager)) 
        {
            Debug.LogError("Cant find scenesDataManager from UsingDatabase.cs");
        }
        else
        {
            scenesDataManager.ClearGeneratedWordsList();
        }
    }

    //use example:englishWordsList = GenerateOneEnglishWordList("A1",1,6);
    public List<EnglishWord> GenerateOneEnglishWordList(string tableName, int startIndex, int endIndex) // endIndex must alway be your real end index +1
    {
        // Connect to database
        string conn = SetDataBaseClass.SetDataBase(dataBaseName + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();

        //Query and using data
        List<EnglishWord> englishWordsList = new();
        List<int> iDList = new();

        for (int i = 0; i < 4; i++)
        {
            EnglishWord englishWord = new();

            int randomIDNumber = UnityEngine.Random.Range(startIndex, endIndex);

            while (iDList.Contains(randomIDNumber) == true)
            {
                randomIDNumber = UnityEngine.Random.Range(startIndex, endIndex);
            }
            iDList.Add(randomIDNumber);

            string SQLQuery = $"SELECT Name,Meaning,TranslationSrc FROM {tableName} Where ID = '" + randomIDNumber + "'  ";
            dbcmd.CommandText = SQLQuery;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                //Getting item from database
                englishWord.Name = reader.GetString(0);
                englishWord.Meaning = reader.GetString(1);
                englishWord.TranslationSource = reader.GetString(2);

                englishWordsList.Add(englishWord);
            }
            reader.Close();
        }

        //clost database connection
        dbcmd.Dispose();
        dbcon.Close();
        return englishWordsList;
    }

    //testing database connection function
    /*    public void testingDatabase()
        {
            // Connect to database
            string conn = SetDataBaseClass.SetDataBase(dataBaseName + ".db");
            IDbConnection dbcon;
            IDbCommand dbcmd;
            IDataReader reader;

            dbcon = new SqliteConnection(conn);
            dbcon.Open();
            dbcmd = dbcon.CreateCommand();

            //Query and using data
            string SQLQuery = $"SELECT Name,Meaning FROM A1 Where ID = '1'  ";
            dbcmd.CommandText = SQLQuery;
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                //Getting item from database
                _enemyWordText.text = reader.GetString(1);
            }
            reader.Close();

            //clost database connection
            dbcmd.Dispose();
            dbcon.Close();
        }
    */
    [SerializeField]
    private TextMeshProUGUI _enemyWordText;
    public string enemyMeaningText;

    [SerializeField] private TextMeshProUGUI[] _buttonTextList;
    private List<EnglishWord> _englishWordsList = new();  

    public void StartAction()
    {
        _englishWordsList = GenerateOneEnglishWordList("A1", 1, 49);
        //Invoke(nameof(AddTextToButton), 0.1f);

        //add generated english words to a list for player to study after combat
        scenesDataManager.englishWords.Add(_englishWordsList[0]);
        for(int i = 0; i < _englishWordsList.Count; i++)
        {
            if (scenesDataManager.CheckIfWordExist(_englishWordsList[i]) != 1)
            {
                scenesDataManager.englishWords.Add(_englishWordsList[i]);
            }
        }

        AddTextToButton();
        //testingDatabase();
    }

    public void AddTextToButton()
    {
        List<int> idList = new();

        int randomIdIndex = UnityEngine.Random.Range(0, 4);

        _enemyWordText.text = _englishWordsList[randomIdIndex].Name;
        enemyMeaningText = _englishWordsList[randomIdIndex].Meaning;

        _buttonTextList[randomIdIndex].text = enemyMeaningText;
        idList.Add(randomIdIndex);

        for (int i = 0; i < 3; i++)
        {
            randomIdIndex = UnityEngine.Random.Range(0, 4);
            while (idList.Contains(randomIdIndex) == true)
            {
                randomIdIndex = UnityEngine.Random.Range(0, 4);
            }
            idList.Add(randomIdIndex);

            if (_englishWordsList[randomIdIndex].Meaning == "")
            {
                _buttonTextList[randomIdIndex].text = "meaninglist null";
            }
            else
            {
                _buttonTextList[randomIdIndex].text = _englishWordsList[randomIdIndex].Meaning;
            }
        }
    }
}

