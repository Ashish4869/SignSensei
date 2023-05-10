source ./venv/Scripts/activate
py reset.py
/git-bash.exe --hide -c "py server.py" &
echo $! > .server_pid
/git-bash.exe --hide -c "py app.py --headless" & 
echo $! > .app_pid