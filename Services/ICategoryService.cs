using Ski.Domain.Entities;
using Ski.Domain.Models;

namespace Apolchevskaya.Services
{
    public interface ICategoryService
    {
        
/// <summary>
/// Получение списка всех категорий
/// </summary>
/// <returns></returns>
public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }

}

