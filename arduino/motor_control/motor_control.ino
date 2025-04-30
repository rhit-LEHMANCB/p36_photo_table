#include <ezButton.h>

const int stepPinIndex = 0;
const int dirPinIndex = 1;
const int enPinIndex = 2;
const int delayIndex = 3;

const int tableDirPin = 2; 
const int tableStepPin = 3; 
const int tableEnPin = 4;
const int tableStepperDelay = 1000;
int tableStepper[] = {tableStepPin, tableDirPin, tableEnPin, tableStepperDelay}; 

const int horizDirPin = 5; 
const int horizStepPin = 6; 
const int horizEnPin = 7;
const int horizStepperDelay = 2500;
int horizStepper[] = {horizStepPin, horizDirPin, horizEnPin, horizStepperDelay}; 

const int verticalDirPin = 8; 
const int verticalStepPin = 9; 
const int verticalEnPin = 10;
const int verticalStepperDelay = 250;
int verticalStepper[] = {verticalStepPin, verticalDirPin, verticalEnPin, verticalStepperDelay};  

const int cameraDirPin = 11; 
const int cameraStepPin = 12; 
const int cameraEnPin = 13;
const int cameraStepperDelay = 1250;
int cameraStepper[] = {cameraStepPin, cameraDirPin, cameraEnPin, cameraStepperDelay};  

const int limitSwitchVerticalPin = 25;
ezButton limitSwitchVertical(limitSwitchVerticalPin);

void setup() {
  pinMode(tableStepPin, OUTPUT);
  pinMode(tableDirPin, OUTPUT);
  pinMode(tableEnPin, OUTPUT);
  digitalWrite(tableEnPin,LOW);

  pinMode(horizStepPin, OUTPUT);
  pinMode(horizDirPin, OUTPUT);
  pinMode(horizEnPin, OUTPUT);
  digitalWrite(horizEnPin,LOW);

  pinMode(verticalStepPin, OUTPUT);
  pinMode(verticalDirPin, OUTPUT);
  pinMode(verticalEnPin, OUTPUT);
  digitalWrite(verticalEnPin,LOW);

  pinMode(cameraStepPin, OUTPUT);
  pinMode(cameraDirPin, OUTPUT);
  pinMode(cameraEnPin, OUTPUT);
  digitalWrite(cameraEnPin,LOW);

  Serial.begin(9600);
}

void handleInput(String input) {
  if (input == "ping") {
    Serial.println("pong");
  } else if (input == "home") {
    handleHomeCommand();
  } else {
    handleMotorCommand(input);
  }
}

void handleHomeCommand() {
  homeMotors();
  Serial.println("homed");
}

void handleMotorCommand(String input) {
  String verticalMotorString = getValue(input, ',', 0);
  String horizMotorString = getValue(input, ',', 1);
  String tableMotorString = getValue(input, ',', 2);
  String cameraMotorString = getValue(input, ',', 3);
  long verticalSteps = verticalMotorString.toInt();
  long horizSteps = horizMotorString.toInt();
  long tableSteps = tableMotorString.toInt();
  long cameraSteps = cameraMotorString.toInt();
  moveMotors(verticalSteps, horizSteps, tableSteps, cameraSteps);
  // moveMotor(verticalStepper, verticalMotorString.toInt());
  // moveMotor(horizStepper, horizMotorString.toInt());
  // moveMotor(tableStepper, tableMotorString.toInt());
  // moveMotor(cameraStepper, cameraMotorString.toInt());
  
  Serial.println("Motors moved -> Vertical: " + verticalMotorString + " Arm: " + horizMotorString + " Table: " + tableMotorString + " Camera: " + cameraMotorString);
}

