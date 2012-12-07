[T4Scaffolding.Scaffolder(Description = "Enter a description of Generator here")][CmdletBinding()]
param(
	[parameter(Mandatory = $true, Position = 0)][string]$ContextTypeName,  
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$context = Get-ProjectType $ContextTypeName
$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$eventsNamespace = $defaultNamespace + ".WebEvents"
$pattern = "DbSet<([^>]+)>"

$context.Members | ForEach {
	$typeName = $_.Type.AsFullName
	If ([System.Text.RegularExpressions.Regex]::IsMatch($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)) {	
		$memberName = $_.Name
		$match = [System.Text.RegularExpressions.Regex]::Match($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		$entityTypeName = $match.Groups[1].Value
		$entityType = Get-ProjectType $entityTypeName
		$primaryKey = Get-PrimaryKey $entityTypeName
		$className = $entityTypeName.Substring($entityTypeName.LastIndexOf('.') + 1)
		$entityNamespace = $entityTypeName.Substring(0, $entityTypeName.LastIndexOf('.'))
		$eventsPath = (Join-Path WebEvents $className) + "WebEvents.generated"
			
		#Write-Host Generating Model for $entityTypeName

		Add-ProjectItemViaTemplate $eventsPath -Template WebEventsTemplate `
			-Model @{ Namespace = $eventsNamespace; 
						DefaultNamespace = $defaultNamespace;
						EntityNamespace = $entityNamespace;
						EntityType = $entityType;
						PrimaryKey = $primaryKey } `
			-SuccessMessage "Added WebEvents output at {0}" `
			-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	}
}