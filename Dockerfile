# =======================================================
# 1-Cİ MƏRHƏLƏ: DƏRLƏMƏ VƏ NƏŞR (BUILD STAGE)
# .NET 8 SDK imici (bütün alətlər və derləyici buradadır)
# =======================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Docker Layer Caching: Əvvəlcə yalnız layihə (.csproj) fayllarını kopyalayırıq
COPY ["DigitalWallet.Domain/DigitalWallet.Domain.csproj", "DigitalWallet.Domain/"]
COPY ["DigitalWallet.Application/DigitalWallet.Application.csproj", "DigitalWallet.Application/"]
COPY ["DigitalWallet.Infrastructure/DigitalWallet.Infrastructure.csproj", "DigitalWallet.Infrastructure/"]
COPY ["DigitalWallet.WebApi/DigitalWallet.WebApi.csproj", "DigitalWallet.WebApi/"]

# Bütün NuGet paketlərini bərpa edirik (restore)
RUN dotnet restore "DigitalWallet.WebApi/DigitalWallet.WebApi.csproj"

# İndi bütün qalan C# kodlarını kopyalayırıq
COPY . .

# Release rejimində WebApi layihəsini publish edirik
WORKDIR "/src/DigitalWallet.WebApi"
RUN dotnet publish "DigitalWallet.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =======================================================
# 2-Cİ MƏRHƏLƏ: ÇALIŞMA MÜHİTİ (RUNTIME STAGE)
# Yalnız yüngül ASP.NET 8 Runtime (SDK yoxdur, təhlükəsiz və kiçikdir)
# =======================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# .NET 8 üçün standart daxili port 8080-dir
EXPOSE 8080

# 1-ci mərhələdə yaranan hazır faylları buraya köçürürük
COPY --from=build /app/publish .

# Tətbiqi işə salırıq
ENTRYPOINT ["dotnet", "DigitalWallet.WebApi.dll"]
