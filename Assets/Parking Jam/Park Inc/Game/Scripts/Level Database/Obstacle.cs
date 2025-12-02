#pragma warning disable 649

using UnityEngine;
using Watermelon;

[CreateAssetMenu(menuName = "Level Database/Obstacle", fileName = "Obstacle")]
public class Obstacle: ScriptableObject
{
    [SerializeField] int shopIndex;
    [SerializeField] private FieldElement fieldElement;
    public bool npc = false;
    public GameObject npcPreb;
    public Vector2Int Size => fieldElement.Size;
    public GameObject Prefab
    {
        get
        {
            if (npc)
            {
                return npcPreb;
            }
            else
            {
                EnvironmentSkinProduct environmentSkinProduct = StoreController.GetSelectedProduct(StoreProductType.EnvironmentSkin) as EnvironmentSkinProduct;

                return environmentSkinProduct.GetObstacleObject(shopIndex);
            }
        }
    }
}