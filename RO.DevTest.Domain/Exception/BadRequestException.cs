using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace RO.DevTest.Domain.Exception
{
    /// <summary>
    /// Returns a <see cref="HttpStatusCode.BadRequest"/> to
    /// the request
    /// </summary>

    public class BadRequestException : ApiException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        
        public BadRequestException(IdentityResult result) : base("Erro de validação")
        {
            ErrorMessages = new List<string>();
            foreach (var error in result.Errors)
            {
                ErrorMessages.Add(error.Description);
            }
        }

        public BadRequestException(string error) : base(error)
        {
            ErrorMessages = new List<string> { error };
        }
        
        public BadRequestException(ValidationResult validationResult) : base("Erro de validação")
        {
            ErrorMessages = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                ErrorMessages.Add(error.ErrorMessage);
            }
        }

        public List<string> ErrorMessages { get; }
        
        public object GetErrorResponse()
        {
            return new
            {
                StatusCode = (int)StatusCode,
                Message = "A requisição não foi concluída devido a erros.",
                Errors = ErrorMessages
            };
        }
    }
}