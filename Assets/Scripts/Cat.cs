using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cat : MonoBehaviour
{
    private int relationship;
    private int fullness;
    private Trait[] traits;
    private bool isOwned;

    [SerializeField]
    private SpriteRenderer sprite;
    
    public Cat (string[] allTraits)
    {
        Trait[] currentTraits = new Trait[allTraits.Length];
        for (int i = 0; i < allTraits.Length; i ++ )
        {
            Trait current = new Trait(allTraits[i]);
            currentTraits[i] = current;
        }
        this.traits = currentTraits;
        this.isOwned = false;
    }

    void Update()
    {
        while (this.fullness > 0)
        {
            int seconds = Mathf.FloorToInt(Time.deltaTime % 60);
            this.fullness -= seconds;
        }
    }

}
