# C# vocabulary mini proj

Brief
-----

This project implements simple dictionary (vocabulary)
with words in different languages. You can perform
several actions:

1. Create new word in any language.
2. Make translation relations between words.
3. Delete translation relations
4. Get all translations of a specified word.

Architecture
------------

There are are domain and presentation layers. Application
layer is mixed up with the domain layer in the service
Vocabulary, that exposing all use cases of the application.

Data integrity managing is encapsulated in repositories.
Base repository implements some basis methods like Commit
(commit changes) and Add (track passed object). Repositories
is able to work only with objects, that has `Id` field (to 
simplify objects mapping in a implementation)

Implementation details
----------------------

To not complicate project, repositories store values just 
within json files via standard C# library `System.Text.Json`.