using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverTest : MonoBehaviour
{
    enum spotsTakenUp {none, firstSpot, secondSpot, thirdSpot, firstAndSecondSpot, secondAndThirdSpot, firstAndThirdSpot}; //very important remember this and use it
    spotsTakenUp spots = spotsTakenUp.none;
    int count;
    bool justChanged = false;
    public bool hasMoved = false;
    enum numberOfCardsAtPlay { noCards, oneCard, twoCards, threeCards };
    numberOfCardsAtPlay currentlyPlayed = numberOfCardsAtPlay.noCards;
    private float moveUpHeight = 1.90f;
    private GameObject ListOfCards;
    private PrefabInstantiate prefabInstantiate;
    private GameObject hand;
    private HandTesting handTesting;
    private float descaleInPlay = 0.3f;
    float[] inPlayPositionX = new float[] { -9.1f, -5.99f, -2.88f };
    float inPlayPositionY = 2.97f;
    float[] cardPositionsX = new float[] { -4.54f, -3.09f, -1.63f, 0.22f, 1.68f };
    float cardPositionsY = -3.87f;

    public void PositionInPlayRemove()
    {
        ListOfCards.GetComponent<PrefabInstantiate>().cardsAtPlay.Remove(ListOfCards.GetComponent<PrefabInstantiate>().cardsAtPlay[this.GetComponent<Statistic>().positionInPlay - 1]);
    }
    public void ChangeStateAndRemoveFromCardsAtPlay ()
    {
        if (currentlyPlayed == numberOfCardsAtPlay.oneCard)
        {
            currentlyPlayed = numberOfCardsAtPlay.noCards;
        }
        if (currentlyPlayed == numberOfCardsAtPlay.twoCards)
        {
            currentlyPlayed = numberOfCardsAtPlay.oneCard;
        }
        if (currentlyPlayed == numberOfCardsAtPlay.threeCards)
        {
            currentlyPlayed = numberOfCardsAtPlay.twoCards;
        }
        if (ListOfCards.GetComponent<PrefabInstantiate>().playFieldFull == true)
        {
            ListOfCards.GetComponent<PrefabInstantiate>().playFieldFull = false;
        }
        if (this.GetComponent<Statistic>().positionInPlay == 1)
        {
            PositionInPlayRemove();
        }
        if (this.GetComponent<Statistic>().positionInPlay == 2)
        {
            PositionInPlayRemove();
        }
        if (this.GetComponent<Statistic>().positionInPlay == 3)
        {
            PositionInPlayRemove();
        }
        justChanged = true;
    }

    public void ReturnCardFromPlay(float[] inPlayPosX, float inPlayPosY)
    {
        transform.position = new Vector3(inPlayPosX[this.GetComponent<Statistic>().orderInHand], inPlayPosY, 0);
        this.GetComponent<Statistic>().inPlay = false;
        ChangeStateAndRemoveFromCardsAtPlay();
    }

    public void AddCardsToPlay()
    {
        if (this.GetComponent<Statistic>().inPlay == false)
        {
            ListOfCards.GetComponent<PrefabInstantiate>().cardsAtPlay.Add(this.gameObject);
            this.GetComponent<Statistic>().inPlay = true;
        }
    }

    public void ReactionOnClick(float[] inPlayPositionX, float inPlayPositionY, float descale)
    {
        if (ListOfCards.GetComponent<PrefabInstantiate>().cardsInHand == 2)
        {
            transform.position = new Vector3(inPlayPositionX[2], inPlayPositionY, 0);
            transform.localScale = new Vector3(1, 1, 0);
            ListOfCards.GetComponent<PrefabInstantiate>().playFieldFull = true;
            currentlyPlayed = numberOfCardsAtPlay.threeCards;
            AddCardsToPlay();
            this.GetComponent<Statistic>().positionInPlay = 3;
        }
        if (ListOfCards.GetComponent<PrefabInstantiate>().cardsInHand == 1)
        {
            transform.position = new Vector3(inPlayPositionX[1], inPlayPositionY, 0);
            transform.localScale = new Vector3(1, 1, 0);
            currentlyPlayed = numberOfCardsAtPlay.twoCards;
            AddCardsToPlay();
            this.GetComponent<Statistic>().positionInPlay = 2;
        }
        if (ListOfCards.GetComponent<PrefabInstantiate>().cardsInHand == 0)
        {
            transform.position = new Vector3(inPlayPositionX[0], inPlayPositionY, 0);
            transform.localScale = new Vector3(1, 1, 0);
            currentlyPlayed = numberOfCardsAtPlay.oneCard;
            AddCardsToPlay();
            this.GetComponent<Statistic>().positionInPlay = 1;
        }
    }

    private void Start()
    {
        //reusable code used to access the other scripts
        ListOfCards = GameObject.Find("ListOfCards");
        prefabInstantiate = ListOfCards.GetComponent<PrefabInstantiate>();
    }
    public void OnMouseEnter()
    {
        if (this.GetComponent<Statistic>().inPlay == false && hasMoved == false)
        {
            transform.localScale += new Vector3(descaleInPlay, descaleInPlay, 0);
            transform.position += new Vector3(0, moveUpHeight, 0);
            this.GetComponent<SpriteRenderer>().sortingOrder = 45 + this.GetComponent<Statistic>().orderInHand + 6;
        }
    }

    public void OnMouseExit()
    {
        if (this.GetComponent<Statistic>().inPlay == false && hasMoved == false)
        {
            transform.localScale = new Vector3(1, 1, 0);
            transform.position -= new Vector3(0, moveUpHeight, 0);
            this.GetComponent<SpriteRenderer>().sortingOrder -= 45;
        }
    }

    public void OnMouseDown()
    {
        this.GetComponent<BoxCollider2D>().size += new Vector2(50f, 50f);
    }

    public void OnMouseUp()
    {
        if (this.GetComponent<Statistic>().inPlay == true)
        {
            ReturnCardFromPlay(cardPositionsX, cardPositionsY);
            ListOfCards.GetComponent<PrefabInstantiate>().cardsInHand--;
        }
        if (this.GetComponent<Statistic>().inPlay == false && ListOfCards.GetComponent<PrefabInstantiate>().playFieldFull == false && justChanged == false)
        {
            ReactionOnClick(inPlayPositionX, inPlayPositionY, descaleInPlay);
            ListOfCards.GetComponent<PrefabInstantiate>().cardsInHand++;
        }
        this.GetComponent<BoxCollider2D>().size -= new Vector2(50f, 50f);
        hasMoved = false;
        justChanged = false;
    }
}
