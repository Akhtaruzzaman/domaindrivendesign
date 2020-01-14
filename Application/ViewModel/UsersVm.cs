using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel
{
    public class UsersVm : Entity
    {
        #region Main Entity
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; } 
        #endregion
        public Users ToEntity()
        {
            return new Users()
            {
                Name = this.Name,
                Address = this.Address,
                Mobile = this.Mobile,
                UserId = this.UserId,
                Password = this.Password,

                Id = this.Id,
                CreatedAt = this.CreatedAt,
                CreatedBy = this.CreatedBy,
                CreatedFrom = this.CreatedFrom,
                UpdatedAt = this.UpdatedAt,
                UpdatedBy = this.UpdatedBy,
                UpdatedFrom = this.UpdatedFrom
            };
        }
    }
}
