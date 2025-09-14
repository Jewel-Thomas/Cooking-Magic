using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeArray;
    private float fryingTime;
    private float burningTime;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;

    private void Start()
    {
        state = State.Idle;
    }


    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    break;
                case State.Frying:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        cuttingProgressNormalized = fryingTime / fryingRecipeSO.fryingTimeMax
                    });
                    fryingTime += Time.deltaTime;
                    if (fryingTime >= fryingRecipeSO.fryingTimeMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObjects(fryingRecipeSO.output, this);
                        burningTime = 0f;
                        burningRecipeSO = GetBurningRecipeSOFromInput(GetKitchenObject().GetKitchenObjectsSO());
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        cuttingProgressNormalized = burningTime / burningRecipeSO.burningTimeMax
                    });
                    burningTime += Time.deltaTime;
                    if (burningTime >= burningRecipeSO.burningTimeMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObjects(burningRecipeSO.output, this);
                        burningTime = 0f;
                        state = State.Burned;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            cuttingProgressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // It does not have kitchen object
            if (player.HasKitchenObject())
            {
                // The player is carrying something that can be cut
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectsSO());
                    fryingTime = 0f;
                    state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        cuttingProgressNormalized = fryingTime / fryingRecipeSO.fryingTimeMax
                    });
                }
            }
            else
            {
                // Nothing needs to be done
            }
        }
        else
        {
            // It does have kitchen object
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            cuttingProgressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    cuttingProgressNormalized = 0f
                });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        // Do nothing
    }

    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputkitchenObjectsSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(inputkitchenObjectsSO);
        if (fryingRecipeSO)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputkitchenObjectsSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromInput(inputkitchenObjectsSO);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOFromInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
