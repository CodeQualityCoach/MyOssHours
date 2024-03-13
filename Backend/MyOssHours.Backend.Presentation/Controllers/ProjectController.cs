using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Projects;
using MyOssHours.Backend.Presentation.Models;
using MyOssHours.Backend.Presentation.Requests;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/v1/[controller]")]
[Authorize()]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator, IMapper mapper, IUserProvider userProvider)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectModel>> Get([FromQuery] GetProjectsQuery query)
    {
        var request = new GetProjects.Query
        {
            Offset = query.Offset,
            Size = query.Size,
            NameLike = query.NameLike
        };
        var response = await _mediator.Send(request);
        return response.Projects.Select(x => _mapper.Map<ProjectModel>(x));
    }

    [HttpGet("{uuid}")]
    public async Task<ProjectModel> Get(Guid uuid)
    {
        var request = new GetProject.Query
        {
            Uuid = uuid
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<ProjectModel>(response.Project);
    }

    // POST api/<ProjectController>
    [HttpPost]
    public async Task<ProjectModel> Post([FromBody] CreateProjectCommand command)
    {
        var request = new CreateProject.Command
        {
            Name = command.Name,
            Description = command.Description,
            Owner = _userProvider.GetCurrentUser().Uuid
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<ProjectModel>(response.Project);
    }

    // PUT api/<ProjectController>/5
    [HttpPut("{uuid}")]
    public void Put(Guid uuid, [FromBody] ProjectModel project)
    {
        throw new NotImplementedException();
    }

    // DELETE api/<ProjectController>/5
    [HttpDelete("{uuid}")]
    public void Delete(Guid uuid)
    {
        throw new NotImplementedException();
    }
}