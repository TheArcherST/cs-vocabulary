using CSExam.Domain.Models;
using CSExam.Domain.Repositories;

namespace CSExam.Presentation;


public class IOC
{
    private void CreateEmptyJsonFile(string path)
    {
        using var fs = File.Open(path, FileMode.Create);
        using var fsW = new StreamWriter(fs);
        fsW.WriteLine("{\"Root\": []}");
    }
    
    public MyDictionary GetDictionary()
    {
        var wordsFilePath = "words.json";
        var translationsFilePath = "translations.json";

        if (!File.Exists(wordsFilePath)) CreateEmptyJsonFile(wordsFilePath);
        if (!File.Exists(translationsFilePath)) CreateEmptyJsonFile(translationsFilePath);
        
        return new MyDictionary(
            new WordRepo(wordsFilePath),
            new TranslationRepo(translationsFilePath)
        );
    }
}