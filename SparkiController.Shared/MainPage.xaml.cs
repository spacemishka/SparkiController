using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Storage.Streams;
using TCD.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace SparkiController
{

    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
            //App.BluetoothManager.MessageReceived += BluetoothManager_MessageReceived;
            App.BluetoothManager.ExceptionOccured += BluetoothManager_ExceptionOccured;
            GripoButton.Content = "<--|-->";
            GripcButton.Content = "-->|<--";
            SonicTurnLeftButton.Content = "<--((0))";
            SonicTurnRightButton.Content = "((0))-->";
        }

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            App.BluetoothManager.Disconnect(); //clean up the mess
        }

        private void WriteLine(string line) //write a new line to the "Console"
        {
            System.Diagnostics.Debug.WriteLine(line);
        }

        #region Bluetooth Connection Lifecycle

        //control
        private async void BluetoothConnect_Click(object sender, RoutedEventArgs e)
        {
            //ask the user to connect 
            await App.BluetoothManager.EnumerateDevicesAsync((sender as Button).GetElementRect());
            //displays a PopupMenu above the ConnectButton - uses GetElementRect() from TCD.Controls to determine the area
        }

        private async void BluetoothManager_ExceptionOccured(object sender, Exception ex)
        {
            var md = new MessageDialog(ex.Message, "We've got a problem with bluetooth...");
            md.Commands.Add(new UICommand("Ah.. thanks.."));
            md.DefaultCommandIndex = 0;
            var result = await md.ShowAsync();
        }

        #endregion

        #region Send & Receive

        private async void LEDButton_Click(object sender, RoutedEventArgs e)
        {
            //the Button controls sending there tag as command in their XAML definition
            string button = (sender as Button).Tag as string;
            //send ON or OFF commands according to the LEDs last known state
            string cmd = string.Format(button);
            //try to send this message
            var res = await App.BluetoothManager.SendMessageAsync(cmd);
        }
    

    /*
        private async void BluetoothManager_MessageReceived(object sender, string message)
        {
            //this function is not working yet.


            //int equIndex = message.IndexOf('=');
            //System.Diagnostics.Debug.WriteLine("2 "+message);
            //await App.BluetoothManager.SendMessageAsync("1");
            if (equIndex > 0)//is this a report? (eg: "A0=245" alternative: confirmation like "RED_ON")
            {
                switch (message.Substring(0, equIndex))//we can receive more than one report...
                {
                    case "A0": A0Bar.Value = Convert.ToInt32(message.Substring(equIndex + 1)); break;
                }
                if (A0Bar.Value > 512)
                    await App.BluetoothManager.SendMessageAsync("2");
                else
                    await App.BluetoothManager.SendMessageAsync("1");
                    
            }
            else
            {
                switch (message)//interpret other messages
                {
                    case "": <condition> = false; break;//remember the LED state
                    .
                    .
                    .
                }
            }
            //log incoming transmission
            WriteLine("< " + message);
        }
        */

    #endregion

}

}