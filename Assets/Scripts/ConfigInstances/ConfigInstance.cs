using UnityEngine;

namespace ConfigInstances {
    [CreateAssetMenu]
    public class ConfigInstance : ScriptableObject {
        public float _forceOfGravity;
        public float _coefOfFriction;
        public float _startHeight;
        public float _springConstant;
        public float _dampingConstant;
        public float _pointMass;
        public int _maxGraphPoints;
    }
}
