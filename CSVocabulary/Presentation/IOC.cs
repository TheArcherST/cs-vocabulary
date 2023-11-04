using CSVocabulary.Domain.Services;
using CSVocabulary.Domain.Repositories;

namespace CSVocabulary.Presentation;


public class IOC
{
    private string _wordsFilePath;
    private string _translationsFilePath;
    
    public IOC(string wordsFilePath, string translationsFilePath)
    {
        _wordsFilePath = wordsFilePath;
        _translationsFilePath = translationsFilePath;
    }
    
    private void CreateEmptyJsonFile(string path)
    {
        using var fs = File.Open(path, FileMode.Create);
        using var fsW = new StreamWriter(fs);
        fsW.WriteLine("{\"Root\": []}");
    }
    
    public Vocabulary GetDictionary()
    {
        if (!File.Exists(_wordsFilePath)) CreateEmptyJsonFile(_wordsFilePath);
        if (!File.Exists(_translationsFilePath)) CreateEmptyJsonFile(_translationsFilePath);
        
        return new Vocabulary(
            new WordRepo(_wordsFilePath),
            new TranslationRepo(_translationsFilePath)
        );
    }
}
