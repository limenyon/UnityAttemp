using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiate : MonoBehaviour
{
    public bool playFieldFull = false;
    public int cardsInHand = 0;
    bool recentlyShuffled = false;
    int positionInDeck = 0;
    int totalCards = 0;
    int layerCheck = 0;
    int sortingLayerIncriment = 6;
    float offScreenCardPosition = 50.0f;
    float[] cardPositionsX = new float[] { -4.54f, -3.09f, -1.63f, 0.22f, 1.68f };
    float cardPositionsY = -5.77f;

    public GameObject hand;
    public HandTesting handTesting;
    public List<GameObject> cardsAtPlay = new List<GameObject>();
    public List<Statistic> testHand = new List<Statistic>();
    public List<GameObject> deck = new List<GameObject>();

    public void CreateDeck(int deckPosition, float vectorPosX, float vectorPosY, int totalCards)
    {
        foreach (Statistic element in testHand)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(testHand[deckPosition], new Vector2(vectorPosX, vectorPosY), Quaternion.identity);
                GameObject.Find("Card" + deckPosition + "(Clone)").name = "Card" + totalCards;
                deck.Add(GameObject.Find("Card" + totalCards));
                totalCards++;
            }
            deckPosition++;
        }
    }
    public void RandomiseDeck(List<GameObject> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        recentlyShuffled = !recentlyShuffled;
    }

    public void DrawCards(List<GameObject> deck, float[] cardPositionsX, float cardPositionsY, int sortingLayerIncriment, int layerCheck)
    {
        for(int i = 0; i < 5; i++)
        {
            deck[0].GetComponent<SpriteRenderer>().sortingOrder = i + sortingLayerIncriment;
            deck[0].transform.position = new Vector2(cardPositionsX[i], cardPositionsY);
            deck[0].GetComponent<Statistic>().orderInHand = i;
            deck.Add(deck[0]);
            deck.Remove(deck[0]);
        }
    }

    void Start()
    {
        CreateDeck(positionInDeck, offScreenCardPosition, offScreenCardPosition, totalCards);
        RandomiseDeck(deck);
        DrawCards(deck, cardPositionsX, cardPositionsY, sortingLayerIncriment, layerCheck);
    }
}