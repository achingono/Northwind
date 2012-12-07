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
$pattern = "DbSet<([^>]+)>"

Add-ProjectItemViaTemplate (Join-Path Extensions GenericExtensions) -Template GenericExtensionsTemplate `
	-Model @{ Namespace = $defaultNamespace; } `
	-SuccessMessage "Added Extensions output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$context.Members | ForEach {
	$typeName = $_.Type.AsFullName
	If ([System.Text.RegularExpressions.Regex]::IsMatch($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)) {	
		$memberName = $_.Name
		$match = [System.Text.RegularExpressions.Regex]::Match($typeName, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		$entityTypeName = $match.Groups[1].Value
		$entityNamespace = $entityTypeName.Substring(0, $entityTypeName.LastIndexOf('.'))
		$entityType = Get-ProjectType $entityTypeName
		$primaryKey = Get-PrimaryKey $entityTypeName
		$className = $entityTypeName.Substring($entityTypeName.LastIndexOf('.') + 1)
		$modelTypeName =  $className + "Model"
		$extensionPath = (Join-Path Extensions $className) + "Extensions.generated"
			
		#Write-Host Generating Model for $entityTypeName

		Add-ProjectItemViaTemplate $extensionPath -Template ExtensionsTemplate `
			-Model @{ Namespace = $defaultNamespace; 
						ClassName = $className;
						ModelTypeName = $modelTypeName; 
						EntityType = $entityType;
						EntityNamespace = $entityNamespace;
						PrimaryKey = $primaryKey } `
			-SuccessMessage "Added Extensions output at {0}" `
			-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	}
}