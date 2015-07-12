/*
 Copyright (C) 2015 Jose Laruta <jose.laruta@ieee.org>

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU General Public License
 version 2 as published by the Free Software Foundation.
 
 Update 2015
 */

/**
 * envia las inclinaciones obtenidas del acelerometro del mpu6050 por rf 
 *
 * NODO TRANSMISOR
 * envia los datos cada 100 ms
 */
 
//librerias para el mpu6050
#include "I2Cdev.h"
#include "MPU6050.h"
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    #include "Wire.h"
#endif
//----------------------

//librerias para el modulo rf
#include <RF24Network.h>
#include <RF24.h>
#include <SPI.h>
//---------------------------

//MPU6050
MPU6050 accelgyro;  //objeto

int16_t ax, ay, az;   //aceleraciones
int16_t gx, gy, gz;   //giroscopio
//inclinaciones
double angleYZ;       
double angleXZ;
//----------------------------

//RF
RF24 radio(9,10);                    // nRF24L01(+) radio attached using Getting Started board 

RF24Network network(radio);          // Network uses that radio
//direcciones
const uint16_t this_node = 01;        // Address of our node in Octal format
const uint16_t other_node = 00;       // Address of the other node in Octal format
//-----------------------------
const unsigned long interval = 20; //ms  // How often to send 'hello world to the other unit

unsigned long last_sent;             // When did we last send?
unsigned long packets_sent;          // How many have we sent already

//estructura para los datos a mandar
struct datos_t{
  int vel_izq;
  int vel_der;
};

struct payload_t {                  // Structure of our payload
  unsigned long ms;
  unsigned long counter;
};
//velocidades a mandar
int vel1 = 0;
int vel2 = 0;
//-------------------
void setup(void)
{
  //inicia el puerto serial
  Serial.begin(57600);
  Serial.println("transmisor de inclinaciones");
  //--------------------------
  
  //iniciar el IMU (I2C)
  #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    Wire.begin();
  #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
    Fastwire::setup(400, true);
  #endif
  Serial.println("Initializing I2C devices...");
  accelgyro.initialize();
  Serial.println(accelgyro.testConnection() ? "MPU6050 connection successful" : "MPU6050 connection failed");

  //-----------------------

  //inicia el rf (SPI)
  SPI.begin();
  radio.begin();
  network.begin(/*channel*/ 90, /*node address*/ this_node);
  //-----------------------

  
}

void loop() {
  
  network.update();                          // Check the network regularly

  
  unsigned long now = millis();              // If it's time to send a message, send it!
  if ( now - last_sent >= interval  )
  {
    last_sent = now;
    //recupera datos IMU
    accelgyro.getAcceleration(&ax, &ay, &az);
    //calcula inclinacion
    angleYZ = atan2((double)ay , (double)az);
    angleYZ = angleYZ*(57.2958);
  
    angleXZ = atan2((double)ax , (double)az);
    angleXZ = angleXZ*(57.2958);
    int iy = (int)angleYZ;
    int ix = (int)angleXZ;
    
    datos_t datos = {iy, ix};
    
    RF24NetworkHeader header(/*to node*/ other_node);
    bool ok = network.write(header,&datos,sizeof(datos));
    if (ok)
      Serial.println("ok.");
    else
      Serial.println("failed.");

    
  }
}


