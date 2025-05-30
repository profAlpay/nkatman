﻿using nkatman.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Services
{
    public interface ISaleService : IService<Sale>
    {
        Task SaleProduct(Sale sale);
    }
}
