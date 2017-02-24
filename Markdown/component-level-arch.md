**Starpoint Object Libraries**

All items shall be
managed with Starpoint Object Libraries (SOLs) made with a custom tool, the Starpoint Object Library Manager (SOLM)

SOLs will be XML encoded files.

 

A Reference in a SOL is made as follows:
BundleName.LibraryName.ObjectName

 

**The BundleName**
references a set of assets and libraries packaged together. This allows mods to
be packaged, and since only the BundleName must be unique, it allows convenient
naming like this:

 

*Mod1.Weapons.Gun* that fires *Mod1.Bullets.5mm*

 

As opposed to
*Mod1Weapons.Gun* that files *Mod1Bullets.5mm*

 

**StarpointObject**

All objects,
equipment, hardware, and supplies inherit from a base class, StarpointObject which has the following
characteristics:

 

- Mass
- Condition
- Model
- Current heat, max heat
- Heat loss rate??
- ColliderShape
- ColliderDimensions

 

**Hardware**

Hardware has two
types of properties:

 

*Interfaces*

- Interfaces are essentially
     externally controllable variables. They come in several flavors:
    - Analog: a floating point
      number with a minimum and a maximum
    - Digital: an integer number
      with a minimum and a maximum
    - List: a list of values, each
      with their own name

*Actions*

- Actions define the way the
     hardware works. There are 2 action types and 3 trigger modes:
    - Action type discrete: each
      firing of the action is an independent action, such as firing a bullet.
        - Resources Consumed (per trigger)
        - Cooldown time
    - Action type continuous: the action
      is continuous as long as the input is triggered. Example: firing a
      thruster
        - Resources consumed (per second)
    - Trigger mode automatic
      (valid for discrete and continuous): continuously trigger as long as
      input is valid, and hardware is cooled down
    - Trigger mode passive (valid
      for discrete and continuous): Always active for continuous, or,
      happens every x interval for discrete.
    - Trigger mode semi-auto
      (valid for discrete only): happens only once per input flip to
      valid, on the leading edge of input.
- Effects
    - Effects are the things that
      happen when an action is triggered, e.g. consuming fuel, showing engine
      thruster FX, added a force to the ship.
    - Effects are designated visible  or not visible indicating whether or not
      they show up in the object's description.
    - Types:
        - Resource Add/Subtract *visible*
        - Physical Effect *visible*
        - Audio Effect *not visible*
        - Visual Effect *not visible*
        - Item *visible*