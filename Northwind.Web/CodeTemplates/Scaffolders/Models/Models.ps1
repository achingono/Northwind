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
$controllerNamespace = $defaultNamespace + ".Controllers"
$modelNamespace = $defaultNamespace + ".Models"
$pattern = "DbSet<([^>]+)>"

$context.Members | ForEach {
	$typeName = $_.Type.AsFullName
	If ([System.Text.RegularExpressions.Regex]::IsMatch($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)) {	
		$memberName = $_.Name
		$match = [System.Text.RegularExpressions.Regex]::Match($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		$entityTypeName = $match.Groups[1].Value
		$entityType = Get-ProjectType $entityTypeName
		$modelTypeName = $entityTypeName.Substring($entityTypeName.LastIndexOf('.') + 1) + "Model"
		$modelPath = (Join-Path Models $modelTypeName) + ".generated"
			
		#Write-Host Generating Model for $entityTypeName

		Add-ProjectItemViaTemplate $modelPath -Template ModelTemplate `
			-Model @{ Namespace = $modelNamespace; 
						TypeName = $modelTypeName; 
						EntityType = $entityType; } `
			-SuccessMessage "Added Model output at {0}" `
			-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	}
}