#include <Servo.h>

Servo thumb;
Servo pointer;
const int offset = 0;

void setup()
{
  Serial.begin(9600);
  thumb.attach(9);
  pointer.attach(10);  
  thumb.write(0+offset);
  pointer.write(30+offset);
}

byte insBuf[1];
byte thumbMode;
byte pointerMode;
void loop()
{
  if(Serial.readBytes((char*)insBuf,1)){//will return 1 if it recieves a byte. will return 0 if it times out
     thumbMode = insBuf[0] & 0x0F;//lower nibble
     pointerMode = insBuf[0] >> 4;//upper nibble
     switch(thumbMode){
       case 0x1://release
         thumb.write(0+offset);
         break;
       case 0x2://brake
         thumb.write(15+offset);
         break;
       case 0xF://hold
         thumb.write(30+offset);
         break;
     }
     
     switch(pointerMode){
       case 0x1://release
         pointer.write(30+offset);
         break;
       case 0x2://brake
         pointer.write(15+offset);
         break;
       case 0xF://holds
         pointer.write(0+offset);
         break;
     }
         
  }else{//serial timeout. release both
    thumb.write(0+offset);
    pointer.write(30+offset);
  }
}
