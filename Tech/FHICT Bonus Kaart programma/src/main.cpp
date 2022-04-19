#include <LiquidCrystal.h>
#include <SPI.h>
#include <MFRC522.h>
 
#define rPin A0
#define gPin A1
#define bPin A2
int rgbPins[3] = { rPin, gPin, bPin };

#define SS_PIN 10
#define RST_PIN 9
MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance.
String uid= "";
String lastUid= "replace text";
bool isCardPresent = false;

bool opTijd;
unsigned long lastCardPresent;
unsigned long lastDataShown;

String serialMessage = "";
bool communicatieGestart = false;
bool checkInProtocol = false;
const char  protocolStartChar = '#';
const char  protocolEndChar  = '%';

int dataIndex = 0;
String naam = "";
int streak = 0;
int punten = 0;

const int rs = 8, en = 5, d4 = 6, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

void setup() 
{
  Serial.begin(9600);   // Initiate a serial communication
  SPI.begin();      // Initiate  SPI bus
  mfrc522.PCD_Init();   // Initiate MFRC522
  Serial.println("Approximate your card to the reader...");
  Serial.println();
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  for (int i = 0; i < 3; i++) {
    pinMode(rgbPins[i], OUTPUT);
  }
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
}

void rgLedWrite(int rVal, int gVal)
{
  analogWrite(rPin, rVal);
  analogWrite(gPin, gVal);
}

void UpdateInterface(String name, bool opTijd, int streak, int punten) {
  lastDataShown = millis();
  String streakString = String(streak);
  lcd.print(name + ": " + punten + " punten");
  lcd.setCursor(0,1);
  lcd.print("Streak: " + streakString);
  if (opTijd) {
    rgLedWrite(0, 150);
  }
  else {
    rgLedWrite(150, 0);
  }
}

void InterpretSerialInput() {
  if (Serial.available() > 0) {
    char readChar = Serial.read();
    if (readChar == protocolStartChar) {
      serialMessage = "";
      communicatieGestart = true;
    }
    else if (communicatieGestart) {
      if (checkInProtocol) {
        if (readChar == ' ') {
          switch (dataIndex)
          {
            case 0:
              naam = serialMessage;
              break;
            case 1:
              streak = serialMessage.toInt();
              break;
            case 2:
              punten = serialMessage.substring(1).toInt();
              break;
            default:
              break;
          }
          dataIndex++;
          serialMessage = "";
        }
      }

      if (serialMessage.startsWith("CheckIn ")) {
        checkInProtocol = true;
        serialMessage = "";
      }
      
      if (readChar == protocolEndChar) {
        communicatieGestart = false;
        dataIndex = 0;
        serialMessage = "";
        //Do stuff
        if (checkInProtocol) {
          UpdateInterface(naam, opTijd, streak, punten);
          lastDataShown = millis();
          Serial.println("\nNaam: " + naam);
          Serial.print("op tijd?: ");
          Serial.println(opTijd);
          Serial.print("Streak: ");
          Serial.println(streak);
          Serial.print("punten: ");
          Serial.println(punten);
          checkInProtocol = false;
        }
      }
      else {
        serialMessage += readChar;
      }
    }
  }
}

void loop() 
{
  InterpretSerialInput();
  //Wipe Interface 2 seconds after displaying data
  // if (millis() - lastDataShown >= 2000) {
  //   lcd.clear();
  //   rgLedWrite(0, 0);
  //   lastUid = "replace text";
  // }

  //Card reading logic
  if (millis() - lastCardPresent >= 500) {
    // Look for new cards
    if ( ! mfrc522.PICC_IsNewCardPresent()) 
    {
      return;
    }

    // Select one of the cards
    if ( ! mfrc522.PICC_ReadCardSerial()) 
    {
      return;
    }
    
    //Show UID on serial monitor
    // Serial.print("UID tag :");
    for (byte i = 0; i < mfrc522.uid.size; i++) 
    {
      uid = "";
      Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
      Serial.print(mfrc522.uid.uidByte[i], HEX);
      uid.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
      uid.concat(String(mfrc522.uid.uidByte[i], HEX));
    }
    if (uid != lastUid) {
      if (millis() / 1000 > 10) {
        opTijd = false;
        Serial.println("Laat");
      }
      else {
        opTijd = true;
        Serial.println("Op tijd");
      }
      //(to implement)
      //Send data to the C# program
      //Get data from the C# program
      //Read data to the interface
      //UpdateInterface(uid, opTijd, 4, 25);
      lastUid = uid;
    }
    lastCardPresent = millis();
  }
}