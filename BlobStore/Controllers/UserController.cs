using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlobStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlobStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return UserList.Users.Select(x=>$"{x.Name}, ");
        }

        // GET: api/User/HARRY
        [HttpGet("{name}", Name = "Get")]
        public async Task<string> Get(string name)
        {
            var result = await Azure.Azure.Instance().UserProcess(name);
            return result;
        }
    }
}
