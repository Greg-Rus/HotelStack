using UnityEngine;

namespace MeshSplitting.Splitables
{
    public class FloorSplitable : Splitable
    {
        public float SplitTorque;
        protected override void PostProcessTopObject(GameObject go, Bounds meshSize)
        {
            var staticFloorHalf = go.GetComponent<Floor>();
            go.AddComponent<BoxCollider>();

            FloorDecorator.Instance.AddRoof(go, SplitPlane.Normal, meshSize);
            
        }

        protected override void PostProcessBottomObject(GameObject go, Bounds meshSize)
        {
            var physicsFloorHalf = go.GetComponent<Floor>();
            AddColliders(go, SplitPlane.Normal);

            physicsFloorHalf.Rigidbody.isKinematic = false;

            physicsFloorHalf.Rigidbody.AddForce(SplitPlane.Normal * -SplitForce, ForceMode.Impulse);
            var torqueVector = Vector3.Cross(SplitPlane.Normal, Vector3.up);
            physicsFloorHalf.Rigidbody.AddRelativeTorque(torqueVector * SplitTorque);
        }

        private void AddColliders(GameObject go, Vector3 splitNormal)
        {
            AddColliderAtSide(go, splitNormal.normalized * -1);
            AddColliderAtSide(go, Vector3.Cross(splitNormal, Vector3.up));
            AddColliderAtSide(go, Vector3.Cross(splitNormal, Vector3.down));
            AddColliderAtSide(go, Vector3.down);
        }

        private void AddColliderAtSide(GameObject go, Vector3 side)
        {
            var collider = go.AddComponent<BoxCollider>();
            var direction = side.ComponentSum();
            var inverted = side.Inverted();
            var scaler = Vector3.Scale(collider.size, side.Abs()).ComponentSum();
            var value = direction < 0 ? 0 : Vector3.Scale(collider.center, side).ComponentSum() + scaler * 0.5f;

            var center = Vector3.Scale(collider.center, inverted);
            center += side * value;
            collider.center = center;

            var absoluteSide = side.Abs();

            collider.size = Vector3.Scale(collider.size, inverted);
            collider.size += absoluteSide * 0.1f;

            collider.center = collider.center + (absoluteSide * (0.1f * -0.5f));
        }


    }
}