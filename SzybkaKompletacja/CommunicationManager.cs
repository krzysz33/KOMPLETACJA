using System;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using DevExpress.Mvvm;
//*****************************************************************************************
//                           LICENSE INFORMATION
//*****************************************************************************************
//   PCCom.SerialCommunication Version 1.0.0.0
//   Class file for managing serial port communication
//
//   Copyright (C) 2007  
//   Richard L. McCutchen 
//   Email: richard@psychocoder.net
//   Created: 20OCT07
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the          qwqqwqwqww
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.
//*****************************************************************************************
namespace KpInfohelp
{
    class CommunicationManager
    {
        #region Manager Enums
        /// <summary>
        /// enumeration to hold our transmission types
        /// </summary>
        public enum TransmissionType { Text, Hex }

        /// <summary>
        /// enumeration to hold our message types
        /// </summary>
        public enum MessageType { Incoming, Outgoing, Normal, Warning, Error };

        public enum FrameType { Radwag, Digi, Axis}

        #endregion

        #region Manager Variables
        //property variables
        private string _miernik = string.Empty;
        private string _baudRate = string.Empty;
        private string _parity = string.Empty;
        private string _stopBits = string.Empty;
        private string _dataBits = string.Empty;
        private string _portName = string.Empty;
        private string _debugport = string.Empty;
        private decimal _waga;
        private TransmissionType _transType;
        private FrameType _frameType;
        private RichTextBox _displayWindow;
         private Label _displayLabel;
        private bool IsCosedPort = true;
 

        //global manager variables
        private Color[] MessageColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };
        private SerialPort comPort = new SerialPort();
        private Object lockingObj = new Object();
        private WagaRamka ramka = new WagaRamka();
        #endregion

        #region Manager Properties

        public FrameType CurrentFrameType
        {
            get { return _frameType; }
            set { _frameType = value; }
        }
        public string Miernik
        {
            get { return _miernik; }
            set { _miernik = value; }
        }

