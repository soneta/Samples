using WebApiService;
using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiService.Interfaces;

[assembly: Service(typeof(ITowarAPI), typeof(TowarWebApi), Published = true)]

namespace WebApiService
{
    public class TowarWebApi : ITowarAPI
    {
        public string NazwaTowaru(string EAN)
        {
            return $"Pewna nazwa towaru EAN: {EAN}";
        }
    }
}
