using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;

namespace EQ.BLL.Abstractions
{
    public interface IWindowsService
    {
        WindowModel CreateWindow(string name);

        IEnumerable<WindowModel> GetWindows();

        void DeleteWindow(Guid id);

        AssignedWindowModel AssigneWindow(Guid windowId, Guid? operatorId, Guid? serviceId);

        AssignedWindowModel ChangeOpenStatus(Guid windowId, bool isOpen);
    }
}
