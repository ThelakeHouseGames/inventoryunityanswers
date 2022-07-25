   using UnityEngine;
   [CreateAssetMenu( menuName="Game/Recipe Asset" , fileName="crafting recipe 0" , order=1 )]
   public class CraftingRecipeAsset : ScriptableObject
   {
       public SCItem product = null;
       public GameObject prefab;
       [Min(1)] public uint batchProducts = 1;
       [Min(0.1f)] public float batchDuration = 1.5f;
       public InventoryItem[] ingredients = new InventoryItem[0];
   }