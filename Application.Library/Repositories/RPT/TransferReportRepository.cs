﻿using AutoMapper;
using Domain.Library.Entities.RPT;
using Infrastructure.Library.BaseService;
using Infrastructure.Library.Models.DTOs.RPT;
using Infrastructure.Library.Models.Views.RPT;

namespace Infrastructure.Library.Repositories.RPT
{
    public abstract class TransferReportRepository : GenericRepository<TransferReport, TransferReportDTO, TransferReportView>, IGenericQueries
    {
        protected TransferReportRepository(IMapper mapper) : base(mapper)
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
    }
}
