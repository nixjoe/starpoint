﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public class Operation {
        public string name { get; set; }
        public string action { get; set; }
        public string trigger { get; set; }
        public float cooldown { get; set; }
        public string description { get; set; }
        public List<Requirement> requirements;
        public List<Effect> effects;
        public Operation() {
            name = "New Operation";
            action = "Discrete";
            trigger = "Auto";
            cooldown = 0;
            description = "";
            requirements = new List<Requirement>();
            effects = new List<Effect>();
        }
        public Operation(Operation copySource) {
            name = copySource.name;
            action = copySource.action;
            trigger = copySource.trigger;
            cooldown = copySource.cooldown;
            description = copySource.description;
            requirements = new List<Requirement>();
            foreach (Requirement r in copySource.requirements) {
                requirements.Add(new Requirement(r));
            }
            effects = new List<Effect>();
            foreach (Effect e in copySource.effects) {
                effects.Add(new Effect(e));
            }
        }
    }
}
