using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private int waitingRecipesMax = 4;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
        
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMathesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // ��� �����ǿ� ���� ���� ���� ���ϱ�.
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // �����Ƕ� �Ѱ��� ��ᰡ �ٸ�!
                        plateContentsMathesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMathesRecipe)
                {
                    // �����ǿ� �´� ��ᰡ ��!.
                    Debug.Log("player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
                
            }
        }

        // no matches found! 
        // player did not deliver a Correct recipe.
        Debug.Log("player did not delivered the correct recipe!");
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
        return;
    }

    public List<RecipeSO> GetWaitingRecipeSOLiST()
    {
        return waitingRecipeSOList;
    }
}