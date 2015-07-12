# Geomatch!
### **Los Super Amigos** -- Julio de 2015

## Descripción
**Geomatch!** es un videojuego interactivo para PC que intenta abordar la enseñanza de la geografía en 
colegios de manera divertida apoyándose de actividades motrices en vez de sentar al estudiante frente a una computadora
con un teclado y mouse. En este caso **Geomatch!** basa su interfaz en dispositivos capaces de detectar y registrar
gestos naturales como movimientos de la mano e inclinómetros. De esta manera la experiencia en el videojuego es más
nutritiva, la necesidad de practicar varias veces ayuda a memorizar los conceptos de geografía presentados.

### Género
**Geomatch!** es un puzzle interactivo para la plataforma Windows.

### Mecánica del juego
Se presenta un globo terráqueo y varias *piezas* correspondientes a distintos países del mundo, el objetivo del juego es
colocar las piezas en los países adecuados, arrastrándolas hasta el lugar correcto en el planeta.
Para interactuar con el juego se tienen 2 controles especiales: 
 * Las piezas se seleccionan y arrastran utilizando movimientos de la mano derecha.
 * El globo terráqueo se manipula a través de un dispositivo inclinómetro sujetado con la mano izquierda, 
    la inclinación en los ejes *x* e *y* se corresponde con el movimiento angular del globo terráqueo en la pantalla.

### Niveles
El prototipo terminado cuenta con 3 niveles, en cada uno de los niveles se debe posicionar de manera correcta:

1. **Banderas**. Posicionar las banderas en la ubicación del país correspondiente.
 screenshot: 
![alt text][Nivel 1]
2. **Monumentos**. Posicionar las fotos de monumentos característicos.
![alt text][Nivel 2]
3. **Personas**. Trajes típicos y personajes característicos de  distintos países
![alt text][Nivel 3]

### Controles del Juego
Para el juego se diseñaron 2 interfaces especiales:

#### Hand Cursor con MS Kinect
Se utiliza el sensor de profundidad [Kinect](https://www.microsoft.com/en-us/download/details.aspx?id=40278 "Kinect"), el cual es un dispositivo de visión que cuenta con una cámara digital y un sensor de profundidad, este sensor permite detectar y reconocer el movimiento de personas frente a la cámara. Este dispositivo ha revolucionado la industria del videojuego desde su salida, haciendo de la experiencia en videojuegos algo más dinámico y menos estático.

// poner foto
![alt text][kinect]

En **Geomatch!** utilizamos Kinect para detectar la de la mano derecha en relación a la pantalla, para poder interactuar directamente utilizando la mano como un puntero en la pantalla, sin necesidad de ningún dispositivo externo. El movimiento de la mano derecha, se transforma en el movimiento de un puntero en el entorno virtual de **Geomatch!** este puntero sirve para *agarrar* las fichas de la pantalla y *arrastrarlas* a la posición correspondiente en el globo terráqueo.

// dibujo
![alt text][diagrama kinect]

Debido a la carencia de tiempo para desarrollar más interactividad, el control se limita a la posición de la mano derecha. Este dispositivo cuenta con una infinidad de aplicaciones que se pueden explorar con mas tiempo.

#### Detector inalámbrico de inclinación con IMU
Otra parte del control la compone un IMU (Unidad de Medida Inercial), el cual, mediante sensores inerciales, proporciona variables de movimiento como aceleración en 3 ejes y velocidad angular en 3 ejes de rotación, el IMU utilizado es el **MPU 6050**, que cuenta con 6 grados de libertad (3 acc, 3 gyro).La interfaz de comunicación maneja el protocolo serial I2C para enviar los datos a un microcontrolador **atmega 328p**, el mismo que, a través de un módulo inalámbrico de radiofrecuencia **nrf24l01**, envía los datos recibidos por el IMU a la PC que los procesa para obtener la inclinación en los ejes *x* e *y*. La pc cuenta con otro módulo RF+microcontrolador conectado por usb para completar la interfaz inalámbrica.

![alt text][mpu]
![alt text][arduino nano]
![alt text][nrf4l01]

El dispositivo, al ser alimentado por una batería, no necesita cables y libera al jugador a poder moverse libremente. Los componentes: IMU, RF, Micricontroladores que se utilizaron son de bajo costo, lo que hace la construcción de este mando muy accesible y de tamaño reducido. En esta ocasión se tiene un prototipo completamente funcional.

![alt text][foto mando]
![alt text][foto pc]

El análisis de los datos obtenidos del IMU es muy básico, dada la falta de tiempo, con un procesado más exhaustivo, se pueden obtener más datos inerciales, tales como orientación, y cosenos directores. 


[Nivel 1]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/nivel%201.png "Nivel 1"
[Nivel 2]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/nivel%202.png "Nivel 2"
[Nivel 3]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/nivel%203.png "Nivel 3"
[menu]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/menu_inicial.png "Menu"
[kinect]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/menu_inicial.png "kinect"
[diagrama kinect]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/diagrama_kinect.png "diagrama kinect"
[mpu]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/mpu.png "mpu"
[arduino nano]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/nano.png "arduino nano"
[nrf4l01]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/rf.jpg "nrf4l01"


[foto mando]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/foto_mando.png "foto mando"
[foto pc]: https://github.com/tabris2015/geomatch/blob/master/unity/imagen/foto_pc.png "foto pc"
