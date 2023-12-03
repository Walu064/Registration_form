# Registration_form
Projekt to prosty formularz rejestracyjny z możliwością obsługi mowy, umożliwiający użytkownikom wypełnianie formularza za pomocą głosu. Aplikacja została zbudowana przy użyciu C# i WPF.

## Funkcje

- **Rozpoznawanie Mowy:** Aplikacja wykorzystuje Microsoft Speech SDK do rozpoznawania wypowiedzianych słów i wypełniania odpowiednich pól formularza.
- **Dynamiczne Kontrole:** Pewne kontrole formularza, takie jak wybór daty i kraju, dynamicznie przełączają się między TextBox a oryginalną kontrolą, aby poprawić doświadczenie użytkownika podczas wprowadzania głosem.
- **Walidacja Danych:** Aplikacja sprawdza, czy wszystkie wymagane pola są wypełnione, zanim użytkownik będzie mógł zapisać formularz.
- **Zapis i Wyczyszczenie:** Użytkownicy mogą zapisać dane formularza, a także istnieje opcja wyczyszczenia wszystkich pól dla świeżego startu.

## Rozpoczęcie Pracy

Aby uruchomić aplikację, upewnij się, że masz zainstalowane niezbędne zależności, w tym Microsoft Speech SDK.

1. Sklonuj repozytorium:

   ```bash
   git clone https://github.com/twoja-nazwa-uzytkownika/formularz-rejestracyjny.git
   ```
2. Otwórz projekt w środowisku Visual Studio.
3. Zbuduj i uruchom projekt.
   
## Zależności
- .NET Core - Upewnij się, że masz zainstalowany .NET Core.
- Microsoft Speech SDK - Postępuj zgodnie z oficjalną dokumentacją, aby skonfigurować Speech SDK.

## Użycie
1. Uruchom aplikację.
2. Włącz rozpoznawanie mowy za pomocą ikony mikrofonu.
3. Wypowiadaj słowa wyświetlone na ekranie, aby wypełnić odpowiadające im pola formularza.
4. Kliknij przycisk "Zapisz", aby zachować dane formularza.
5. Opcjonalnie kliknij przycisk "Wyczyść", aby zresetować wszystkie pola.
