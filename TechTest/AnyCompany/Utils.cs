using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AnyCompany
{
    public class Utils
    {
        public string ValidateModel<T>(T model)
        {
            StringBuilder sb = new StringBuilder();
            var validationresults = new List<ValidationResult>();
            var ctx = new ValidationContext(model);
            Validator.TryValidateObject(model, ctx, validationresults, true);
            validationresults.ForEach(x =>
            {
                sb.AppendFormat($"{x.ErrorMessage} {Environment.NewLine}");
            });

            return sb?.ToString() ?? "";
        }

    }
}
