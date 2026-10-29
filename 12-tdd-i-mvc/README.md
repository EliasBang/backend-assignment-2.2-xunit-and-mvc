# 12 · Nytt behov: vis bare ufullførte oppdrag

## Oppdrag

I de forrige oppgavene har du bygget og testet en oppdragstavle med Model, View og Controller.

Nå kommer et nytt behov fra besetningen:

**"Vi vil kunne se hvilke oppdrag som fortsatt venter på å bli fullført."**

Du skal legge til menyvalg `4. Vis ufullførte` i konsollapplikasjonen.

Bruk TDD (*Red–Green–Refactor*) til å utvikle funksjonaliteten i modellen. Deretter skal du koble den til kontrolleren og gjenbruke den eksisterende visningen.

Målet er å legge til funksjonalitet uten å ødelegge det som allerede fungerer.

## Dette trenger du å vite

Når vi legger til en ny funksjon i en MVC-applikasjon, kan vi måtte gjøre endringer i flere deler av programmet.

Men hver del skal fortsatt ha sitt eget ansvar:

- **Model:** Finner oppdragene som ikke er fullført.
- **Controller:** Håndterer det nye menyvalget og ber modellen om oppdragene.
- **View:** Viser oppdragene som kontrolleren sender videre.

Vi skal bruke den eksisterende metoden `ShowTasks()` til å vise resultatet. Den trenger ikke vite om den får alle oppdragene eller bare de ufullførte.

### Krav til den nye funksjonaliteten

Metoden `MissionBoard.GetPending()` skal:

- Returnere oppdrag som har `IsCompleted == false`.
- Bevare rekkefølgen oppdragene ble lagt til i.
- Returnere en ny liste, ikke tavlens interne liste.
- Ikke endre eller fjerne noen oppdrag fra tavlen.
- Returnere en tom liste hvis ingen oppdrag venter.

Listen skal inneholde referanser til de eksisterende oppdragsobjektene, slik du lærte i oppgave 09.

## Gjør dette

### 1. Undersøk den eksisterende applikasjonen

Åpne disse filene:

- `Core/MissionBoard.cs`
- `App/MissionController.cs`
- `App/MissionView.cs`
- `Tests/ExerciseTests.cs`

Finn ut:

- Hva gjør `GetPending()` akkurat nå?
- Hvordan henter menyvalg 2 alle oppdragene?
- Hvordan viser `ShowTasks()` en liste med oppdrag?
- Hvor ligger menyteksten?

Tenk over hvilke filer du trenger å endre for å legge til menyvalg 4.

Du skal ikke implementere funksjonaliteten ennå.

### 2. Red – kjør testene og undersøk feilen

Åpne terminalen og kjør:

```sh
dotnet build 12-tdd-i-mvc/Exercise.slnx
dotnet test 12-tdd-i-mvc/Tests/Tests.csproj
```

Det finnes fem ferdige tester. **Én av dem skal feile**, fordi `GetPending()` ennå ikke er implementert.

Undersøk:

- Hvilken test feiler?
- Hva forventer testen?
- Hva returnerer `GetPending()` akkurat nå?
- Hvorfor består de andre testene?

Dette er utgangspunktet for Red-fasen: En test viser at det nye behovet ennå ikke er oppfylt.

Skriv ned resultatet i [REFLEKSJON.md](REFLEKSJON.md).

### 3. Skriv flere tester før du implementerer

Nå skal du utvide testene for `GetPending()`.

Start med to nye `[Fact]`-tester.

**Test A: Tom tavle**

Opprett en ny `MissionBoard` uten oppdrag.

Kontroller at `GetPending()` returnerer en tom liste.

Kjør testen.

Denne testen vil sannsynligvis allerede bestå. Det er greit. Selv en uferdig implementasjon kan gi riktig resultat i enkelte tilfeller.

**Test B: Alle oppdrag er ufullførte**

Opprett en tavle med to oppdrag. Ikke fullfør noen av dem.

Kontroller at `GetPending()` returnerer begge oppdragene.

Kjør testene igjen.

Denne testen skal feile med den uferdige implementasjonen.

Du har nå tester for både en tom tavle og en tavle som inneholder ufullførte oppdrag.

### 4. Green – implementer GetPending

