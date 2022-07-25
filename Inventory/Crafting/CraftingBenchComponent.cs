using System.Collections;
 using UnityEngine;
 
 public class CraftingBenchComponent : MonoBehaviour
 {
     public InventoryComponent inventory;
     public CraftingRecipeAsset recipe;
 
     Coroutine _craftingInProgress;
 
     public void StartCrafting ()
     {
         if( DoIHaveAllTheIngredients()==true )
         {
             if( _craftingInProgress!=null ) StopCoroutine( _craftingInProgress );// prevent multiple crafting routines
             _craftingInProgress = StartCoroutine( CraftingRoutine() );
         }
         else Debug.Log("can't craft - some ingredients are missing");
     }
 
     IEnumerator CraftingRoutine ()
     {
         Debug.Log($"{recipe.name} crafting started...");
 
         float batchTime = 0;
         while( batchTime<recipe.batchDuration )
         {
             yield return null;
             batchTime += Time.deltaTime;
         }
 
         ConsumeIngredients();
         CreateItem();
 
         Debug.Log($"\t... {recipe.name} crafting completed!");
     }
     
     bool DoIHaveAllTheIngredients ()
     {
         for( int i=0 ; i<recipe.ingredients.Length ; i++ )
         {
             var ingredient = recipe.ingredients[i];
             bool passed = false;
             foreach( var item in inventory.content )
             {
                 if( item.type==ingredient.type && item.quantity>=ingredient.quantity )
                 {
                     passed = true;
                     break;
                 }
             }
             if( !passed ) return false;
         }
         return true;
     }
 
     void ConsumeIngredients ()
     {
         for( int i=0 ; i<recipe.ingredients.Length ; i++ )
         {
             var ingredient = recipe.ingredients[i];
             for( int k=0 ; k<inventory.content.Count ; k++ )
             {
                 var item = inventory.content[k];// pull struct copy from array
                 if( item.type==ingredient.type )
                 {
                     item.quantity -= ingredient.quantity;
                     inventory.content[k] = item;// push struct copy on array back again
                 }
             }
         }
     }
 
     void CreateItem ()
     {
         bool itemCreated = false;
         var items = inventory.content;
         for( int i=0 ; i<items.Count ; i++ )
         {
             var item = items[i];// pull struct copy from array
             if( item.type==recipe.product )
             {
                 item.quantity += recipe.batchProducts;
                 items[i] = item;// push struct copy on array back again
                 itemCreated = true;
                 break;
             }
         }
         if( !itemCreated )
         {
             items.Add( new InventoryItem{
                 type        = recipe.product ,
                 quantity    = recipe.batchProducts
             } );
             itemCreated = true;
         }
          Instantiate( recipe.prefab , transform.position , transform.rotation );
     }
 
 }