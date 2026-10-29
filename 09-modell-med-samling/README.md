# 09 · Oppdragstavlen: test modellen før menyen

## Oppdrag

I forrige oppgave skilte du en forretningsregel fra konsollapplikasjonen slik at den kunne testes direkte.

Nå skal du arbeide med en modell som håndterer flere oppdrag.

En oppdragstavle skal kunne legge til oppdrag, gi dem egne ID-er og markere bestemte oppdrag som fullført.

Du skal **skrive tester og implementere fullføring i modellen**, uten å lage en konsollmeny.

## Dette trenger du å vite

Når et program håndterer flere objekter, kan vi bruke en samling til å holde oversikt over dem.

I denne oppgaven har vi to klasser:

- **`MissionTask`** representerer ett oppdrag. Det har en ID, en tittel og informasjon om det er fullført.
- **`MissionBoard`** håndterer en liste med oppdrag. Tavlen kan legge til oppdrag, hente listen og fullføre et bestemt oppdrag.

`MissionBoard` gir hvert nytt oppdrag en ID, med start på 1.

En **ID er ikke det samme som en listeindeks**. ID-en brukes til å identifisere oppdraget, uavhengig av hvor det ligger i listen.

### Hva betyr det å returnere en kopi av listen?

Metoden `GetAll()` returnerer en ny liste med oppdragene.

Det betyr at du kan tømme den returnerte listen med `Clear()` uten å tømme listen som tavlen bruker internt.

Men selve `MissionTask`-objektene er ikke kopiert. Begge listene inneholder referanser til de samme oppdragene.

Med xUnit kan vi bruke `Assert.Same(expected, actual)` til å kontrollere at to referanser peker til **nøyaktig samme objekt**.

### Krav til oppdragstavlen

- En ny tavle skal være tom.
- `Add(title)` skal legge til et nytt oppdrag og gi det neste tilgjengelige ID.
- Nye oppdrag skal starte som ufullførte.
- `Complete(id)` skal fullføre oppdraget med riktig ID og returnere `true`.
- Hvis oppdraget allerede er fullført, skal `Complete(id)` fortsatt returnere `true`.
- Hvis ID-en ikke finnes, skal `Complete(id)` returnere `false` uten å endre oppdragene.
- `GetAll()` skal returnere en ny liste med referanser til de samme oppdragsobjektene.
- To forskjellige tavler skal ha uavhengig tilstand.

Vi forutsetter at titlene er gyldige og ikke tomme. Validering av titler kommer ikke i denne oppgaven.

## Gjør dette

### 1. Undersøk startkoden

Åpne `MissionBoard.cs` og `MissionTask.cs` i `Core`-prosjektet.

Undersøk:

- Hvilken klasse inneholder listen med oppdrag?
- Hva skjer når `MissionBoard.Add()` kalles?
- Hvordan får nye oppdrag en ID?
- Hva gjør `MissionTask.Complete()`?
- Hva gjør `MissionBoard.Complete()` akkurat nå?

Legg merke til at `MissionTask` allerede kan fullføre seg selv, mens `MissionBoard` ennå ikke kan fullføre et oppdrag ved hjelp av ID.

### 2. Kjør de eksisterende testene

Åpne `ExerciseTests.cs` i `Tests`-prosjektet.

Det finnes allerede fire `[Fact]`-tester som kontrollerer forskjellige deler av oppdragstavlen.

Åpne terminalen og kjør:

```sh
dotnet build 09-modell-med-samling/Exercise.slnx
dotnet test 09-modell-med-samling/Tests/Tests.csproj
```

**Én av de fire testene skal feile.** Dette er forventet fordi `MissionBoard.Complete()` ennå ikke er implementert.

Undersøk:

- Hvilken test feiler?
- Hva forventer testen?
- Hva returnerer `Complete()` akkurat nå?
- Hvorfor består testen med en ukjent ID?

Ikke endre produksjonskoden ennå.

### 3. Skriv flere tester

Før du implementerer `Complete()`, skal du utvide testene.

Du har allerede fire tester. Nå skal du skrive fire nye `[Fact]`-tester.

**Test A: En ny tavle er tom**

Opprett et nytt `MissionBoard`-objekt.

Kontroller at:

- `Count` er 0.
- Listen fra `GetAll()` er tom.

**Test B: Samme oppdrag kan fullføres to ganger**

Opprett en tavle og legg til ett oppdrag.

Kall `Complete()` med oppdragets ID to ganger.

Kontroller at:

- Begge kallene returnerer `true`.
- Oppdraget fortsatt er fullført.
- Tavlen fortsatt inneholder ett oppdrag.

**Test C: To tavler er uavhengige**

Opprett to forskjellige `MissionBoard`-objekter.

