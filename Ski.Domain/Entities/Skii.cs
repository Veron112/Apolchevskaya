using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Domain.Entities
{
    public class Skii
    {
        [Key]
        public int SkiId { get; set; } // id блюда
        public string SkiName { get; set; } // название блюда
        public string Description { get; set; } // описание блюда

        public string Price { get; set; }//цена
         
        public string? Image { get; set; } // имя файла изображения 

        // Навигационные свойства
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
