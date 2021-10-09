using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTemplApiProject.Core.Service
{
    public interface ISysExLogService
    {
        Task ClearExLog();

        Task<dynamic> QueryExLogPageList([FromQuery] ExLogPageInput input);
    }
}
