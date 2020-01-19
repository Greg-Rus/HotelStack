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
            physicsFloorHalf.gameObject.AddComponent<BoxCollider>();
            physicsFloorHalf.Rigidbody.isKinematic = false;

            

            physicsFloorHalf.Rigidbody.AddForce(SplitPlane.Normal * -SplitForce,ForceMode.Impulse);
            var torqueVector = Vector3.Cross(SplitPlane.Normal, Vector3.up);
            physicsFloorHalf.Rigidbody.AddRelativeTorque(torqueVector * SplitTorque);
        }
    }
}