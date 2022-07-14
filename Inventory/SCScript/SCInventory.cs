using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable/Inventory")]
public class SCInventory : ScriptableObject
{
  public Player player;
  public List<Slot> inventorySlots = new List<Slot>();
  int stackLimit = 16;

  public void DeleteItem(int index){
    inventorySlots[index].isfull = false;
    inventorySlots[index].itemcount = 0;
    inventorySlots[index].item = null;
  }
 
  public void EatItem(int index){
    inventorySlots[index].isfull = false;
    inventorySlots[index].itemcount = 0;
    inventorySlots[index].item = null;

  }
  
  
  public void DropItem(int index, Vector3 position){
    
    for(int i = 0; i < inventorySlots[index].itemcount;i++){
        GameObject tempItem = Instantiate(inventorySlots[index].item.itemPrefab);
        tempItem.transform.position = position+new Vector3(i,0,0);
    }
    DeleteItem(index);
  }
  
  public bool AddItem(SCItem item){
    foreach(Slot slot in inventorySlots){
        if(slot.item == item){
            if(slot.item.canStackable){
                if(slot.itemcount < stackLimit){
                    slot.itemcount++;
                    if(slot.itemcount >= stackLimit){
                        slot.isfull = true;
                    }
                    return true;
                }
            }
        }
        else if(slot.itemcount == 0){
            slot.AddItemToSlot(item);
            return true;
        }
    }
    return false;
  }
}







//------------------------------//
//             SLOT             //
//         SCRİPT ALANI         //
//------------------------------//

[System.Serializable]
public class Slot 
{
    public bool isfull;
    public int itemcount;
    public SCItem item;
    public void AddItemToSlot(SCItem item){
        this.item = item;
        if(item.canStackable == false){
            isfull = true;
        }
        itemcount++;


    }
}