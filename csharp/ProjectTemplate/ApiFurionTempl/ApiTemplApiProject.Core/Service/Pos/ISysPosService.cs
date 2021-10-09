using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTemplApiProject.Core.Service
{
    public interface ISysPosService
    {
        Task AddPos(AddPosInput input);

        Task DeletePos(DeletePosInput input);

        Task<SysPos> GetPos([FromQuery] QueryPosInput input);

        Task<dynamic> GetPosList([FromQuery] PosInput input);

        Task<dynamic> QueryPosPageList([FromQuery] PosInput input);

        Task UpdatePos(UpdatePosInput input);
    }
}
