using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public int damage;
    public Sprite testSprite;
    public int orderInHand;
    public bool inPlay = false;
    public int positionInPlay;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = testSprite;
    }
}
