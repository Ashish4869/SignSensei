import tensorflow as tf
from tensorflow.keras.preprocessing.image import ImageDataGenerator

# Set the environment variable to the GPU index
import os
os.environ['CUDA_VISIBLE_DEVICES'] = '0'

# Define the device strategy
strategy = tf.distribute.OneDeviceStrategy(device='/gpu:0')

# Define the image data generator for data augmentation
train_datagen = ImageDataGenerator(
    rescale=1./255,
    rotation_range=10,
    zoom_range=0.1,
    width_shift_range=0.1,
    height_shift_range=0.1,
    horizontal_flip=True,
    vertical_flip=False
)

test_datagen = ImageDataGenerator(rescale=1./255)

# Convert the data generators to GPU compatible format
with strategy.scope():
    train_generator = train_datagen.flow_from_directory(
        '../input/asl-alphabet/asl_alphabet_train/asl_alphabet_train/',
        target_size=(224, 224),
        batch_size=32,
        class_mode='categorical'
    )

    test_generator = test_datagen.flow_from_directory(
        '../input/asl-alphabet/asl_alphabet_test/asl_alphabet_test/',
        target_size=(224, 224),
        batch_size=32,
        class_mode='categorical'
    )

# Define the model architecture
with strategy.scope():
    pretrainedModel = tf.keras.applications.MobileNet(
        input_shape=(224, 224, 3),
        include_top=False,
        weights='imagenet',
        pooling='avg'
    )
    pretrainedModel.trainable = False

    inputs = pretrainedModel.input

    x = tf.keras.layers.Dense(256, activation='relu')(pretrainedModel.output)
    x = tf.keras.layers.Dense(512, activation='relu')(x)

    outputs = tf.keras.layers.Dense(29, activation='softmax')(x)

    model = tf.keras.Model(inputs=inputs, outputs=outputs)

    # Define the optimizer
    optimizer = tf.keras.optimizers.Adam(learning_rate=0.001)

    # Compile the model
    model.compile(optimizer=optimizer, loss='categorical_crossentropy', metrics=['accuracy'])

# Train the model
with strategy.scope():
    history = model.fit(
        train_generator,
        epochs=20,
        validation_data=test_generator
    )

model.save('./main_model.h5')