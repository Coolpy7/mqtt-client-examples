package protocols

import (
	"fmt"
	"log"
	"strconv"
	"time"

	mqtt "github.com/eclipse/paho.mqtt.golang"
)

func connectByMQTT(config Config) mqtt.Client {
	opts := mqtt.NewClientOptions()
	broker := fmt.Sprintf("tcp://%s:%d", config.Host, config.Port)
	opts.AddBroker(broker)
	opts.SetClientID("cp7_"+strconv.FormatInt(time.Now().UnixNano(),10))
	opts.SetUsername(config.Username)
	opts.SetPassword(config.Password)
	opts.SetCleanSession(true)
	opts.SetKeepAlive(60)
	client := mqtt.NewClient(opts)
	token := client.Connect()
	if token.Wait() && token.Error() != nil {
		log.Fatal(token.Error())
	}
	return client
}

func connectByMQTTS(config Config) mqtt.Client {
	opts := mqtt.NewClientOptions()
	broker := fmt.Sprintf("tls://%s:%d", config.Host, config.Port)
	println(broker)
	opts.AddBroker(broker)
	opts.SetClientID("cp7_"+strconv.FormatInt(time.Now().UnixNano(),10))
	opts.SetUsername(config.Username)
	opts.SetPassword(config.Password)
	opts.SetCleanSession(true)
	opts.SetKeepAlive(60)
	client := mqtt.NewClient(opts)
	token := client.Connect()
	for !token.WaitTimeout(3 * time.Second) {
	}
	if err := token.Error(); err != nil {
		log.Fatal(err)
	}
	return client
}

func connectByWS(config Config) mqtt.Client {
	opts := mqtt.NewClientOptions()
	broker := fmt.Sprintf("ws://%s:%d/mqtt", config.Host, config.Port)
	opts.AddBroker(broker)
	opts.SetClientID("cp7_"+strconv.FormatInt(time.Now().UnixNano(),10))
	opts.SetUsername(config.Username)
	opts.SetPassword(config.Password)
	opts.SetCleanSession(true)
	opts.SetKeepAlive(60)
	client := mqtt.NewClient(opts)
	token := client.Connect()
	for !token.WaitTimeout(3 * time.Second) {
	}
	if err := token.Error(); err != nil {
		log.Fatal(err)
	}
	return client
}

func connectByWSS(config Config) mqtt.Client {
	opts := mqtt.NewClientOptions()
	broker := fmt.Sprintf("wss://%s:%d/mqtt", config.Host, config.Port)
	opts.AddBroker(broker)
	opts.SetClientID("cp7_"+strconv.FormatInt(time.Now().UnixNano(),10))
	opts.SetUsername(config.Username)
	opts.SetPassword(config.Password)
	opts.SetCleanSession(true)
	opts.SetKeepAlive(60)
	client := mqtt.NewClient(opts)
	token := client.Connect()
	for !token.WaitTimeout(3 * time.Second) {
	}
	if err := token.Error(); err != nil {
		log.Fatal(err)
	}
	return client
}
