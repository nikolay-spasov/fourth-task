﻿using System.Collections.Generic;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDetailsDTO>> GetOrdersForCustomer(string customerId);
    }
}