Åpne `MissionBoard.cs`.

Implementer `GetPending()` slik at metoden følger kravene.

Du skal finne oppdragene som ikke er fullført, og legge dem i en ny resultatliste.

Behold rekkefølgen fra den opprinnelige listen.

**Ikke fjern oppdrag fra `_tasks`, og ikke endre `IsCompleted`.**

Kjør testene:

```sh
dotnet test 12-tdd-i-mvc/Tests/Tests.csproj
```

Kontroller at alle testene du har skrevet så langt, består.

Dette er Green-fasen: Modellen har nå fått den nye funksjonaliteten som testene beskriver.

### 5. Utvid testdekningen

Nå skal du undersøke flere situasjoner.

Lag tre nye `[Fact]`-tester.

**Test C: Alle oppdrag er fullførte**

Legg til to oppdrag og fullfør begge.

Kontroller at:

- `GetPending()` returnerer en tom liste.
- Tavlens `Count` fortsatt er 2.

**Test D: Rekkefølgen bevares**

Legg til tre oppdrag i denne rekkefølgen:

1. Sjekk antenne
2. Lad rover
3. Kontroller drivstoff

Fullfør oppdrag 2.

Kontroller at `GetPending()` returnerer oppdrag 1 og 3, i denne rekkefølgen.

Kontroller også at tavlen fortsatt inneholder tre oppdrag.

**Test E: Resultatlisten er en kopi**

Legg til to ufullførte oppdrag.

Hent listen med `GetPending()` og kontroller at den inneholder begge oppdragene.

Kall deretter `Clear()` på resultatlisten.

Kontroller at:

- Tavlens `Count` fortsatt er 2.
- Et nytt kall til `GetPending()` fortsatt returnerer begge oppdragene.

Dette viser at du kan endre den returnerte listen uten å endre tavlens interne liste.

Kjør testene på nytt.

Noen av de nye testene kan allerede bestå. Det er helt greit. Hvis en test feiler, undersøk hvorfor og rett implementasjonen.

Når du er ferdig, skal du ha **ti beståtte tester totalt**: fem ferdige og fem du har skrevet selv.

### 6. Refactor – vurder modellen

Se på implementasjonen av `GetPending()`.

Vurder:

- Er koden enkel å lese?
- Er variabelnavnene tydelige?
- Finnes det unødvendig kode?
- Er ansvaret riktig plassert i modellen?

Hvis du finner noe som kan forbedres uten å endre oppførselen, gjør en liten endring og kjør testene på nytt.

Hvis koden allerede er tydelig, trenger du ikke refaktorere bare for å gjøre en endring.

Alle ti testene skal fortsatt bestå.

### 7. Legg til menyvalg 4

Nå fungerer filtreringen i modellen. Neste steg er å gjøre funksjonaliteten tilgjengelig for brukeren.

**Endre View**

Åpne `MissionView.cs`.

Utvid `ShowMenu()` slik at menyen også inneholder valg 4:

```text
1. Legg til | 2. Vis alle | 3. Fullfør | 4. Vis ufullførte | 0. Avslutt
```

Du skal ikke endre `ShowTasks()`. Den kan allerede vise en liste med oppdrag.

**Endre Controller**

Åpne `MissionController.cs`.

Legg til håndtering av menyvalg `4` i `switch`-setningen.

Når brukeren velger 4, skal kontrolleren:

1. Hente de ufullførte oppdragene fra `_board.GetPending()`.
2. Sende resultatet til `_view.ShowTasks()`.

Ikke skriv en ny filtreringsregel i kontrolleren eller visningen.

Du trenger heller ikke endre `Program.cs` eller `MissionTask.cs`.

### 8. Test hele applikasjonen

Kjør konsollapplikasjonen:

```sh
dotnet run --project 12-tdd-i-mvc/App/App.csproj
```

Utfør handlingene i tabellen **i samme programkjøring og i oppgitt rekkefølge**.

