﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public abstract class StarpointCollider {
        public float xOffset, yOffset, zOffset, xRot, yRot, zRot;
        public StarpointCollider() {
            xOffset = 0;
            yOffset = 0;
            zOffset = 0;
            xRot = 0;
            yRot = 0;
            zRot = 0;
        }
        public StarpointCollider(float xOffset, float yOffset, float zOffset, float xRot, float yRot, float zRot) {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.zOffset = zOffset;
            this.xRot = xRot;
            this.yRot = yRot;
            this.zRot = zRot;
        }
        public StarpointCollider(StarpointCollider copySource) {
            xOffset = copySource.xOffset;
            yOffset = copySource.yOffset;
            zOffset = copySource.zOffset;
            xRot = copySource.xRot;
            yRot = copySource.yRot;
            zRot = copySource.zRot;
        }
    }
    public class Cube : StarpointCollider {
        public float xSize, ySize, zSize;
        public Cube() : base() {
            xSize = 0;
            ySize = 0;
            zSize = 0;
        }
        public Cube(float xOffset, float yOffset, float zOffset, float xRot, float yRot, float zRot, float xSize, float ySize, float zSize) : base(xOffset, yOffset, zOffset, xRot, yRot, zRot) {
            this.xSize = xSize;
            this.ySize = ySize;
            this.zSize = zSize;
        }
        public Cube(Cube copySource) : base(copySource) {
            xSize = copySource.xSize;
            ySize = copySource.ySize;
            zSize = copySource.zSize;
        }
    }
    public class Sphere : StarpointCollider {
        public float radius;
        public Sphere() : base() {
            radius = 0;
        }
        public Sphere(float xOffset, float yOffset, float zOffset, float xRot, float yRot, float zRot, float radius) : base(xOffset, yOffset, zOffset, xRot, yRot, zRot) {
            this.radius = radius;
        }
        public Sphere(Sphere copySource):base(copySource) {
            radius = copySource.radius;
        }
    }
    public class Capsule : StarpointCollider {
        public float radius, ySize;
        public Capsule() : base() {
            radius = 0;
            ySize = 0;
        }
        public Capsule(float xOffset, float yOffset, float zOffset, float xRot, float yRot, float zRot, float radius, float ySize) : base(xOffset, yOffset, zOffset, xRot, yRot, zRot) {
            this.radius = radius;
            this.ySize = ySize;
        }

        public Capsule(Capsule copySource) : base(copySource) {
            radius = copySource.radius;
            ySize = copySource.ySize;
        }
    }
    public class Cylinder : StarpointCollider {
        public float radius, ySize;
        public Cylinder() : base() {
            radius = 0;
            ySize = 0;
        }
        public Cylinder(float xOffset, float yOffset, float zOffset, float xRot, float yRot, float zRot, float radius, float ySize) : base(xOffset, yOffset, zOffset, xRot, yRot, zRot) {
            this.radius = radius;
            this.ySize = ySize;
        }
        public Cylinder(Cylinder copySource) : base(copySource) {
            radius = copySource.radius;
            ySize = copySource.ySize;
        }
    }
}