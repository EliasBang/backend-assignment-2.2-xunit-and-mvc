# 10 · Visningen: vis data uten å endre dem

## Oppdrag

I forrige oppgave testet du en modell som håndterer flere oppdrag. Nå skal du vise informasjonen fra modellen til brukeren.

En oppdragstavle skal kunne vise hvilke oppdrag som finnes, og om de venter eller er fullført.

Du skal implementere `MissionView.ShowTasks()` slik at den presenterer oppdragene i konsollen, **uten å endre dataene i modellen**.

## Dette trenger du å vite

I MVC (*Model–View–Controller*) har modellen og visningen forskjellige ansvar.

- **Model:** Håndterer data og regler. For eksempel å legge til eller fullføre et oppdrag.
- **View:** Bestemmer hvordan informasjonen skal vises til brukeren.

I denne oppgaven skal du arbeide med View.

Klassen `MissionView` får en liste med oppdrag som argument. Den skal bruke informasjonen i listen til å skrive ut tekst.

For eksempel skal et oppdrag som ikke er fullført, vises med teksten `Venter`, mens et fullført oppdrag skal vises med teksten `Fullført`.

Dette er **presentasjonslogikk**: Vi bestemmer hvordan eksisterende informasjon skal vises.

Å fullføre et oppdrag med `Complete()` er derimot en endring av modellen, og skal ikke skje i visningen.

### Krav til visningen

Metoden `ShowTasks()` skal følge disse reglene:

- Hvis listen er tom, skal den skrive `Ingen oppdrag.`
- Hvis listen inneholder oppdrag, skal hvert oppdrag vises på sin egen linje.
- Hver linje skal inneholde oppdragets ID, tittel og status.
- Status skal være `Venter` eller `Fullført`, avhengig av `IsCompleted`.
- Visningen skal ikke legge til, fjerne eller endre oppdrag.
- Hvis de samme oppdragene vises på nytt uten endringer i modellen, skal teksten være den samme.

Du kan bruke **strenginterpolasjon** til å sette verdier inn i tekst, for eksempel:

```csharp
$"{task.Id}: {task.Title}"
```

## Gjør dette

### 1. Undersøk hvordan programmet er satt sammen

Åpne `Program.cs` og `MissionView.cs` i `App`-prosjektet.

Undersøk:

- Hvor opprettes `MissionBoard`?
- Hvordan legges oppdrag til i tavlen?
- Hvordan sendes oppdragene til `ShowTasks()`?
- Hva gjør `ShowTasks()` akkurat nå?

Legg merke til at `ShowTasks()` får en liste med oppdrag som argument.

Den trenger derfor ikke opprette sin egen `MissionBoard` eller hente oppdragene selv.

De andre metodene i `MissionView`, som `ShowMenu()` og `ReadInput()`, er allerede ferdige. Du skal ikke endre dem i denne oppgaven.

### 2. Kjør testene og demonstrasjonsappen

Åpne terminalen og kjør:

```sh
dotnet build 10-visning/Exercise.slnx
dotnet test 10-visning/Tests/Tests.csproj
```

Kontroller at alle fire modelltestene består.

Kjør deretter konsollapplikasjonen:

```sh
dotnet run --project 10-visning/App/App.csproj
```

Programmet forsøker allerede å vise tre situasjoner:

- **Demo 1:** En tom tavle.
- **Demo 2:** En tavle med to oppdrag.
- **Demo 3:** De samme oppdragene vist på nytt.

Undersøk hva som vises i terminalen.

Du vil se at programmet skriver ut overskriftene til demonstrasjonene, men ennå ikke viser selve oppdragslisten.

**Hvorfor består modelltestene selv om visningen ikke er ferdig?**

Testene i `Tests` kontrollerer modellen, ikke teksten som `MissionView` skriver til konsollen.

### 3. Implementer visningen av en tom liste

Åpne `MissionView.cs`.

Start med å implementere den delen av `ShowTasks()` som håndterer en tom liste.

Når `tasks` er tom, skal metoden skrive:

