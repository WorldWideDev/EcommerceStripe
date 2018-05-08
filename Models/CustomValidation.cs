using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class MinAttribute : ValidationAttribute
    {
        private int _min;
        public MinAttribute(int min)
        {
            _min = min;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (int)value < _min ? new ValidationResult($"Value must be larger than {_min}") : ValidationResult.Success; 
        }
    }
    public class MaxOrderAttribute : ValidationAttribute
    {
        private int _max;
        public MaxOrderAttribute(int max)
        {
            _max = max;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (int)value > _max ? new ValidationResult($"Slow down buddy! You can only order {_max} at a time") : ValidationResult.Success; 
        }
    }
}