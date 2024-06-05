using Apolchevskaya.Services;
using Ski.Domain.Entities;
using Ski.Domain.Models;
using System.IO;
using System.Security.Policy;
using System.Text.Json;

namespace Apolchevskaya.Services
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        public async Task<ResponseData<Skii>> CreateProductAsync(Skii product, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Подготовить объект, возвращаемый методом
            var responseData = new ResponseData<Skii>();

            // Послать запрос к API для сохранения объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);
            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Не удалось создать объект:{ response.StatusCode}";
            return responseData;
            }

            // Если файл изображения передан клиентом
            if (formFile != null)
            {

                // получить созданный объект из ответа Api-сервиса
                var skii = await response.Content.ReadFromJsonAsync<Skii>();

                // создать объект запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}{skii.SkiId}")
                };

                // Создать контент типа multipart form-data
                var content = new MultipartFormDataContent();

                // создать потоковый контент из переданного файла
                var streamContent = new StreamContent(formFile.OpenReadStream());

                // добавить потоковый контент в общий контент по именем "image"
                content.Add(streamContent, "image", formFile.FileName);

                // поместить контент в запрос
                request.Content = content;

                // послать запрос к Api-сервису
                response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Не удалось сохранить изображение:{ response.StatusCode} ";
                }
            }
            return responseData;
        }


        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<Skii>> GetProductByIdAsync(int id)
        {
            var apiUrl = $"{httpClient.BaseAddress.AbsoluteUri}{id}";
            var response = await httpClient.GetFromJsonAsync<Skii>(apiUrl);
            return new ResponseData<Skii>() { Data = response };
        }

        public async Task<ResponseData<ListModel<Skii>>>GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>
            {
                { "pageNo", pageNo.ToString() }
            };
            if (!String.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }
            var query = QueryString.Create(queryData);
            var result = await httpClient.GetAsync(uri + query.Value);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content
                .ReadFromJsonAsync<ResponseData<ListModel<Skii>>>();
            };
            var response = new ResponseData<ListModel<Skii>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }

        public Task UpdateProductAsync(int id, Skii product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}