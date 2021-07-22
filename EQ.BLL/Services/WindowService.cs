using EQ.BLL.Abstractions;
using EQ.Constants;
using EQ.DAL.Models;
using EQ.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EQ.BLL.Services
{
    internal class WindowService : BaseDbService.BaseDbServices, IWindowsService
    {
        public WindowService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public WindowModel CreateWindow(string name)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var existedWindow = uow.Repository<Window>().Get(x => x.WindowName == name).FirstOrDefault();
                if (existedWindow == null)
                {
                    var window = new Window()
                    {
                        WindowName = name
                    };

                    uow.Repository<Window>().Add(window);
                    uow.Save();

                    return new WindowModel()
                    {
                        Id = window.Id,
                        Name = window.WindowName
                    };
                }

                return null;
            });
        }

        public IEnumerable<WindowModel> GetWindows()
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                return uow.Repository<Window>().Get().Select(x => new WindowModel()
                {
                    Id = x.Id,
                    Name = x.WindowName
                });
            });
        }

        public void DeleteWindow(Guid id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var window = uow.Repository<Window>().Get(id);
                if (window != null)
                {
                    uow.Repository<Window>().Delete(window);
                    uow.Save();
                }
            });
        }

        public AssignedWindowModel AssigneWindow(Guid windowId, Guid? operatorId, Guid? serviceId)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var existedWindow = uow.Repository<Window>().Get(windowId);
                if (existedWindow != null)
                {
                    if(operatorId.HasValue)
                    {
                        var existedOperator = uow.Repository<User>().Include(x => x.Role).Where(x => x.Id == operatorId.Value && x.Role.RoleName == RoleConstatns.Operator).FirstOrDefault();
                        if(existedOperator != null)
                        {
                            existedWindow.User = existedOperator;
                        }
                    }
                    else
                    {
                        existedWindow.UserId = null;
                    }

                    if (serviceId.HasValue)
                    {
                        var existedService = uow.Repository<Service>().Get(serviceId.Value);
                        if (existedService != null)
                        {
                            existedWindow.Service = existedService;
                        }
                    }
                    else
                    {
                        existedWindow.ServiceId = null;
                    }

                    uow.Repository<Window>().Update(existedWindow);
                    uow.Save();

                    return new AssignedWindowModel()
                    {
                        WindowId = existedWindow.Id,
                        OperatorId = existedWindow.UserId,
                        ServiceId = existedWindow.ServiceId
                    };
                }

                return null;
            });
        }

        public AssignedWindowModel ChangeOpenStatus(Guid windowId, bool isOpen)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var window = uow.Repository<Window>().Include(x => x.Service).Include(x => x.User).Where(x => x.Id == windowId).FirstOrDefault();
                if(window != null)
                {
                    window.IsOpen = isOpen;

                    uow.Repository<Window>().Update(window);
                    uow.Save();

                    return new AssignedWindowModel()
                    {
                        WindowId = window.Id,
                        WindowName = window.WindowName,
                        IsOpen = window.IsOpen,
                        OperatorId = window.UserId,
                        OperaroeName = window.User?.Email,
                        ServiceId = window.ServiceId,
                        ServiceName = window.Service?.ServiceName
                    };
                }

                return null;
            });
        }
    }
}
