# 02 · Første xUnit-test og Arrange–Act–Assert

## Oppdrag

I forrige oppgave kjørte du programmet og kontrollerte resultatene selv. Nå skal du skrive tester som gjør denne kontrollen automatisk.
Du skal lære å bruke **xUnit** til å teste forsyningsberegningen, og se hvordan en test oppdager at noe er feil.

## Dette trenger du å vite

En **enhetstest** er kode som kontrollerer at en liten del av programmet fungerer som forventet.
Vi bruker **xUnit** til å kjøre testene. En testmetode merket med `[Fact]` blir gjenkjent som en test av xUnit.

En test består vanligvis av tre deler:

- Arrange (forbered): Gjør klar verdiene du skal teste med.
- Act (utfør): Kall metoden du vil teste.
- Assert (kontroller): Sjekk at resultatet er som forventet.

Med `Assert.Equal(forventet, faktisk)` kan du sammenligne to verdier. Hvis verdiene er forskjellige, rapporterer **xUnit** at testen feilet.

Koden som skal testes, ligger i `Core`. Testene ligger i `Tests`. Prosjektene er allerede koblet sammen.


## Gjør dette

### 1. Undersøk den ferdige testen

Åpne testfilen i `Tests`-prosjektet. Det finnes allerede én ferdig test.

Finn ut:

- Hvor brukes `[Fact]`?
- Hvilke deler av testen er Arrange, Act og Assert?
- Hva er det forventede resultatet?

### 2. Bygg og kjør testen

Åpne terminalen og kjør:

```sh
dotnet build 02-foerste-fact/Exercise.slnx
dotnet test 02-foerste-fact/Tests/Tests.csproj
```

Se på resultatet i terminalen. Hvor mange tester ble kjørt, og besto de?

### 3. Skriv dine første tester

Den ferdige testen kontrollerer allerede ett tilfelle. Nå skal du legge til to nye tester.

| Inndata           | Forventet resultat |                 |
| ----------------- | ------------------ | --------------- |
| `Calculate(3, 4)` | 24                 | Finnes allerede |
| `Calculate(0, 4)` | 0                  | Lag en ny test  |
| `Calculate(1, 1)` | 2                  | Lag en ny test  |

Bruk `[Fact]` og følg samme `Arrange–Act–Assert`-struktur som i den ferdige testen. Husk at `Assert.Equal` skal få den forventede verdien først og den faktiske verdien etterpå.

Gi testene navn som beskriver hva de kontrollerer.

Kjør testene på nytt med `dotnet test 02-foerste-fact/Tests/Tests.csproj`. Kontroller at alle tre består.

### 4. Se om testene oppdager en feil

Åpne produksjonskoden i `Core` og endre midlertidig multiplikatoren fra 2 til 3.

Ikke endre forventningene i testene.

Kjør testene på nytt.

- Hvilke tester feiler?
- Hvilke tester består fortsatt?
- Hva forteller feilmeldingen om forventet og faktisk resultat?

Endre multiplikatoren tilbake til 2, og kjør testene igjen. Kontroller at alle består.

### 5. Dokumenter det du har gjort

Skriv i [REFLEKSJON.md](REFLEKSJON.MD) hvilke tester du laget, hva som skjedde da du innførte feilen, og hva som skjedde etter at du rettet den.

## Ferdig når

Du har tre `[Fact]`-tester som består, og du har sett at minst én av dem feiler når multiplikatoren er feil. 

