param($rootPath, $toolsPath, $package, $project)

# Bail out if scaffolding is disabled (probably because you're running an incompatible version of T4Scaffolding.dll)
if (-not (Get-Module T4Scaffolding)) { return }

Set-DefaultScaffolder -Name DbContext -Scaffolder T4Scaffolding.EFDbContext -SolutionWide -DoNotOverwriteExistingSetting
Set-DefaultScaffolder -Name Repository -Scaffolder T4Scaffolding.EFRepository -SolutionWide -DoNotOverwriteExistingSetting