﻿using Domain.Library.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Library.Entities.RPT
{
    [Table("TransactionReports", Schema = "RPT")]
    public class TransactionReport : GeneralEntity
    {
    }
}
