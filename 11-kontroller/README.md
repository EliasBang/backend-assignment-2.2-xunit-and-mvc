# 11 · Kontrolleren: koble sammen en hel konsollapp

## Oppdrag

I de forrige oppgavene har du arbeidet med modellen og visningen hver for seg.

Nå skal du koble dem sammen gjennom en **Controller**, slik at brukeren kan legge til, vise og fullføre oppdrag fra en meny.

Det meste av applikasjonen fungerer allerede. Menyvalget for å fullføre et oppdrag er ikke ferdig.

Du skal implementere `CompleteTask()` i `MissionController` og kontrollere at hele applikasjonen fungerer som forventet.

## Dette trenger du å vite

I MVC har de tre delene forskjellige ansvar:

- **Model:** Håndterer oppdragene og reglene, for eksempel å fullføre et oppdrag.
- **View:** Leser inndata og viser informasjon til brukeren.
- **Controller:** Tar imot brukerens valg, bestemmer hva som skal skje, og kobler sammen View og Model.

Kontrolleren skal ikke selv endre oppdragenes tilstand. Den skal be modellen gjøre det.

### Hvordan håndterer vi brukerinput?

Når brukeren velger å fullføre et oppdrag, må programmet vite hvilken ID brukeren mener.

`ReadInput()` returnerer tekst, men `MissionBoard.Complete()` forventer et heltall.

Du kan bruke `int.TryParse()` til å undersøke om teksten kan gjøres om til et heltall:

```csharp
int.TryParse(input, out int id)
```

Metoden returnerer `true` hvis teksten kan tolkes som et heltall, og `false` hvis det ikke går. Variabelen `id` får tallet når tolkningen lykkes.

For eksempel:

| Tekst fra brukeren | Resultat | ID |
|---|---|---|
| `"7"` | `true` | 7 |
| `"abc"` | `false` | Ingen gyldig ID |

Det er viktig å skille mellom to spørsmål:

1. **Er inndataene et heltall?** Det kontrollerer `TryParse`.
2. **Finnes det et oppdrag med denne ID-en?** Det avgjør `MissionBoard.Complete()`.

Hvis brukeren skriver ugyldig tekst, skal kontrolleren vise en feilmelding og gå tilbake til menyen.

Hvis brukeren skriver en ID som ikke finnes, skal modellen returnere `false`, og kontrolleren skal vise riktig melding.

## Gjør dette

### 1. Undersøk hvordan applikasjonen er koblet sammen

Åpne `Program.cs` i `App`-prosjektet.

Undersøk:

- Hvor opprettes `MissionBoard`?
- Hvor opprettes `MissionView`?
- Hvordan får `MissionController` tilgang til disse objektene?
- Hva skjer når `controller.Run()` kalles?

Legg merke til at objektene opprettes én gang før menyløkken starter.

Åpne deretter `MissionController.cs`.

Undersøk hvordan `Run()` bruker `switch` til å håndtere menyvalgene.

Se spesielt på `AddTask()`. Metoden viser et nyttig mønster:

1. Les inndata gjennom View.
2. Kontroller inndataene.
3. Kall riktig metode i Model.
4. Vis resultatet gjennom View.

Du skal bruke en lignende arbeidsmåte når du implementerer `CompleteTask()`.

### 2. Kjør applikasjonen før du endrer noe

Åpne terminalen og kjør:

```sh
dotnet build 11-kontroller/Exercise.slnx
dotnet test 11-kontroller/Tests/Tests.csproj
```

Kontroller at de fire eksisterende modelltestene består.

Kjør deretter konsollapplikasjonen:

```sh
dotnet run --project 11-kontroller/App/App.csproj
```

Prøv følgende:

1. Velg `1` og skriv `Sjekk antenne` som tittel.
2. Velg `2` og kontroller at oppdraget vises med status `Venter`.
3. Velg `3` og se hva programmet skriver.

Du skal se meldingen:

```text
Fullføring er ikke koblet til ennå.
```

Dette er forventet. `CompleteTask()` er ikke implementert.

**Hvorfor består modelltestene selv om menyvalg 3 ikke fungerer?**

De eksisterende testene kontrollerer modellen, ikke at menyvalgene er riktig koblet til den.

Avslutt programmet med menyvalg `0`.

### 3. Implementer fullføring i kontrolleren

Åpne `MissionController.cs` og finn metoden `CompleteTask()`.

Du skal erstatte den midlertidige meldingen med funksjonalitet som fullfører et oppdrag.

Metoden skal gjøre følgende:

1. Be brukeren om en ID ved hjelp av `_view.ReadInput("ID: ")`.
2. Bruke `int.TryParse()` til å kontrollere om teksten er et heltall.
3. Hvis teksten ikke er et heltall, vise `ID må være et heltall.` og gå tilbake til menyen.
4. Hvis teksten er et heltall, kalle `_board.Complete(id)`.
5. Hvis modellen returnerer `true`, vise `Oppdrag 1 fullført.` når ID-en er 1, og tilsvarende melding for andre ID-er.
6. Hvis modellen returnerer `false`, vise `Fant ikke oppdraget.`

Bruk `_view.ShowMessage()` til å vise meldingene.

**Ikke bruk `Console.ReadLine()` eller `Console.WriteLine()` direkte i kontrolleren.** Disse oppgavene håndteres allerede av View.