| Steg | Handling | Forventet resultat |
|---|---|---|
| 1 | Velg `4` på tom tavle | `Ingen oppdrag.` |
| 2 | Legg til `Sjekk antenne` | Oppdrag 1 opprettes |
| 3 | Legg til `Lad rover` | Oppdrag 2 opprettes |
| 4 | Velg `4` | Begge oppdrag vises som `Venter` |
| 5 | Velg `3`, fullfør ID `1` | Oppdraget fullføres |
| 6 | Velg `4` | Bare `2: Lad rover [Venter]` vises |
| 7 | Velg `2` | Begge oppdrag vises, med riktig status |
| 8 | Velg `3`, fullfør ID `2` | Oppdraget fullføres |
| 9 | Velg `4` | `Ingen oppdrag.` |
| 10 | Velg `2` | Begge oppdragene vises som `Fullført` |
| 11 | Velg `0` | Programmet avsluttes |

Når det ikke finnes noen ufullførte oppdrag, vil `ShowTasks()` vise `Ingen oppdrag.` fordi den mottar en tom liste. Det betyr ikke at tavlen er tømt.

Kontroller spesielt at:

- Menyvalg 4 bare viser ufullførte oppdrag.
- Menyvalg 2 fortsatt viser alle oppdrag.
- Fullførte oppdrag ikke blir slettet.
- Oppdrag forsvinner fra den filtrerte visningen når de fullføres.
- De eksisterende menyvalgene fortsatt fungerer.

Dette er en manuell kontroll av hele konsollapplikasjonen.

### 9. Kjør alle automatiserte tester igjen

Kjør:

```sh
dotnet test 12-tdd-i-mvc/Tests/Tests.csproj
```

Kontroller at alle ti modelltestene består.

Husk forskjellen:

- De automatiserte testene undersøker `MissionBoard` og oppførselen til `GetPending()`.
- Den manuelle gjennomgangen undersøker om det nye menyvalget fungerer sammen med Model, View og Controller.

### 10. Dokumenter og forklar løsningen

Skriv i [REFLEKSJON.md](REFLEKSJON.md):

| Del | Hva skal du dokumentere? |
|---|---|
| Red | Hvilke tester feilet før implementasjonen, og hvorfor? |
| Green | Hva endret du i `GetPending()`, og hva ble testresultatet? |
| Refactor | Forbedret du noe? Hvis ikke, hvorfor var det unødvendig? |
| MVC | Hvilke endringer gjorde du i Model, View og Controller? |
| Manuell kontroll | Hva skjedde da du testet menyvalg 4 og deretter viste alle oppdrag? |

Gjør deg også klar til en kort individuell kodegjennomgang der du kan vise hvor du implementerte funksjonaliteten, og forklare hvorfor endringene hører hjemme akkurat der.

## Ferdig når

- `GetPending()` returnerer riktig liste med ufullførte oppdrag.
- Rekkefølgen bevares.
- Filtreringen endrer ikke tavlens tilstand.
- Resultatlisten kan tømmes uten at tavlens oppdrag forsvinner.
- Alle ti automatiserte tester består.
- Menyvalg 4 fungerer og bruker den eksisterende `ShowTasks()`-metoden.
- Menyvalg 1, 2, 3 og 0 fungerer fortsatt.
- Du har dokumentert TDD-forløpet og kan begrunne endringene i MVC.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor skal filtreringen ligge i `MissionBoard` i stedet for i `MissionView`?
2. Hvorfor skal `GetPending()` returnere en ny liste i stedet for å fjerne fullførte oppdrag fra den eksisterende listen?
3. Hvorfor kan en test for tom tavle bestå selv om `GetPending()` ennå ikke er implementert riktig?
4. Hvorfor trengte du ikke skrive en ny `ShowTasks()`-metode for menyvalg 4?
5. Hvordan hjalp de eksisterende testene deg med å kontrollere at den nye funksjonaliteten ikke ødela tidligere oppførsel?

## Hint ved behov

<details>
<summary>Åpne først etter et eget forsøk</summary>

**Modellen:** Opprett en ny resultatliste i `GetPending()`. Gå gjennom `_tasks`, og legg bare til oppdrag som ikke er fullført. Returner resultatlisten.

**Kontrolleren:** Se på hvordan menyvalg 2 henter alle oppdragene og sender dem til `ShowTasks()`.

Menyvalg 4 skal følge samme mønster, men bruke `GetPending()` i stedet for `GetAll()`.

**Visningen:** Du trenger bare å legge til menyteksten. `ShowTasks()` fungerer allerede med både tomme og fylte lister.

</details>
