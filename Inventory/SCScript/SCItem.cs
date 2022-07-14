using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCItem : ScriptableObject
{
   public string itemname;
   public string itemDescription;
   public bool canStackable;
   public Sprite itemIcon;
   public GameObject itemPrefab;
   public Player player;
}
