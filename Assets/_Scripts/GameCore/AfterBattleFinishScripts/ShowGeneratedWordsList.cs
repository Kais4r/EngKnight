using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShowGeneratedWordsList : MonoBehaviour
{
    // Start is called before the first frame update
    private ScenesDataManager scenesDataManager;
    [SerializeField] GameObject englishWordPanelPrefab;
    [SerializeField] Transform scrollViewContent;

    /*[SerializeField] TextMeshProUGUI wordName;
    [SerializeField] TextMeshProUGUI wordMeaning;*/

    public void Awake()
    {
        scenesDataManager = GameObject.Find("ScenesDataManager").GetComponent<ScenesDataManager>();
        if (scenesDataManager == null ) 
        {
            Debug.LogError("cant get scenesDataManager from ShowGeneratedWordsList");
        }

    }

    // Update is called once per frame
    void Start()
    {
        /*wordName.text = scenesDataManager.englishWords[3].Name;
        wordMeaning.text = scenesDataManager.englishWords[3].Meaning;*/

        foreach (EnglishWord englishWord in scenesDataManager.englishWords)
        {
            GameObject item = Instantiate(englishWordPanelPrefab);
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = englishWord.Name;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = englishWord.Meaning;
            item.GetComponent<LoadHyperLinkForWordListPanel>().url = englishWord.TranslationSource;

            item.transform.SetParent(scrollViewContent);
            item.transform.localScale = Vector2.one;

            /*Debug.Log(englishWord.Name);
            Debug.Log(englishWord.Meaning);*/
        }
    }
}
