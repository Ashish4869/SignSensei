from flask import Flask
import json
server = Flask(__name__)


@server.route('/detect')
def get_sign():
    result = {}
    file = open('./json/detection.json')
    result = json.load(file)
    return result, 200


@server.route('/status')
def get_status():
    result = {}
    file = open('./json/status.json')
    result = json.load(file)
    return result, 200


if __name__ == '__main__':
    server.run()
