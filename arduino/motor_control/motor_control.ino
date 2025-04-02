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
const int horizStepperDelay = 750;
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



void setup() {
  // Setup vertical stepper
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
  moveMotor(verticalStepper, verticalMotorString.toInt());
  moveMotor(horizStepper, horizMotorString.toInt());
  moveMotor(tableStepper, tableMotorString.toInt()); // 1000 full rotation
  moveMotor(cameraStepper, cameraMotorString.toInt());
  
  Serial.println("Motors moved -> Vertical: " + verticalMotorString + " Arm: " + horizMotorString + " Table: " + tableMotorString + " Camera: " + cameraMotorString);
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
    digitalWrite(stepper[stepPinIndex],HIGH); 
    delayMicroseconds(stepper[delayIndex]); 
    digitalWrite(stepper[stepPinIndex],LOW); 
    delayMicroseconds(stepper[delayIndex]); 
  }
}

void homeMotors() {

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
