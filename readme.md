<img src="icon.png" align="right" />

# Synword

Приложение для проверки и повышения уникальности текста.

**[Website](https://synword.com)**

## Строки подключения
1. Для инициализации подключения к SQLite необходимо включить пользовательские секреты. Данную команду необходимо прописать внутри PublicApi проекта

    ```
    dotnet user-secrets init
    ```

1. Определение строки подключения

    ```
    dotnet user-secrets set "UserDataConnection" "data source=.\\UserDataDB.sqlite"
    ```
    
    ```
    dotnet user-secrets set "IdentityConnection" "data source=.\\IdentityDB.sqlite"
    ```

## Создание БД

1. Для начала создадим миграцию в окне Package Manager Console
с помощью команды:

    ```
    add-migration InitialMigration -Context UserDataContext -OutputDir "UserData/Migrations"
    ```
1. Применение миграции:

    ```
    update-database -Context UserDataContext
    ```
1. IdentityDB:

    ```
    add-migration InitialMigration -Context AppIdentityDbContext -OutputDir "Identity/Migrations"
    ```
    
    ```
    update-database -Context AppIdentityDbContext
    ```
    
## JWT
1. Добавьте закрытый ключ для подписи JWT

    ```
    dotnet user-secrets set "JWT_SECRET_KEY" "test"
    ```
## Domain database model

<p align="center">
    <img src="docs/db_model.png" alt="drawing" width="80%"/>
</p>

## App Screens

<p align="center">
    <img src="docs/Images/Main screen.png" alt="drawing" width="30%"/>
</p>

<p align="center">
    <img src="docs/Images/Plagiarism check layer.png" alt="drawing" width="30%"/>
</p>

<p align="center">
    <img src="docs/Images/Rephrase layer.png" alt="drawing" width="30%"/>
</p>