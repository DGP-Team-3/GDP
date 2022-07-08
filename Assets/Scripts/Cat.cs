using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Sprite portrait;
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

        SelectionManager selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectionManager.setCat(this);
        selectionManager.setDisplay(true);
    }

}
