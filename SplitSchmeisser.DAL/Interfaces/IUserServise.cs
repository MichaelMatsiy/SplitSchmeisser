using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.DAL.Interfaces
{
    public interface IUserServise
    {
        void GetUserDebsByGroup(int userId, int groupId);

        void GetUserDebsByGroupPerUrers(int userId, int groupId);

        //void GetUsersByGroup(int groupId);
    }
}
