namespace Roguelike.Rules;

public static class Shuffler {
    public static void Shuffle(Random random, List<Card> cards) {
        for (int n = cards.Count - 1; n > 0; n--) {
            int k = random.Next(n + 1);
            (cards[n], cards[k]) = (cards[k], cards[n]);
        }
    }
}