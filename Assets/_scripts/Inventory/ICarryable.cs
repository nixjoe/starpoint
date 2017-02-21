using UnityEngine;
using System.Collections.Generic;

public interface ICarryable {
    float bulk { get; set; }
    float mass { get; set; }
    Texture2D thumbnail { get; set; }
    List<string> displayProperties { get; }
}
