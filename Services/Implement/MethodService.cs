using Models;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class MethodService : IMethodService
    {
        private readonly IRepositoryBase<Method> _methodRepo;

        public MethodService(IRepositoryBase<Method> methodRepo)
        {
            _methodRepo = methodRepo;
        }

        public async Task<List<Method>> GetAllMethod()
        {
            var methods = await _methodRepo.GetAllAsync();

            return methods;
        }

    }
}
