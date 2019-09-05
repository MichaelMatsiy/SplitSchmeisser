using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IReportGenerateResolver
    {
        Task<T> Enqueue<T>(Func<Task<T>> taskGenerator);
    }
}
