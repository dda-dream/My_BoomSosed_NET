# ���� � ������� ����������
$rootPath = Get-Location

# �������� ��� .cs �����
$files = Get-ChildItem -Path $rootPath -Recurse -Include *.cs

Write-Host "������� ��������� ������..."

$modifiedFiles = @()

foreach ($file in $files) {
    try {
        # ������ �������� ���������� � ��������� Windows-1251
        $originalContent = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::GetEncoding(1251))

        # ������ ���������� � UTF-8 ��� ��������� (��� BOM)
        $utf8NoBom = New-Object System.Text.UTF8Encoding $false
        $utf8Content = [System.IO.File]::ReadAllText($file.FullName, $utf8NoBom)

        # ���� ���������� ��������� > ����������
        if ($originalContent -eq $utf8Content) {
            Write-Host "���� '$($file.Name)' ��� � ���������� ���������. ���������."
            continue
        }

        Write-Host "����������� ����: $($file.FullName)"

        # ��������� � UTF-8 � BOM
        $utf8WithBom = New-Object System.Text.UTF8Encoding $true
        [System.IO.File]::WriteAllText($file.FullName, $originalContent, $utf8WithBom)

        # ��������� � ������ ���������
        $modifiedFiles += $file.FullName

    }
    catch {
        Write-Warning "������ ��� ��������� ����� '$($file.FullName)': $_"
    }
}

# ������� �������� ������ ��������� ������
if ($modifiedFiles.Count -gt 0) {
    Write-Host "`n���� �������� ��������� �����:" -ForegroundColor Green
    foreach ($f in $modifiedFiles) {
        Write-Host "- $f"
    }
}
else {
    Write-Host "`n�� ���� �������� �� ������ �����." -ForegroundColor Yellow
}