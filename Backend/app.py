import cv2
from cvzone.HandTrackingModule import HandDetector
import numpy as np
from keras.models import load_model
import sys
from utils.constants import Mode, NOT_FOUND
from utils.utility import save_in_detection_file, save_in_status_file

# clear session
from keras.backend import clear_session
clear_session()

# Initiallly, set is_running status to False since the AI is not running
save_in_status_file(False)

# Disable scientific notation for clarity
np.set_printoptions(suppress=True)

# Load the model
model = load_model("./model/model.h5", compile=False)

# Load the labels
labels = {}
with open("./model/labels.txt", "r") as f:
    for line in f:
        index = int(line.split(' ')[0])
        label = line.split(' ')[1][0]
        labels[index] = label

# Prints the supported labels
print("Application supported labels are:")
print(labels)

# Load the model and the camera
cap = cv2.VideoCapture(0)
detector = HandDetector(maxHands=1)

# Set the parameters
mode = Mode.GUI.value
if len(sys.argv) > 1 and sys.argv[1] == '--headless':
    mode = Mode.HEADLESS.value

# Set intiial values
save_in_detection_file(NOT_FOUND, 0)

print("Application is now running...")
# Set is_running status to True since the AI is running
save_in_status_file(True)

# Run the AI
while True:
        # If the user presses q, stop the AI
        if cv2.waitKey(1) & 0xFF == ord('q'):
            save_in_status_file(False)
            break

        success, img = cap.read()
        img_output = img.copy()
        hands, img = detector.findHands(img)

        if hands:
            # Grab the webcamera's image.
            ret, image = cap.read()

            # Resize the raw image into (224-height,224-width) pixels
            image = cv2.resize(image, (224, 224), interpolation=cv2.INTER_AREA)

            # Show the image in a window
            if mode == Mode.GUI.value:
                cv2.imshow("Sign Type detector", image)
                cv2.imshow("Hand Presence Detector", img_output)

            # Make the image a numpy array and reshape it to the models input shape.
            image = np.asarray(image, dtype=np.float32).reshape(1, 224, 224, 3)

            # Normalize the image array
            image = (image / 127.5) - 1

            # Predicts the model
            prediction = model.predict(image)
            index = np.argmax(prediction)
            label_name = labels[index]
            confidence_score = prediction[0][index]

            # Print prediction and confidence score
            print("Class:", label_name)
            print("Confidence Score:", str(
                np.round(confidence_score * 100))[:-2], "%")

            # Save the prediction and confidence score in a file
            save_in_detection_file(label_name, int(
                str(np.round(confidence_score * 100))[:-2]))
        else:
            # If no hand is detected, set the detected sign to NOT_FOUND
            save_in_detection_file(NOT_FOUND, 0)

cap.release()
cv2.destroyAllWindows()
