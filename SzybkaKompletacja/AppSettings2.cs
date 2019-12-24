/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class AppSettings
{
    private AppSettingsBazaDanych bazaDanychField;
    private AppSettingsUstawieniaAplikacji ustawieniaAplikacjiField;
    private AppSettingsUstawieniaPortuCom ustawieniaPortuComField;
    private UstawieniaKamer UstawieniaKamerField;
    public UstawieniaKamer UstawieniaKamer
    {
        get
        {
            return this.UstawieniaKamerField;
        }
        set
        {
            this.UstawieniaKamerField = value;
        }
    }
    /// <remarks/>
    public AppSettingsBazaDanych BazaDanych
    {
        get
        {
            return this.bazaDanychField;
        }
        set
        {
            this.bazaDanychField = value;
        }
     }
    /// <remarks/>
    public AppSettingsUstawieniaAplikacji UstawieniaAplikacji
    {
        get
        {
            return this.ustawieniaAplikacjiField;
        }
        set
        {
            this.ustawieniaAplikacjiField = value;
        }
    }
    /// <remarks/>
    public AppSettingsUstawieniaPortuCom UstawieniaPortuCom
    {
        get
        {
            return this.ustawieniaPortuComField;
        }
        set
        {
            this.ustawieniaPortuComField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsBazaDanych
{

    private string nazwaBazyDanychField;

    private string katalogBazyDanychField;

    private int rodzajAutoryzacjiField;

    private string uzytkownikBazyField;

    private string hasloField;

    private byte lokalnySerwerBazyDanychField;

    private string hostZdalnyField;

    private string providerField;
    private int kopiaPrzyZamknieciuField;
    private int wyslijFtpField;
    /// <remarks/>
    public string NazwaBazyDanych
    {
        get
        {
            return this.nazwaBazyDanychField;
        }
        set
        {
            this.nazwaBazyDanychField = value;
        }
    }

    /// <remarks/>
    public string KatalogBazyDanych
    {
        get
        {
            return this.katalogBazyDanychField;
        }
        set
        {
            this.katalogBazyDanychField = value;
        }
    }

    /// <remarks/>
    public int RodzajAutoryzacji
    {
        get
        {
            return this.rodzajAutoryzacjiField;
        }
        set
        {
            this.rodzajAutoryzacjiField = value;
        }
    }

    /// <remarks/>
    public string UzytkownikBazy
    {
        get
        {
            return this.uzytkownikBazyField;
        }
        set
        {
            this.uzytkownikBazyField = value;
        }
    }

    /// <remarks/>
    public string Haslo
    {
        get
        {
            return this.hasloField;
        }
        set
        {
            this.hasloField = value;
        }
    }

    /// <remarks/>
    public byte LokalnySerwerBazyDanych
    {
        get
        {
            return this.lokalnySerwerBazyDanychField;
        }
        set
        {
            this.lokalnySerwerBazyDanychField = value;
        }
    }

    /// <remarks/>
    public string HostZdalny
    {
        get
        {
            return this.hostZdalnyField;
        }
        set
        {
            this.hostZdalnyField = value;
        }
    }

    /// <remarks/>
    public string provider
    {
        get
        {
            return this.providerField;
        }
        set
        {
            this.providerField = value;
        }
    }
    public int KopiaPrzyZamknieciu
    {
        get
        {
            return this.kopiaPrzyZamknieciuField;
        }
        set
        {
            this.kopiaPrzyZamknieciuField = value;
        }
    }
    public int WyslijFtp
    {
        get
        {
            return this.wyslijFtpField;
        }
        set
        {
            this.wyslijFtpField = value;
        }
    }
    private bool _wysylkaftp;
    private string ftpHostField;
    public string FtpHost
    {
        get
        {
            return ftpHostField;
        }
        set
        {
            ftpHostField = value;
     
        }
    }
    private string ftpUserField;
    public string FtpUser
    {
        get
        {
            return ftpUserField;
        }
        set
        {
            ftpUserField = value;
    
        }
    }
    private string ftpPassField;
    public string FtpPass
    {
        get
        {
            return ftpPassField;
        }
        set
        {
            ftpPassField = value;
   
        }
    }


}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsUstawieniaAplikacji
{

    private AppSettingsUstawieniaAplikacjiParametr[] parametryField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Parametr", IsNullable = false)]
    public AppSettingsUstawieniaAplikacjiParametr[] Parametry
    {
        get
        {
            return this.parametryField;
        }
        set
        {
            this.parametryField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsUstawieniaAplikacjiParametr
{

    private int idIhpParametryField;

    private byte rodzajField;

    private AppSettingsUstawieniaAplikacjiParametrWartosci[] wartosciField;

    /// <remarks/>
    public int IdIhpParametry
    {
        get
        {
            return this.idIhpParametryField;
        }
        set
        {
            this.idIhpParametryField = value;
        }
    }

    /// <remarks/>
    public byte Rodzaj
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
    [System.Xml.Serialization.XmlElementAttribute("Wartosci")]
    public AppSettingsUstawieniaAplikacjiParametrWartosci[] Wartosci
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsUstawieniaAplikacjiParametrWartosci
{

    private string opisField;

    private string wartoscField;

    /// <remarks/>
    public string opis
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
    public string Wartosc
    {
        get
        {
            return this.wartoscField;
        }
        set
        {
            this.wartoscField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsUstawieniaPortuCom
{

    private AppSettingsUstawieniaPortuComWAGA1 wAGA1Field;

    /// <remarks/>
    public AppSettingsUstawieniaPortuComWAGA1 WAGA1
    {
        get
        {
            return this.wAGA1Field;
        }
        set
        {
            this.wAGA1Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppSettingsUstawieniaPortuComWAGA1
{

    private string miernikField;

    private string portNameField;

    private string speedField;

    private string sterPrzepField;

    private string parittyField;

    private int bitDataField;

    private string bitStopField;



    /// <remarks/>
    public string PortName
    {
        get
        {
            return this.portNameField;
        }
        set
        {
            this.portNameField = value;
        }
    }

    /// <remarks/>
    public string Speed
    {
        get
        {
            return this.speedField;
        }
        set
        {
            this.speedField = value;
        }
    }
    /// <remarks/>
    public string SterPrzep
    {
        get
        {
            return this.sterPrzepField;
        }
        set
        {
            this.sterPrzepField = value;
        }
    }
    /// <remarks/>
    public string Paritty
    {
        get
        {
            return this.parittyField;
        }
        set
        {
            this.parittyField = value;
        }
    }
    /// <remarks/>
    public int BitData
    {
        get
        {
            return this.bitDataField;
        }
        set
        {
            this.bitDataField = value;
        }
    }
    /// <remarks/>
    public string BitStop
    {
        get
        {
            return this.bitStopField;
        }
        set
        {
            this.bitStopField = value;
        }
    }
    public string Miernik
    {
        get
        {
            return this.miernikField;
        }
        set
        {
            this.miernikField = value;
        }
    }
}


/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class UstawieniaKamer
{

    private UstawieniaKamerKamera1 kamera1Field;

    private UstawieniaKamerKamera2 kamera2Field;

    private UstawieniaKamerKamera3 kamera3Field;

    /// <remarks/>
    public UstawieniaKamerKamera1 Kamera1
    {
        get
        {
            return this.kamera1Field;
        }
        set
        {
            this.kamera1Field = value;
        }
    }

    /// <remarks/>
    public UstawieniaKamerKamera2 Kamera2
    {
        get
        {
            return this.kamera2Field;
        }
        set
        {
            this.kamera2Field = value;
        }
    }

    /// <remarks/>
    public UstawieniaKamerKamera3 Kamera3
    {
        get
        {
            return this.kamera3Field;
        }
        set
        {
            this.kamera3Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class UstawieniaKamerKamera1
{

    private int idIhpParametryField;
    private string URIField;
    private string URILiveField;
    private string adressIPField;
    private string ProducentField;
    private string ModelField;
    private int portField;
    private string userField;
    private string passField;
    private int deviceField;

    /// <remarks/>
    public int IdIhpParametry
    {
        get
        {
            return this.idIhpParametryField;
        }
        set
        {
            this.idIhpParametryField = value;
        }
    }
    /// <remarks/>

    public string URI
    {
        get
        {
            return this.URIField;
        }
        set
        {
            this.URIField = value;
        }
    }


    public string URILive
    {
        get
        {
            return this.URILiveField;
        }
        set
        {
            this.URILiveField = value;
        }
    }


    /// <remarks/>
    public string Producent
    {
        get
        {
            return this.ProducentField;
        }
        set
        {
            this.ProducentField = value;
        }
    }
    /// <remarks/>
    public string Model
    {
        get
        {
            return this.ModelField;
        }
        set
        {
            this.ModelField = value;
        }
    }
    
    
    /// <remarks/>
    public string AdressIP
    {
        get
        {
            return this.adressIPField;
        }
        set
        {
            this.adressIPField = value;
        }
    }
    /// <remarks/>
    public int Port
    {
        get
        {
            return this.portField;
        }
        set
        {
            this.portField = value;
        }
    }
    /// <remarks/>
    public string User
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }
    /// <remarks/>
    public string Pass
    {
        get
        {
            return this.passField;
        }
        set
        {
            this.passField = value;
        }
    }
    /// <remarks/>
    public int Device
    {
        get
        {
            return this.deviceField;
        }
        set
        {
            this.deviceField = value;
        }
    }
}
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class UstawieniaKamerKamera2
{
    private int idIhpParametryField;
    private string adressIPField;
    private string URIField;
    private string URILiveField;
    private string ProducentField;
    private string ModelField;
    private int portField;
    private string userField;
    private string passField;
    private int deviceField;
    /// <remarks/>
    public int IdIhpParametry
    {
        get
        {
            return this.idIhpParametryField;
        }
        set
        {
            this.idIhpParametryField = value;
        }
    }
    public string URILive
    {
        get
        {
            return this.URILiveField;
        }
        set
        {
            this.URILiveField = value;
        }
    }
    public string URI
    {
        get
        {
            return this.URIField;
        }
        set
        {
            this.URIField = value;
        }
    }
    /// <remarks/>
    public string AdressIP
    {
        get
        {
            return this.adressIPField;
        }
        set
        {
            this.adressIPField = value;
        }
    }
    public string Producent
    {
        get
        {
            return this.ProducentField;
        }
        set
        {
            this.ProducentField = value;
        }
    }
    /// <remarks/>
    public string Model
    {
        get
        {
            return this.ModelField;
        }
        set
        {
            this.ModelField = value;
        }
    }
    /// <remarks/>
    public int Port
    {
        get
        {
            return this.portField;
        }
        set
        {
            this.portField = value;
        }
    }
    /// <remarks/>
    public string User
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }
    /// <remarks/>
    public string Pass
    {
        get
        {
            return this.passField;
        }
        set
        {
            this.passField = value;
        }
    }
    /// <remarks/>
    public int Device
    {
        get
        {
            return this.deviceField;
        }
        set
        {
            this.deviceField = value;
        }
    }
}
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class UstawieniaKamerKamera3
{
    private int idIhpParametryField;
    private string URIField;
    private string URILiveField;
    private string adressIPField;
    private string ProducentField;
    private string ModelField;
    private int portField;
    private string userField;
    private string passField;
    private int deviceField;
    /// <remarks/>
    public int IdIhpParametry
    {
        get
        {
            return this.idIhpParametryField;
        }
        set
        {
            this.idIhpParametryField = value;
        }
    }
    public string URILive
    {
        get
        {
            return this.URILiveField;
        }
        set
        {
            this.URILiveField = value;
        }
    }
    public string URI
    {
        get
        {
            return this.URIField;
        }
        set
        {
            this.URIField = value;
        }
    }
    /// <remarks/>
    public string Producent
    {
        get
        {
            return this.ProducentField;
        }
        set
        {
            this.ProducentField = value;
        }
    }
    /// <remarks/>
    public string Model
    {
        get
        {
            return this.ModelField;
        }
        set
        {
            this.ModelField = value;
        }
    }
    public string AdressIP
    {
        get
        {
            return this.adressIPField;
        }
        set
        {
            this.adressIPField = value;
        }
    }
    /// <remarks/>
    public int Port
    {
        get
        {
            return this.portField;
        }
        set
        {
            this.portField = value;
        }
    }
    /// <remarks/>
    public string User
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }
    /// <remarks/>
    public string Pass
    {
        get
        {
            return this.passField;
        }
        set
        {
            this.passField = value;
        }
    }
    /// <remarks/>
    public int Device
    {
        get
        {
            return this.deviceField;
        }
        set
        {
            this.deviceField = value;
        }
    }
}

