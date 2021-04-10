const fs = require('fs')
const mqtt = require('mqtt')
const { Command } = require('commander')

const program = new Command()
program
  .option('-p, --protocol <type>', 'connect protocol: mqtt, ws. default is mqtt', 'mqtt')
  .parse(process.argv)


const HOST = 'iotiot.net'
const DEF_PORT = 1883
const TOPIC= `node-js/${program.protocol}`

// connect options
const OPTIONS = {
  clean: true,
  connectTimeout: 4000,
  clientId: `mqtt_${Math.random().toString(16).slice(3)}`,
  username: 'coolpy7',
  password: 'public',
  reconnectPeriod: 1000,
}
// protocol list
const PROTOCOLS = ['mqtt', 'ws']

// default is mqtt, unencrypted tcp connection
let connectUrl = `mqtt://${HOST}:${DEF_PORT}`
if (program.protocol && PROTOCOLS.indexOf(program.protocol) === -1) {
  console.log('protocol must one of mqtt, ws.')
} else if (program.protocol === 'ws') {
  // ws, unencrypted WebSocket connection
  const mountPath = '/mqtt' // mount path, connect emqx via WebSocket
  connectUrl = `ws://${HOST}:8083${mountPath}`
} else {}

const client = mqtt.connect(connectUrl, OPTIONS)

client.on('connect', () => {
  console.log(`${program.protocol}: Connected`)
  client.subscribe([TOPIC], () => {
    console.log(`${program.protocol}: Subscribe to topic '${TOPIC}'`)
  })
})

client.on('reconnect', (error) => {
  console.log(`Reconnecting(${program.protocol}):`, error)
})

client.on('error', (error) => {
  console.log(`Cannot connect(${program.protocol}):`, error)
})

client.on('message', (topic, payload) => {
  console.log('Received Message:', topic, payload.toString())
})
