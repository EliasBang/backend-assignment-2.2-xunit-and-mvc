# 04 · Lasterom: uavhengige tester av tilstand

## Oppdrag

I de forrige oppgavene testet du metoder som returnerte et resultat. Nå skal du teste en metode som også endrer tilstanden til et objekt.

Et lasterom kan inneholde maksimalt to enheter. Du skal skrive tester som kontrollerer hva som skjer når du legger inn last, og når lasterommet er fullt.

Du skal også sørge for at **hver test kan kjøres alene**, uten å være avhengig av andre tester.

## Dette trenger du å vite

Når en metode endrer tilstanden til et objekt, er det ikke alltid nok å kontrollere returverdien. Vi må også undersøke hvordan objektet ser ut etter handlingen.

I denne oppgaven bruker du klassen `CargoBay`.

Et lasterom følger disse reglene:

- Et nytt lasterom er tomt (`Count == 0`).
- Det er plass til maksimalt to enheter.
- `TryAdd` returnerer `true` når noe blir lagt inn.
- Hvis lasterommet er fullt, returnerer `TryAdd` `false`, og innholdet skal ikke endres.

Alle navn er gyldige, og samme navn kan legges inn flere ganger.

### Tester skal være uavhengige

Hver test skal opprette sitt eget `CargoBay`-objekt i Arrange.

Hvis du skal teste den tredje innleggingen, må du først fylle lasterommet med to enheter **i den samme testen**.

Ikke la én test være avhengig av at en annen test allerede har kjørt. Testene skal fungere uansett hvilken rekkefølge de kjøres i.

## Gjør dette

### 1. Undersøk og kjør den ferdige testen

Åpne testfilen i `Tests`-prosjektet. Det finnes allerede en test som kontrollerer at et nytt lasterom er tomt.

Finn ut:

- Hvor opprettes `CargoBay`-objektet?
- Hvordan kontrollerer testen at `Count` er 0?

Åpne terminalen og kjør:

```sh
dotnet build 04-uavhengige-tester/Exercise.slnx
dotnet test 04-uavhengige-tester/Tests/Tests.csproj
```

Kontroller at den ferdige testen består.

### 2. Skriv tester for innlegging av last

Lag tre nye `[Fact]`-tester.

Hver test skal starte med et nytt `CargoBay`-objekt.

| Testtilfelle | Forberedelse | Forventet resultat |
|---|---|---|
| Første innlegging | Nytt lasterom | `TryAdd("mat")` gir `true`, `Count` er 1 |
| Andre innlegging | Nytt lasterom med én enhet | `TryAdd("vann")` gir `true`, `Count` er 2 |
| Tredje innlegging | Nytt lasterom med to enheter | `TryAdd("verktøy")` gir `false`, `Count` er fortsatt 2 |

Bruk Arrange–Act–Assert som i de tidligere oppgavene.

Husk at du selv må bygge opp starttilstanden i hver test. For eksempel må testen for andre innlegging først legge inn én enhet.

I testen for tredje innlegging skal du kontrollere **både returverdien og `Count`**.

Kjør testene, og kontroller at alle består.

### 3. Test to forskjellige lasterom

Lag en ekstra `[Fact]`-test.

I denne testen skal du:

1. Opprette to forskjellige `CargoBay`-objekter.
2. Legge én enhet i det første lasterommet.
3. Kontrollere at det første har `Count == 1`.
4. Kontrollere at det andre fortsatt har `Count == 0`.

Hvorfor er det viktig at det andre lasterommet fortsatt er tomt?

### 4. Kjør en test alene

Til nå har du kjørt alle testene samtidig. Nå skal du kontrollere at en test også fungerer alene.

Finn først navnene på testene:

```sh
dotnet test 04-uavhengige-tester/Tests/Tests.csproj --list-tests
```

Kjør deretter én bestemt test:

```sh
dotnet test 04-uavhengige-tester/Tests/Tests.csproj --filter "FullyQualifiedName~NavnPaaTest"
```

Bytt ut `NavnPaaTest` med navnet på en av testmetodene dine.

Kontroller at bare den valgte testen ble kjørt, og at den består.

Kjør deretter alle testene igjen:

```sh
dotnet test 04-uavhengige-tester/Tests/Tests.csproj
```

Kontroller at alle fem testene består.

### 5. Dokumenter det du har gjort

Skriv i [REFLEKSJON.md](REFLEKSJON.md) hvilke tester du laget, hva de kontrollerer, og hva som skjedde da du kjørte én test alene.

## Ferdig når

Du har minst fem `[Fact]`-tester som består.

Testene skal kontrollere at:

- Et nytt lasterom er tomt.
- Første og andre innlegging lykkes.
- Tredje innlegging avvises uten at `Count` endres.
- To forskjellige lasterom har uavhengig tilstand.

Du har også kjørt minst én test alene med `--filter` og kontrollert at den består.

## Refleksjon

Svar på spørsmålene i [REFLEKSJON.md](REFLEKSJON.md):

1. Hvorfor må testen for tredje innlegging selv legge inn de to første enhetene?
2. Hvorfor er det viktig å kontrollere både returverdien fra `TryAdd` og verdien av `Count`?
3. Hva kan gå galt hvis alle testene deler samme `static CargoBay`-objekt?