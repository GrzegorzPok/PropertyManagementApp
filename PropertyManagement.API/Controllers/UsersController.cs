using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;

namespace PropertyManagement.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class UsersController
    {
        private readonly IPropertyManagementRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IPropertyManagementRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
    }
}