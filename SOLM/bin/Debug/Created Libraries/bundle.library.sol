<?xml version="1.0"?>
<SOL xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <objects>
    <StarpointObject>
      <properties>
        <Property xsi:type="IntegerProperty">
          <description>sdf</description>
          <name>New Property</name>
          <visible>true</visible>
          <uBound>123</uBound>
          <lBound>-12</lBound>
        </Property>
        <Property xsi:type="ContainerProperty">
          <description>asda</description>
          <name>New Property 2</name>
          <visible>false</visible>
          <uBound>100</uBound>
          <resource>resource 1</resource>
        </Property>
      </properties>
      <operations>
        <Operation>
          <requirements>
            <Requirement xsi:type="PropertyRequirement">
              <value>1</value>
              <comparison>GreaterThan</comparison>
              <property>New Property 2</property>
            </Requirement>
          </requirements>
          <effects>
            <Effect xsi:type="PropertyEffect">
              <property>New Property 2</property>
              <assignmentType>Subtractive</assignmentType>
              <value>0.01</value>
            </Effect>
            <Effect xsi:type="ObjectEffect">
              <obj>bundle.library.New Object 1</obj>
              <xPos>0</xPos>
              <yPos>0</yPos>
              <zPos>0</zPos>
              <xRot>1</xRot>
              <yRot>1</yRot>
              <zRot>10</zRot>
              <xVel>2</xVel>
              <yVel>3413</yVel>
              <zVel>345</zVel>
              <xAng>6</xAng>
              <yAng>878</yAng>
              <zAng>76</zAng>
            </Effect>
            <Effect xsi:type="VisualEffect">
              <visual>dfzfgasd</visual>
              <xPos>1</xPos>
              <yPos>2</yPos>
              <zPos>3</zPos>
              <xRot>4</xRot>
              <yRot>5</yRot>
              <zRot>6</zRot>
            </Effect>
            <Effect xsi:type="AudioEffect">
              <audioMode>PlayOnce</audioMode>
              <audioClip>asdfasda</audioClip>
            </Effect>
            <Effect xsi:type="PhysicalEffect">
              <xPos>52345</xPos>
              <yPos>2345234</yPos>
              <zPos>23450</zPos>
              <xValue>42</xValue>
              <yValue>1</yValue>
              <zValue>4352</zValue>
              <physicalType>Force</physicalType>
            </Effect>
            <Effect xsi:type="PhysicalEffect">
              <xPos>234</xPos>
              <yPos>23435</yPos>
              <zPos>456</zPos>
              <xValue>234</xValue>
              <yValue>4234</yValue>
              <zValue>4234</zValue>
              <physicalType>Torque</physicalType>
            </Effect>
            <Effect xsi:type="ResourceEffect">
              <resource>1ddf</resource>
              <assignmentType>Multiplicative</assignmentType>
              <value>1.02</value>
            </Effect>
          </effects>
          <name>New Operation</name>
          <action>Continuous</action>
          <trigger>Auto</trigger>
          <cooldown>0</cooldown>
          <description />
        </Operation>
      </operations>
      <colliders>
        <StarpointCollider xsi:type="Cube">
          <xOffset>2</xOffset>
          <yOffset>3</yOffset>
          <zOffset>-4</zOffset>
          <xRot>123</xRot>
          <yRot>321</yRot>
          <zRot>2345</zRot>
          <xSize>1</xSize>
          <ySize>1</ySize>
          <zSize>1</zSize>
        </StarpointCollider>
      </colliders>
      <name>New Object 1</name>
      <dryWeight>234</dryWeight>
      <model>Model Name.fbx</model>
    </StarpointObject>
  </objects>
  <name>library</name>
  <bundle>bundle</bundle>
</SOL>