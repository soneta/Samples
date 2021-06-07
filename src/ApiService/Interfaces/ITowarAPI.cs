using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiService.Models;

namespace WebApiService.Interfaces
{
    public interface ITowarAPI
    {
        string NazwaTowaru(string EAN);
        TowarModel TowarInfo(string EAN, string NazwaMagazynu);
        bool UtworzTowar(TowarModel towarModel);
    }
}
