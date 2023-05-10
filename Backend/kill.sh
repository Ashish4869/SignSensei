app_pid=$(cat .app_pid)
server_pid=$(cat .server_pid)
py reset.py
kill $app_pid
kill $server_pid