using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace mqtt_client_cSharp
{
    class Program
    {
        public static IMqttClient mqttClient; 
        
        static void Main(string[] args)
        {
            ConnectMqttServerAsync();
            ImportData();
        }
        
        private static async void ConnectMqttServerAsync()
        {
            try
            {

                var factory = new MqttFactory();

                mqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("iotiot.net", 1883)
                    .WithCredentials("test", "test")
                    .WithClientId(Guid.NewGuid().ToString().Substring(0, 5))
                    .Build();

                //消息
                mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    Console.WriteLine("### 收到的信息 ###");
                    Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");//主题
                    Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");//页面信息
                    Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");//消息等级
                    Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");//是否保留
                    Console.WriteLine();
                });

                //重连机制
                mqttClient.UseDisconnectedHandler(async e =>
                {
                    Console.WriteLine("与服务器断开连接！");
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    try
                    {
                        await mqttClient.ConnectAsync(options);
                    }
                    catch (Exception exp)
                    {
                        Console.Write($"重新连接服务器失败 Msg：{exp}");
                    }
                });

               await mqttClient.ConnectAsync(options);

               Console.Write("连接服务器成功！输入任意内容并回车进入菜单页面！");
            }
            catch (Exception exp)
            {
                Console.Write($"连接服务器失败 Msg：{exp}");
            }
        }

        private static void ImportData()
        {
            Console.ReadLine();
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine(@"请输入
                    1.订阅主题
                    2.取消订阅
                    3.发送消息
                    4.退出");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine(@"请输入主题名称：");
                        var topicName = Console.ReadLine();
                        Subscribe(topicName);
                        break;
                    case "2":
                        Console.WriteLine(@"请输入需要取消订阅主题名称：");
                        topicName = Console.ReadLine();
                        Unsubscribe(topicName);
                        break;
                    case "3":
                        Console.WriteLine("请输入需要发送的主题名称");
                        topicName = Console.ReadLine();
                        Console.WriteLine("请输入需要发送的消息");
                        var message = Console.ReadLine();
                        Publish(topicName, message);
                        break;
                    case "4":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("请输入正确指令！");
                        break;
                }
            }
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="topicName"></param>
        private static async void Subscribe(string topicName)
        {
            string topic = topicName.Trim();
            if (string.IsNullOrEmpty(topic))
            {
                Console.Write("订阅主题不能为空！");
                return;
            }

            if (!mqttClient.IsConnected)
            {
                Console.Write("MQTT客户端尚未连接！");
                return;
            }
            await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topicName"></param>
        private static async void Unsubscribe(string topicName)
        {
            string topic = topicName.Trim();
            if (string.IsNullOrEmpty(topic))
            {
                Console.Write("订阅主题不能为空！");
                return;
            }

            if (!mqttClient.IsConnected)
            {
                Console.Write("MQTT客户端尚未连接！");
                return;
            }
            await mqttClient.UnsubscribeAsync(topic);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        private static async void Publish(string topicName, string message)
        {
            string topic = topicName.Trim();
            string msg = message.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                Console.Write("主题不能为空！");
                return;
            }
            if (!mqttClient.IsConnected)
            {
                Console.Write("MQTT客户端尚未连接！");
                return;
            }

            var MessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(msg)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await mqttClient.PublishAsync(MessageBuilder);

        }

    }
}