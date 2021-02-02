using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Areas.Admin.Validation
{
    public class NoSpaceAttribute : ValidationAttribute, IClientValidatable
    {
        public NoSpaceAttribute() : base("Không được chứa khoảng trắng trong chuỗi")
        {
            NoSpace = true;
        }

        public bool NoSpace { get; set; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("nospace", NoSpace);
            rule.ValidationType = "nospace";
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var space = value.ToString().IndexOf(' ');
                if (space >= 0)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}