using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cat : MonoBehaviour
{
    [SerializeField]
    public int relationship;
    public int fullness;
    private Trait[] traits;
    [SerializeField]
    private bool isOwned;

    [SerializeField]
    private SpriteRenderer sprite;
    private float hungerTimer = 0f;


    /*public Cat (string[] allTraits)
    {
        Trait[] currentTraits = new Trait[allTraits.Length];
        for (int i = 0; i < allTraits.Length; i ++ )
        {
            Trait current = new Trait(allTraits[i]);
            currentTraits[i] = current;
        }
        this.traits = currentTraits;
        this.isOwned = false;
    }*/

    private void Awake()
    {
        
    }

    void Update()
    {
        if (fullness > 0)
        {
            hungerTimer += Time.deltaTime;
            int seconds = Mathf.FloorToInt(hungerTimer % 60);
            fullness = 100 - seconds;
        }
    }

    public int getFullness()
    {
        return fullness;
    }

    void OnMouseDown()
    {
        GameObject.Find("SelectionManager").GetComponent<SelectionManager>().toggleSelection();
    }

}