void moveMotors(long verticalSteps, long horizSteps, long tableSteps, long cameraSteps) {
  long absVerticalSteps = abs(verticalSteps);
  long absHorizSteps = abs(horizSteps);
  long absTableSteps = abs(tableSteps);
  long absCameraSteps = abs(cameraSteps);

  // setup directions
  digitalWrite(verticalStepper[dirPinIndex], verticalSteps < 0 ? LOW : HIGH); // negative goes up
  digitalWrite(horizStepper[dirPinIndex], horizSteps < 0 ? LOW : HIGH); // negative extends
  digitalWrite(tableStepper[dirPinIndex], tableSteps < 0 ? LOW : HIGH);
  digitalWrite(cameraStepper[dirPinIndex], cameraSteps < 0 ? LOW : HIGH); // negative spins clockwise

  long currentVerticalSteps = 0;
  long currentHorizSteps = 0;
  long currentTableSteps = 0;
  long currentCameraSteps = 0;


  unsigned long microSeconds = micros();
  unsigned long prevMicrosVertical = micros();
  unsigned long prevMicrosHoriz = micros();
  unsigned long prevMicrosTable = micros();
  unsigned long prevMicrosCamera = micros();

  bool verticalSteppedHigh = false;
  bool horizSteppedHigh = false;
  bool tableSteppedHigh = false;
  bool cameraSteppedHigh = false;

  while (currentVerticalSteps < absVerticalSteps || currentHorizSteps < absHorizSteps || currentTableSteps < absTableSteps || currentCameraSteps < absCameraSteps) {
    microSeconds = micros();

    // Serial.println("Micros" + String(microSeconds) + ", " + String(prevMicrosVertical) + ", " + String(prevMicrosHoriz) + ", " + String(prevMicrosTable) + ", " + String(prevMicrosCamera));

    // Vertical Stepper
    if (currentVerticalSteps < absVerticalSteps) {
      if (!verticalSteppedHigh && microSeconds - prevMicrosVertical >= verticalStepper[delayIndex]) {
        // Serial.println("pulse vertical high");
        verticalSteppedHigh = true;
        prevMicrosVertical = micros();
        digitalWrite(verticalStepper[stepPinIndex], HIGH);
      }
      else if (microSeconds - prevMicrosVertical >= verticalStepper[delayIndex]) {
        // Serial.println("pulse vertical low: " + String(currentVerticalSteps));
        verticalSteppedHigh = false;
        prevMicrosVertical = micros();
        digitalWrite(verticalStepper[stepPinIndex], LOW);
        currentVerticalSteps++;
      }
    }

    // Horizontal Stepper
    if (currentHorizSteps < absHorizSteps) {
      if (!horizSteppedHigh && microSeconds - prevMicrosHoriz >= horizStepper[delayIndex]) {
        horizSteppedHigh = true;
        prevMicrosHoriz = microSeconds;
        digitalWrite(horizStepper[stepPinIndex], HIGH);
      }
      else if (microSeconds - prevMicrosHoriz >= horizStepper[delayIndex]) {
        horizSteppedHigh = false;
        prevMicrosHoriz = microSeconds;
        digitalWrite(horizStepper[stepPinIndex], LOW);
        currentHorizSteps++;
      }
    }

    // Table Stepper
    if (currentTableSteps < absTableSteps) {
      if (!tableSteppedHigh && microSeconds - prevMicrosTable >= tableStepper[delayIndex]) {
        tableSteppedHigh = true;
        prevMicrosTable = microSeconds;
        digitalWrite(tableStepper[stepPinIndex], HIGH);
      }
      else if (microSeconds - prevMicrosTable >= tableStepper[delayIndex]) {
        tableSteppedHigh = false;
        prevMicrosTable = microSeconds;
        digitalWrite(tableStepper[stepPinIndex], LOW);
        currentTableSteps++;
      }
    }

    // Camera Stepper
    if (currentCameraSteps < absCameraSteps) {
      if (!cameraSteppedHigh && microSeconds - prevMicrosCamera >= cameraStepper[delayIndex]) {
        cameraSteppedHigh = true;
        prevMicrosCamera = microSeconds;
        digitalWrite(cameraStepper[stepPinIndex], HIGH);
      }
      else if (microSeconds - prevMicrosCamera >= cameraStepper[delayIndex]) {
        cameraSteppedHigh = false;
        prevMicrosCamera = microSeconds;
        digitalWrite(cameraStepper[stepPinIndex], LOW);
        currentCameraSteps++;
      }
    }

    delayMicroseconds(25);
  }
}

void stepMotor(int stepper[]) {
  digitalWrite(stepper[stepPinIndex],HIGH); 
  delayMicroseconds(stepper[delayIndex]); 
  digitalWrite(stepper[stepPinIndex],LOW); 
  delayMicroseconds(stepper[delayIndex]); 
}

void moveMotor(int stepper[], int steps) {
  int absSteps = abs(steps);
  if (steps < 0) {
    // Counterclockwise
    digitalWrite(stepper[dirPinIndex],LOW);
  } else {
    // Clockwise
    digitalWrite(stepper[dirPinIndex],HIGH);
  }
  for(int x = 0; x < absSteps; x++) {
    stepMotor(stepper);
  }
}

void homeMotors() {
  limitSwitchVertical.loop();

  while(limitSwitchVertical.getState() != HIGH) {
    limitSwitchVertical.loop(); // MUST call the loop() function first

    moveMotor(verticalStepper, -1);
  }
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length()-1;

  for(int i=0; i<=maxIndex && found<=index; i++){
    if(data.charAt(i)==separator || i==maxIndex){
        found++;
        strIndex[0] = strIndex[1]+1;
        strIndex[1] = (i == maxIndex) ? i+1 : i;
    }
  }

  return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
}

void loop() {

  if (Serial.available() > 0) {

    String input = Serial.readString();

    if (input.length() > 0) {
      handleInput(input);
    }
  
    delay(100);

  }
  
}
