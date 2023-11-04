using CSVocabulary.Domain.Services;
using CSVocabulary.Domain.Repositories;

namespace CSVocabulary.Presentation;


public class IOC
{
    private void CreateEmptyJsonFile(string path)
    {
        using var fs = File.Open(path, FileMode.Create);
        using var fsW = new StreamWriter(fs);
        fsW.WriteLine("{\"Root\": []}");
    }
    
    public Vocabulary GetDictionary()
    {
        var wordsFilePath = "words.json";
        var translationsFilePath = "translations.json";

        if (!File.Exists(wordsFilePath)) CreateEmptyJsonFile(wordsFilePath);
        if (!File.Exists(translationsFilePath)) CreateEmptyJsonFile(translationsFilePath);
        
        return new Vocabulary(
            new WordRepo(wordsFilePath),
            new TranslationRepo(translationsFilePath)
        );
    }
}