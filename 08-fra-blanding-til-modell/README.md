# 08 · Skill oppskytingsregelen fra konsollen

## Oppdrag

I tidligere oppgaver har du skrevet tester for metoder og brukt dem til å forbedre kode.

Nå skal du undersøke hvordan vi kan organisere en applikasjon slik at **forretningsregler kan testes uten å kjøre hele programmet**.

Kontrollrommet har en konsollapplikasjon som avgjør om en oppskyting kan gjennomføres. Akkurat nå ligger beregningen, brukerinput og utskrifter samlet i `Program.cs`.

Du skal flytte oppskytingsregelen til en egen modell, teste den og la konsollapplikasjonen bruke modellen.

## Dette trenger du å vite

Når vi organiserer en applikasjon, ønsker vi å skille mellom forskjellige ansvarsområder.

I MVC (*Model–View–Controller*) deles ansvaret vanligvis slik:

- **Model (modell):** Inneholder data og regler for hvordan applikasjonen skal fungere.
- **View (visning):** Viser informasjon til brukeren og håndterer det brukeren skriver inn.
- **Controller (kontroller):** Styrer flyten og kobler brukerens handlinger til modellen.

I denne oppgaven skal du ikke bygge en fullstendig MVC-applikasjon. Du skal øve på én viktig del av ansvarsdelingen: **å skille forretningsregler fra konsollkode**.

### Krav til oppskytingen

En oppskyting er tillatt når begge disse kravene er oppfylt:

- Drivstoffet er minst 50 enheter.
- Sikkerhetssjekken er godkjent.

Hvis ett av kravene ikke er oppfylt, skal oppskytingen avvises.

Drivstoff er et heltall fra 0 til 100.

Modellen skal kunne avgjøre om oppskytingen er tillatt, uten å lese fra eller skrive til konsollen.

Vi forutsetter at brukeren skriver inn gyldige tall i denne oppgaven. Håndtering av ugyldig input kommer senere.

## Gjør dette

### 1. Undersøk startkoden

Åpne `Program.cs` i `App`-prosjektet.

Finn ut:

- Hvor leser programmet inn drivstoff og sikkerhetsgodkjenning?
- Hvor avgjør programmet om oppskytingen er tillatt?
- Hvor skriver programmet meldinger til brukeren?

Åpne deretter `LaunchPolicy.cs` i `Core`-prosjektet.

Her finnes allerede metoden `CanLaunch()`, men den inneholder ennå ikke oppskytingsregelen.

Tenk over hvilke oppgaver som hører til modellen, og hvilke som fortsatt bør ligge i konsollapplikasjonen.

Skriv kort om dette i [REFLEKSJON.md](REFLEKSJON.md).

### 2. Kjør testene og undersøk feilen

Åpne `ExerciseTests.cs` i `Tests`-prosjektet.

Det finnes allerede én `[Theory]` med to datarader som tester `LaunchPolicy.CanLaunch()`.

Åpne terminalen og kjør:

```sh
dotnet build 08-fra-blanding-til-modell/Exercise.slnx
dotnet test 08-fra-blanding-til-modell/Tests/Tests.csproj
```

**Ett av de to testtilfellene skal feile.** Det er forventet fordi oppskytingsregelen ennå ikke er implementert i modellen.

Undersøk:

- Hvilket testtilfelle feiler?
- Hva forventer testen?
- Hva returnerer `CanLaunch()` akkurat nå?

Ikke endre modellen ennå.

### 3. Utvid testene

Før du implementerer regelen, skal du legge til flere testtilfeller.

Utvid den eksisterende `[Theory]` med de manglende `[InlineData]`-radene.

| Drivstoff | Sikkerhet godkjent | Forventet resultat | |
|---|---|---|---|
| 50 | `true` | `true` | Finnes allerede |
| 49 | `true` | `false` | Finnes allerede |
| 50 | `false` | `false` | Legg til |
| 100 | `true` | `true` | Legg til |
| 0 | `true` | `false` | Legg til |

