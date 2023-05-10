source ./venv/Scripts/activate
echo "Would you like to run the app in headless mode? (y/n)"
read headless

if [ $headless = "y" ]; then
    echo "Running app in headless mode"
    /git-bash.exe -c "py server.py" &
    echo $! > .server_pid
    /git-bash.exe -c "py app.py --headless" &
    echo $! > .app_pid
else
    echo "Running app in development mode"
    /git-bash.exe -c "py server.py" &
    echo $! > .server_pid
    /git-bash.exe -c "py app.py" &
    echo $! > .app_pid
fi