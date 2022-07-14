using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public SCInventory playerInventory;
  public PlayerActions playerAction;
  InventoryUIController InventoryUI;
  Player player;

  bool isSwapping;
  int tempIndex;
  Slot tempslot;

  
  public void currentItem(int index){
    if(playerInventory.inventorySlots[index].item){
      playerAction.SetItem(playerInventory.inventorySlots[index].item.itemPrefab);
    }
  }
  private void Start() {
     InventoryUI = gameObject.GetComponent<InventoryUIController>();
     InventoryUI.UpdateUI();
  }
  
  public void DeleteItem(){
    if(isSwapping == true){
      playerInventory.DeleteItem(tempIndex);
      isSwapping = false;
      InventoryUI.UpdateUI();
    }
  }
  
  public void DropItem(){
    if(isSwapping == true){
      playerInventory.DropItem(tempIndex,gameObject.transform.position+Vector3.forward);
      isSwapping = false;
      InventoryUI.UpdateUI();
    }
  }

  public void EatItem(){
    if(isSwapping == true){
           gameObject.GetComponent<Player>().hunger += 10f;
           isSwapping = false; 
           playerInventory.DeleteItem(tempIndex);
           InventoryUI.UpdateUI(); 
        }
  }


  public void SwapItem(int index){
    if(isSwapping == false){
      tempIndex = index;
      tempslot = playerInventory.inventorySlots[tempIndex];
      isSwapping = true;
    }
    else if (isSwapping == true){
      playerInventory.inventorySlots[tempIndex] = playerInventory.inventorySlots[index];
      playerInventory.inventorySlots[index] = tempslot;
      isSwapping = false;
    }
    InventoryUI.UpdateUI();


  }
  
  private void OnTriggerEnter(Collider other) {
    if(other.gameObject.CompareTag("item")){
        if(playerInventory.AddItem(other.gameObject.GetComponent<Item>().item))
        {
            Destroy(other.gameObject);
            InventoryUI.UpdateUI();
        }
    }
  }

}
