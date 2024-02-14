﻿using Account.Application.Library.BaseService;
using Account.Application.Library.Models.Controls;
using Account.Application.Library.Models.DTOs.WEB;
using Account.Application.Library.Models.Views.WEB;
using Account.Domain.Library.Entities.WEB;
using Account.Infrastructure.Library.ApplicationContext.DatabaseContext;
using Account.Infrastructure.Library.BaseService;
using Account.Infrastructure.Library.Patterns;
using System;
using System.Collections.Generic;

namespace Account.Application.Library.Repositories.WEB
{
    public abstract class WebServiceRepository : GenericRepository<WebService, WebServiceDTO, WebServiceView>, IBaseQueries
    {
        private readonly UnitOfWork<ContextDbApplication> unitOfWork = new UnitOfWork<ContextDbApplication>();
        //private GenericRepository<Employee> genericRepository;
        //private IEmployeeRepository employeeRepository;
        protected WebServiceRepository(UnitOfWork<ContextDbApplication> unitOfWork) : base(unitOfWork)
        {
            //genericRepository = new GenericRepository<Employee>(unitOfWork);
            //employeeRepository = new EmployeeRepository(unitOfWork);
        }
        protected WebServiceRepository(ContextDbApplication context) : base(context) { }

        public string GetCount()
        {
            throw new NotImplementedException();
        }

        public string Search(string value)
        {
            throw new NotImplementedException();
        }

        public string ShowAll(string paging)
        {
            throw new NotImplementedException();
        }

        public string ShowFromTo(string from, string to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValue<long>> TitleValue()
        {
            throw new NotImplementedException();
        }
    }
}