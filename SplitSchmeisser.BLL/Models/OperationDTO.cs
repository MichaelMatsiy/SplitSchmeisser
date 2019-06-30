using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Models
{
    public class OperationDTO
    {
        public int Id { get; set; }

        public UserDTO Owner { get; set; }
        public GroupDTO Group { get; set; }
        public DateTime DateOfLoan { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
