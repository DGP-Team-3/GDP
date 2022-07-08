using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait
{
    private string traitName;

    public Trait(string nameParam)
    {
        this.traitName = nameParam;
    }

    public string getTraitName()
    {
        return this.traitName;
    }
}
