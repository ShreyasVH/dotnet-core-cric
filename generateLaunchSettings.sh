mkdir -p Properties

echo '{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:45722",
      "sslPort": 44301
    }
  },
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:$PORT",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "MSSQL_IP": "'$MSSQL_IP'",
        "MSSQL_PORT": "'$MSSQL_PORT'",
        "MSSQL_DB": "'$MSSQL_DB'",
        "MSSQL_USER": "'$MSSQL_USER'",
        "MSSQL_PASSWORD": "'$MSSQL_PASSWORD'"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7021;http://localhost:5030",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
' > Properties/launchSettings.json