/*
 Copyright (C) 2012 James Coliz, Jr. <maniacbug@ymail.com>

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU General Public License
 version 2 as published by the Free Software Foundation.
 
 Update 2014 - TMRh20
 */

/**
 * Simplest possible example of using RF24Network,
 *
 * RECEIVER NODE
 * Listens for messages from the transmitter and prints them out.
 */

#include <RF24Network.h>
#include <RF24.h>
#include <SPI.h>

// ----------- definicion de pines para los motores
const int VEL_MOT_IZQ = 5;
const int S1_MOT_IZQ = 2;
const int S2_MOT_IZQ = 4;
const int VEL_MOT_DER = 6;
const int S1_MOT_DER = 7;
const int S2_MOT_DER = 8;



RF24 radio(9,10);                // nRF24L01(+) radio attached using Getting Started board 

RF24Network network(radio);      // Network uses that radio
const uint16_t this_node = 00;    // Address of our node in Octal format ( 04,031, etc)
const uint16_t other_node = 01;   // Address of the other node in Octal format

struct datos_t{
  int ix;
  int iy;
};

struct payload_t {                 // Structure of our payload
  unsigned long ms;
  unsigned long counter;
};


void setup(void)
{
  Serial.begin(9600);
  Serial.println("auto a control remoto...");
 
  SPI.begin();
  radio.begin();
  network.begin(/*channel*/ 90, /*node address*/ this_node);
  Detener();
}

void loop(void){
  
  network.update();                  // Check the network regularly

  
  while ( network.available() ) 
  {     // Is there anything ready for us?
    
    RF24NetworkHeader header;        // If so, grab it and print it out
    datos_t datos;
    //payload_t payload;
    //network.read(header,&payload,sizeof(payload));
    network.read(header, &datos, sizeof(datos));
    //Serial.print("Received packet #");
    Serial.print(datos.ix);
    Serial.print(" ; ");
    Serial.println(datos.iy);
    //motIzq(datos.vel_izq);
    //motDer(datos.vel_der);

  }
  
}

void Detener()
{
  izqStop();
  derStop();

}

void Conducir(int vel)
{
  if (vel > 0)
  {
    izqAd((byte)(abs(vel)));
    derAd((byte)(abs(vel)));
  }
  else
  {
    izqAt((byte)(abs(vel)));
    derAt((byte)(abs(vel)));
  }
}

void motIzq(int vel)
{
  if (vel > 0)
  {
    izqAd((byte)(abs(vel)));
  }
  else
  {
    izqAt((byte)(abs(vel)));
  }
}

void motDer(int vel)
{
  if (vel > 0)
  {
    derAd((byte)(abs(vel)));
  }
  else
  {
    derAt((byte)(abs(vel)));
  }
}

void izqAd(byte vel)
{
  digitalWrite(S1_MOT_IZQ, 1);
  digitalWrite(S2_MOT_IZQ, 0);
  analogWrite(VEL_MOT_IZQ, vel);
}
void izqAt(byte vel)
{
  digitalWrite(S1_MOT_IZQ, 0);
  digitalWrite(S2_MOT_IZQ, 1);
  analogWrite(VEL_MOT_IZQ, vel);
}
void derAd(byte vel)
{
  digitalWrite(S1_MOT_DER, 1);
  digitalWrite(S2_MOT_DER, 0);
  analogWrite(VEL_MOT_DER, vel);
}
void derAt(byte vel)
{
  digitalWrite(S1_MOT_DER, 1);
  digitalWrite(S2_MOT_DER, 0);
  analogWrite(VEL_MOT_DER, vel);
}

void derStop()
{
  digitalWrite(S1_MOT_DER, 1);
  digitalWrite(S2_MOT_DER, 1);
}
void izqStop()
{
  digitalWrite(S1_MOT_IZQ, 1);
  digitalWrite(S2_MOT_IZQ, 1);
  
}
