public class EnglishWord
{
    //Field and property
    private string _name;
    public string Name
    { 
        get { return _name; }
        set { _name = value; }
    }

    private string _meaning;
    public string Meaning
    {
        get { return _meaning; }
        set { _meaning = value; }
    }
    private string _translationSource;
    public string TranslationSource
    {
        get { return _translationSource; }
        set { _translationSource = value; }
    }

    //Methods
    //Create methods
    public EnglishWord()
    {

    }

    public EnglishWord(string inputName, string inputMeaning, string translationSource)
    {
        _name = inputName; _meaning = inputMeaning; _translationSource = translationSource;
    }
}
