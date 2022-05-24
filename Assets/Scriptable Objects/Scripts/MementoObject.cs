using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ei liitet� mihink��, tekee Asset menun valikkoon. Voidaan tehd� useampi esim. "Weapons", "Helmets" etc.
[CreateAssetMenu(fileName ="New Memento Object", menuName = "Inventory System/Mementos")]
public class MementoObject : ItemObjects
{
    //Awake laittaa automaattisesti itemtypeksi Mementos
    public void Awake()
    {
        type = ItemType.Mementos;
    }
}

/*Jos halutaan erityyppisi� esineit�, vaihdetaan esim. ItemType.ConceptArt; riippuen mit� "ItemObjects" scriptiss� on laitettu listaan.
 * ItemType:n voi my�s laittaa manuaalisesti ilman ett� erillisi� scriptej� tarvitsee tehd�.
 * 
Jos halutaan laittaa erilaisia variableja mit� ItemObjectissa ei ole listattu voit sen laittaa n�in:

[CreateAssetMenu(fileName ="New ConceptArt Object", menuName = "Inventory System/ConceptArt")]

public class ConceptArtObject : ItemObjects

public float points;
{
    public void Awake()
    {
        type = ItemType.ConceptArt;
    }
}*/