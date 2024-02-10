﻿using AutoMapper;
using Domain.Library.Entities.LOG;
using Infrastructure.Library.ApplicationContext.DatabaseContext;
using Infrastructure.Library.BaseService;
using Infrastructure.Library.Models.Controls;
using Infrastructure.Library.Models.DTOs.LOG;
using Infrastructure.Library.Models.Views.LOG;
using Infrastructure.Library.Patterns;

namespace Infrastructure.Library.Repositories.LOG
{
    public abstract class NLogRepository : GenericRepository<NLog, NLogDTO, NLogView>, IGenericQueries
    {
        protected NLogRepository(IUnitOfWork<ContextDbApplication> unitOfWork) : base(unitOfWork)
        {
        }

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