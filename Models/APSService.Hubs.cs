using Autodesk.DataManagement.Model;

public partial class APSService
{
	public async Task<IEnumerable<dynamic>> GetVersions(string projectId, string itemId, Tokens tokens)
	{
		Versions versions = await _dataManagementClient.GetItemVersionsAsync(projectId, itemId, accessToken: tokens.InternalToken);
		return versions.Data;
	}
}
