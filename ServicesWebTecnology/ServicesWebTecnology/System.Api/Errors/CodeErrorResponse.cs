using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebApi.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tiene autoirzación para este recurso",
                404 => " El Recurso no se encontro",
                500 => " Se producieron errores en el servidor",
                _ => null

            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
