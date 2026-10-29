# 06 · Oppdragskort: første Red–Green–Refactor

## Oppdrag

I tidligere oppgaver har du skrevet tester for kode som allerede finnes. Nå skal du prøve en annen arbeidsmåte: **Skriv testen før du implementerer funksjonaliteten.**

Et oppdragskort skal kunne markeres som fullført. Klassen `MissionTask` finnes allerede, men metoden `Complete()` gjør foreløpig ingenting.

Du skal bruke **testdrevet utvikling (TDD)** for å implementere fullføringen.

## Dette trenger du å vite

TDD står for *Test-Driven Development*, eller testdrevet utvikling.

I TDD jobber vi i tre steg:

- **Red (rød):** Skriv en test for det programmet skal gjøre. Kjør testen og se at den feiler fordi funksjonaliteten mangler.
- **Green (grønn):** Skriv akkurat nok kode til at testen består.
- **Refactor (forbedre):** Vurder om koden kan gjøres enklere eller tydeligere, uten å endre hva den gjør. Testene skal fortsatt bestå.

<escape>Vi kan gjenta disse stegene hver gang vi skal legge til ny funksjonalitet.</escape>

### Hva skal oppdragskortet gjøre?

Klassen `MissionTask` representerer et oppdragskort.

Den skal følge disse reglene:

- Et nytt oppdragskort har en `Id` og en `Title`.
- Et nytt oppdragskort starter med `IsCompleted == false`.
- Når `Complete()` kalles, skal `IsCompleted` bli `true`.
- Hvis `Complete()` kalles flere ganger, skal oppdraget fortsatt være fullført.
- `Id` og `Title` skal ikke endres når oppdraget fullføres.

## Gjør dette

### 1. Undersøk startkoden

Åpne `MissionTask.cs` i `Core`-prosjektet.

Finn ut:

- Hvilke egenskaper har `MissionTask`?
- Hva gjør konstruktøren?
- Hva gjør `Complete()` akkurat nå?

Åpne deretter `ExerciseTests.cs` i `Tests`-prosjektet.

Det finnes allerede én ferdig test som kontrollerer at et nytt oppdragskort får riktig starttilstand.

Åpne terminalen og kjør:

```sh
dotnet build 06-foerste-tdd/Exercise.slnx
dotnet test 06-foerste-tdd/Tests/Tests.csproj
```

Kontroller at den ferdige testen består.

### 2. Red – skriv en test som skal feile

Nå skal du skrive en test for funksjonalitet som ikke er implementert ennå.

Lag en ny `[Fact]`-test som:

1. Oppretter et nytt `MissionTask`-objekt.
2. Kaller `Complete()`.
3. Kontrollerer at `IsCompleted` er `true`.

Bruk Arrange–Act–Assert som du har gjort i tidligere oppgaver.

**Ikke endre `Complete()` ennå.**

Kjør testene:

```sh
dotnet test 06-foerste-tdd/Tests/Tests.csproj
```

Den nye testen skal feile.

Undersøk feilmeldingen:

- Hvilken test feilet?
- Hva forventet testen?
- Hva var det faktiske resultatet?
- Hvorfor feilet testen?

**Dette er Red-fasen.** Testen viser at funksjonaliteten vi ønsker, ennå ikke fungerer.

En viktig forskjell: Testen skal kunne kjøre og feile fordi resultatet er feil. Hvis koden ikke kompilerer, må du først rette kompileringsfeilen.

Skriv ned resultatet i [REFLEKSJON.md](REFLEKSJON.md) før du fortsetter.

### 3. Green – få testen til å bestå

Åpne `MissionTask.cs` igjen.

Implementer `Complete()` slik at den oppfyller kravet fra testen.

Skriv bare den koden som er nødvendig for å få oppdraget til å bli fullført.

Ikke endre testen eller den forventede verdien for å få et grønt resultat.

Kjør testene igjen:

```sh
dotnet test 06-foerste-tdd/Tests/Tests.csproj
```

Kontroller at både konstruktørtesten og den nye fullføringstesten består.

**Dette er Green-fasen.** Du har implementert funksjonalitet som først ble beskrevet gjennom en test.

### 4. Test gjentatt fullføring

Lag en ny `[Fact]`-test.

Testen skal:

1. Opprette et nytt oppdragskort med `Id` 7 og tittelen `"Sjekk antenne"`.
2. Kalle `Complete()` to ganger.
3. Kontrollere at `IsCompleted` fortsatt er `true`.
4. Kontrollere at `Id` fortsatt er 7, og at `Title` fortsatt er `"Sjekk antenne"`.

Kjør testene.

Denne nye testen kan allerede bestå med implementasjonen du laget i Green-fasen. Det er helt greit.

**Du trenger ikke lage en kunstig feil for å få testen til å bli rød.**

Testen skal kontrollere at oppdragskortet også fungerer riktig når `Complete()` kalles flere ganger.

### 5. Refactor – vurder om koden kan forbedres

Se på implementasjonen av `Complete()` og testene dine.

Spør deg selv:

- Er koden enkel å lese?
- Har testene beskrivende navn?
- Finnes det unødvendig kode?
- Kan noe gjøres tydeligere uten å endre oppførselen?

Hvis du finner en forbedring, gjør den og kjør testene på nytt.

Hvis koden allerede er enkel og tydelig, trenger du ikke endre noe.

**Refaktorering betyr å forbedre koden uten å endre hva den gjør.**

### 6. Dokumenter TDD-syklusen

Skriv i [REFLEKSJON.md](REFLEKSJON.md) hva som skjedde i de forskjellige fasene:

| Fase | Hva skal du dokumentere? |
|---|---|
| Red | Hvilken test skrev du, og hvorfor feilet den? |
| Green | Hva endret du i produksjonskoden, og hva ble testresultatet? |
| Refactor | Forbedret du noe? Hvis ikke, hvorfor var det unødvendig? |

Beskriv også resultatet av testen med to `Complete()`-kall.

## Ferdig når

Du har tre `[Fact]`-tester som består:

- En test for oppdragskortets starttilstand.
- En test for fullføring.
- En test for gjentatt fullføring.

Du har dokumentert at fullføringstesten først var rød, og deretter ble grønn etter at du implementerte `Complete()`.

Du har også vurdert om koden trenger refaktorering.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor skrev du fullføringstesten før du implementerte `Complete()`?
2. Hvorfor var det positivt at testen først feilet?
3. Hvorfor er det å implementere `Complete()` en funksjonsendring og ikke refaktorering?
4. Hvorfor trenger du ikke nødvendigvis endre koden i Refactor-fasen?

## Hint ved behov

<details>
<summary>Åpne først etter et eget forsøk</summary>

**Red:** Opprett et `MissionTask`-objekt i Arrange, kall `Complete()` i Act, og kontroller `IsCompleted` i Assert.

**Green:** Se på egenskapen `IsCompleted` i `MissionTask.cs`. Hvordan kan metoden `Complete()` endre verdien?

**Refactor:** Hvis implementasjonen er kort og tydelig, er det ingen grunn til å gjøre den mer komplisert.

</details>
