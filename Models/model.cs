using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace lab.Models
{
    public class labData
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string name { get; set; } // Название книги
        public string author { get; set; } // Автор
        public string publ { get; set; } // Издательство
        public int date { get; set; } // Дата написания
        public double price { get; set; } // Цена

        public BaseModelValidationResult Validate()
        {
           var validationResult = new BaseModelValidationResult();
           
           if (string.IsNullOrWhiteSpace(name)) validationResult.Append($"Book title cannot be empty");
           if (string.IsNullOrWhiteSpace(author)) validationResult.Append($"Author name cannot be empty");
           if (string.IsNullOrWhiteSpace(publ)) validationResult.Append($"Publisher cannot be empty");
           if (!(date > 0)) validationResult.Append($"Date must be higher than 0");
           if (!(price > 0)) validationResult.Append($"Price must be higher than 0");

           return validationResult;
        }

        public override string ToString()
        {
            return $"Book title: {name}; Author name: {author}; Publisher: {publ}; Date: {date}; Price: {price}";
        }
    }
}