import json


def save_in_status_file(is_running=False):
    status_object = {
        "is_running": is_running,
    }
    status_json = json.dumps(status_object)
    with open("./json/status.json", "w") as status_file:
        status_file.write(status_json)


def save_in_detection_file(detected_sign='?', accuracy=0):
    detect_object = {
        'detected_sign': detected_sign,
        "accuracy": accuracy,
    }
    detect_json = json.dumps(detect_object)
    with open("./json/detection.json", "w") as detection_file:
        detection_file.write(detect_json)
