using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

//using Newtonsoft.Json;

namespace WindowsFormsSignalRClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        ScanStartCommand startscancommand;
        private List<IntermData> intermDatas;
        public Form1()
        {
            InitializeComponent();
             intermDatas=new List<IntermData>();
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8886/SignalR")
               .Build();
            
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            startscancommand = new ScanStartCommand();
            try
            {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                //connectButton.IsEnabled = false;
                //sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
            
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messagesList.Items.Add(newMessage);
                }));
            });

            //connection.On<object>("UpdateDeviceInfo", (message) =>
            //{
            //    this.Invoke((Action)(() =>
            //    {
            //       var json = JsonSerializer.Serialize(message);
            //        messagesList.Items.Add(json);
            //        var options = new JsonDocumentOptions
            //    {
            //        AllowTrailingCommas = true
            //    };
                    
            //        using (JsonDocument document = JsonDocument.Parse(json, options))
            //        {
            //            foreach (JsonElement element in document.RootElement.EnumerateArray())
            //            {
            //                var name = element.GetProperty("name").GetString();
            //                if (name.Contains("HP LaserJet M1130 MFP TWAIN"))
            //                {
            //                    var idcanon = element.GetProperty("id").GetInt32();
            //                    startscancommand.Id = idcanon.ToString();
            //                }
            //            }
            //        }
            //        messagesList.Items.Add(message.GetType().ToString());
            //    }));
            //});


            connection.On<ScanedImage>("scanCompleted", (message) =>
            {
                this.Invoke((Action) (() =>
                {
                    
                    IntermData intermData = new IntermData();

                    intermData.id = message.Id.ToString();
                    intermDatas.Add(intermData);


                   
                }));
            });

            connection.On<byte[]>("fileGenerated", (message) =>
            {
                this.Invoke((Action)(() =>
                {
                    
                        File.WriteAllBytes($"Output.pdf", message);
                        messagesList.Items.Add(Convert.ToBase64String(message));
                }));
            });






        }




        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage",
                    userTextBox.Text, messageTextBox.Text);
                messagesList.Items.Add($"{userTextBox.Text}:{messageTextBox}");



                startscancommand.deviceName = "Canon MF8200C ser_28C2DD719DDD";
                //startscancommand.DPI = "50";
                //startscancommand.Mode = "RGB";
                //Name = "HP LaserJet M1130 MFP TWAIN"
                //startscancommand.deviceName = "HP LaserJet M1130 MFP TWAIN";
                //startscancommand.DPI = "150";
                //startscancommand.Mode = "RGB";
                //startscancommand.Size = "A4";
                //src.Name = "WIA-HP LaserJet Professional M113"
                //startscancommand.deviceName = "WIA-HP LaserJet Professional M113";
                //startscancommand.DPI = "100";
                //startscancommand.Mode = "RGB";
                //startscancommand.Size = "A4";
                await connection.InvokeAsync("startScan",
                    startscancommand);

            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            try
            {

               await connection.InvokeAsync("generatePdf",
                    intermDatas);

            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }

        }

        private async void button3_Click(object sender, EventArgs e)
        {

            try
            {
                await connection.InvokeAsync("SendMessage",
                    userTextBox.Text, messageTextBox.Text);
                messagesList.Items.Add($"{userTextBox.Text}:{messageTextBox}");



                //startscancommand.Name = "Canon MF8200C ser_28C2DD719DDD";
                //startscancommand.DPI = "50";
                //startscancommand.Mode = "RGB";
                //Name = "HP LaserJet M1130 MFP TWAIN"
                //startscancommand.deviceName = "HP LaserJet M1130 MFP TWAIN";
                //startscancommand.DPI = "150";
                //startscancommand.Mode = "RGB";
                //startscancommand.Size = "A4";
                //src.Name = "WIA-HP LaserJet Professional M113"
                startscancommand.deviceName = "WIA-HP LaserJet Professional M113";
                //startscancommand.DPI = "100";
                //startscancommand.Mode = "RGB";
                //startscancommand.Size = "A4";
                await connection.InvokeAsync("startScan",
                    startscancommand);

            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }

        }
    }
}
