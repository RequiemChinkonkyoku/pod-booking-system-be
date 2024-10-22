using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IMethodService
    {
        public Task<List<Method>> GetAllMethod();
    }
}
