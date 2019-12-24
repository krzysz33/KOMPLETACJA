using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KpInfohelp
{
    [Serializable()]
    [XmlRoot("SqlStatmentCollection")]
    public class SqlStatmentsModel
    {
        [XmlElement("IdentStatusWpisane")]
        public int IdentStatusWpisane { get; set; }

        [XmlElement("IdentStatusGotowe")]
        public int IdentStatusGotowe { get; set; }

        [XmlElement("IdentStatusWyciete")]
        public int IdentStatusWyciete { get; set; }

        [XmlElement("defaultThemeName")]
        public string defaultThemeName { get; set; }

        [XmlElement("StartPages")]
        public string StartPages { get; set; }

        [XmlElement("ObslugaSkanera")]
        public int ObslugaSkanera { get; set; }

        [XmlElement("PicturePath")]
        public string PicturePath { get; set; }


        [XmlElement("IdDefStawkaVat")]
        public int IdDefStawkaVat { get; set; }

        [XmlArray("SqlStatments")]
        [XmlArrayItem("SqlStatment", typeof(SqlStatment))]
        public SqlStatment[] sqlStatments;
        [XmlArray("SequenceEvents")]
        //    [XmlArrayItem("Item", typeof(Item))]
        //   public Item[] items;
             [XmlArrayItem("Kartoteka", typeof(Kartoteka))]
            public Kartoteka[] Kartoteki;

        [XmlArray("EventsConfig")]
        [XmlArrayItem("Upadki")]
    [XmlArrayItem("itemsSelekcja", typeof(ItemSelekcja))]
         public ItemSelekcja[] itemsSelekcja;

        //    [XmlArrayItem("Kartoteka", typeof(Kartoteka))]
        //     public Kartoteka[] kartoteki;


        [XmlElement("DodatkiWymiar")]
        public int IdWystKartSzlifX { get; set; }
         public int IdWystKartSzlifY { get; set; }
         public int IdWystKartFazaX { get; set; }
         public int IdWystKartFazaY { get; set; }
    }


 
    public class Kartoteka
    {
        [XmlElement("IdKartoteka")]
        public int IdKartoteka { get; set; }

    }

    public class Upadki
    {
        [XmlArrayItem("ItemUpadki", typeof(ItemUpadki))]
        public ItemUpadki[] ItemsUpadki;
    }


    public class Selekcja
    {
        [XmlArrayItem("ItemSelekcja", typeof(ItemSelekcja))]
        public ItemSelekcja[] ItemsSelekcja;
    }

    public class ItemSelekcja
    {
        [XmlElement("Lp")]
        public int Lp { get; set; }

        [XmlElement("IdZdarzenia")]
        public int IdZdarzenia { get; set; }

    }

    public class ItemUpadki
    {
        [XmlElement("Lp")]
        public int Lp { get; set; }

        [XmlElement("IdZdarzenia")]
        public int IdZdarzenia { get; set; }

    }



    public class Item
    {
        [XmlElement("Lp")]
        public int Lp { get; set; }

        [XmlElement("IdZdarzenia")]
        public int IdZdarzenia { get; set; }
       
    }


    public class SqlStatment
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("DefoultSearching")]
        public string DefoultSearching { get; set; }
        [XmlElement("Sql")]
        public string Sql { get; set; }
        [XmlElement("Label1")]
        public string Label1 { get; set; }
        [XmlElement("Info1")]
        public string Info1 { get; set; }
        [XmlElement("Label2")]
        public string Label2 { get; set; }
        [XmlElement("Info2")]
        public string Info2 { get; set; }
        [XmlElement("Label3")]
        public string Label3 { get; set; }
        [XmlElement("Info3")]
        public string Info3 { get; set; }
        [XmlElement("Label4")]
        public string Label4 { get; set; }
        [XmlElement("Info4")]
        public string Info4 { get; set; }
        [XmlElement("Label5")]
        public string Label5 { get; set; }
        [XmlElement("Info5")]
        public string Info5 { get; set; }
        [XmlArray("Mapping")]
        [XmlArrayItem("Column", typeof(Column))]
        public Column[] columns;
    }

    public class Column
    {
        [XmlElement("Binding")]
        public string Binding { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Visable")]
        public bool Visable { get; set; }
        [XmlElement("Text")]
        public string Text { get; set; }
        [XmlElement("TypDanych")]
        public string TypDanych { get; set; }
        [XmlElement("Width")]
        public int Width { get; set; }
    }
}
