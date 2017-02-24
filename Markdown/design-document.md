**Summary**

This game is focused
around assymetrical cooperative play, where players must work together to
control, repair, and modify the various systems of a complex spaceship. The
players take on emergent missions to investigate phenomena, find needed
resources, and upgrade their ship as they try to achieve a larger goal chosen
at game start.

 

**Platform**

This game will be
designed for mid-to-high end PCs, played on a peer-hosted multiplayer
connection through LAN or Internet.

 

**Art style**

The art will be
medium polygon count, with a high emphasis on quality texture and lighting.
This is highly up to change.

 

**Gameplay**

 

Player Count

Initially,
the game will be designed for small, 2 man crews, with flexible roles. 

 

Play Style

The
game will be first person, in zero-G environments unless ship gravity is
functional or on a celestial body.

 

Emphasis will be
made on fun, rather than realism. This means much of the realism of spaceflight
will be thrown out with the bathwater in favor of more "arcade" or
atmospheric flight physics.

 

Ship Systems

systems
will be designed in layers:  

**See: [Component-level Architecture](component-level-arch.md)**  

Chassis:

- The chassis is a ship
     skeleton. It has many "hardpoints", both internal and external,
     where components can be mounted. **The ship skeleton MAY be able to connect at key
     points to other pieces of chassis, but that is not planned as a near-term
     goal**
- Components may be mounted to
     hardpoints with matching "HP" codes, e.g.:
    - HP-P1: small panel
    - HP-P2: medium panel
    - HP-C1: small component
    - HP-C2: medium component

- Components may then be
     linked, based on their connection ports.
    - There are several types of
      ports, and corresponding types of wires and tubes.
    - Wires and tubes are spooled
      out until either the player runs out of line or they connect to another
      component.
    - Wires and tubes can be
      broken, and must be repaired to restore connection

- Major system types:
    - Power
      generation/distribution
    - Propulsion
    - Weapons
    - Attitude control
    - Iife support
    - Shields
    - Heat dissipation

 

**Power generation:**

Power generation can
occur from solar, nuclear, or other methods. Distribution is either wired,
wireless, or some combination of the two. Larger amounts of power tend to
involve wired power, and larger ships tend to employ more wireless power
distribution

 

**Propulsion:**

There are several
types of propulsion: plasma, chemical, and antimatter. Chemical providing the
most thrust and some power generation but requiring the most fuel, plasma using
little fuel, high power, and generating medium thrust, and antimatter, consuming
tiny amounts of fuel, producing power, and generating high thrust, but fuel is
difficult to produce and contain.

 

**Weapons:**

Weapons come in many
types and sizes. There are rail guns, missiles, mines, plasma bursts,
antimatter and ion beams. Each type comes in tracking, turret, and dumb, which
are, respectively: pilot controlled with aim assist, separate control a la
millennium falcon, and pilot controlled without aim assist.

 

**Attitude control:**

Attitude control is
done primarily with reaction wheels, but smaller ships may use small
thrusters(usually chemical) as RCS systems.

 

**Life Support:**

All players are
encased in life support space suits, but these must be periodically recharged.
Wired or wireless power distribution can be connected to the player to recharge
them. If a player takes damage, it will decrease their suits supply of nanobots,
which must be refilled from a nanobot station or nanobot pen.

 

**Shields**

shield systems come
in a variety of flavors. There are magnetic shields that deflect many types of
physical projectiles while generating energy, antimatter shields which block
all types of physical projectiles except antimatter, run-of-the-mill deflector
and absorption shields. Shield come in different degrees of coverage, from very
narrow (20°) to full coverage. Shields charge over time, depending on the
amount of power allocated.

 

**Heat dissipation**

Various components
generate heat, especially weapons and shields, and any components being
overpowered. Heat can be concentrated into heat sinks, which can be actively or
passively cooled. Overheating components can be damaged.

 

 

Other systems, such
as lighting, navigation aid (mag plates and grav gens), and scanning and
exploration tools will also be implemented.

 

**Navigation**

The player navigates
around a ship using 4 methods:

1. Zero G propulsion: the player
     uses suit thrusters to fly/orient around the ship. They are not bound to
     the ship's reference frame in this mode and will be bounced around if the
     ship changes attitude or velocity
2. Ladders: the player
     crawls/climbs ladders around the ship. The can be peeled off the ladders
     if high enough Gees are experienced.
3. Magnetic plates: the player
     walks/crawls along magnetic plates around the ship. These must be powered
     to function, but the player will not peel off while powered on
4. Gravity generation: the
     player experiences normal gravity in the ship while this is powered. Gees
     will still cause the player to be thrown around.

Ship's may use any
combination of these methods, but, generally, smaller ships will use zero-G and
ladders more and larger ships will use gravity and mag plates more.

**Inventory**
[Inventory](inventory.md)