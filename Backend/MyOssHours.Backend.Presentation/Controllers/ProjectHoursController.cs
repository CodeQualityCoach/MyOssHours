using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOssHours.Backend.Application.ProjectHours;
using MyOssHours.Backend.Presentation.Contracts.Models;

namespace MyOssHours.Backend.Presentation.Controllers;

[Route("api/v1/[controller]")]
[Authorize()]
[ApiController]
public class ProjectHoursController
    (IMediator mediator, IMapper mapper)
    : ControllerBase
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    // POST api/<ProjectController>
    [HttpPost]
    public async Task<ProjectHourModel> Post([FromBody] CreateProjectHourCommand command)
    {
        var request = new CreateProjectHour.Command
        {
            Project = command.Project,
            WorkItem = command.WorkItem,
            Date = DateOnly.FromDateTime(command.Date),
            Duration = TimeSpan.FromMinutes(command.DurationInMinutes),
            User = command.User,
            Description = command.Description
        };
        var response = await _mediator.Send(request);
        return _mapper.Map<ProjectHourModel>(response.ProjectHour);
    }

    #region Commands

    public class CreateProjectHourCommand
    {
        public Guid Project { get; set; }
        public Guid WorkItem { get; set; }
        public DateTime Date { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid User { get; set; }
        public string? Description { get; set; }
    }

    #endregion
}