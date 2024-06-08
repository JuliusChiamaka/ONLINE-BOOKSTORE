﻿using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces
{
    public interface IPurchaseHistoryService
    {
        Task<IEnumerable<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId);
    }
}
