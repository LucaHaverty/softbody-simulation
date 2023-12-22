using UnityEngine;

public class PointMass : MonoBehaviour {
   
   [SerializeField] private Vector3 velocity;
   
   public Vector3 Velocity => velocity;

   public void ApplyGravity() {
      ApplyForce(SimConfig.ForceOfGravity*SimConfig.PointMass, Vector3.down);
   }

   public void ApplyForce(float force, Vector3 normal) {
      velocity += (force * normal) / SimConfig.PointMass;
   }

   public void UpdatePosition() {
      transform.position += velocity * Time.fixedDeltaTime;

      if (transform.position.y < 0) {
         velocity.y = 0;
         transform.position = new Vector3(transform.position.x, 0, transform.position.z);
         SimRunner.Instance.HasCollidedWithFloor = true;
      }
   }
}
