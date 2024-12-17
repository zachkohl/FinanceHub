using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace FinanceHub.Models
{
    [DelimitedRecord(","), IgnoreFirst()]
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        [FieldConverter(ConverterKind.Date, "MM/dd/yyyy")]
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public string? OriginalDescription { get; set; }
        public string? BankCatagory { get; set; }
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Amount { get; set; }
        public string? BankStatus { get; set; }
    }
}
