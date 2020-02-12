using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Communications
{
    public class ServiceResponse<T> : BaseResponse<T> where T : class
    {

        private ServiceResponse(bool success, string message, T entity) : base(success, message, entity)
        { }
        
        public ServiceResponse(T entity) : this(true, string.Empty, entity)
        { }

        public ServiceResponse(string message) : this(false, message, null)
        { }
    }
}
