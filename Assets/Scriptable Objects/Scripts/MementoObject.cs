using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ei liitetä mihinkää, tekee Asset menun valikkoon. Voidaan tehdä useampi esim. "Weapons", "Helmets" etc.
[CreateAssetMenu(fileName ="New Memento Object", menuName = "Inventory System/Mementos")]
public class MementoObject : ItemObjects
{
    //Awake laittaa automaattisesti itemtypeksi Mementos
    public void Awake()
    {
        type = ItemType.Mementos;
    }
}

/*Jos halutaan erityyppisiä esineitä, vaihdetaan esim. ItemType.ConceptArt; riippuen mitä "ItemObjects" scriptissä on laitettu listaan.
 * ItemType:n voi myös laittaa manuaalisesti ilman että erillisiä scriptejä tarvitsee tehdä.
 * 
Jos halutaan laittaa erilaisia variableja mitä ItemObjectissa ei ole listattu voit sen laittaa näin:

[CreateAssetMenu(fileName ="New ConceptArt Object", menuName = "Inventory System/ConceptArt")]

public class ConceptArtObject : ItemObjects

public float points;
{
    public void Awake()
    {
        type = ItemType.ConceptArt;
    }
}*/