Kjør testene på nytt.

Undersøk hvilke testtilfeller som består, og hvilke som fortsatt feiler.

Husk at en test som består, ikke nødvendigvis betyr at hele metoden fungerer riktig.

### 4. Implementer regelen i modellen

Åpne `LaunchPolicy.cs` igjen.

Implementer `CanLaunch()` slik at den følger kravene til oppskyting.

Metoden skal:

- Bruke parameterne `fuel` og `safetyApproved`.
- Returnere `true` når begge kravene er oppfylt.
- Returnere `false` ellers.

**Modellen skal ikke bruke `Console.ReadLine()` eller `Console.WriteLine()`.**

Behold den eksisterende metodesignaturen.

Kjør testene:

```sh
dotnet test 08-fra-blanding-til-modell/Tests/Tests.csproj
```

Kontroller at alle fem testtilfellene består.

### 5. La konsollapplikasjonen bruke modellen

Nå fungerer oppskytingsregelen i `LaunchPolicy`, men `Program.cs` har fortsatt sin egen beregning.

Åpne `Program.cs`.

Finn linjen som avgjør om oppskytingen er tillatt.

Erstatt den eksisterende betingelsen med et kall til `LaunchPolicy.CanLaunch()`.

Bruk verdiene som konsollapplikasjonen allerede har lest inn.

Behold resten av programmet:

- Programmet skal fortsatt spørre etter drivstoff.
- Programmet skal fortsatt spørre om sikkerheten er godkjent.
- Programmet skal fortsatt skrive ut om oppskytingen er tillatt eller avvist.

Målet er at **oppskytingsregelen bare skal finnes ett sted: i modellen**.

### 6. Test konsollapplikasjonen

Kjør først enhetstestene:

```sh
dotnet test 08-fra-blanding-til-modell/Tests/Tests.csproj
```

Kontroller at alle fem testtilfellene består.

Kjør deretter konsollapplikasjonen:

```sh
dotnet run --project 08-fra-blanding-til-modell/App/App.csproj
```

Prøv disse inndataene ved å starte programmet på nytt for hvert tilfelle:

| Drivstoff | Skriv ved sikkerhetsspørsmålet | Forventet melding |
|---|---|---|
| 50 | `ja` | Klar for oppskyting. |
| 49 | `ja` | Oppskyting avvist. |
| 50 | `nei` | Oppskyting avvist. |

Sammenlign resultatene med forventningene.

Nå har du kontrollert både at modellen fungerer gjennom automatiserte tester, og at konsollapplikasjonen bruker regelen riktig i disse tilfellene.

### 7. Dokumenter endringene

Skriv i [REFLEKSJON.md](REFLEKSJON.md):

- Hvilken test feilet i starten, og hvorfor?
- Hvilke nye testtilfeller la du til?
- Hva endret du i `LaunchPolicy.cs`?
- Hva endret du i `Program.cs`?
- Hva viste testene og konsollkjøringene etter endringene?

## Ferdig når

- Du har én `[Theory]` med fem `[InlineData]`-rader som alle består.
- `LaunchPolicy.CanLaunch()` inneholder oppskytingsregelen.
- Modellen kan brukes uten `Console`.
- `Program.cs` kaller `LaunchPolicy.CanLaunch()` i stedet for å gjenta regelen.
- Du har kjørt konsollapplikasjonen og kontrollert de tre oppgitte tilfellene.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor er det enklere å teste `LaunchPolicy.CanLaunch()` når metoden ikke bruker `Console`?
2. Hvorfor bør oppskytingsregelen bare finnes ett sted?
3. Hva er forskjellen på det enhetstestene kontrollerer, og det du kontrollerer når du kjører konsollapplikasjonen?
4. Hvorfor er det ikke nok å lage egne mapper for Model, View og Controller hvis forretningsreglene fortsatt er blandet med resten av koden?

