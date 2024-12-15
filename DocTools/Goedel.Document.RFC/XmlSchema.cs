using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Collections.Generic;

#pragma warning disable IDE1006
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

namespace Goedel.Document.RFCx; 

public partial class rfc {

    public string number { get; set; }
    public string obsoletes { get; set; } = "";
    public string updates { get; set; } = "";
    public string category { get; set; }
    public string consensus { get; set; }
    public string seriesNo { get; set; }
    public string ipr { get; set; }

    public string iprExtract { get; set; }
    public string submissionType { get; set; }
    public string docName { get; set; }
    public string lang { get; set; }


    public front front { get; set; } = new front();

    public middle middle { get; set; } = new middle();

    public back back { get; set; } = new back();
    }


public partial class front {

    public title title { get; set; }
    public List<author> author { get; set; }
    public date date { get; set; }
    public List<area> area { get; set; }
    public List<workgroup> workgroup { get; set; }
    public List<keyword> keyword { get; set; }
    public @abstract @abstract { get; set; }
    public List<note> note { get; set; }
    }


public partial class title {
    public string abbrev { get; set; }

    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value { get; set; }
    }

public partial class author {
    public string initials { get; set; }
    public string surname { get; set; }
    public string fullname { get; set; }
    public string role { get; set; }

    public organization organization { get; set; }
    public address address { get; set; }
    }

public partial class organization {
    public string abbrev { get; set; }

    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value { get; set; }
    }

public partial class address {
    public postal postal { get; set; }
    public phone phone { get; set; }
    public facsimile facsimile { get; set; }
    public email email { get; set; }
    public uri uri { get; set; }


    }

public partial class postal {
    // TBS
    // postal = element postal { street+, (city | region | code | country)* }
    }

public partial class street {
    }

public partial class city {
    }

public partial class region {
    }

public partial class code {
    }

public partial class country {
    }

public partial class phone {
    }

public partial class facsimile {
    }
public partial class email {
    }
public partial class uri {
    }
public partial class date {
    }
public partial class area {
    }
public partial class workgroup {
    }
public partial class keyword {
    }
public partial class @abstract {
    }
public partial class note {
    }




public partial class middle {
    }

public partial class section {
    }
public partial class t {
    }
public partial class list {
    }
public partial class xref {
    }
public partial class eref {
    }
public partial class iref {
    }
public partial class cref {
    }
public partial class spanx {
    }
public partial class vspace {
    }
public partial class figure {
    }
public partial class preamble {
    }
public partial class artwork {
    }
public partial class postamble {
    }
public partial class texttable {
    }
public partial class ttcol {
    }

public partial class c {
    }



public partial class back {
    }

public partial class references {
    }
public partial class reference {
    }
public partial class seriesInfo {
    }
public partial class format {
    }
public partial class annotation {
    }




