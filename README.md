# Blackjack

## Innehåll
1. [Om programmet](#om-programmet)
2. [Starta programmet](#starta-programmet)
3. [Klassöversikt](#klassöversikt)
4. [Vem som gjort vad](#vem-som-gjort-vad)
5. [Regler](#regler)
6. [Kända brister](#kända-brister)

---

## Om programmet
<!-- Vad programmet gör -->

## Starta programmet
1. Öppna en terminal i projektmappen.
2. Skriv:
   ```
   dotnet run
   ```
3. Programmet startar.

## Klassöversikt
Koden är uppdelad i tre mappar: **Cards** (kort och lekar), **Participants** (de som spelar) och **GameFunctionality** (spelets flöde, konsolen och sparning). `Game` startar spelet och skapar en `Round` för varje runda. En `Round` delar ut kort från en `Deck` till varje deltagares `Hand`. All text som visas går via `ConsoleUI`.

| Mapp              | Klass                     | Ansvar                                                                                   |
|:------------------|:--------------------------|:-----------------------------------------------------------------------------------------|
| Cards             | `Card`                    | Ett kort med färg, valör och poängvärde.                                                 |
|                   | `Deck`                    | Kortleken med 52 kort. Blandar och delar ut kort.                                        |
|                   | `Hand`                    | En deltagares kort. Räknar poäng (ess som 11 eller 1), mjuk hand, spricka och blackjack. |
|                   | `Suit`, `Rank`            | Enums för färg och valör.                                                                |
| Participants      | `Participant`             | Basklass för spelare och givare: namn, hand och att dra ett kort.                        |
|                   | `Player`                  | En spelare med vinster, förluster, oavgjorda och vinstprocent.                           |
|                   | `Dealer`                  | Givaren. Har ett dolt hålkort och drar kort på 16 eller lägre.                           |
| GameFunctionality | `Game`                    | Huvudmenyn, skapar spelarna och kör rundor. Byter lek när korten börjar ta slut.         |
|                   | `Round`                   | En runda: given, spelarnas turer, givarens tur och vem som vinner.                       |
|                   | `ConsoleUI`               | Allt som skrivs ut och läses in i konsolen, bland annat kortgrafiken.                    |
|                   | `GameStorage`             | Sparar och laddar spelare med deras statistik från `Leaderboard.txt`, så att en spelare som har spelat förut fortsätter med sin gamla statistik. Samlar även fel från trasiga rader. |
|                   | `Move`, `Outcome`         | Enums för drag (Hit/Stand) och resultat (Win/Lose/Push/Blackjack).                       |
| Rules             | `RuleSet`                 | Husreglerna. Läses från `rules.json` när programmet startar. Saknas filen eller är den trasig används standardreglerna. |

## Vem som gjort vad
<!-- Vilket tillval var och en har byggt -->

| Namn     | Tillval / bidrag                       |
|:---------|:---------------------------------------|
| Martin   | 1: Kortgrafik i konsolen               |
| Nikolai  | 2: Datorspelare med strategi           |
| Hamzeh   | 3: Husregler från konfigurationsfil    |
| Eimantas | 4: Satsningar och marker               |
| Albin    | 5: Sko med flera lekar och korträkning |

## Regler
<!-- Vilka regler som gäller -->

### Husregler (tillval 3)
Reglerna går att ändra i `rules.json` i projektmappen, utan att kompilera om:

```json
{
  "DealerHitsSoft17": false,
  "NumberOfDecks": 6,
  "ReshuffleAt": 15,
  "BlackjackBeats21": true
}
```

| Inställning        | Betyder                                                       | Standard |
|:-------------------|:--------------------------------------------------------------|:---------|
| `DealerHitsSoft17` | Köper givaren även på mjuk 17?                                | `false`  |
| `NumberOfDecks`    | Hur många lekar skon har när man spelar med sko (minst 2). Utan sko spelas det med en lek | `6` |
| `ReshuffleAt`      | Utan sko: ny lek när färre än så här många kort är kvar (minst 15, dubbelt vid 3+ spelare). Med sko bestämmer skiljekortet | `15` |
| `BlackjackBeats21` | Vinner naturlig tjugoett över 21 av tre kort eller fler?      | `true`   |

Saknas filen, saknas en regel, är den trasig eller har orimliga värden (till exempel 1 lek i skon eller färre än 15 kort) startar spelet ändå med standardreglerna och skriver ut en varning. Reglerna visas efter frågan om sko, så att antal lekar stämmer med det man valt.

### Avvikelser från specifikationen
* Minsta satsningen är 25. Spelare under 25 marker får välja att köpa sig in eller lämna bordet. 
* Korleksgränsen är 15 eller 30 baserat på spelare. 

## Kända brister
- `Move` används inte. Spelarens val hanteras med en `bool` från `ConsoleUI.WantsToHit()`.
DecideWinners()`, så blackjack-meddelandet i `ConsoleUI.ShowOutcome()` visas aldrig.
- `Round.AddStats()` är tom och anropas inte. Statistiken sköts redan av `Player.AddResult()`.
- Givaren drar kort även när alla spelare har blackjack eller har spruckit. Hen borde bara vända hålkortet. Resultatet blir ändå rätt, eftersom blackjack slår 21 med fler kort.
- Givaren tittar inte efter blackjack innan spelarna gör sina drag. Har givaren blackjack får spelarna ändå dra kort, fast de redan har förlorat.
- Dealern drar naturlig 21 men det står i utskriften (mjuk hand 21)
