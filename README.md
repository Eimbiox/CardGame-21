# Tjugoett

## Innehåll
1. [Om programmet](#om-programmet)
2. [Starta programmet](#starta-programmet)
3. [Så spelar man](#så-spelar-man)
4. [Klassöversikt](#klassöversikt)
5. [Klassdiagram](#klassdiagram)
6. [Vem som gjort vad](#vem-som-gjort-vad)
7. [Regler](#regler)
8. [Kända brister](#kända-brister)

---

## Om programmet
Tjugoett för en till fem spelare mot en given. Varje spelare kan vara en människa eller en datorspelare. Upp till 2 datorspelare. Spelet följer reglerna i uppgiften, och alla fem tillval finns med:

1. **Kortgrafik**: korten ritas som rutor med ram och står bredvid varandra. Givarens hålkort visas med baksidan upp.
2. **Datorspelare**: två strategier, *Simpel* (köper under 17) och *Grund* (grundstrategitabell).
3. **Husregler**: reglerna läses från `rules.json` vid start.
4. **Marker och satsningar**: spelarna(människor) satsar varje runda, och vinster betalas ut enligt reglerna.
5. **Sko och korträkning**: man kan spela med en sko med flera lekar och ett skiljekort. En Hi-Lo-räknare visar hur läget ser ut.

Spelarnas vinster, förluster, lika och marker sparas i `Leaderboard.txt`. Statistiken finns kvar nästa gång programmet startas och visas i en topplista.

## Starta programmet

1. Klona repot och öppna en terminal i projektmappen.
2. Kör:
   ```
   dotnet run
   ```
3. Huvudmenyn visas:
   ```
   1. Starta
   2. Visa topplistan
   3. Sök efter spelares reslutat
   4. Avsluta
   ```
## Så spelar man
1. Välj **1. Starta**.
2. Svara om du vill spela med sko (`ja`/`nej`). Husreglerna visas.
3. Ange antal spelare (1–5). Ange för varje plats om det är en `människa` eller en `dator`.
   - **Människa:** skriv ett namn. Finns namnet redan i topplistan fortsätter spelaren med sin gamla statistik och sina marker.
   - **Dator:** välj `Simpel` eller `Grund`. Det kan bara sitta en av varje sort vid bordet.
4. Varje mänsklig spelare satsar minst 25 marker.
5. Korten delas ut. Varje spelare skriver `köp` eller `stanna` tills hen stannar, spräcker eller når 21.
6. Givaren vänder hålkortet och spelar enligt sin regel. Sedan visas resultatet och utbetalningen.
7. Svara om du vill spela en runda till. Resultatet sparas efter varje runda.

Felaktig inmatning, till exempel bokstäver där det ska vara en siffra eller ett tomt svar, ger ett felmeddelande i gult och en ny fråga. Programmet kraschar inte.

## Klassöversikt
`Game` kör huvudmenyn och spelloopen. Varje runda skapas som en `Round`, som delar ut kort från en `Deck` till varje deltagares `Hand`. Spelare fattar beslut genom sin `IPlayerStrategy`, och givaren följer sin fasta regel i `Dealer.ShouldHit()`. All in och utmatning sker i `ConsoleUI`: regelklasserna (`Card`, `Deck`, `Hand`, `Participant`, `Player`, `Dealer`, `Round`) skriver aldrig till konsolen.

| Mapp              | Klass                              | Ansvar |
|:------------------|:-----------------------------------|:-------|
| Cards             | `Suit`, `Rank`                     | Enums för färg och valör. |
|                   | `Card`                             | Ett kort med färg, valör och poängvärde. Esset har värdet 11, och `Hand` räknar ner det till 1 vid behov. |
|                   | `Deck`                             | Bygger en eller flera lekar, blandar med Fisher–Yates och delar ut kort. Om man valt sko skickas allt vidare till en `Shoe`. |
|                   | `Hand`                             | Håller deltagares kort. Räknar om värdet efter varje nytt kort och vet om handen är mjuk, spräckt eller naturlig tjugoett. |
| Participants      | `Participant`                      | Basklass för spelare och givare: namn, hand och att köpa ett kort. |
|                   | `Player`                           | En spelare med statistik, plånbok och strategi. Frågar strategin vilket drag som ska göras. |
|                   | `Dealer`                           | Givaren. Har hålkortet och regeln: köp på 16 eller lägre, stanna på 17. Husregeln kan göra att givaren köper på mjuk 17. |
|                   | `Wallet`                           | Spelarens marker. Lägger till och drar av, och tillåter aldrig ett negativt saldo. |
| Strategies        | `IPlayerStrategy`                  | Gränssnitt: `ChooseMove(hand, dealerUpcard)` returnerar ett `Move`. |
|                   | `HumanStrategy`                    | Frågar människan via `ConsoleUI`. |
|                   | `SimpleStrategy`                   | Köper så länge handen är under 17. |
|                   | `BasicStrategy`                    | Slår upp draget i två tabeller, en för hårda och en för mjuka händer, utifrån givarens öppna kort. |
| GameFunctionality | `Game`                             | Huvudmeny, skapar spelare, läser husreglerna och kör rundor. Byter lek när korten börjar ta slut. |
|                   | `Round`                            | En giv: insatser, utdelning, spelarnas turer, givarens tur och avgörande. |
|                   | `RoundBets`                        | Håller rundans insatser och betalar ut efter resultatet. |
|                   | `ConsoleUI`                        | All in- och utmatning: menyer, frågor, validering, händer, topplista och korträkning. |
|                   | `CardRenderer`                     | Bygger kortgrafiken som `string[]` (en rad per element). `ConsoleUI` lägger korten bredvid varandra och sätter färgen. |
|                   | `GameStorage`                      | Läser och skriver spelare till `Leaderboard.txt`. Trasiga rader hoppas över och visas som fel vid start. |
|                   | `Move`, `Outcome`                  | Enums för drag (`Hit`/`Stand`) och resultat (`Win`/`Lose`/`Push`/`Blackjack`). |
| Rules             | `RuleSet`                          | Husreglerna. Läses från `rules.json`, och standardvärden används om filen saknas eller är trasig. |
| Tillval-5         | `Shoe`                             | Flera lekar blandade till en, med ett skiljekort på slumpmässig plats i den bakre fjärdedelen. |
|                   | `CardCounter`                      | Hi-Lo-räknare. Observerar kort som visas och räknar löpande och sann räkning. Ändrar ingenting i spelet. |
|                   | `CountAdvantage`                   | Enum för hur gynnsamt läget är (`Unfavorable`/`Neutral`/`Favorable`). |

### Designfrågorna
- **Ska `Card` ha ett värde?** Ja, `Card.Point`. Esset har 11, men det är `Hand.CountPoints()` som räknar om hela handen efter varje kort och gör ess till 1 när det behövs. Handens värde sparas alltså aldrig en gång för alla.
- **Är givaren en spelare?** Nej. `Player` och `Dealer` ärver båda `Participant` (namn, hand, köpa kort). `Player` har en strategi, statistik och marker. `Dealer` har hålkortet och en fast regel.
- **Vem bestämmer vad en spelare gör?** Spelarens `IPlayerStrategy`. `Round` anropar `player.Decide(...)` och vet inte om det är en människa eller en dator som svarar.
- **Var ligger givarens regel?** I `Dealer.ShouldHit()`. `Round` frågar bara givaren om hen ska köpa.

## Klassdiagram

## Vem som gjort vad

| Namn     | Tillval                                | Egna klasser |
|:---------|:---------------------------------------|:-------------|
| Martin   | 1: Kortgrafik i konsolen               | `CardRenderer` |
| Nikolai  | 2: Datorspelare med strategi           | `IPlayerStrategy`, `HumanStrategy`, `SimpleStrategy`, `BasicStrategy` |
| Hamzeh   | 3: Husregler från konfigurationsfil    | `RuleSet` |
| Eimantas | 4: Satsningar och marker               | `Wallet`, `RoundBets` |
| Albin    | 5: Sko med flera lekar och korträkning | `Shoe`, `CardCounter`, `CountAdvantage` |

Grunden (kort, lek, hand, deltagare, runda, spelloop, sparning och konsol) byggdes gemensamt.

## Regler
Spelet följer reglerna i uppgiften. Det som går att ställa in, och de avvikelser vi gjort, står nedan.

### Husregler
Reglerna ändras i `rules.json` utan att kompilera om:

```json
{
  "DealerHitsSoft17": false,
  "NumberOfDecks": 1,
  "ShoeDecks": 6,
  "ReshuffleAt": 15,
  "BlackjackBeats21": true
}
```

| Inställning        | Betyder                                                                 | Standard |
|:-------------------|:------------------------------------------------------------------------|:---------|
| `DealerHitsSoft17` | Köper givaren även på mjuk 17?                                          | `false`  |
| `NumberOfDecks`    | Antal lekar utan sko (minst 1)                                          | `1`      |
| `ShoeDecks`        | Antal lekar i skon (minst 2)                                            | `6`      |
| `ReshuffleAt`      | Utan sko: ny lek när färre kort än så är kvar (minst 15). Med sko bestämmer skiljekortet i stället | `15` |
| `BlackjackBeats21` | Vinner naturlig tjugoett över 21 av tre kort eller fler?                | `true`   |

Om filen saknas, en regel fattas, filen inte är giltig JSON eller värdena är orimliga, startar spelet med standardreglerna och skriver ut en varning.

### Avvikelser från specifikationen
- **Minsta insats är 25 marker.** Specifikationen säger bara att insatsen inte får vara noll.
- **Spelare utan marker kan köpa sig in.** Har en spelare under 25 marker får hen välja att köpa in sig för 200 nya marker eller lämna bordet. Specifikationen säger att spelaren lämnar bordet.
- **Datorspelare satsar inte.** De har 0 marker och spelar bara om vinster och förluster i statistiken.
- **Ny lek vid 30 kort med många spelare.** Utan sko byts leken när färre än `ReshuffleAt` kort är kvar (15 som standard). Med tre eller fler spelare dubblas gränsen till 30, så att leken inte tar slut mitt i en runda.
- **Högst en datorspelare av varje sort.** Det kan sitta en *Simpel* och en *Grund* samtidigt, inte två av samma.
- **Försäkring är inte gjord.** Den var valfri i tillval 4. Dubbla och dela par (överkurs) är inte heller gjorda.

## Kända brister
- **Givarens naturliga tjugoett visas som "mjuk hand, 21".** Den räknas rätt i avgörandet, men det står inte "BLACKJACK" i utskriften som för spelarna.
- **`Leaderboard.txt` går att redigera för hand.** Man kan ändra sina vinster eller marker direkt i filen.
- **Skon kan i teorin ta slut.** Skiljekortet ligger minst 15 kort från slutet. Med fem spelare och många köp i samma runda kan fler än 15 kort dras efter skiljekortet.
- **Kortgrafiken:** varje kort är 10 tecken brett. Med många kort i en smal terminal bryts raderna och korten ser trasiga ut. En lösning vore att dela upp handen efter så många kort som får plats på bredden, och rita resten på en ny rad under.
- **Deck och Shoe hänger inte ihop logiskt.** I verkligheten innehåller en sko flera lekar, men i koden är det Deck som innehåller en Shoe. Deck gör därför två olika saker beroende på om UseShoe är på eller av.

