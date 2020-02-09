using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTesting : MonoBehaviour
{
    //What I'm using to get the other script
    public GameObject ListOfCards;
    public PrefabInstantiate prefabInstantiate;

    public List<Statistic> testOfHand = new List<Statistic>();
    // Start is called before the first frame update
    void Start()
    {
        //The other part of what is used to instantiate the script, the object was never found so it would return null
        ListOfCards = GameObject.Find("ListOfCards");
        prefabInstantiate = ListOfCards.GetComponent<PrefabInstantiate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testOfHand.Add(prefabInstantiate.testHand[0]);
        }
    }
}
