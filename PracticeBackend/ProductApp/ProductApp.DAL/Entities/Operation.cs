using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductApp.DAL.Constants;

namespace ProductApp.DAL.Entities
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public OperationType OperationType { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Amount { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}