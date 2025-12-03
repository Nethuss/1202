git config user.name "Nethuss"
git config user.email "ddidcvee@gmail.com"

git config --system --unset credential.helper

dotnet --version // 8 
dotnet new install Avalonia.Templates

dotnet new avalonia.app -o MyApp


dotnet new --uninstall Avalonia.Templates // удалить темплейты avalonia


dotnet new install Avalonia.Templates::11.0.10 // для dotnet 8

dotnet new avalonia.app -o Success

dotnet build

dotnet run

dotnet add package Npgsql