const int stepPinIndex = 0;
const int dirPinIndex = 1;
const int enPinIndex = 2;
const int delayIndex = 3;

const int tableDirPin = 2; 
const int tableStepPin = 3; 
const int tableEnPin = 4;
const int tableStepperDelay = 2000;
int tableStepper[] = {tableStepPin, tableDirPin, tableEnPin, tableStepperDelay}; 

const int horizDirPin = 5; 
const int horizStepPin = 6; 
const int horizEnPin = 7;
const int horizStepperDelay = 15000;
int horizStepper[] = {horizStepPin, horizDirPin, horizEnPin, horizStepperDelay}; 

// const int verticalStepPin = 5; 
// const int verticalDirPin = 2; 
// const int verticalEnPin = 8;
// const int verticalStepperDelay = 2000;
// int verticalStepper[] = {verticalStepPin, verticalDirPin, verticalEnPin, verticalStepperDelay};  



void setup() {
  // Setup vertical stepper
  pinMode(tableStepPin, OUTPUT);
  pinMode(tableDirPin, OUTPUT);
  pinMode(tableEnPin, OUTPUT);

  pinMode(horizStepPin, OUTPUT);
  pinMode(horizDirPin, OUTPUT);
  pinMode(horizEnPin, OUTPUT);

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
  String armMotorString = getValue(input, ',', 1);
  String tableMotorString = getValue(input, ',', 2);
  String cameraMotorString = getValue(input, ',', 3);
  moveMotor(tableStepper, 1000);
  moveMotor(horizStepper, 200);
  Serial.println("Vertical: " + verticalMotorString + " Arm: " + armMotorString + " Table: " + tableMotorString + " Camera: " + cameraMotorString);
}

void moveMotor(int stepper[], int steps) {
  int absSteps = abs(steps);
  digitalWrite(stepper[enPinIndex], LOW);
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
  digitalWrite(stepper[enPinIndex], HIGH);
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
