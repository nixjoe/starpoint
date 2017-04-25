﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EnumPropertyValue {
    public string name { get; set; }
    public int value { get; set; }
    public EnumPropertyValue() { }
    public EnumPropertyValue(string name, int value) {
        this.name = name;
        this.value = value;
    }
}