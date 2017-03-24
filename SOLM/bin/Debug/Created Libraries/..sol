<?xml version="1.0"?>
<SOL xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <objects>
    <StarpointObject>
      <properties>
        <Property xsi:type="RealProperty">
          <description />
          <name>dsfd</name>
          <visible>false</visible>
          <uBound xsi:nil="true" />
          <lBound xsi:nil="true" />
        </Property>
      </properties>
      <operations>
        <Operation>
          <requirements>
            <Requirement xsi:type="ResourceRequirement">
              <value>123</value>
              <comparison>LessThanOrEquals</comparison>
              <resource>sdfsdf</resource>
            </Requirement>
            <Requirement xsi:type="PropertyRequirement">
              <value>0.333</value>
              <comparison>LessThan</comparison>
              <property>dsfd</property>
            </Requirement>
          </requirements>
          <effects />
          <name>New Operation</name>
          <action>Discrete</action>
          <trigger>Auto</trigger>
          <cooldown>0</cooldown>
          <description />
        </Operation>
        <Operation>
          <requirements>
            <Requirement xsi:type="ResourceRequirement">
              <value>123</value>
              <comparison>LessThanOrEquals</comparison>
              <resource>sdfsdf</resource>
            </Requirement>
            <Requirement xsi:type="ResourceRequirement">
              <value>0.333</value>
              <comparison>LessThan</comparison>
              <resource />
            </Requirement>
          </requirements>
          <effects />
          <name>New Operation</name>
          <action>Discrete</action>
          <trigger>Auto</trigger>
          <cooldown>0</cooldown>
          <description />
        </Operation>
      </operations>
      <colliders />
      <name>New Object</name>
      <dryWeight>0</dryWeight>
      <model>Model Name.fbx</model>
    </StarpointObject>
  </objects>
</SOL>