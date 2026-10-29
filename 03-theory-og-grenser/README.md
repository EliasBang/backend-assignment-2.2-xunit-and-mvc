# 03 · Setereservasjon: Theory og grensetilfeller

## Oppdrag

I forrige oppgave skrev du flere `[Fact]`-tester for å kontrollere ulike tilfeller. Nå skal du lære å teste flere tilfeller med én testmetode.

En shuttle avviser en setereservasjon som egentlig burde vært godkjent. Du skal bruke tester til å finne feilen og deretter rette den.

## Dette trenger du å vite

Med `[Fact]` tester du vanligvis ett bestemt tilfelle. Med `[Theory]` kan du kjøre samme testmetode flere ganger med forskjellige inndata.

Hver `[InlineData]` inneholder verdiene for ett testtilfelle.

For eksempel:

```csharp
[InlineData(4, 4, true)]
```

Denne raden betyr:

- **4:** Antall seter som ønskes reservert.
- **4:** Antall ledige seter.
- **true:** Forventet resultat. Reservasjonen skal godkjennes.

Verdiene sendes til testmetodens parametere i samme rekkefølge.

Hver datarad blir kontrollert som et eget testtilfelle.

Du kan fortsatt bruke Arrange–Act–Assert fra forrige oppgave. Med `Assert.Equal(expected, actual)` sjekker du om resultatet stemmer med forventningen.

### Kravet til setereservasjonen

En reservasjon skal godkjennes når:

- Antall ønskede seter er større enn 0.
- Antall ønskede seter ikke er større enn antall ledige seter.

Hvis ett av kravene ikke er oppfylt, skal reservasjonen avvises.

Antall ledige seter er aldri negativt i denne oppgaven.

## Gjør dette

### 1. Undersøk den ferdige testen

Åpne testfilen i `Tests`-prosjektet. Den inneholder allerede en `[Theory]` med tre datarader.

**Tenk på:**

1. Hva er hensikten med `[Theory]` og `[InlineData]` i xUnit, og hvorfor brukes de sammen?
2. Hvordan kobles verdiene i `[InlineData]` til parameterne i testmetoden, og hvor mange testtilfeller kjøres?
3. Hva kontrollerer `Assert.Equal(expected, actual)`, og hva skjer dersom verdiene er forskjellige?

### 2. Bygg og kjør testene

Åpne terminalen og kjør:

```sh
dotnet build 03-theory-og-grenser/Exercise.slnx
dotnet test 03-theory-og-grenser/Tests/Tests.csproj
```

**Ett av de tre testtilfellene skal feile.** Dette er forventet. Startkoden inneholder en feil som du skal undersøke.

Se på feilmeldingen i terminalen.

- Hvilket testtilfelle feiler?
- Hva er forventet resultat, og hva er faktisk resultat?
- Hvorfor burde dette tilfellet vært godkjent eller avvist?

Ikke rett produksjonskoden ennå.

### 3. Legg til flere testtilfeller

Utvid den eksisterende `[Theory]`-testen med de manglende dataradene fra tabellen.

| Ønskede seter | Ledige seter | Forventet resultat |
|---|---|---|
| 1 | 4 | `true` |
| 3 | 4 | `true` |
| 4 | 4 | `true` |
| 5 | 4 | `false` |
| 0 | 4 | `false` |
| -1 | 4 | `false` |
| 1 | 0 | `false` |

Kontroller at du har totalt syv `[InlineData]`-rader.

Kjør testene på nytt. Undersøk hvilke tilfeller som består, og hvilke som feiler.

### 4. Finn og rett feilen

Åpne produksjonskoden i `Core`.

Undersøk betingelsen som avgjør om en reservasjon er tillatt.

Se spesielt på disse tre tilfellene:

- 3 ønskede seter, 4 ledige seter.
- 4 ønskede seter, 4 ledige seter.
- 5 ønskede seter, 4 ledige seter.

Hva er forskjellen mellom dem?

Rett betingelsen slik at den følger kravet til setereservasjonen.

Ikke endre de forventede resultatene i testene.

Kjør testene igjen:

```sh
dotnet test 03-theory-og-grenser/Tests/Tests.csproj
```

Kontroller at alle syv testtilfellene består.

### 5. Dokumenter det du har gjort

Skriv i [REFLEKSJON.md](REFLEKSJON.md) hvilket testtilfelle som feilet først, hva som var feil i produksjonskoden, og hva som skjedde etter at du rettet den.

## Ferdig når

Du har én `[Theory]` med minst syv `[InlineData]`-rader, alle testtilfellene består, og du kan forklare hvorfor en reservasjon med like mange ønskede og ledige seter skal godkjennes.
## 