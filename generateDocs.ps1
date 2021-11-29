﻿param($username, $password, [switch]$Buildserver)

$PSScriptRoot = split-path -parent $MyInvocation.MyCommand.Definition

$module = "Scrambler"

Import-Module "$PSScriptRoot\packages\Unic.Bob.Keith\Keith"

Import-Module "$PSScriptRoot\src\$module" -Force

New-PsDoc -Module $module -Path "$PSScriptRoot\docs\" -OutputLocation "$PSScriptRoot\docs-generated" -Verbose
New-GitBook "$PSScriptRoot\docs-generated" "$PSScriptRoot\temp" $username $password -Buildserver:$Buildserver




