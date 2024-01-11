using BlazorWasm.BurmeseRecipes.Models;

namespace BlazorWasm.BurmeseRecipes.Services;

public class BurmeseRecipeStateContainer
{
    private BurmeseRecipeDetailModel? data;

    public BurmeseRecipeDetailModel Data
    {
        get => data!;
        set
        {
            data = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
