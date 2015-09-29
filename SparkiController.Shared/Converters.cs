using System;
using Windows.UI.Xaml.Data;
using TCD.Arduino.Bluetooth;

namespace SparkiComController
{
    public class UiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            switch (parameter as string)//parameter tells us who's calling
            {
                //BluetoothConnectionState
                case "BluetoothConnect": return ((BluetoothConnectionState)value == BluetoothConnectionState.Disconnected);
                case "BluetoothInProgress":
                case "BluetoothConnecting": return ((BluetoothConnectionState)value == BluetoothConnectionState.Connecting);
                case "BluetoothDisconnect": return ((BluetoothConnectionState)value == BluetoothConnectionState.Connected);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return null;
        }
    }
}
