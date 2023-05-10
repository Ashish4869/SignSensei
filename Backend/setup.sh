function setup {
    echo "Creating virtual env..."
    python -m venv ./venv
    echo "Entering virtual env..."
    source ./venv/Scripts/activate
    echo "Installing requirements..."
    pip install -r requirements.txt
    echo "Requirements installed!"
    echo "Virtual env setup completed!"
    echo "Please run either local.sh or main.sh."
}

if [ ! -d ./venv ]; then
    echo "You do not have virtual env set up, installing now"
    setup
else 
    echo "You already have virtual env set up!"
    echo "Reinstall? (y/n)"
    read reinstall
    if [ $reinstall == "y" ]; then
        echo "Deleting virtual env..."
        rm -rf ./venv
        setup
    else
        echo "Quitting setup..."
    fi
fi