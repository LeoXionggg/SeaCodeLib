$NewName='NewProjName'
$OldName='ApiTemplApiProject'

$OutputEncoding = New-Object System.Text.UTF8Encoding

Write-Host ' ===内容替换处理开始=== ' -ForegroundColor Yellow
Get-ChildItem -Path '.' -Name -File -Recurse -Exclude $MyInvocation.MyCommand.Name | ForEach-Object {
    (Get-Content $_ -encoding utf8).replace($OldName,$NewName) | Set-Content $_ -encoding utf8
	Write-Host ' *处理文件*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_ -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
Write-Host ' ===内容替换处理完成=== ' -ForegroundColor Yellow
Start-Sleep -s 1
Write-Host ' ===文件更名处理开始=== ' -ForegroundColor Yellow
Get-ChildItem -Path '.' -Recurse  -Include $OldName* | ForEach-Object {
	Rename-Item $_.FullName$_.FullName.Replace($OldName,$NewName)
	Write-Host ' *文件更名*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_.FullName -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
#文件更名需要循环2次才能更名全，
#第一次只更名了一层当前目录，原因不知
Start-Sleep -s 1
Get-ChildItem -Path '.' -Recurse  -Include $OldName* | ForEach-Object {
	Rename-Item $_.FullName$_.FullName.Replace($OldName,$NewName)
	Write-Host ' *文件更名*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_.FullName -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
Write-Host ' ===文件更名处理完成=== ' -ForegroundColor Yellow
Write-Host '按任意键继续...' -NoNewLine
$null = [Console]::ReadKey('?')
