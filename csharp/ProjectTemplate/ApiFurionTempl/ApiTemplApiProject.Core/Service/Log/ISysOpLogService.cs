using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTemplApiProject.Core.Service
{
    public interface ISysOpLogService
    {
        Task ClearOpLog();

        Task<dynamic> QueryOpLogPageList([FromQuery] OpLogPageInput input);
    }
}
