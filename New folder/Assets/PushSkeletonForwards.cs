using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSkeletonForwards : MonoBehaviour
{
    int life = 49;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life < 50)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
        }
    }
}
