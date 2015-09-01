param($installPath, $toolsPath, $package, $project)

$file = $project.ProjectItems.Item("7za.exe")

# set 'Copy To Output Directory' to 'Copy Always'
$copyToOutput = $file.Properties.Item("CopyToOutputDirectory")
$copyToOutput.Value = 1
