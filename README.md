# Uke 2 · xUnit og MVC

## Oppdrag: gjør romstasjonens programvare pålitelig

Gjennom disse 12 minioppgavene skal du kontrollere forsyninger, seter og last med automatiserte tester. Deretter bygger du en oppdragstavle der modell, visning og kontroller har tydelige ansvar.

Hver oppgave har startkode, et avgrenset oppdrag, korte begrepsforklaringer, testtilfeller, refleksjon og en egen `AGENTS.md`.

## Oppgavene

| Nr. | Oppgave |
|---|---|
| 01 | [Forsyninger: fra krav til testeksempler](01-forventet-og-faktisk/README.md) |
| 02 | [Første xUnit-test og Arrange–Act–Assert](02-foerste-fact/README.md) |
| 03 | [Setereservasjon: Theory og grensetilfeller](03-theory-og-grenser/README.md) |
| 04 | [Lasterom: test tilstand uten testrekkefølge](04-uavhengige-tester/README.md) |
| 05 | [Lastestabel: LIFO og forventede unntak](05-unntak-og-generics/README.md) |
| 06 | [Oppdragskort: første Red–Green–Refactor](06-foerste-tdd/README.md) |
| 07 | [Oppskytingspris: forbedre grønn kode](07-trygg-refaktorering/README.md) |
| 08 | [Skill oppskytingsregelen fra konsollen](08-fra-blanding-til-modell/README.md) |
| 09 | [Oppdragstavlen: test modellen før menyen](09-modell-med-samling/README.md) |
| 10 | [Visningen: vis data uten å endre dem](10-visning/README.md) |
| 11 | [Kontrolleren: koble sammen en hel konsollapp](11-kontroller/README.md) |
| 12 | [Nytt behov: vis bare ufullførte oppdrag](12-tdd-i-mvc/README.md) |

## Dette skal du kunne etterpå

- Beskrive normaltilfeller, ugyldige handlinger og grenser før koding.
- Skrive og kjøre uavhengige xUnit-tester med Fact, Theory og InlineData.
- Bruke Equal, True, False, Throws, Same og Single når de passer kravet.
- Gjennomføre Red–Green–Refactor og skille TDD fra testing generelt.
- Refaktorere kjent kode med tester som støtte.
- Fordele data/regler, brukerkommunikasjon og flyt mellom Model, View og Controller.
- Teste modellen automatisk og kontrollere hele konsollflyten manuelt.

## Struktur
De aller fleste oppgaver følger denne strukturen:

- `Core/`: regler og modellklasser.
- `Tests/`: xUnit-tester som refererer til Core; ikke til konsollappen.
- `App/`: kjørbar konsollapp når oppgaven trenger det. I MVC-delen ligger View og Controller her.
- `Exercise.slnx`: samler prosjektene i den enkelte oppgaven.
- `REFLEKSJON.md`: arbeidslogg og refleksjon.

Oppgavene bruker samme prosjektnavn, men separate mapper og ingen referanser mellom oppgavene. Kjør mot **én oppgave om gangen**. Mapper og prosjekter er ikke i seg selv MVC; det er ansvarsdelingen som teller.