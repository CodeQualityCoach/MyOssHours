using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.ProjectHours;
using MyOssHours.Backend.Presentation.Models;
using MyOssHours.Backend.Presentation.Requests;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProjectHoursController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectHoursController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    }

    // POST api/<ProjectController>
    [HttpPost]
    public async Task<ProjectHourModel> Post([FromBody] CreateProjectHourCommand command)
    {
        var request = new CreateProjectHour.Command
        {
            Project = command.Project,
            WorkItem = command.WorkItem,
            Date = command.Date,
            Duration = TimeSpan.FromMinutes(command.DurationInMinutes),
            User = command.User,
            Description = command.Description
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<ProjectHourModel>(response.ProjectHour);
    }


}