Legg til et oppdrag i den første tavlen.

Kontroller at:

- Den første tavlen har ett oppdrag.
- Den andre tavlen fortsatt er tom.
- Når du legger til det første oppdraget i den andre tavlen, får det ID 1.

**Test D: Listekopien inneholder de samme objektene**

Opprett en tavle og legg til ett oppdrag.

Hent deretter listen med `GetAll()`.

Bruk `Assert.Same()` til å kontrollere at oppdraget du la til, er det samme objektet som ligger i den returnerte listen.

Dette viser at `GetAll()` kopierer listen, men ikke selve oppdragsobjektene.

Kjør testene på nytt:

```sh
dotnet test 09-modell-med-samling/Tests/Tests.csproj
```

Noen av de nye testene kan allerede bestå. Andre vil fortsatt feile fordi `MissionBoard.Complete()` ikke er implementert.

Det er helt greit. Du trenger ikke lage kunstige feil for å få testene til å bli røde.

### 4. Implementer fullføring i modellen

Åpne `MissionBoard.cs` igjen.

Nå skal du implementere `Complete(int id)`.

Metoden skal:

1. Finne oppdraget som har den oppgitte ID-en.
2. Bruke oppdragets eksisterende `Complete()`-metode til å fullføre det.
3. Returnere `true` hvis oppdraget finnes.
4. Returnere `false` hvis ingen oppdrag har denne ID-en.

Husk at en ID ikke er det samme som en listeindeks.

**Ikke lag en ny fullføringsregel i `MissionBoard`.** Bruk funksjonaliteten som allerede finnes i `MissionTask`.

Ikke endre forventningene i testene.

### 5. Kjør alle testene

Kjør:

```sh
dotnet test 09-modell-med-samling/Tests/Tests.csproj
```

Kontroller at alle åtte testene består.

Testene skal nå dekke følgende oppførsel:

| Handling | Forventet resultat |
|---|---|
| Opprett en ny tavle | `Count == 0`, `GetAll()` er tom |
| Legg til to oppdrag | ID 1 og 2, begge ufullførte |
| Fullfør ID 2 | `true`, bare oppdrag 2 er fullført |
| Fullfør ukjent ID 99 | `false`, ingen oppdrag endres |
| Fullfør samme ID to ganger | Begge kall gir `true`, oppdraget er fortsatt fullført |
| Tøm listen fra `GetAll()` | Tavlens `Count` er uendret |
| Opprett to forskjellige tavler | De har uavhengig tilstand og ID-telling |
| Sammenlign oppdraget fra `Add()` med objektet fra `GetAll()` | `Assert.Same()` består |

Hvis en test feiler, undersøk først hva den forventer og hva som faktisk skjer.

Ikke endre testen bare for å få et grønt resultat.

### 6. Dokumenter arbeidet

Skriv i [REFLEKSJON.md](REFLEKSJON.md):

- Hvilken test feilet i starten, og hvorfor?
- Hvilke fire nye tester skrev du?
- Hva endret du i `MissionBoard.Complete()`?
- Hvor mange tester besto etter implementasjonen?
- Hva viste testen med `Assert.Same()`?

## Ferdig når

- Du har minst åtte `[Fact]`-tester som består.
- `MissionBoard.Complete()` fullfører riktig oppdrag ved hjelp av ID.
- Ukjente ID-er avvises uten å endre tilstanden.
- Gjentatt fullføring fungerer.
- To forskjellige tavler har uavhengig tilstand.
- Du har testet både at `GetAll()` returnerer en ny liste, og at objektene i listen fortsatt er de samme.
- Modellen fungerer uten konsollinput eller en meny.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor bør `MissionBoard` bruke `MissionTask.Complete()` i stedet for å lage sin egen fullføringsregel?
2. Hva beskytter listekopien fra `GetAll()` mot, og hva beskytter den ikke mot?
3. Hva er forskjellen mellom å sammenligne verdier med `Assert.Equal()` og å kontrollere referanseidentitet med `Assert.Same()`?
4. Hvorfor bør hver test opprette sin egen `MissionBoard`?

## Hint ved behov

<details>
<summary>Åpne først etter et eget forsøk</summary>

**For `MissionBoard.Complete()`:**

Gå gjennom oppdragene i listen.

Sammenlign `task.Id` med ID-en du leter etter.

Når du finner riktig oppdrag, kan du kalle oppdragets egen `Complete()`-metode.

Hva skal metoden returnere hvis den ikke finner noen oppdrag med riktig ID?

**For testen med `Assert.Same()`:**

Ta vare på objektet som returneres fra `Add()`.

Hent deretter listen med `GetAll()`.

Bruk `Assert.Same()` til å sammenligne det opprinnelige objektet med objektet du finner i listen.

</details>

