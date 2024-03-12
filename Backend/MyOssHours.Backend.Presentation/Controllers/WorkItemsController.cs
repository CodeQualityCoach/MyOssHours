using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.WorkItems;
using MyOssHours.Backend.Presentation.Models;
using MyOssHours.Backend.Presentation.Requests;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WorkItemsController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public WorkItemsController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    }

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
}