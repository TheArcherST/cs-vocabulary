using CSVocabulary.Presentation;


var ioc = new IOC(
    "words.json",
    "translations.json");

var cli = new CLI(ioc);
cli.Run();
