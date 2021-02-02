using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Areas.Admin.Validation
{
    public class MaxLengthAttribute : ValidationAttribute, IClientValidatable
    {
        public MaxLengthAttribute(int maxLength) : base("Tối đa " + maxLength + " ký tự")
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; set; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("length", MaxLength);
            rule.ValidationType = "maxlength";
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var length = value.ToString().Length;
                if (length > MaxLength)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}