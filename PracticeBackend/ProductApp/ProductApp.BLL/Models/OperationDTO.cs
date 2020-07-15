using System;
using ProductApp.DAL.Constants;

namespace ProductApp.BLL.Models
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public OperationType OperationType { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
