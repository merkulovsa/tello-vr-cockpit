using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TelloLib;

public class TelloFlightTracking : MonoBehaviour
{
    struct LocalState
    {
        public int flyMode;
        public int height;
        public int verticalSpeed;
        public int flySpeed;
        public int eastSpeed;
        public int northSpeed;
        public int flyTime;
        public bool flying;//
        public bool downVisualState;
        public bool droneHover;
        public bool eMOpen;
        public bool onGround;
        public bool pressureState;
        public int batteryPercentage;//
        public bool batteryLow;
        public bool batteryLower;
        public bool batteryState;
        public bool powerState;
        public int droneBatteryLeft;
        public int droneFlyTimeLeft;
        public int cameraState;//
        public int electricalMachineryState;
        public bool factoryMode;
        public bool frontIn;
        public bool frontLSC;
        public bool frontOut;
        public bool gravityState;
        public int imuCalibrationState;
        public bool imuState;
        public int lightStrength;//
        public bool outageRecording;
        public int smartVideoExitMode;
        public int temperatureHeight;
        public int throwFlyTimer;
        public int wifiDisturb;//
        public int wifiStrength;// = 100;//
        public bool windState;//
        //From log
        public float velX;
        public float velY;
        public float velZ;
        public float posX;
        public float posY;
        public float posZ;
        public float posUncertainty;
        public float velN;
        public float velE;
        public float velD;
        public float quatX;
        public float quatY;
        public float quatZ;
        public float quatW;
    }

    LocalState localState = new LocalState();
    GameObject lastFlightPoint = null;
    Vector3 originalPosition;
    Vector3 originalRotation;
    float heightOffset;
    bool isStarted = false;

    public GameObject flightPoint;

    public void StartTracking() {
        isStarted = true;
    }

    public void StopTracking() {
        isStarted = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocalState();

        if (isStarted) {
            transform.eulerAngles = GetCurrentRotation();

            if (localState.flyMode == 6) {
                BeforeTakeOff();
            } else {
                transform.position = GetCurrentPosition();

                if (lastFlightPoint == null) {
                    DrawFlightPoint(transform.position);
                } else if (Vector3.Distance(transform.position, lastFlightPoint.transform.position) > 0.01f) {
                    DrawFlightPoint(transform.position);
                }
            }
        }

        
    }

    Vector3 GetCurrentPosition() {
        return new Vector3(
            localState.posX - originalPosition.x, 
            localState.posY - originalPosition.y, 
            localState.posZ - originalPosition.z
        );
    }

    Vector3 GetCurrentRotation() {
        var euler = Tello.state.toEuler();
        var pitch = -(float)euler[0] * (180 / Mathf.PI);
        var roll = -(float)euler[1] * (180 / Mathf.PI);
        var yaw = (float)euler[2] * (180 / Mathf.PI);

        return new Vector3(pitch, yaw, roll);
    }

    void DrawFlightPoint(Vector3 position) {
        var nextFlightPoint = Instantiate(flightPoint);
        nextFlightPoint.transform.parent = flightPoint.transform.parent;
        nextFlightPoint.transform.position = position;
        var lineRenderer = nextFlightPoint.GetComponent<LineRenderer>();

        if (lastFlightPoint != null) {
            lineRenderer.SetPosition(0, position - lastFlightPoint.transform.position);
        }

        lastFlightPoint = nextFlightPoint;
    }

    void BeforeTakeOff() {
        originalPosition = GetCurrentPosition();
        heightOffset = localState.height;
    }

    void UpdateLocalState() {
        localState.flyMode = Tello.state.flyMode; 
        localState.height = Tello.state.height; 
        localState.verticalSpeed = Tello.state.verticalSpeed; 
        localState.flySpeed = Tello.state.flySpeed; 
        localState.eastSpeed = Tello.state.eastSpeed; 
        localState.northSpeed = Tello.state.northSpeed; 
        localState.flyTime = Tello.state.flyTime; 
        localState.flying = Tello.state.flying; 
        localState.downVisualState = Tello.state.downVisualState; 
        localState.droneHover = Tello.state.droneHover; 
        localState.eMOpen = Tello.state.eMOpen; 
        localState.onGround = Tello.state.onGround; 
        localState.pressureState = Tello.state.pressureState; 
        localState.batteryPercentage = Tello.state.batteryPercentage; 
        localState.batteryLow = Tello.state.batteryLow; 
        localState.batteryLower = Tello.state.batteryLower; 
        localState.batteryState = Tello.state.batteryState; 
        localState.powerState = Tello.state.powerState; 
        localState.droneBatteryLeft = Tello.state.droneBatteryLeft; 
        localState.droneFlyTimeLeft = Tello.state.droneFlyTimeLeft; 
        localState.cameraState = Tello.state.cameraState; 
        localState.electricalMachineryState = Tello.state.electricalMachineryState; 
        localState.factoryMode = Tello.state.factoryMode; 
        localState.frontIn = Tello.state.frontIn; 
        localState.frontLSC = Tello.state.frontLSC; 
        localState.frontOut = Tello.state.frontOut; 
        localState.gravityState = Tello.state.gravityState; 
        localState.imuCalibrationState = Tello.state.imuCalibrationState; 
        localState.imuState = Tello.state.imuState; 
        localState.lightStrength = Tello.state.lightStrength; 
        localState.outageRecording = Tello.state.outageRecording; 
        localState.smartVideoExitMode = Tello.state.smartVideoExitMode; 
        localState.temperatureHeight = Tello.state.temperatureHeight; 
        localState.throwFlyTimer = Tello.state.throwFlyTimer; 
        localState.wifiDisturb = Tello.state.wifiDisturb; 
        localState.wifiStrength = Tello.state.wifiStrength; 
        localState.windState = Tello.state.windState; 
        localState.velX = Tello.state.velX; 
        localState.velY = Tello.state.velY; 
        localState.velZ = Tello.state.velZ; 
        localState.posX = Tello.state.posX; 
        localState.posY = Tello.state.posY; 
        localState.posZ = Tello.state.posZ; 
        localState.posUncertainty = Tello.state.posUncertainty; 
        localState.velN = Tello.state.velN; 
        localState.velE = Tello.state.velE; 
        localState.velD = Tello.state.velD; 
        localState.quatX = Tello.state.quatX; 
        localState.quatY = Tello.state.quatY; 
        localState.quatZ = Tello.state.quatZ; 
        localState.quatW = Tello.state.quatW; 
    }
}
