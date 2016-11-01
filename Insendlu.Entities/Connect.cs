using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insendlu.Entities.Connection;

namespace Insendlu.Entities
{
    public class Connect
    {
        private readonly InsendluEntities _insendluEntities;

        public Connect()
        {
            _insendluEntities = new InsendluEntities();

        }

        public InsendluEntities GetConnection()
        {
            return _insendluEntities;
        }
    }
}
