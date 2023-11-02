using UnityEngine;

public class PointMass : MonoBehaviour {
   private const float FORCE_OF_GRAVITY = 9.807f;
   private const float FLOOR_LEVEL = -5;
   
   [SerializeField] private float mass;
   [SerializeField] private Vector3 velocity;

   public void SetValues(float mass) {
      this.mass = mass;
   }

   private void LateUpdate() {
      ApplyGravity();
      transform.position += velocity * Time.deltaTime;

      if (transform.position.y < FLOOR_LEVEL) {
         velocity.y = 0;
         transform.position = new Vector3(transform.position.x,FLOOR_LEVEL,transform.position.z);
      }
   }

   private void ApplyGravity() {
      ApplyForce(FORCE_OF_GRAVITY*mass, Vector3.down);
   }

   public void ApplyForce(float force, Vector3 normal) {
      velocity += (force * normal) / mass;
   }
}
