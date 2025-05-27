if ! lsof -i :$PORT > /dev/null; then
    echo "Starting"
    dotnet ef database update > migrations.log 2>&1 &
    dotnet publish/dotnet.dll > server.log 2>&1 &
fi