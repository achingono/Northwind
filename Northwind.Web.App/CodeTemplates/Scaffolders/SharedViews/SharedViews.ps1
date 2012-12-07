[T4Scaffolding.Scaffolder(Description = "Enter a description of Generator here")][CmdletBinding()]
param(
	[parameter(Mandatory = $true, Position = 0)][string]$ContextTypeName,  
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Add = $false,
	[switch]$Edit = $false,
	[switch]$List = $false,
	[switch]$Display = $false,
	[switch]$All = $false,
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
		$primaryKey = Get-PrimaryKey $entityTypeName
		$controllerTypeName = $memberName
		$modelTypeName = $entityTypeName.Substring($entityTypeName.LastIndexOf('.') + 1) + "Model"
		$modelType = Get-ProjectType $modelTypeName
		$viewPath = Join-Path Shared $memberName
			
		#Write-Host List Path: (Join-Path $viewPath _List)

		if ($Add -or $All) {
			Add-ProjectItemViaTemplate (Join-Path $viewPath _Add) -Template AddViewTemplate `
				-Model @{ Name = $memberName;
						  Type = $modelType; 
						  EntityType = $entityType
						  PrimaryKey = $primaryKey } `
				-SuccessMessage "Added Generator output at {0}" `
				-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
		}

		if ($Edit -or $All) {
			Add-ProjectItemViaTemplate (Join-Path $viewPath _Edit) -Template EditViewTemplate `
				-Model @{ Name = $memberName;
						  Type = $modelType; 
						  EntityType = $entityType
						  PrimaryKey = $primaryKey } `
				-SuccessMessage "Added Generator output at {0}" `
				-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
		}

		if ($List -or $All) {
			Add-ProjectItemViaTemplate (Join-Path $viewPath _List) -Template ListViewTemplate `
				-Model @{ Name = $memberName;
						  Type = $modelType; 
						  EntityType = $entityType
						  PrimaryKey = $primaryKey } `
				-SuccessMessage "Added Generator output at {0}" `
				-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
		}

		if ($Display -or $All) {
			Add-ProjectItemViaTemplate (Join-Path $viewPath _Display) -Template DisplayViewTemplate `
				-Model @{ Name = $memberName;
						  Type = $modelType; 
						  EntityType = $entityType
						  PrimaryKey = $primaryKey } `
				-SuccessMessage "Added Generator output at {0}" `
				-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
		}
	}
}