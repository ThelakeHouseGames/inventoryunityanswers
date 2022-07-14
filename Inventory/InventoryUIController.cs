using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public List<SlotUI> uilist = new List<SlotUI>();
    public Inventory userInventory;

    private void Start() {
        userInventory = gameObject.GetComponent<Inventory>();
    }
    
    public void UpdateUI(){
        for(int i = 0; i < uilist.Count; i++){
            if(userInventory.playerInventory.inventorySlots[i].itemcount > 0){
                uilist[i].itemImage.sprite = userInventory.playerInventory.inventorySlots[i].item.itemIcon;
                if(userInventory.playerInventory.inventorySlots[i].item.canStackable == true){
                    uilist[i].itemCountText.gameObject.SetActive(true);
                    uilist[i].itemCountText.text = userInventory.playerInventory.inventorySlots[i].itemcount.ToString();
                }
                else{
                    uilist[i].itemCountText.gameObject.SetActive(false);
                }

            }
            else{
                uilist[i].itemImage.sprite = null;
            }

        }

    }
}
