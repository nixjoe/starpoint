<?xml version="1.0"?>
<SOL xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <objects>
    <StarpointObject>
      <properties>
        <Property xsi:type="RealProperty">
          <description>The hit points of this object.</description>
          <name>hp</name>
          <visible>true</visible>
          <control>false</control>
          <uBound>1</uBound>
          <lBound>0</lBound>
          <defaultValue>1</defaultValue>
        </Property>
        <Property xsi:type="RealProperty">
          <description>The maximum damage this object can block before it takes damage to hit points.</description>
          <name>armor</name>
          <visible>true</visible>
          <control>false</control>
          <uBound xsi:nil="true" />
          <lBound>0</lBound>
          <defaultValue>0</defaultValue>
        </Property>
        <Property xsi:type="RealProperty">
          <description>The mass of this object.</description>
          <name>mass</name>
          <visible>true</visible>
          <control>false</control>
          <uBound xsi:nil="true" />
          <lBound>0</lBound>
          <defaultValue>0</defaultValue>
        </Property>
        <Property xsi:type="RealProperty">
          <description>The maximum temperature this object can reach before it is destroyed.</description>
          <name>max temperature</name>
          <visible>true</visible>
          <control>false</control>
          <uBound xsi:nil="true" />
          <lBound>0</lBound>
          <defaultValue>0</defaultValue>
        </Property>
      </properties>
      <operations />
      <colliders>
        <StarpointCollider xsi:type="Cube">
          <xOffset>0</xOffset>
          <yOffset>0</yOffset>
          <zOffset>0</zOffset>
          <xRot>0</xRot>
          <yRot>0</yRot>
          <zRot>0</zRot>
          <xSize>1</xSize>
          <ySize>1</ySize>
          <zSize>1</zSize>
        </StarpointCollider>
        <StarpointCollider xsi:type="Capsule">
          <xOffset>0</xOffset>
          <yOffset>0</yOffset>
          <zOffset>0</zOffset>
          <xRot>0</xRot>
          <yRot>0</yRot>
          <zRot>0</zRot>
          <radius>1</radius>
          <ySize>1</ySize>
        </StarpointCollider>
        <StarpointCollider xsi:type="Sphere">
          <xOffset>0</xOffset>
          <yOffset>0</yOffset>
          <zOffset>0</zOffset>
          <xRot>0</xRot>
          <yRot>0</yRot>
          <zRot>0</zRot>
          <radius>1.4</radius>
        </StarpointCollider>
        <StarpointCollider xsi:type="Cylinder">
          <xOffset>0</xOffset>
          <yOffset>0</yOffset>
          <zOffset>0</zOffset>
          <xRot>0</xRot>
          <yRot>0</yRot>
          <zRot>90</zRot>
          <radius>1</radius>
          <ySize>1</ySize>
        </StarpointCollider>
      </colliders>
      <name>New Object</name>
      <xScale>1</xScale>
      <yScale>1</yScale>
      <zScale>1</zScale>
      <model>cube.fbx</model>
    </StarpointObject>
  </objects>
  <name>library</name>
  <bundle>bundle</bundle>
  <version>1.1.1</version>
</SOL>