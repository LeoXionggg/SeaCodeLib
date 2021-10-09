$NewName='NewProjName'
$OldName='ApiTemplApiProject'

$OutputEncoding = New-Object System.Text.UTF8Encoding

Write-Host ' ===�����滻����ʼ=== ' -ForegroundColor Yellow
Get-ChildItem -Path '.' -Name -File -Recurse -Exclude $MyInvocation.MyCommand.Name | ForEach-Object {
    (Get-Content $_ -encoding utf8).replace($OldName,$NewName) | Set-Content $_ -encoding utf8
	Write-Host ' *�����ļ�*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_ -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
Write-Host ' ===�����滻�������=== ' -ForegroundColor Yellow
Start-Sleep -s 1
Write-Host ' ===�ļ���������ʼ=== ' -ForegroundColor Yellow
Get-ChildItem -Path '.' -Recurse  -Include $OldName* | ForEach-Object {
	Rename-Item $_.FullName$_.FullName.Replace($OldName,$NewName)
	Write-Host ' *�ļ�����*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_.FullName -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
#�ļ�������Ҫѭ��2�β��ܸ���ȫ��
#��һ��ֻ������һ�㵱ǰĿ¼��ԭ��֪
Start-Sleep -s 1
Get-ChildItem -Path '.' -Recurse  -Include $OldName* | ForEach-Object {
	Rename-Item $_.FullName$_.FullName.Replace($OldName,$NewName)
	Write-Host ' *�ļ�����*: '-ForegroundColor Gray -BackgroundColor White -NoNewLine
	Write-Host $_.FullName -ForegroundColor Black -BackgroundColor White -NoNewLine
	Write-Host '...Done! ' -ForegroundColor Blue -BackgroundColor White
}
Write-Host ' ===�ļ������������=== ' -ForegroundColor Yellow
Write-Host '�����������...' -NoNewLine
$null = [Console]::ReadKey('?')
