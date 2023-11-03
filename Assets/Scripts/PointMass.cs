using UnityEngine;

public class PointMass : MonoBehaviour {
   
   [SerializeField] private float mass;
   [SerializeField] private Vector3 velocity;

   public void SetValues(float mass) {
      this.mass = mass;
   }

   public void ApplyGravity() {
      ApplyForce(SimulationConfig.ForceOfGravity*mass, Vector3.down);
   }

   public void ApplyForce(float force, Vector3 normal) {
      velocity += (force * normal) / mass;
   }

   public void UpdatePosition() {
      transform.position += velocity * Time.deltaTime;

      if (transform.position.y < SimulationConfig.FloorLevel) {
         velocity.y = 0;
         transform.position = new Vector3(transform.position.x, SimulationConfig.FloorLevel, transform.position.z);
      }
   }
}
