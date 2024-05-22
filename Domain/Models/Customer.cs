using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TblCustomer")]
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
