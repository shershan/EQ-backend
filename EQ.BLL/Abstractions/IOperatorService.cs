using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;

namespace EQ.BLL.Abstractions
{
    public interface IOperatorService
    {
        OperatorModel CreateOperator(string email, string password);

        IEnumerable<OperatorModel> GetOperators();

        void DeleteOperator(Guid id);
    }
}
