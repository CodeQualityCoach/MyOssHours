using System.Globalization;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.WorkItems;
using MyOssHours.Backend.Presentation.Models;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/v1/[controller]")]
[Authorize()]
[ApiController]
public class WorkItemsController
    (IMediator mediator, IMapper mapper, IHttpContextAccessor httpContext)
    : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    // GET: api/<ProjectController>
    [HttpGet]
    public async Task<IEnumerable<WorkItemModel>> Get([FromQuery] GetWorkItemsQuery query)
    {
        var request = new GetWorkItems.Query
        {
            Offset = query.Offset,
            Size = query.Size,
            Project = query.Project
        };
        var response = await _mediator.Send(request);
        return response.WorkItems.Select(x => _mapper.Map<WorkItemModel>(x));
    }

    [HttpPost]
    public async Task<ActionResult<WorkItemModel>> Post([FromBody] CreateWorkItemCommand command)
    {
        var request = new CreateWorkItem.Command
        {
            Project = command.Project,
            Name = command.Name,
            Description = command.Description
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<WorkItemModel>(response.WorkItem);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] DeleteWorkItemCommand command)
    {
        var request = new DeleteWorkItem.Command
        {
            WorkItem = command.WorkItem,
        };
        var response = await _mediator.Send(request);

        return response.Success ? Ok() : BadRequest(response.Exception?.Message ?? "unknown fatal and anonymous exception");
    }

    #region Commands

    public class DeleteWorkItemCommand
    {
        public Guid WorkItem { get; set; }
    }
    
    public class GetWorkItemsQuery
    {
        public required int Offset { get; set; }
        public required int Size { get; set; }
        public required Guid Project { get; set; }
    }
    
    public class CreateWorkItemCommand
    {
        public required Guid Project { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
    #endregion
}