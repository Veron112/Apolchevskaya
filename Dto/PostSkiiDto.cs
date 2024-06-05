using Ski.Domain.Entities;

namespace Apolchevskaya.Dto
{
    public class PostSkiiDto
    {
        public string SkiName { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }

        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
