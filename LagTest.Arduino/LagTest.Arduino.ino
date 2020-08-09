/*
 Name:		LagTest.ino
 Created:	09.08.2020 18:24:55
 Author:	Kulagin
*/

#include <Keyboard.h>

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
}

// the loop function runs over and over again until power down or reset
void loop() {
	while(true) {
		if(Serial.available() > 0) {
			Keyboard.write('a');
			while(Serial.read() >= 0); //flush buffer
		}
	}
}
