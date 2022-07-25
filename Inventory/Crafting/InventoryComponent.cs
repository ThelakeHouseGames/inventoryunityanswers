 using System.Collections.Generic;
 using UnityEngine;
 
 [DisallowMultipleComponent]
 public class InventoryComponent : MonoBehaviour
 {
     public List<InventoryItem> content;
     public void AddItems ( SCItem type , uint quantity )
     {
         for( int i=0 ; i<content.Count ; i++ )
         {
             if( content[i].type==type )
             {
                 var item = content[i];// pull struct copy from array
                 item.quantity += quantity;// modify it
                 content[i] = item;// push struct copy on array back again
                 return;
             }
         }
         // item type not found so create a new entry:
         content.Add( new InventoryItem{
             type        = type ,
             quantity    = quantity
         } );
     }
 }
 
 [System.Serializable]
 public struct InventoryItem
 {
     public SCItem type;
     public uint quantity;
 }