[T4Scaffolding.Scaffolder(Description = "Enter a description of Pages here")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, Position = 0)][string]$ContextTypeName,  
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$context = Get-ProjectType $ContextTypeName
$pattern = "DbSet<([^>]+)>"

$context.Members | ForEach {
	$typeName = $_.Type.AsFullName
	If ([System.Text.RegularExpressions.Regex]::IsMatch($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)) {	
		$memberName = $_.Name
		$match = [System.Text.RegularExpressions.Regex]::Match($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		$entityTypeName = $match.Groups[1].Value
		$entityName = $entityTypeName.Substring($entityTypeName.LastIndexOf('.') + 1)
		$entityType = Get-ProjectType $entityTypeName
		$primaryKey = Get-PrimaryKey $entityTypeName
		$controllerName = $memberName + "Controller"
		$pagePath = Join-Path Pages $memberName

		Add-ProjectItemViaTemplate $pagePath -Template PagesTemplate `
			-Model @{ TableName = $memberName; EntityName = $entityName; ControllerName = $controllerName; PrimaryKey = $primaryKey; } `
			-SuccessMessage "Added Pages output at {0}" `
			-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	}
}