        /// <summary>
        /// Property to hold the BaudRate
        /// of our manager class
        /// </summary>
        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        /// <summary>
        /// property to hold the Parity
        /// of our manager class
        /// </summary>
        public string Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        /// <summary>
        /// property to hold the StopBits
        /// of our manager class
        /// </summary>
        public string StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        /// <summary>
        /// property to hold the DataBits
        /// of our manager class
        /// </summary>
        public string DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }
        public decimal Waga
        {
            get { return _waga; }
            set { _waga = value; }
        }

        /// <summary>
        /// property to hold our TransmissionType
        /// of our manager class
        /// </summary>
        public TransmissionType CurrentTransmissionType
        {
            get { return _transType; }
            set { _transType = value; }
        }

        /// <summary>
        /// property to hold our display window
        /// value
        /// </summary>
        public RichTextBox DisplayWindow
        {
            get { return _displayWindow; }
            set { _displayWindow = value; }
        }

        public Label DisplayLabel
        {
            get { return _displayLabel; }
            set { _displayLabel = value; }
        }

       
        #endregion
        #region Manager Constructors
        /// <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// <param name="par">Desired Parity</param>
        /// <param name="sBits">Desired StopBits</param>
        /// <param name="dBits">Desired DataBits</param>
        /// <param name="name">Desired PortName</param>
        public CommunicationManager(string baud, string par, string sBits, string dBits, string name,string miernik)
        {
            _miernik = miernik;
            _baudRate = baud;
            _parity = par;
            _stopBits = sBits;
            _dataBits = dBits;
            _portName = name;
             // _displayWindow = rtb;
            //now add an event handler

            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public CommunicationManager()
        {
            _debugport = ConfigurationManager.AppSettings["DEBUGPORT"].ToString();
            _miernik = ConfigurationManager.AppSettings["MIERNIK"].ToString();
            _baudRate = ConfigurationManager.AppSettings["BAUD"].ToString();
            _parity = ConfigurationManager.AppSettings["PAR"].ToString();
            _stopBits = ConfigurationManager.AppSettings["SBITS"].ToString();
            _dataBits = ConfigurationManager.AppSettings["DBITS"].ToString();
            _portName = ConfigurationManager.AppSettings["PORT"].ToString();
  
                       _displayWindow = null;
            //add event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        #endregion

        #region WriteData
        public void WriteData(string msg)
        {
            switch (CurrentTransmissionType)
            {
                case TransmissionType.Text:
                    //first make sure the port is open
                    //if its not open then open it
                    if (!(comPort.IsOpen == true)) comPort.Open();
                    //send the message to the port

                    comPort.Write(msg);
                    //display the message
                    //  DisplayData(MessageType.Outgoing, msg + "\n");
                    //DisplayData(MessageType.Outgoing, msg);
                   // DisplayDataLabel(MessageType.Normal, 0);
                    break;
                case TransmissionType.Hex:
                    try
                    {
                        //convert the message to byte array
                        byte[] newMsg = HexToByte(msg);
                        //send the message to the port
                        comPort.Write(newMsg, 0, newMsg.Length);
                        //convert back to hex and display
                        //  DisplayData(MessageType.Outgoing, ByteToHex(newMsg) + "\n");
                      //  DisplayDataLabel(MessageType.Normal, 0);
                    }
                    catch (FormatException ex)
                    {
                        //display error message
                        //DisplayData(MessageType.Error, ex.Message);
                        DisplayDataLabel(MessageType.Normal, 0);
                    }
                    finally
                    {
                        _displayWindow.SelectAll();
                    }
                    break;
                default:
                    //first make sure the port is open
                    //if its not open then open it
                    if (!(comPort.IsOpen == true)) comPort.Open();
                    //send the message to the port
                    comPort.Write(msg);
                    //display the message
               //     DisplayData(MessageType.Outgoing, msg + "\n");
                    break;
                    break;
            }
        }
        #endregion

        #region HexToByte
        /// <summary>
        /// method to convert hex string into a byte array
        /// </summary>
        /// <param name="msg">string to convert</param>
        /// <returns>a byte array</returns>
        private byte[] HexToByte(string msg)
        {
            //remove any spaces from the string
            msg = msg.Replace(" ", "");
            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            //loop through the length of the provided string
            for (int i = 0; i < msg.Length; i += 2)
                //convert each set of 2 characters to a byte
                //and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            //return the array
            return comBuffer;
        }
        #endregion

        #region ByteToHex
        /// <summary>
        /// method to convert a byte array into a hex string
        /// </summary>
        /// <param name="comByte">byte array to convert</param>
        /// <returns>a hex string</returns>
        private string ByteToHex(byte[] comByte)
        {
            //create a new StringBuilder object
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            //loop through each byte in the array
            foreach (byte data in comByte)
                //convert the byte to a string and add to the stringbuilder
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            //return the converted value
            return builder.ToString().ToUpper();
        }
        #endregion

        #region DisplayData
        /// <summary>
        /// method to display the data to & from the port
        /// on the screen
        /// </summary>
        /// <param name="type">MessageType of the message</param>
        /// <param name="msg">Message to display</param>
        [STAThread]
        private void DisplayData(MessageType type, string msg)
        {
            if (_displayWindow.IsHandleCreated && !_displayWindow.Disposing)
            {
                _displayWindow.Invoke(new EventHandler(delegate
             {
            _displayWindow.SelectedText = string.Empty;
            _displayWindow.SelectionFont = new Font(_displayWindow.SelectionFont, FontStyle.Bold);
            _displayWindow.SelectionColor = MessageColor[(int)type];
            _displayWindow.AppendText(msg);
            _displayWindow.ScrollToCaret();
         }));
            }
        }

        private void DisplayDataLabel(MessageType type, decimal msg)
        {
            if (_displayLabel.IsHandleCreated && !_displayLabel.Disposing)
            {
                _displayLabel.Invoke(new EventHandler(delegate
                 {
                     _displayLabel.Text = msg.ToString();
                 }));
            }
        }

        #endregion

        public void ClosePort()
        {
            if (comPort.IsOpen == true)
            {
                IsCosedPort = true;
                Thread.Sleep(100);
                comPort.Close();
            }
        }

        #region OpenPort
        public bool OpenPort()
        {
            try
            {
                //first check if the port is already open
                //if its open then close it
                if (comPort.IsOpen == true) comPort.Close();

                //set the properties of our SerialPort Object
                comPort.BaudRate = int.Parse(_baudRate);    //BaudRate
                comPort.DataBits = int.Parse(_dataBits);    //DataBits
                comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBits);    //StopBits
                comPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity);    //Parity
                comPort.PortName = _portName;   //PortName
              //  comPort.ReadBufferSize = 200;
                       //now open the port
                       comPort.Open();
                IsCosedPort = false;
                //display message
                //   DisplayData(MessageType.Normal, "Port opened at " + DateTime.Now + "\n");
                //    DisplayDataLabel(MessageType.Normal, "Port opened at " + DateTime.Now + "\n");
                // DisplayDataLabel(MessageType.Normal,0);
                return true;
            }
            catch (Exception ex)
            {
                //   DisplayData(MessageType.Error, ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region SetParityValues
        public void SetParityValues(string par)
        {
      //
        }
        #endregion

        #region SetStopBitValues
        public void SetStopBitValues(string dit)
        {
      //
        }
        #endregion

        #region SetPortNameValues
        public void SetPortNameValues(object obj)
        {
   //
        }
        #endregion

        #region comPort_DataReceived
        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsCosedPort) return;
            lock (lockingObj)
            {
                string msg = String.Empty;
                //determine the mode the user selected (binary/string)
                switch (CurrentTransmissionType)
                {
                    //user chose string
                    case TransmissionType.Text:
                        switch (_miernik)
                        {
                            case "Axis":
                                msg = comPort.ReadLine().Trim();// WAGA RADWAG 
                                break;
                            case "Radwag":
                                msg = comPort.ReadLine().Trim();// WAGA RADWAG 
                                break;
                            case "Rinstrum":
                                //   msg = comPort.ReadTo("");// WAGA RISTORUM miedzy ciapkami specjalne znaki tj char(2) cahr(3)
                                msg = comPort.ReadExisting();
                                break;
                            //   msg = comPort.ReadExisting(); 
                            ////display the data to the user
                            case "Rinstrum2":
                                //   msg = comPort.ReadTo("");// WAGA RISTORUM miedzy ciapkami specjalne znaki tj char(2) cahr(3)
                                msg = comPort.ReadLine().Trim();
                                break;

                            case "Rewa":
                                //   msg = comPort.ReadTo("");// WAGA RISTORUM miedzy ciapkami specjalne znaki tj char(2) cahr(3)
                                msg = comPort.ReadLine().Trim();
                                //       msg = comPort.ReadExisting();
                                break;
                                //   msg = comPort.ReadExisting(); 
                                ////display the data to the user
                        }
                        if (_debugport == "1")
                            DisplayData(MessageType.Normal, msg + "\n");

                        _waga = DataTypeConvert.ConvertFrameToDecimal(_miernik, msg);

                        if (string.IsNullOrEmpty(msg))
                        {
                            ramka.ErrMsg = "Er. 1";
                        }
                        ramka.Miernik = _miernik;
                        ramka.Waga = _waga;
                        ramka.WagaStr = msg;
                        
                        //set message 
                        Messenger.Default.Send<WagaRamka>(ramka);
                    
                        byte[] data = new byte[comPort.BytesToRead];
                        comPort.Read(data, 0, data.Length);
                        if (data.Length > 14)
                        {
                            comPort.DiscardInBuffer();
                            comPort.DiscardOutBuffer();
                        }
                        break;
                    //user chose binary
                    case TransmissionType.Hex:
                        //retrieve number of bytes in the buffer
                        int bytes = comPort.BytesToRead;
                        //create a byte array to hold the awaiting data
                        byte[] comBuffer = new byte[bytes];
                        //read the data and store it
                        comPort.Read(comBuffer, 0, bytes);
                        //display the data to the user
                        //  DisplayData(MessageType.Incoming, ByteToHex(comBuffer) + "\n");
                        break;
                    default:
                        //read data waiting in the buffer
                        //  string str = comPort.ReadExisting();
                        //  _waga = DataTypeConvert.ConvertFrameToDecimal("Radwag", str);
                        //  DisplayDataLabel(MessageType.Incoming, _waga);
                        //display the data to the user
                        //      DisplayData(MessageType.Incoming, str + "\n");
                        break;

                }
            }
        }
        #endregion
    }
}
