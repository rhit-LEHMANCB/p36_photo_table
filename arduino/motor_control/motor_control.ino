void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
}

void loop() {

  if (Serial.available() > 0) {

    String input = Serial.readString();

    if (input == "ping") {
      Serial.println("pong");
    }

    if (input == "1") {
      delay(1000);

      digitalWrite(LED_BUILTIN, HIGH);

      Serial.println("LED On");
    }

    if (input == "0") {
      delay(1000);

      digitalWrite(LED_BUILTIN, LOW);

      Serial.println("LED Off");
    }
  
    delay(100);

  }
  
}
