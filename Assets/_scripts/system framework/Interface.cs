using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Base class for control interfaces for Gadgets, primarily Hardware
/// </summary>
public abstract class Interface {
    public string name { get; set; }
    public Interface(string name) {
        this.name = name;
    }
}
/// <summary>
/// Interface with a continuous range of float-point values
/// </summary>
public class AnalogInterface : Interface {
    public float minimum { get; private set; }
    public float maximum { get; private set; }
    public float defaultValue { get; private set;}
    public float value { get; private set; }

    public AnalogInterface(string name, float minimum, float maximum, float defaultValue) : base(name) {
        this.minimum = minimum;
        this.maximum = maximum;
        this.defaultValue = defaultValue;
        value = defaultValue;
    }

    public void SetValue(float value) {
        this.value = Mathf.Clamp(value, minimum, maximum);
    }
}
/// <summary>
/// Interface with a discrete range of integer values.
/// </summary>
public class DigitalInterface : Interface {
    public int minimum { get; private set; }
    public int maximum { get; private set; }
    public int defaultValue { get; private set; }
    public int value { get; private set; }

    public DigitalInterface(string name, int minimum, int maximum, int defaultValue) : base(name) {
        this.minimum = minimum;
        this.maximum = maximum;
        this.defaultValue = defaultValue;
        value = defaultValue;
    }

    public void SetValue(int value) {
        this.value = Mathf.Clamp(value, minimum, maximum);
    }
}
/// <summary>
/// Interface with an enumerated list of values.
/// </summary>
public class ListInterface : Interface {
    private List<KeyValuePair<string, float>> _list;
    public List<KeyValuePair<string, float>> list { get { return new List<KeyValuePair<string, float>>(_list); } }

    public ListInterface(string name, string[] valueNames, float[] values) : base(name) {
        if (valueNames.Length != values.Length) {
            throw new System.Exception("The number of values does not match the number of value names!");
        } else {
            _list = new List<KeyValuePair<string, float>>();
            for (int i = 0; i < valueNames.Length; i++) {
                _list.Add(new KeyValuePair<string, float>(valueNames[i], values[i]));
            }
        }
    }
}
