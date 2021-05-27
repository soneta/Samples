using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiService.Interfaces
{
    public interface ITowarAPI
    {
        string NazwaTowaru(string EAN);
    }
}
