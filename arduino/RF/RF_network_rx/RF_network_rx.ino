/**
 * Simplest possible example of using RF24Network,
 *
 * RECEIVER NODE
 * Listens for messages from the transmitter and prints them out.
 */

#include <RF24Network.h>
#include <RF24.h>
#include <SPI.h>

RF24 radio(9,10);                // nRF24L01(+) radio attached using Getting Started board 

RF24Network network(radio);      // Network uses that radio
const uint16_t this_node = 00;    // Address of our node in Octal format ( 04,031, etc)
const uint16_t other_node = 01;   // Address of the other node in Octal format

struct datos_t{
  float yaw;
  float pitch;
  float roll;
  int16_t mx;
  int16_t my;
  int16_t mz;
};

struct payload_t {                 // Structure of our payload
  unsigned long ms;
  unsigned long counter;
};


void setup(void)
{
  Serial.begin(115200);
  Serial.println("estacion receptora...");
 
  SPI.begin();
  radio.begin();
  network.begin(/*channel*/ 90, /*node address*/ this_node);
  
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
    Serial.print(datos.yaw * 180/M_PI);
    Serial.print(";");
    Serial.print(datos.pitch * 180/M_PI);
    Serial.print(";");
    Serial.print(datos.roll * 180/M_PI);
    Serial.print(";");
    Serial.print(datos.mx);
    Serial.print(";");
    Serial.print(datos.my);
    Serial.print(";");
    Serial.println(datos.mz);
    
    //motIzq(datos.vel_izq);
    //motDer(datos.vel_der);

  }
  
}

