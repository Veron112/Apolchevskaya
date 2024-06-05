using Microsoft.AspNetCore.Mvc;
using Ski.Domain.Entities;
using Ski.Domain.Models;

namespace Apolchevskaya.Services
{
    public class MemoryProductService : IProductService
    {
        List<Skii> _skies;
        List<Category> _categories;
        IConfiguration _config;



        public MemoryProductService(ICategoryService categoryService, [FromServices] IConfiguration config)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync()
                .Result
                .Data;

            SetupData();
        }
        /// <summary>
        /// Инициализация списков
        /// </summary>
        public void SetupData()
        {
            Category category = _categories.Find(c => c.NormalizedName.Equals("горные"));
            Category category1 = _categories.Find(c => c.NormalizedName.Equals("беговые"));
            _skies = new List<Skii>
            {
                new Skii {SkiId = 1, SkiName="Atomic горные",
                Description="лыжи",
                Image="Images/атомикгор.jpg",
                Price="300",
                CategoryId =category.Id,
                Category=category },

                new Skii { SkiId = 1, SkiName="Salomon горные",
                Description="лыжи",
                 Image="Images/саломонгор.jpg",
                  Price="300",
                CategoryId=category.Id,
                Category=category},

                new Skii { SkiId = 2, SkiName="Tisa беговые",
                Description="лыжи",
                 Image="Images/тисабег.jpg",
                  Price="300",
                CategoryId=category1.Id,
                Category=category1 },

                new Skii { SkiId = 2, SkiName="Fischer беговые",
                Description="лыжи",
                 Image="Images/фишербег.jpg",
                  Price="300",
                CategoryId=category1.Id,
                 Category=category1 },

                new Skii { SkiId = 2, SkiName="CST беговые",
                Description="лыжи",
                 Image="Images/цстбег.jpg",
                  Price="300",
                CategoryId=category1.Id ,
                Category = category1},

                new Skii { SkiId = 1, SkiName="Fischer горные",
                Description="лыжи",
                 Image="Images/фишергор.jpg",
                  Price="300",
                CategoryId=category.Id,
                Category=category }
            };

        }
        public Task<ResponseData<ListModel<Skii>>> GetProductListAsync(string? categoryNormalizedName, int pageNo /*= 1*/)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Skii>>();

            // Id категории для фильрации
            int? categoryId = null;

            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c =>
                c.NormalizedName.Equals(categoryNormalizedName))
                ?.Id;

            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _skies
            .Where(d => categoryNormalizedName == null || d.CategoryId==categoryId)?
            .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            int totalPages;
            if (pageSize == 0)
            {
                pageSize = -1;
                totalPages = 1;
            }
            else
            {
                totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            }
            // получить общее количество страниц
            
            // получить данные страницы
            var listData = new ListModel<Skii>()
            {
                Items = pageSize==-1? data.ToList() : data.Skip((pageNo - 1) *
            pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить ранные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);

        }

        public Task<ResponseData<Skii>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Skii product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Skii>> CreateProductAsync(Skii product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

    }
}

