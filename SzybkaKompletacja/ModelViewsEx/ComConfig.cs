using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace KpInfohelp
{

    public class ComConfig
    {

        public string PORTNAME { get; set; }
        public string SPEED { get; set; }
        public int BITDATA { get; set; }
        public string PARITY { get; set; }
        public int BITSTOP { get; set; }
        public string  STERPRZEP { get; set; }

    }

 
 
     public class Miernik
       {
         public int IdMiernik { get; set; }
        
        public string Nazwa { get; set; }
      }


 

    public class Speed
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

    public class PortName
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

    public class BitData
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }


    public class Paritty
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

    public class BitStop
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

    public class SterPrzep
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }

}
