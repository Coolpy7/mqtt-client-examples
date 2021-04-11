# Java MQTT 客户端使用示例

- 客户端库：[Eclipse Paho Java Client](https://github.com/eclipse/paho.mqtt.java)
- 构建工具：Maven

by http://coolpy.net

## 编译
```
mvn compile
``` 

## 运行
```bash
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample"

# TLS
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b tls://iotiot.net:8883"

# Websocket
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b ws://iotiot.net:8083/mqtt"

# Websocket over TLS
mvn exec:java -Dexec.mainClass="net.coolpy.mqtt.MqttExample" -Dexec.args="-b wss://iotiot.net:8084/mqtt"
```

参数：
```
Args:
-h Help information
-b MQTT broker url [default: tcp://iotiot.net:1883]
-a Publish/Subscribe action [default: publish]
-u Username [default: coolpy7]
-z Password [default: public]
-c Clean session [default: true]
-t Publish/Subscribe topic [default: test/topic]
-q QoS [default: 0]
```