# Java MQTT client examples

- MQTT client：[Eclipse Paho Java Client](https://github.com/eclipse/paho.mqtt.java)
- Build tool：Maven

by http://coolpy.net

## Compile
```
mvn compile
``` 

## Run
```bash
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample"

# TLS
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b ssl://iotiot.net:8883"

# Websocket
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b ws://iotiot.net:8083/mqtt"

# Websocket over TLS
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b wss://iotiot.net:8084/mqtt"
```

Args:
```
Args:
-h Help informatione
-b MQTT broker url [default: tcp://iotiot.net:1883]
-a Publish/Subscribe action [default: publish]
-u Username [default: coolpy7]
-z Password [default: public]
-c Clean session [default: true]
-t Publish/Subscribe topic [default: test/topic]
-q QoS [default: 0]
```