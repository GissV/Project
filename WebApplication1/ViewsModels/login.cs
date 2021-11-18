using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;//Iniciamos el namespace ModelBinding Validation

namespace WebApplication1.CustomValidation

{

    public class login : Attribute, IModelValidator//Acceder a la interfaz IModelValidator

    {

        public string ErrorMessage { get; set; }

        /// <summary>

        /// Función para la retornar validación - función con inyección de dependencia

        /// </summary>

        /// <param name="context"></param>

        /// <returns></returns>

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)

        {

            return new List<ModelValidationResult>

                {

                    new ModelValidationResult("", ErrorMessage)

                };

        }

    }

}