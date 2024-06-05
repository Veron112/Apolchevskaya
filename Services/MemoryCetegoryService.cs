using Ski.Domain.Entities;
using Ski.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Apolchevskaya.Services
{
    public class MemoryCetegoryService: ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
            new Category {Id=1, SkiGroupName="Лыжи горные",
            NormalizedName="горные"},
            new Category {Id=2, SkiGroupName="Лыжи беговые",
            NormalizedName="беговые"}

            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }

    }
}
