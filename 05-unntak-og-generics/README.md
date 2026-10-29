# 05 · Lastestabel: LIFO og forventede unntak

## Oppdrag

I forrige oppgave testet du hvordan metoder endrer tilstanden til et objekt. Nå skal du undersøke hva som skjer når en operasjon ikke kan gjennomføres.

En lastestabel fungerer etter prinsippet **LIFO (Last In, First Out)**: Det som legges inn sist, tas ut først.

Når du prøver å hente noe fra en tom stabel, skal programmet kaste et bestemt unntak. Startkoden kaster feil type unntak.

Du skal finne og rette feilen, og skrive tester som kontrollerer både rekkefølgen og tilstanden til stabelen.

## Dette trenger du å vite

### Hva er en generisk stabel?

Klassen `CargoStack<T>` er generisk. Det betyr at vi kan bruke samme klasse med forskjellige typer verdier.  
  
For eksempel:

```csharp
var tekstStabel = new CargoStack<string>();
var tallStabel = new CargoStack<int>();
```

- `CargoStack<string>` lagrer tekst, for eksempel `"A"` og `"B"`.
- `CargoStack<int>` lagrer heltall, for eksempel `7` og `12`.

Du skal ikke lage en ny generisk klasse. Den finnes allerede. Du skal bare teste den med to forskjellige typer.

### Hva er et forventet unntak?

Noen ganger skal en metode avvise en operasjon ved å kaste et unntak.

I denne oppgaven skal `Pop()` kaste `InvalidOperationException` når stabelen er tom.

Med xUnit kan du teste dette:

```csharp
Assert.Throws<InvalidOperationException>(() => stack.Pop());
```

Dette betyr: Kjør `Pop()` og kontroller at den kaster akkurat denne typen unntak.

Uttrykket `() => stack.Pop()` gjør at xUnit kan utføre metodekallet og kontrollere unntaket. Du trenger ikke bruke `try/catch` i testen.

### Krav til lastestabelen

- `Push` legger en verdi øverst på stabelen.
- `Pop` fjerner og returnerer verdien som ligger øverst.
- `Count` viser hvor mange verdier stabelen inneholder.
- `Pop` på en tom stabel skal kaste `InvalidOperationException`.
- Når `Pop` avvises fordi stabelen er tom, skal `Count` fortsatt være 0.
- Stabelen skal kunne brukes igjen etter at den er tømt.

## Gjør dette

### 1. Undersøk hvordan stabelen fungerer

Før du kjører testene, se for deg en tom stabel.

Følg disse handlingene på papir:

| Handling | Hva returnerer Pop? | Count etter handlingen |
|---|---|---|
| Ny stabel | – | |
| `Push("A")` | – | |
| `Push("B")` | – | |
| `Pop()` | | |
| `Pop()` | | |

Hvilken verdi må komme ut først dersom stabelen følger LIFO-prinsippet?

Åpne deretter testfilen i `Tests`-prosjektet og undersøk den ferdige testen for tom stabel.

Finn ut:

- Hvor opprettes stabelen?
- Hvordan bruker testen `Assert.Throws`?
- Hvilket unntak forventer testen?

### 2. Bygg og kjør testene

Åpne terminalen o kjør:

```sh
dotnet build 05-unntak-og-generics/Exercise.slnx
dotnet test 05-unntak-og-generics/Tests/Tests.csproj
```

**Én av de ferdige testene skal feile.** Dette er forventet fordi startkoden inneholder en feil.

Undersøk feilmeldingen:

- Hvilken test feiler?
- Hvilken unntakstype forventer testen?
- Hvilken unntakstype blir faktisk kastet?

Ikke endre testen for å godta det uventede unntaket.

### 3. Finn og rett feilen

Åpne `CargoStack<T>` i `Core`-prosjektet.

Undersøk hva `Pop()` gjør når stabelen er tom.

Endre metoden slik at den kontrollerer om stabelen er tom **før den forsøker å hente et element**.

Når stabelen er tom, skal metoden kaste `InvalidOperationException`.

Behold resten av oppførselen uendret.

Kjør testene på nytt:

```sh
dotnet test 05-unntak-og-generics/Tests/Tests.csproj
```

Kontroller at testen som feilet tidligere, nå består.

### 4. Test LIFO med tekst

Lag en ny `[Fact]`-test med `CargoStack<string>`.

Testen skal:

1. Opprette en tom stabel.
2. Legge inn `"A"` og deretter `"B"`.
3. Hente ut den øverste verdien med `Pop()`.
4. Kontrollere at resultatet er `"B"` og at `Count` er 1.
5. Kalle `Pop()` igjen og kontrollere at resultatet er `"A"` og at `Count` er 0.

Bruk Arrange–Act–Assert og `Assert.Equal` slik du har gjort i tidligere oppgaver.

### 5. Test med heltall

Lag en ny `[Fact]`-test med `CargoStack<int>`.

Legg inn tallet `7`, hent det ut igjen med `Pop()`, og kontroller at:

- `Pop()` returnerer `7`.
- `Count` er 0 etterpå.

Du bruker den samme generiske klassen, men med en annen elementtype.

### 6. Test tom stabel og gjenbruk

Utvid testen for tom stabel slik at den også kontrollerer at `Count` fortsatt er 0 etter det forventede unntaket.

Lag deretter en test som viser at stabelen kan brukes igjen etter at den er tømt.

For eksempel:

1. Opprett en ny stabel.
2. Legg inn `"A"` og hent den ut.
3. Kontroller at stabelen er tom.
4. Legg inn `"C"` i den samme stabelen.
5. Kontroller at `Pop()` returnerer `"C"` og at `Count` igjen er 0.

Husk at hver test skal opprette sitt eget stabelobjekt, slik du lærte i oppgave 04.

### 7. Kjør alle testene

Kjør:

```sh
dotnet test 05-unntak-og-generics/Tests/Tests.csproj
```

Kontroller at testene dekker disse tilfellene:

| Handling | Forventet resultat |
|---|---|
| Ny `CargoStack<string>` | `Count == 0` |
| `Push("A")`, `Push("B")` | `Count == 2` |
| Første `Pop()` | `"B"`, `Count == 1` |
| Andre `Pop()` | `"A"`, `Count == 0` |
| `Pop()` på tom stabel | `InvalidOperationException`, `Count == 0` |
| `Push("C")` etter tømming, deretter `Pop()` | `"C"`, `Count == 0` |
| `Push(7)`, `Pop()` med `CargoStack<int>` | `7`, `Count == 0` |

Skriv i [REFLEKSJON.md](REFLEKSJON.md) hva som feilet i starten, hva du endret, og hvordan testresultatene ble etterpå.

## Ferdig når

Du har tester som kontrollerer at:

- `Pop()` på en tom stabel kaster riktig unntak.
- `Count` fortsatt er 0 etter et avvist `Pop()`-kall.
- Verdiene kommer ut i riktig LIFO-rekkefølge.
- `Count` oppdateres riktig når verdier legges inn og tas ut.
- Stabelen fungerer med både `string` og `int`.
- Stabelen kan brukes igjen etter at den er tømt.

Alle testene skal bestå.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor ville det vært feil å endre testen slik at den forventer det utilsiktede unntaket?
2. Hvorfor bruker vi `Assert.Throws` i stedet for `Assert.Equal` når vi tester et forventet unntak?
3. Hva viser testene med `CargoStack<string>` og `CargoStack<int>` om generiske klasser?

