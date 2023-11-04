using System.Runtime.InteropServices.JavaScript;
using CSExam.Domain.Models;

namespace CSExam.Presentation;

public class CLI
{
    private IOC _ioc = new();
    private readonly MyDictionary _dictionary;
    private string? _defaultLang1;
    private string? _defaultLang2;

    public CLI()
    {
        _dictionary = _ioc.GetDictionary();
    }

    private string Input(string q)
    {
        Console.Write(q + "\n>? ");
        return Console.ReadLine() ?? "";
    }
    
    private void ProcessCommand(int n)
    {
        string word1;
        string word2;
        string lang1;
        string lang2;
        
        switch (n)
        {
            case 1:  // add word
                lang1 = Input("Введи язык");
                word1 = Input($"Введи слово на языке {lang1}");
                _dictionary.AddWord(word1, lang1);
                break;
            case 2:  // add translation
                lang1 = _defaultLang1 ?? Input("Введи исходный язык");
                word1 = Input($"Введи исходное слово на языке {lang1}");
                lang2 = _defaultLang2 ?? Input("Введи конечный язык");
                word2 = Input($"Введи перевод на языке {lang2}");
                _dictionary.RegisterTranslation(word1, lang1, word2, lang2);
                break;
            case 3:  // translate word
                lang1 = _defaultLang2 ?? Input("Введи язык");
                word1 = Input($"Введи слово для перевода на языке {lang1}");
                var translations = _dictionary.Translate(word1, lang1);
                foreach (var translation in translations)
                {
                       Console.WriteLine($"{translation.Language}: {translation.Value}");
                }    
                break;
            case 4:  // set default languages
                _defaultLang1 = Input("Введи язык, который ты знаешь");
                _defaultLang2 = Input("Введи язык, который ты НЕ знаешь");
                Console.WriteLine("Языки успешно установлены");
                break;
            case 5:  //  remove translation
                lang1 = _defaultLang1 ?? Input("Введи язык перевого слова");
                word1 = Input($"Введи слово для удаления на языке {lang1}");
                lang2 = _defaultLang2 ?? Input("Введи язык второго слова");
                word2 = Input($"Введи слово для удаления на языке {lang2}");
                _dictionary.RemoveTranslation(word1, lang1, word2, lang2);
                break;
        }
    }
    public void Run()
    {
        while (true)
        {
            Console.Write("Выбери действие\n\n" +
                          "[1] - Добавить слово\n" +
                          "[2] - Добавить перевод\n" +
                          "[3] - Перевести слово\n" +
                          "[4] - Установить языки по умолчанию\n" +
                          "[5] - Удалить перевод\n" +
                          ">? ");
            var inp = Console.ReadLine();

            if (inp is null) break;
            var number = -1;
            try
            {
                number = int.Parse(inp);
            } catch (ArgumentException)
            {
                Console.Write("Введи число\n" +
                              ">? ");
                continue;
            }
            ProcessCommand(number);
            Console.WriteLine();
        }
    }
}