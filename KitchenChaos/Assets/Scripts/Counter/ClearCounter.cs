using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // 들고 올리기.
        if(!HasKitchenObject())
        {
            // not having kitchen object
            if (player.HasKitchenObject())
            {
                // player is carrying object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player has nothing. (do nothing)
            }
        }
        else
        {
            // having kitchen object
            if (player.HasKitchenObject())
            {
                // player is carrying object
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
                else
                {
                    //player is not carrying plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                // player has nothing. (do nothing)
            }
        }
    }

}
