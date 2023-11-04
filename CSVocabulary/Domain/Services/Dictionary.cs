using CSVocabulary.Domain.Errors;
using CSVocabulary.Domain.Models;
using CSVocabulary.Domain.Repositories;

namespace CSVocabulary.Domain.Services;


public record Vocabulary(WordRepo WordRepo, TranslationRepo TranslationRepo)
{
    
    public Word AddWord(string value, string language)
    {
        var id = WordRepo.GetNextId();
        var word = new Word(id, value, language);
        WordRepo.Add(word);
        WordRepo.Commit();
        return word;
    }
    
    public void RegisterTranslation(string val1, string lang1, string val2, string lang2)
    {
        var word1 = WordRepo.Merge(val1, lang1);
        var word2 = WordRepo.Merge(val2, lang2);
        
        var translation = TranslationRepo.GetByWordId(word1.Id);
        var ids = new List<int> { word1.Id, word2.Id };
        if (translation == null)
        {
            var obj = TranslationRepo.Create(
                new List<int> { word1.Id, word2.Id }
            );
            TranslationRepo.Add(obj);
        }
        else
        {
            var notPresentedIds = translation.WordIds
                .Where(i => !ids.Contains(i)).ToList();
            translation.WordIds.AddRange(notPresentedIds);
        }
        WordRepo.Commit();
        TranslationRepo.Commit();
    }

    public void RemoveTranslation(string val1, string lang1, string val2, string lang2)
    {
        var word1 = WordRepo.Merge(val1, lang1);
        var word2 = WordRepo.Merge(val2, lang2);
        var translation = TranslationRepo.GetByWordId(word1.Id);
        if (translation == null) return;
        translation.WordIds.Remove(word2.Id);
        TranslationRepo.Add(translation);
        TranslationRepo.Commit();
    }
    
    public IEnumerable<Word> Translate(string originalWord, string originalLang)
    {
        var word = WordRepo.GetByValue(originalWord, originalLang);
        if (word == null)
        {
            throw new WordNotFound();
        }
        var translation = TranslationRepo.GetByWordId(word.Id);
        var resultWordIds = translation.WordIds;
        resultWordIds.Remove(word.Id);
        return resultWordIds.Select(i => WordRepo.GetById(i)!);
    }
};