Du skal heller ikke endre `MissionBoard.Complete()`. Modellen vet allerede hvordan den finner og fullfører et oppdrag.

### 4. Kontroller at ugyldig input håndteres

Kjør applikasjonen igjen:

```sh
dotnet run --project 11-kontroller/App/App.csproj
```

Velg `3`.

Når programmet spør etter ID, skriver du:

```text
abc
```

Programmet skal svare:

```text
ID må være et heltall.
```

Deretter skal menyen vises igjen.

Husk at `return` inne i `CompleteTask()` bare avslutter denne metoden. Det avslutter ikke menyløkken i `Run()`.

Prøv deretter menyvalg `3` med ID `99`.

Hvis det ikke finnes et oppdrag med denne ID-en, skal programmet skrive:

```text
Fant ikke oppdraget.
```

Menyen skal fortsatt fungere etterpå.

### 5. Test hele applikasjonen

Nå skal du kontrollere at Model, View og Controller fungerer sammen.

Start applikasjonen på nytt:

```sh
dotnet run --project 11-kontroller/App/App.csproj
```

Utfør handlingene i tabellen **i den oppgitte rekkefølgen og i samme programkjøring**.

| Steg | Det du gjør | Forventet resultat |
|---|---|---|
| 1 | Velg `2` | `Ingen oppdrag.` |
| 2 | Velg `1`, skriv `Sjekk antenne` | `Opprettet oppdrag 1.` |
| 3 | Velg `2` | `1: Sjekk antenne [Venter]` |
| 4 | Velg `3`, skriv `1` | `Oppdrag 1 fullført.` |
| 5 | Velg `2` | `1: Sjekk antenne [Fullført]` |
| 6 | Velg `3`, skriv `abc` | `ID må være et heltall.` |
| 7 | Velg `3`, skriv `99` | `Fant ikke oppdraget.` |
| 8 | Velg `2` | Oppdrag 1 vises fortsatt som `Fullført` |
| 9 | Velg `9` | `Ukjent menyvalg.` |
| 10 | Velg `0` | Programmet avsluttes |

Kontroller at menyen fortsetter etter ugyldige inndata og ukjente menyvalg.

Legg spesielt merke til at oppdraget du opprettet i steg 2, fortsatt finnes når du viser og fullfører det i senere steg.

Det fungerer fordi kontrolleren bruker den samme `MissionBoard`-instansen gjennom hele programkjøringen.

### 6. Kjør modelltestene på nytt

Kjør:

```sh
dotnet test 11-kontroller/Tests/Tests.csproj
```

Kontroller at alle fire modelltestene fortsatt består.

Du har nå gjennomført to forskjellige typer kontroll:

- **Automatiserte tester:** Kontrollerer at modellen oppfyller kravene.
- **Manuelle scenarioer:** Kontrollerer at brukeren kan utføre handlingene gjennom konsollapplikasjonen.

De manuelle scenarioene blir ikke automatisk testet av `dotnet test` i denne oppgaven.

### 7. Forklar hvordan delene samarbeider

Se på hva som skjer når brukeren fullfører et oppdrag.

Beskriv arbeidsflyten med dine egne ord:

**View → Controller → Model → Controller → View**

Tenk gjennom:

- Hvem leser ID-en fra brukeren?
- Hvem gjør teksten om til et heltall?
- Hvem avgjør om oppdraget finnes?
- Hvem endrer oppdragets tilstand?
- Hvem viser resultatet til brukeren?

Skriv forklaringen i [REFLEKSJON.md](REFLEKSJON.md).

### 8. Dokumenter arbeidet

Skriv i [REFLEKSJON.md](REFLEKSJON.md):

- Hva som skjedde da du prøvde menyvalg 3 før implementasjonen.
- Hvordan du brukte `TryParse` til å håndtere ugyldig input.
- Hva du endret i `CompleteTask()`.
- Resultatene fra de manuelle scenarioene.
- Om modelltestene fortsatt besto.
- Hvordan Model, View og Controller samarbeider.

## Ferdig når

- Menyvalg 1 kan legge til oppdrag.
- Menyvalg 2 viser oppdragene med riktig status.
- Menyvalg 3 kan fullføre et eksisterende oppdrag.
- Ugyldig ID-tekst gir riktig feilmelding uten å avslutte programmet.
- En ID som ikke finnes, gir riktig feilmelding uten å endre oppdragene.
- Ukjente menyvalg håndteres, og menyvalg 0 avslutter programmet.
- Oppdragene beholdes mellom menyvalgene.
- Alle fire eksisterende modelltester består.
- Kontrolleren bruker View til inndata og utskrifter, og Model til å endre oppdrag.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor er det viktig at `Program.cs` oppretter `MissionBoard` bare én gang før menyløkken starter?
2. Hvorfor bruker vi `TryParse` før vi kaller `MissionBoard.Complete()`?
3. Hvorfor skal kontrolleren ikke selv lete gjennom listen med oppdrag og endre `IsCompleted`?
4. Hvorfor kan modelltestene fortsatt være grønne selv om menyvalg 3 ikke fungerer?
5. Hva er ansvaret til Model, View og Controller når brukeren fullfører et oppdrag?

## 