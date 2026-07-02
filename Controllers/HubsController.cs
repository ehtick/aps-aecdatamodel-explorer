using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class HubsController : ControllerBase
{
	private readonly ILogger<HubsController> _logger;
	private readonly APSService _apsService;

	public HubsController(ILogger<HubsController> logger, APSService forgeService)
	{
		_logger = logger;
		_apsService = forgeService;
	}

	[HttpGet("{project}/contents/{item}/versions")]
	public async Task<ActionResult> ListVersions(string project, string item)
	{
		var tokens = await AuthController.PrepareTokens(Request, Response, _apsService);
		if (tokens == null)
		{
			return Unauthorized();
		}
		var versions = await _apsService.GetVersions(project, item, tokens);
		return Ok(versions);
	}
}
