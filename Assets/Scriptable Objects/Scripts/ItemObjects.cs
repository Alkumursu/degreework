using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Mementos,
    ConceptArt,
    Other
}
public class ItemObjects : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public Sprite icon;
    [TextArea(15,20)]
    public string description;
    
}
