using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ICodeGeneratorService
    {
        string GenerateRandomString(int string_length);
    }
}
