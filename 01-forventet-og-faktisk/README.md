# 01 · Forsyninger: fra krav til testeksempler

## Oppdrag

Romstasjonen trenger pålitelige forsyningsberegninger. Lag forventninger før du kjører programmet.

## Dette trenger du å vite

Et krav beskriver hva programmet skal gjøre. Når vi skal undersøke om programmet oppfyller kravet, må vi først bestemme hvilket resultat vi forventer for bestemte inndata. Deretter kan vi kjøre programmet og sammenligne forventningen med det faktiske resultatet.


Hvert besetningsmedlem trenger 2 forsyningsenheter per dag. Programmet skal kunne beregne forsyninger for 0–20 personer og 0–30 dager. Du trenger ikke teste tall utenfor disse områdene.

Det er viktig å teste tall helt på grensen av det som er tillatt, for eksempel 0 personer eller 30 dager. Disse kalles **grenseverdier**.

## Gjør dette

### 1. Finn forventede resultater

Før du kjører programmet, skal du finne ut hva resultatene bør være.
Bruk kravet om at hver person trenger 2 forsyningsenheter per dag. Regn ut forventet resultat for hvert tilfelle i tabellen.  

| Inndata eller handling | Forventet resultat |
|---|---|
| 3 personer, 4 dager | 24 |
| 0 personer, 4 dager | 0 |
| 3 personer, 0 dager | 0 |
| 1 person, 1 dag | 2 |
| 20 personer, 30 dager | 1200 |

### 2. Bygg og kjør programmet

Åpne terminalen, og kjør:

```sh
dotnet build 01-forventet-og-faktisk/Exercise.slnx
dotnet run --project 01-forventet-og-faktisk/App/App.csproj
```
  
Se på resultatene som skrives ut.

### 3. Prøv alle tilfellene

Åpne Program.cs. Undersøk hvilke tilfeller programmet allerede beregner.

Legg til de manglende tilfellene fra tabellen, og kjør programmet på nytt.

### 4. Sammenlign resultatene

Sammenlign det du forventet i steg 1, med det programmet faktisk skrev ut.

Skriv ned resultatene i REFLEKSJON.md. Stemte alle resultatene med forventningene?

### 5. Prøv å innføre en feil

Endre midlertidig multiplikatoren i beregningen fra 2 til 3.

Kjør programmet på nytt og sammenlign resultatene med forventningene dine.

- Hvilke tilfeller avslører feilen?
- Hvilke tilfeller gir fortsatt riktig resultat?
- Hvorfor skjer dette?

Endre multiplikatoren tilbake til 2 når du er ferdig.


### 6. Reflekter

Forklar i  [REFLEKSJON.md](REFLEKSJON.md) hvorfor det ikke er nok at programmet skriver ut et resultat, for å avgjøre om en test er bestått.
