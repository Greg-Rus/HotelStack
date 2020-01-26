using UnityEngine;

namespace MeshSplitting.Splitables
{
    public class FloorSplitable : Splitable
    {
        public float SplitTorque;
        protected override void PostProcessTopObject(GameObject go)
        {
            var staticFloorHalf = go.GetComponent<Floor>();
            go.AddComponent<BoxCollider>();
        }

        protected override void PostProcessBottomObject(GameObject go)
        {
            var physicsFloorHalf = go.GetComponent<Floor>();
            //physicsFloorHalf.gameObject.AddComponent<BoxCollider>();
            physicsFloorHalf.Rigidbody.isKinematic = false;

            physicsFloorHalf.Rigidbody.AddForce(SplitPlane.Normal * -SplitForce, ForceMode.Impulse);
            var torqueVector = Vector3.Cross(SplitPlane.Normal, Vector3.up);
            physicsFloorHalf.Rigidbody.AddRelativeTorque(torqueVector * SplitTorque);
            AddColliders(go, SplitPlane.Normal);
        }

        private void AddColliders(GameObject go, Vector3 splitNormal)
        {
            //AddColliderAtSide(go, Vector3.forward);
            //AddColliderAtSide(go, Vector3.back);
            //AddColliderAtSide(go, Vector3.left);
            //AddColliderAtSide(go, Vector3.right);
            //AddColliderAtSide(go, Vector3.down);

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
            var value = direction < 0 ? 0 : Vector3.Scale(collider.center, side).ComponentSum() * 2;

            var center = Vector3.Scale(collider.center, inverted);
            center += side * value;
            collider.center = center;

            collider.size = Vector3.Scale(collider.size, inverted);
            collider.size += side * 0.1f;

            collider.center = collider.center + (side * (0.1f * -0.5f));
        }


    }
}