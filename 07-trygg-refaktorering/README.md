# 07 · Oppskytingspris: forbedre grønn kode

## Oppdrag

I forrige oppgave brukte du Red–Green–Refactor for å utvikle ny funksjonalitet.

Nå skal du konsentrere deg om **Refactor**: å forbedre kode som allerede fungerer, uten å endre resultatene den gir.

Kontrollrommet har en prisberegning som gir riktige resultater, men koden er vanskeligere å lese enn nødvendig.

Du skal gjøre koden tydeligere og enklere, og bruke automatiserte tester til å kontrollere at oppførselen er uendret.

## Dette trenger du å vite

**Refaktorering** betyr å forbedre hvordan koden er skrevet, uten å endre hva den gjør.

Det kan for eksempel være å:

- Gi variabler og parametere tydeligere navn.
- Fjerne unødvendige variabler.
- Unngå å gjenta samme beregning flere steder.

Når vi refaktorerer, bør vi gjøre små endringer og kjøre testene mellom dem. Da blir det enklere å oppdage om en endring har ført til feil.

Husk at grønne tester viser at de testede tilfellene fungerer. De garanterer ikke at alle mulige tilfeller er riktige.

### Krav til prisberegningen

Prisen for en oppskyting beregnes slik:

- Hver person koster 100.
- Prioritert ombordstigning koster 20 ekstra per person.
- Antall personer kan være fra 0 til 20.

Metoden `Calculate` skal fortsatt ta imot et heltall og en boolsk verdi, og returnere et heltall.

Prisregelen og de forventede resultatene skal ikke endres.

## Gjør dette

### 1. Undersøk koden og kjør testene

Åpne `LaunchPrice.cs` i `Core`-prosjektet.

Se på metoden `Calculate`.

Undersøk:

- Hva brukes parameterne `n` og `p` til?
- Hva brukes variabelen `x` til?
- Finnes det beregninger som gjentas?

Ikke endre koden ennå.

Åpne deretter `ExerciseTests.cs` i `Tests`-prosjektet.

Det finnes allerede én `[Theory]` med fem datarader som kontrollerer prisberegningen.

Åpne terminalen og kjør:

```sh
dotnet build 07-trygg-refaktorering/Exercise.slnx
dotnet test 07-trygg-refaktorering/Tests/Tests.csproj
```

Kontroller at alle fem testtilfellene består.

**Dette er utgangspunktet for refaktoreringen: Koden fungerer allerede.**

### 2. Utvid testene før du endrer koden

Før du begynner å forbedre produksjonskoden, skal du kontrollere at testene også dekker det høyeste tillatte antallet personer.

Utvid den eksisterende `[Theory]` med to nye `[InlineData]`-rader.

| Antall personer | Prioritet | Forventet pris | |
|---|---|---|---|
| 0 | `false` | 0 | Finnes allerede |
| 1 | `false` | 100 | Finnes allerede |
| 2 | `false` | 200 | Finnes allerede |
| 1 | `true` | 120 | Finnes allerede |
| 2 | `true` | 240 | Finnes allerede |
| 20 | `false` | 2000 | Legg til |
| 20 | `true` | 2400 | Legg til |

Kjør testene igjen:

```sh
dotnet test 07-trygg-refaktorering/Tests/Tests.csproj
```

Kontroller at alle syv testtilfellene består.

Nå har du flere tester som kan hjelpe deg å oppdage feil under refaktoreringen.

### 3. Forbedre navnene

Åpne `LaunchPrice.cs` igjen.

Se på navnene på parameterne og de lokale variablene.

Endre navnene slik at det blir lettere å forstå hva verdiene betyr.

Ikke endre beregningen ennå.

Behold metodenavnet `Calculate`, parametertypene og returtypen.

Kjør testene:

```sh
dotnet test 07-trygg-refaktorering/Tests/Tests.csproj
```

Kontroller at alle syv testtilfellene fortsatt består.

### 4. Fjern unødvendig og gjentatt kode

Se på beregningen i `Calculate` igjen.

Undersøk:

- Blir den samme beregningen utført flere steder?
- Kan du unngå gjentakelsen?
- Er alle variablene nødvendige?
- Blir koden enklere å forstå etter endringen?

Gjør en liten forbedring om gangen.

Kjør testene etter hver endring.

Målet er ikke å skrive kortest mulig kode, men å gjøre beregningen tydelig og enkel å vedlikeholde.

**Ikke endre prisregelen eller de forventede resultatene.**

### 5. Kontroller at testene oppdager en feil

Nå skal du undersøke om testene reagerer når prisberegningen blir feil.

Endre midlertidig beregningen slik at prioritert ombordstigning koster 21 ekstra per person i stedet for 20.

Kjør testene:

```sh
dotnet test 07-trygg-refaktorering/Tests/Tests.csproj
```

Undersøk resultatet:

- Hvilke testtilfeller feiler?
- Hvilke testtilfeller består fortsatt?
- Hva er forventet og faktisk resultat i en av testene som feiler?

Ikke endre forventningene i testene.

Rett prisberegningen tilbake slik at prioritet igjen koster 20 ekstra per person.

Kjør alle testene på nytt, og kontroller at alle syv består.

### 6. Dokumenter refaktoreringen

Skriv i [REFLEKSJON.md](REFLEKSJON.md) hva du gjorde.

| Steg | Hva skal du dokumentere? |
|---|---|
| Før refaktorering | Hvor mange tester besto? |
| Bedre navn | Hvilke navn endret du, og hvorfor? |
| Enklere kode | Hvilken gjentakelse eller unødvendig kode fjernet du? |
| Midlertidig feil | Hvilke tester feilet da prioritetsprisen ble endret? |
| Etterpå | Hva ble sluttresultatet da du rettet tilbake? |

## Ferdig når

- Den eksisterende `[Theory]` har syv `[InlineData]`-rader, og alle består.
- Du har forbedret navn og gjort beregningen enklere å lese.
- Du har kjørt testene mellom endringene.
- Du har sett at minst én test feiler når prioritetsprisen endres til 21.
- Du har rettet tilbake og kontrollert at alle testene består.
- Du kan forklare hvorfor endringene dine er refaktorering og ikke ny funksjonalitet.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hva ble lettere å forstå etter refaktoreringen?
2. Hvorfor kjørte du testene mellom endringene?
3. Hva er forskjellen på å forbedre beregningen og å endre prisregelen?
4. Kan en grønn testpakke garantere riktig oppførsel for tilfeller testene ikke undersøker? Hvorfor eller hvorfor ikke?

## Hint ved behov

<details>
<summary>Åpne først etter et eget forsøk</summary>

Alle personer betaler en grunnpris på 100 hver, uansett om de velger prioritet.

Se på beregningen i begge grenene av `if`-setningen.

Er det noe som beregnes på samme måte begge steder?

Prøv å gjøre denne delen av beregningen bare én gang.

</details>
