using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Exceptions
{
    /// <summary>
    /// Excepción lanzada cuando se intenta crear un restaurante con un nombre que ya existe en el sistema.
    /// </summary>
    public class RestaurantAlreadyExistsException : Exception
    {
        public RestaurantAlreadyExistsException()
        {
        }
        public RestaurantAlreadyExistsException(string message)
            : base(message)
        {
        }

        public RestaurantAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