```text
Ingen oppdrag.
```

Bruk `Console.WriteLine()` til å skrive teksten.

Sørg for at metoden ikke forsøker å vise oppdrag når listen er tom.

Kjør konsollapplikasjonen igjen, og kontroller at Demo 1 nå viser riktig melding.

### 4. Implementer visningen av oppdrag

Utvid `ShowTasks()` slik at den også kan vise en liste som inneholder oppdrag.

Gå gjennom oppdragene i listen med en løkke.

For hvert oppdrag skal du:

1. Hente oppdragets `Id` og `Title`.
2. Undersøke verdien av `IsCompleted`.
3. Velge teksten `Venter` eller `Fullført`.
4. Skrive ut oppdraget på én linje.

Formatet skal være:

```text
1: Sjekk antenne [Venter]
2: Lad rover [Fullført]
```

Bruk gjerne strenginterpolasjon når du setter sammen teksten.

**Husk:** `ShowTasks()` skal bare lese informasjonen. Ikke kall `Complete()`, og ikke endre listen eller oppdragsobjektene.

### 5. Kjør demonstrasjonsappen og sammenlign

Kjør:

```sh
dotnet run --project 10-visning/App/App.csproj
```

Når `ShowTasks()` er ferdig implementert, skal programmet skrive:

```text
Demo 1: tom tavle
Ingen oppdrag.
Demo 2: to oppdrag
1: Sjekk antenne [Venter]
2: Lad rover [Fullført]
Demo 3: vis igjen
1: Sjekk antenne [Venter]
2: Lad rover [Fullført]
Antall fortsatt: 2; rover fullført: True
```

Sammenlign utskriften fra programmet med eksemplet.

Kontroller spesielt:

- At den tomme tavlen viser riktig melding.
- At begge oppdragene vises i riktig rekkefølge.
- At oppdragene får riktig status.
- At Demo 2 og Demo 3 viser de samme opplysningene.

### 6. Kontroller at visningen ikke endrer modellen

Se på resultatet fra Demo 3 og den siste linjen i utskriften.

Undersøk:

- Er antall oppdrag fortsatt 2?
- Er oppdraget «Lad rover» fortsatt fullført?
- Vises «Sjekk antenne» fortsatt som `Venter`?
- Inneholder `ShowTasks()` bare kode som leser informasjon og skriver ut tekst?

Visningen skal ikke endre dataene den får.

Kjør deretter modelltestene på nytt:

```sh
dotnet test 10-visning/Tests/Tests.csproj
```

Kontroller at alle fire fortsatt består.

Husk at modelltestene ikke automatisk kontrollerer konsollutskriften. Derfor har du også undersøkt visningen ved å kjøre programmet selv.

### 7. Dokumenter arbeidet

Skriv i [REFLEKSJON.md](REFLEKSJON.md):

- Hva som manglet i konsollutskriften før du implementerte `ShowTasks()`.
- Hvordan du håndterte en tom liste.
- Hvordan du valgte mellom `Venter` og `Fullført`.
- Om de tre demonstrasjonene ga forventet resultat.
- Hvordan du kontrollerte at visningen ikke endret oppdragene.

## Ferdig når

- `ShowTasks()` viser `Ingen oppdrag.` når listen er tom.
- Oppdragene vises med riktig ID, tittel og status.
- Demo 2 og Demo 3 viser de samme opplysningene.
- Visningen endrer ikke listen eller oppdragsobjektene.
- Alle fire eksisterende modelltester består.
- Du har kjørt konsollapplikasjonen og sammenlignet utskriften med forventningene.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor kan alle modelltestene bestå selv om konsollvisningen er feil?
2. Hvorfor skal `ShowTasks()` bare lese oppdragenes tilstand og ikke endre den?
3. Hvorfor får `ShowTasks()` en liste som argument, i stedet for å opprette sin egen `MissionBoard`?
4. Hvorfor hører valget mellom tekstene `Venter` og `Fullført` hjemme i View, mens selve fullføringen hører hjemme i modellen?

