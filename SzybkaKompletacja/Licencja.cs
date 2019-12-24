
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class LicencjaClass
{

    private LicencjaFirma firmaField;

    private LicencjaModul[] modulyField;

    /// <remarks/>
    public LicencjaFirma Firma
    {
        get
        {
            return this.firmaField;
        }
        set
        {
            this.firmaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Modul", IsNullable = false)]
    public LicencjaModul[] Moduly
    {
        get
        {
            return this.modulyField;
        }
        set
        {
            this.modulyField = value;
        }
    }
}
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class LicencjaFirma
{

    private string nazwaField;

    private string nIPField;

    private string eMAILField;

    private string tELEFONField;
    private int rODZAJLICField;

    /// <remarks/>
    public string Nazwa
    {
        get
        {
            return this.nazwaField;
        }
        set
        {
            this.nazwaField = value;
        }
    }

    /// <remarks/>
    public string NIP
    {
        get
        {
            return this.nIPField;
        }
        set
        {
            this.nIPField = value;
        }
    }

    /// <remarks/>
    public string EMAIL
    {
        get
        {
            return this.eMAILField;
        }
        set
        {
            this.eMAILField = value;
        }
    }

    /// <remarks/>
    public string TELEFON
    {
        get
        {
            return this.tELEFONField;
        }
        set
        {
            this.tELEFONField = value;
        }
    }

    public int RODZAJLIC
    {
        get
        {
            return this.rODZAJLICField;
        }
        set
        {
            this.rODZAJLICField = value;
        }
    }
}
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class LicencjaModul
{
   private int idIhpModulField;
    private string opisField;
    private int rodzajField;
    private string wartosciField;
    /// <remarks/>
    public int IdIhpModul
    {
        get
        {
            return this.idIhpModulField;
        }
        set
        {
            this.idIhpModulField = value;
        }
    }
    /// <remarks/>
    public string Opis
    {
        get
        {
            return this.opisField;
        }
        set
        {
            this.opisField = value;
        }
    }
    /// <remarks/>
    public int Rodzaj
    {
        get
        {
            return this.rodzajField;
        }
        set
        {
            this.rodzajField = value;
        }
    }
    /// <remarks/>
    public string Wartosci
    {
        get
        {
            return this.wartosciField;
        }
        set
        {
            this.wartosciField = value;
        }
    }
}

 