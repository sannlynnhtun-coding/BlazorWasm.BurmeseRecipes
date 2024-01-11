using BlazorWasm.BurmeseRecipes.Models;
using System.Net.Http.Json;

namespace BlazorWasm.BurmeseRecipes.Services
{
    public class BurmeseRecipeService
    {
        private readonly HttpClient _httpClient;
        private static List<BurmeseRecipeModel>? BurmeseRecipesLst = null;
        private static List<UserTypeModel>? UserTypeLst = null;
        public BurmeseRecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BurmeseRecipeResonseModel> BurmeseRecipes
            (int pageNo = 1, int pageSize = 4)
        {
            List<BurmeseRecipeModel> lst = new();
            if (BurmeseRecipesLst is null)
            {
                lst = await GetData<BurmeseRecipeModel>("data/BurmeseRecipes.json");
                BurmeseRecipesLst = lst;
            }
            else{
                lst = BurmeseRecipesLst;
            }
            var count = lst.Count();
            var totalPage = count / pageSize;
            if (count % pageSize > 0)
                totalPage++;
            var model = new BurmeseRecipeResonseModel
            {
                DataList = lst.Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .ToList(),
                PageCount = totalPage
            };
            return model;
        }

        public async Task<List<UserTypeModel>?> UserTypes()
        {
            var lst = new List<UserTypeModel>();
            if(UserTypeLst is null)
            {
                lst = await GetData<UserTypeModel>("data/UserTypes.json");
                UserTypeLst = lst;
            }
            else
            {
                lst = UserTypeLst;
            }
            return lst;
        }

        public async Task<UserTypeModel?> UserTypeData(string userCode)
        {
            var lst = await UserTypes();
            return lst.FirstOrDefault(x=> x.UserCode == userCode);
        } 
        private async Task<List<T>?> GetData<T>(string filePath)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(filePath);
        }
    }
}
