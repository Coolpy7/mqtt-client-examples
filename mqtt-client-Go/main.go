package main

import (
	"flag"
	"log"
	"mqtt-client-Go/protocols"
)

//iotiot.net
//tcp port 1883
//tls port 8883
//ws port 8083
//wss port 8084

var Host     = flag.String("host", "iotiot.net", "server hostname or IP")
var Port     = flag.Int("port", 1883, "server port")
var Action   = flag.String("action", "pubsub", "pub/sub/pubsub action")
var Protocol = flag.String("protocol", "mqtt", "mqtt/mqtts/ws/wss")
var Topic    = flag.String("topic", "golang-mqtt/test", "publish/subscribe topic")
var Username = flag.String("username", "coolpy7", "username")
var Password = flag.String("password", "public", "password")
var Qos      = flag.Int("qos", 0, "MQTT QOS")
var Tls      = flag.Bool("tls", false, "Enable TLS/SSL")

func main() {
	flag.Parse()
	config := protocols.Config{Host: *Host, Port: *Port, Action: *Action, Topic: *Topic, Username: *Username, Password: *Password, Qos: *Qos, Tls: *Tls}
	protocol := *Protocol
	switch protocol {
	case "mqtt":
		protocols.MQTTConnection(config)
	case "mqtts":
		protocols.MQTTSConnection(config)
	case "ws":
		protocols.WSConnection(config)
	case "wss":
		protocols.WSSConnection(config)
	default:
		log.Fatalf("Unsupported protocol: %s", protocol)
	}
}
