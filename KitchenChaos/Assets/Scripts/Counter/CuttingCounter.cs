using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
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
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                // player has nothing. (do nothing)
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            // there is a kitchen object here
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
