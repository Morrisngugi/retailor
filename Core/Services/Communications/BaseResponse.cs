using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Communications
{
    public abstract class BaseResponse<T> where T : class
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Data { get; set; }

        public BaseResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
