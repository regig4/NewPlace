from ast import Num
import json
from flask import Flask

import numpy as np
import pandas as pd

import tensorflow as tf
from tensorflow import keras
from sklearn.preprocessing import StandardScaler, MinMaxScaler
from sklearn.model_selection import train_test_split

app = Flask(__name__)

@app.route("/")
def hello_world():
    user_NN = tf.keras.models.Sequential([
        tf.keras.layers.Dense(256, activation='relu'),            
        tf.keras.layers.Dense(128, activation='relu'),            
        tf.keras.layers.Dense(32)            
    ])
    advertisement_NN = tf.keras.models.Sequential([
        tf.keras.layers.Dense(256, activation='relu'),            
        tf.keras.layers.Dense(128, activation='relu'),            
        tf.keras.layers.Dense(32),            
    ])
    
    num_user_features = 8
    # create the user input and point to the base network
    input_user = tf.keras.layers.Input(shape=(num_user_features))
    vu = user_NN(input_user)
    vu = tf.linalg.l2_normalize(vu, axis=1)

    num_item_features = 8
    # create the item input and point to the base network
    input_item = tf.keras.layers.Input(shape=(num_item_features))
    vm = advertisement_NN(input_item)
    vm = tf.linalg.l2_normalize(vm, axis=1)

    # compute the dot product of the two vectors vu and vm
    output = tf.keras.layers.Dot(axes=1)([vu, vm])

    # specify the inputs and output of the model
    model = tf.keras.Model([input_user, input_item], output)
    
    model.summary()

    tf.random.set_seed(1)
    cost_fn = tf.keras.losses.MeanSquaredError()
    opt = keras.optimizers.Adam(learning_rate=0.01)
    model.compile(optimizer=opt,
                  loss=cost_fn)
    # likes for sale, likes for rent, likes for exchange, likes houses, likes flats, likes rooms, no of observed no of reviews
    model.fit([[[0.9, 0.1, 0.1, 0.9, 0.6, 0.1, 0.9, 0.9],
               [0.9, 0.1, 0.1, 0.5, 0.9, 0.1, 0.9, 0.9],
               [0.2, 0.9, 0.1, 0.2, 0.1, 0.9, 0.9, 0.9]],
    # is for sale, is for rent, is for exchange, is house, is flat, is room, no of observations, no of reviews
              [[1, 0, 0, 1, 0, 0, 0.9, 0.9],
               [0, 1, 0, 1, 0, 0, 0.9, 0.5],
               [0, 0, 1, 0, 1, 0, 0.9, 0.9]]],
              [[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
               [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1]],
               epochs=30)

    return json.dumps({ "data": "asdf" })